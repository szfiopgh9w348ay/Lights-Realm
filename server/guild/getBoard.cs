using db;
using System;
using System.Text;

namespace server.guild
{
    internal class getBoard : RequestHandler
    {
        protected override void HandleRequest()
        {
            using (var db = new Database())
            {
                var acc = db.Verify(Query["guid"], Query["password"]);
                byte[] status;
                if (acc == null)
                    status = Encoding.UTF8.GetBytes("<Error>Bad login</Error>");
                else
                {
                    try
                    {
                        status = Encoding.UTF8.GetBytes(db.GetGuildBoard(acc));
                    }
                    catch (Exception e)
                    {
                        status = Encoding.UTF8.GetBytes("<Error>" + e.Message + "</Error>");
                    }
                }
                Context.Response.OutputStream.Write(status, 0, status.Length);
            }
        }
    }
}
