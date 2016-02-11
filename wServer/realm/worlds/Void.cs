﻿using wServer.networking;

namespace wServer.realm.worlds
{
    public class VoidWorld : World
    {
        public VoidWorld()
        {
            Name = "The Void";
            Background = 1;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.void.wmap"));
        }

        public override World GetInstance(Client client)
        {
            return RealmManager.AddWorld(new VoidWorld());
        }
    }
}
