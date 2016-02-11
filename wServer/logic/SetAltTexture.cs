using System.Collections.Generic;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.logic
{
    internal class SetAltTexture : Behavior
    {
        private int index;

        private SetAltTexture(int index)
        {
            this.index = index;
        }

        private static readonly Dictionary<int, SetAltTexture> instances = new Dictionary<int, SetAltTexture>();

        public static SetAltTexture Instance(int index)
        {
            SetAltTexture ret;
            if (!instances.TryGetValue(index, out ret))
                ret = instances[index] = new SetAltTexture(index);
            return ret;
        }

        protected override bool TickCore(RealmTime time)
        {
            if ((Host.Self as Enemy).AltTextureIndex != index)
            {
                (Host.Self as Enemy).AltTextureIndex = index;
                Host.Self.UpdateCount++;
            }
            return true;
        }
    }
}
