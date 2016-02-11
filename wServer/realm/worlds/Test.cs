using System.IO;
using wServer.realm.terrain;

namespace wServer.realm.worlds
{
    public class Test : World
    {
        public string js = null;

        public Test()
        {
            Id = TEST_ID;
            Name = "Test";
            Background = 0;
            //Mining = true;
        }

        public void LoadJson(string json)
        {
            js = json;
            FromWorldMap(new MemoryStream(Json2Wmap.Convert(json)));
        }
    }
}
