using System;
using System.Collections.Generic;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.logic
{
    internal class SpawnMinionImmediate : Behavior
    {
        private short objType;
        private float radius;
        private int minCount;
        private int maxCount;

        private SpawnMinionImmediate(short objType, float radius, int minCount, int maxCount)
        {
            this.objType = objType;
            this.radius = radius;
            this.minCount = minCount;
            this.maxCount = maxCount;
        }

        private static readonly Dictionary<Tuple<short, float, int, int>, SpawnMinionImmediate> instances
            = new Dictionary<Tuple<short, float, int, int>, SpawnMinionImmediate>();

        public static SpawnMinionImmediate Instance(short objType, float radius, int minCount, int maxCount)
        {
            var key = new Tuple<short, float, int, int>(objType, radius, minCount, maxCount);
            SpawnMinionImmediate ret;
            if (!instances.TryGetValue(key, out ret))
                ret = instances[key] = new SpawnMinionImmediate(objType, radius, minCount, maxCount);
            return ret;
        }

        private Random rand = new Random();

        protected override bool TickCore(RealmTime time)
        {
            int count = rand.Next(minCount, maxCount + 1);
            for (int i = 0; i < count; i++)
            {
                Entity entity = Entity.Resolve(objType);
                entity.Move(Host.Self.X + (float)(rand.NextDouble() * 2 - 1) * radius, Host.Self.Y + (float)(rand.NextDouble() * 2 - 1) * radius);
                (entity as Enemy).Terrain = (Host as Enemy).Terrain;
                Host.Self.Owner.EnterWorld(entity);
            }
            return true;
        }
    }
}
