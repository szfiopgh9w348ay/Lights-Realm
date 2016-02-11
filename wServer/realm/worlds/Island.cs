using wServer.networking;

namespace wServer.realm.worlds
{
    public class Island : World
    {
        public Island()
        {
            Name = "Forgotten Island";
            Background = 0;
            AllowTeleport = true;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.island.wmap"));
        }

        public override World GetInstance(Client client)
        {
            return RealmManager.AddWorld(new Island());
        }
    }
}
