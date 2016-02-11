using wServer.networking;

namespace wServer.realm.worlds
{
    public class Tutorial : World
    {
        public Tutorial(bool isLimbo)
        {
            Id = TUT_ID;
            Name = "Tutorial";
            Background = 0;
            if (!(IsLimbo = isLimbo))
            {
                base.FromWorldMap(typeof(RealmManager).Assembly.GetManifestResourceStream("wServer.realm.worlds.tutorial.wmap"));
            }
        }

        public override World GetInstance(Client client)
        {
            return RealmManager.AddWorld(new Tutorial(false));
        }
    }
}
