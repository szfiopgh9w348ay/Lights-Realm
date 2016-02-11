using wServer.networking;

namespace wServer.realm.worlds
{
    public class UndeadLair : World
    {
        public UndeadLair()
        {
            Name = "Undead Lair";
            Background = 0;
            AllowTeleport = false;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.UDL.wmap"));
        }

        public override World GetInstance(Client client)
        {
            return RealmManager.AddWorld(new UndeadLair());
        }
    }
}
