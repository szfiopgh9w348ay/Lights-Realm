using db;
using System.Text;

namespace server.@char
{
    internal class delete : RequestHandler
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
                    var cmd = db.CreateQuery();
                    cmd.CommandText = @"DELETE FROM characters WHERE accId = @accId AND charId = @charId;";
                    cmd.Parameters.AddWithValue("@accId", acc.AccountId);
                    cmd.Parameters.AddWithValue("@charId", Query["charId"]);
                    if (cmd.ExecuteNonQuery() > 0)
                        status = Encoding.UTF8.GetBytes("<Success />");
                    else
                        status = Encoding.UTF8.GetBytes("<Error>Internal Error</Error>");
                }
                Context.Response.OutputStream.Write(status, 0, status.Length);
            }
        }
    }
}
