using wServer.networking;

namespace wServer.realm.worlds
{
    public class OceanTrench : World
    {
        public OceanTrench()
        {
            Name = "Ocean Trench";
            Background = 0;
            AllowTeleport = true;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.oceantrench.wmap"));
        }

        public override World GetInstance(Client client)
        {
            return RealmManager.AddWorld(new OceanTrench());
        }
    }
}
