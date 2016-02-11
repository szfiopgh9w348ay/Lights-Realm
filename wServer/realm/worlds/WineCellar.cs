using wServer.networking;

namespace wServer.realm.worlds
{
    public class WineCellarMap : World
    {
        public WineCellarMap()
        {
            Id = WC;
            Name = "Wine Cellar";
            Background = 0;
            AllowTeleport = true;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.winecellar.wmap"));
        }

        public override World GetInstance(Client client)
        {
            return RealmManager.AddWorld(new WineCellarMap());
        }
    }
}
