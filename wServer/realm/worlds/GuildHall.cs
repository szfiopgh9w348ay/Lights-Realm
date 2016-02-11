using db;
using wServer.networking;

namespace wServer.realm.worlds
{
    public class GuildHall : World
    {
        public string Guild { get; set; }

        public GuildHall(string guild)
        {
            Id = GHALL;
            Guild = guild;
            Name = "Guild Hall";
            Background = 0;
            AllowTeleport = true;
            switch (this.Level())
            {
                case 0:
                    base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.ghall0.wmap"));
                    break;
                case 1:
                    base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.ghall1.wmap"));
                    break;
                case 2:
                    base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.ghall2.wmap"));
                    break;
                case 3:
                    base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.ghall3.wmap"));
                    break;
            }
            //base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.guildhall0old.wmap"));
        }

        public override World GetInstance(Client client)
        {
            return RealmManager.AddWorld(new GuildHall(Guild));
        }

        public int Level()
        {
            using (var db = new Database())
            {
                int id = db.GetGuildId(this.Guild);
                return db.GetGuildLevel(id);
            }
        }
    }
}
