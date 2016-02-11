﻿using wServer.logic;
using wServer.networking.svrPackets;

namespace wServer.realm.entities
{
    internal class Trap : StaticObject
    {
        private const int LIFETIME = 10;

        private Player player;
        private float radius;
        private int dmg;
        private ConditionEffectIndex effect;
        private int duration;

        public Trap(Player player, float radius, int dmg, ConditionEffectIndex eff, float effDuration)
            : base(0x0711, LIFETIME * 1000, true, true, false)
        {
            this.player = player;
            this.radius = radius;
            this.dmg = dmg;
            this.effect = eff;
            this.duration = (int)(effDuration * 1000);
        }

        private int t = 0;
        private int p = 0;

        public override void Tick(RealmTime time)
        {
            if (t / 500 == p)
            {
                Owner.BroadcastPacket(new ShowEffectPacket()
                {
                    EffectType = EffectType.Trap,
                    Color = new ARGB(0xff9000ff),
                    TargetId = Id,
                    PosA = new Position() { X = radius / 2 }
                }, null);
                p++;
                if (p == LIFETIME * 2)
                {
                    Explode(time);
                    return;
                }
            }
            t += time.thisTickTimes;

            bool monsterNearby = false;
            Behavior.AOE(Owner, this, radius / 2, false, enemy => monsterNearby = true);
            if (monsterNearby)
                Explode(time);

            base.Tick(time);
        }

        private void Explode(RealmTime time)
        {
            Owner.BroadcastPacket(new ShowEffectPacket()
            {
                EffectType = EffectType.AreaBlast,
                Color = new ARGB(0xff9000ff),
                TargetId = Id,
                PosA = new Position() { X = radius }
            }, null);
            Behavior.AOE(Owner, this, radius, false, enemy =>
            {
                (enemy as Enemy).Damage(player, time, dmg, false, new ConditionEffect()
                {
                    Effect = effect,
                    DurationMS = duration
                });
            });
            Owner.LeaveWorld(this);
        }
    }
}
