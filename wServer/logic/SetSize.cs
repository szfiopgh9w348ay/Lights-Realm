using System;
using System.Collections.Generic;
using wServer.realm;

namespace wServer.logic
{
    internal class SetSize : Behavior
    {
        private int size;

        private SetSize(int size)
        {
            this.size = size;
        }

        private static readonly Dictionary<int, SetSize> instances = new Dictionary<int, SetSize>();

        public static SetSize Instance(int size)
        {
            SetSize ret;
            if (!instances.TryGetValue(size, out ret))
                ret = instances[size] = new SetSize(size);
            return ret;
        }

        protected override bool TickCore(RealmTime time)
        {
            if (Host.Self.Size != size)
            {
                Host.Self.Size = size;
                Host.Self.UpdateCount++;
            }
            return true;
        }
    }

    internal class GrowSize : Behavior
    {
        private int rate;
        private int target;

        private GrowSize(int rate, int target)
        {
            this.rate = rate;
            this.target = target;
        }

        private static readonly Dictionary<Tuple<int, int>, GrowSize> instances = new Dictionary<Tuple<int, int>, GrowSize>();

        public static GrowSize Instance(int rate, int target)
        {
            var key = new Tuple<int, int>(rate, target);
            GrowSize ret;
            if (!instances.TryGetValue(key, out ret))
                ret = instances[key] = new GrowSize(rate, target);
            return ret;
        }

        protected override bool TickCore(RealmTime time)
        {
            if (Host.Self.Size < target)
            {
                Host.Self.Size += rate / 2;
                Host.Self.UpdateCount++;
                return true;
            }
            else
                return false;
        }
    }

    internal class ShrinkSize : Behavior
    {
        private int rate;
        private int target;

        private ShrinkSize(int rate, int target)
        {
            this.rate = rate;
            this.target = target;
        }

        private static readonly Dictionary<Tuple<int, int>, ShrinkSize> instances = new Dictionary<Tuple<int, int>, ShrinkSize>();

        public static ShrinkSize Instance(int rate, int target)
        {
            var key = new Tuple<int, int>(rate, target);
            ShrinkSize ret;
            if (!instances.TryGetValue(key, out ret))
                ret = instances[key] = new ShrinkSize(rate, target);
            return ret;
        }

        protected override bool TickCore(RealmTime time)
        {
            if (Host.Self.Size > target)
            {
                Host.Self.Size -= rate / 2;
                Host.Self.UpdateCount++;
                return true;
            }
            else
                return false;
        }
    }
}
