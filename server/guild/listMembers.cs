using db;
using System;
using System.Text;

namespace server.guild
{
    internal class listMembers : RequestHandler
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
                        status = Encoding.UTF8.GetBytes(db.HTTPGetGuildMembers(Convert.ToInt32(Query["num"]), Convert.ToInt32(Query["offset"]), acc));
                    }
                    catch
                    {
                        status = Encoding.UTF8.GetBytes("<Error>Guild member error</Error>");
                    }
                }
                Context.Response.OutputStream.Write(status, 0, status.Length);
                Context.Response.Close();
            }
        }
    }
}
