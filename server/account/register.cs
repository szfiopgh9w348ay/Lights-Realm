using db;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace server.account
{
    internal class register : RequestHandler
    {
        public bool IsValidEmail(string strIn)
        {
            var invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            MatchEvaluator DomainMapper = match =>
            {
                // IdnMapping class with default property values.
                IdnMapping idn = new IdnMapping();

                string domainName = match.Groups[2].Value;
                try
                {
                    domainName = idn.GetAscii(domainName);
                }
                catch (ArgumentException)
                {
                    invalid = true;
                }
                return match.Groups[1].Value + domainName;
            };

            // Use IdnMapping class to convert Unicode domain names.
            strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper);
            if (invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn,
                      @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                      RegexOptions.IgnoreCase);
        }

        protected override void HandleRequest()
        {
            using (var db = new Database())
            {
                byte[] status;
                if (0 != 0/*!IsValidEmail(query["newGUID"])*/)
                    status = Encoding.UTF8.GetBytes("<Error>Invalid Username</Error>");
                else
                {
                    if (db.HasUuid(Query["guid"]) &&
                        db.Verify(Query["guid"], "") != null)
                    {
                        if (db.HasUuid(Query["newGUID"]))
                            status = Encoding.UTF8.GetBytes("<Error>Username is already taken!</Error>");
                        else
                        {
                            var cmd = db.CreateQuery();
                            cmd.CommandText = "UPDATE accounts SET uuid=@newUuid, name=@newUuid, password=SHA1(@password), guest=FALSE WHERE uuid=@uuid, name=@name;";
                            cmd.Parameters.AddWithValue("@uuid", Query["guid"]);
                            cmd.Parameters.AddWithValue("@newUuid", Query["newGUID"]);
                            cmd.Parameters.AddWithValue("@password", Query["newPassword"]);
                            if (cmd.ExecuteNonQuery() > 0)
                                status = Encoding.UTF8.GetBytes("<Success />");
                            else
                                status = Encoding.UTF8.GetBytes("<Error>Internal Error</Error>");
                        }
                    }
                    else
                    {
                        if (db.Register(Query["newGUID"], Query["newPassword"], false) != null)
                            status = Encoding.UTF8.GetBytes("<Success />");
                        else
                            status = Encoding.UTF8.GetBytes("<Error>Internal Error</Error>");
                    }
                }
                Context.Response.OutputStream.Write(status, 0, status.Length);
            }
        }
    }
}
