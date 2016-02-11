﻿using System;
using System.Collections.Generic;
using wServer.networking.svrPackets;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.logic.attack
{
    internal class Heal : Behavior
    {
        private float radius;
        private int amount;
        private short? objType;

        private Heal(float radius, int amount, short? objType)
        {
            this.radius = radius;
            this.amount = amount;
            this.objType = objType;
        }

        private static readonly Dictionary<Tuple<float, int, short?>, Heal> instances = new Dictionary<Tuple<float, int, short?>, Heal>();

        public static Heal Instance(float radius, int amount, short? objType)
        {
            var key = new Tuple<float, int, short?>(radius, amount, objType);
            Heal ret;
            if (!instances.TryGetValue(key, out ret))
                ret = instances[key] = new Heal(radius, amount, objType);
            return ret;
        }

        protected override bool TickCore(RealmTime time)
        {
            if (objType == null)
            {
                var host = Host.Self as Enemy;
                int hp = host.HP;
                hp = Math.Min(hp + amount, host.ObjectDesc.MaxHP);
                if (hp != host.HP)
                {
                    int n = hp - host.HP;
                    host.HP = hp;
                    host.UpdateCount++;
                    host.Owner.BroadcastPacket(new ShowEffectPacket()
                    {
                        EffectType = EffectType.Potion,
                        TargetId = host.Id,
                        Color = new ARGB(0xffffffff)
                    }, null);
                    host.Owner.BroadcastPacket(new NotificationPacket()
                    {
                        ObjectId = host.Id,
                        Text = "+" + n,
                        Color = new ARGB(0xff00ff00)
                    }, null);

                    return true;
                }
            }
            else
            {
                float dist = radius;
                Enemy entity = GetNearestEntity(ref dist, objType) as Enemy;
                while (entity != null)
                {
                    int hp = entity.HP;
                    hp = Math.Min(hp + amount, entity.ObjectDesc.MaxHP);
                    if (hp != entity.HP)
                    {
                        int n = hp - entity.HP;
                        entity.HP = hp;
                        entity.UpdateCount++;
                        entity.Owner.BroadcastPacket(new ShowEffectPacket()
                        {
                            EffectType = EffectType.Potion,
                            TargetId = entity.Id,
                            Color = new ARGB(0xffffffff)
                        }, null);
                        entity.Owner.BroadcastPacket(new ShowEffectPacket()
                        {
                            EffectType = EffectType.Trail,
                            TargetId = Host.Self.Id,
                            PosA = new Position() { X = entity.X, Y = entity.Y },
                            Color = new ARGB(0xffffffff)
                        }, null);
                        entity.Owner.BroadcastPacket(new NotificationPacket()
                        {
                            ObjectId = entity.Id,
                            Text = "+" + n,
                            Color = new ARGB(0xff00ff00)
                        }, null);

                        return true;
                    }
                    entity = GetNearestEntity(ref dist, null) as Enemy;
                }
            }
            return false;
        }
    }
}
