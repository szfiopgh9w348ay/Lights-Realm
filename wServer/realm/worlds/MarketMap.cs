using wServer.networking;

namespace wServer.realm.worlds
{
    public class MarketMap : World
    {
        public MarketMap()
        {
            Id = MARKET;
            Name = "Market";
            Background = 0;
            AllowTeleport = true;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.MarketMap.wmap"));
        }

        public override World GetInstance(Client client)
        {
            return RealmManager.AddWorld(new GauntletMap());
        }
    }
}
