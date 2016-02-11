using wServer.networking;

namespace wServer.realm.worlds
{
    public class SnakePit : World
    {
        public SnakePit()
        {
            Name = "Snake Pit";
            Background = 0;
            AllowTeleport = true;
            base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.snakepit.wmap"));
        }

        public override World GetInstance(Client client)
        {
            return RealmManager.AddWorld(new SnakePit());
        }
    }
}
