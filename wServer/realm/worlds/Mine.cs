using wServer.networking;

namespace wServer.realm.worlds
{
    public class Mine : World
    {
        public Mine()
        {
            Name = "Underground Mine";
            Background = 0;
            AllowTeleport = false;
            Mining = true;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.mine.wmap"));
        }

        public override World GetInstance(Client client)
        {
            return RealmManager.AddWorld(new Mine());
        }
    }
}
