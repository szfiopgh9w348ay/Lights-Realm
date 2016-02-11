using System;
using System.Collections.Generic;
using wServer.realm;

namespace wServer.logic.movement
{
    internal class Planewalk : Behavior
    {
        private float radius;
        private short? objType;

        private Planewalk(float radius, short? objType)
        {
            this.radius = radius;
            this.objType = objType;
        }

        private static readonly Dictionary<Tuple<float, short?>, Planewalk> instances = new Dictionary<Tuple<float, short?>, Planewalk>();

        public static Planewalk Instance(float radius, short? objType)
        {
            var key = new Tuple<float, short?>(radius, objType);
            Planewalk ret;
            if (!instances.TryGetValue(key, out ret))
                ret = instances[key] = new Planewalk(radius, objType);
            return ret;
        }

        private Random rand = new Random();

        protected override bool TickCore(RealmTime time)
        {
            if (Host.Self.HasConditionEffect(ConditionEffects.Paralyzed)) return true;

            float dist = radius;
            Entity entity = GetNearestEntity(ref dist, objType);
            if (entity != null)
                ValidateAndMove(entity.X, entity.Y);
            return true;
        }
    }
}
