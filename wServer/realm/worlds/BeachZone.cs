using wServer.networking;

namespace wServer.realm.worlds
{
    public class BeachZone : World
    {
        public BeachZone()
        {
            Name = "Beachzone";
            Background = 0;
            AllowTeleport = true;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.bz.wmap"));
        }

        public override World GetInstance(Client client)
        {
            return RealmManager.AddWorld(new Abyss());
        }
    }
}
