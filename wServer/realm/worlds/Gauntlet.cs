using wServer.networking;

namespace wServer.realm.worlds
{
    public class GauntletMap : World
    {
        public GauntletMap()
        {
            Id = GAUNTLET;
            Name = "The Gauntlet";
            Background = 0;
            AllowTeleport = false;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.gauntlet.wmap"));
        }

        public override World GetInstance(Client client)
        {
            return RealmManager.AddWorld(new GauntletMap());
        }
    }
}
