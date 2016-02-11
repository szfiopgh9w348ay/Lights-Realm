using db;
using System.Text;

namespace server.account
{
    internal class setName : RequestHandler
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
                    var cmd = db.CreateQuery();
                    object exescala;
                    cmd.CommandText = "SELECT COUNT(name) FROM accounts WHERE name=@name;";
                    cmd.Parameters.AddWithValue("@name", Query["name"]);
                    exescala = cmd.ExecuteScalar();
                    if (int.Parse(exescala.ToString()) > 0)
                        status = Encoding.UTF8.GetBytes("<Error>Duplicated name</Error>");
                    else
                    {
                        cmd = db.CreateQuery();
                        cmd.CommandText = "UPDATE accounts SET name=@name, namechosen=TRUE WHERE id=@accId;";
                        cmd.Parameters.AddWithValue("@accId", acc.AccountId);
                        cmd.Parameters.AddWithValue("@name", Query["name"]);
                        if (cmd.ExecuteNonQuery() != 0)
                            status = Encoding.UTF8.GetBytes("<Success />");
                        else
                            status = Encoding.UTF8.GetBytes("<Error>Internal error</Error>");
                    }
                }
                Context.Response.OutputStream.Write(status, 0, status.Length);
            }
        }
    }
}
