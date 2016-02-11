using db;
using System.Text;

namespace server.guild
{
    class setBoard : RequestHandler
    {
        protected override void HandleRequest()
        {
            using (var db = new Database())
            {
                var acc = db.Verify(Query["guid"], Query["password"]);
                byte[] status;
                if (acc == null)
                {
                    status = Encoding.UTF8.GetBytes("<Error>Bad login</Error>");
                }
                else
                {
                    status = Encoding.UTF8.GetBytes(db.SetGuildBoard(Query["board"], acc));
                }
                Context.Response.OutputStream.Write(status, 0, status.Length);
            }
        }
    }
}
