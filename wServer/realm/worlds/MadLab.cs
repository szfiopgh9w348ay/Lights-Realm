using wServer.networking;

namespace wServer.realm.worlds
{
    public class MadLabMap : World
    {
        public MadLabMap()
        {
            Name = "Mad Lab";
            Background = 0;
            AllowTeleport = true;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.madlab.wmap"));
        }

        public override World GetInstance(Client client)
        {
            return RealmManager.AddWorld(new MadLabMap());
        }
    }
}
