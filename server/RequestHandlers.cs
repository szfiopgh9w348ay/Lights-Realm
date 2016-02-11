using db;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web;

namespace server
{
    public abstract class RequestHandler
    {
        protected NameValueCollection Query { get; private set; }
        protected HttpListenerContext Context { get; private set; }

        public void HandleRequest(HttpListenerContext context)
        {
            this.Context = context;
            if (ParseQueryString())
            {
                Query = new NameValueCollection();
                using (StreamReader rdr = new StreamReader(context.Request.InputStream))
                    Query = HttpUtility.ParseQueryString(rdr.ReadToEnd());

                if (Query.AllKeys.Length == 0)
                {
                    string currurl = context.Request.RawUrl;
                    int iqs = currurl.IndexOf('?');
                    if (iqs >= 0)
                        Query = HttpUtility.ParseQueryString((iqs < currurl.Length - 1) ? currurl.Substring(iqs + 1) : string.Empty);
                }
            }

            HandleRequest();
        }

        public bool CheckAccount(Account acc, Database db, bool checkAccInUse = true)
        {
            if (acc == null && !String.IsNullOrWhiteSpace(Query["password"]))
            {
                WriteErrorLine("Account credentials not valid");
                return false;
            }
            else if (acc == null && String.IsNullOrWhiteSpace(Query["password"]))
                return true;

            if (acc.Banned)
            {
                using (StreamWriter wtr = new StreamWriter(Context.Response.OutputStream))
                    wtr.WriteLine("<Error>Account under maintenance</Error>");
                Context.Response.Close();
                return false;
            }
            return true;
        }

        public void WriteLine(string value, params object[] args)
        {
            using (StreamWriter wtr = new StreamWriter(Context.Response.OutputStream))
                if (args == null || args.Length == 0) wtr.Write(value);
                else wtr.Write(value, args);
        }

        public void WriteErrorLine(string value, params object[] args)
        {
            using (StreamWriter wtr = new StreamWriter(Context.Response.OutputStream))
                wtr.Write("<Error>" + value + "</Error>", args);
        }

        protected virtual bool ParseQueryString() => true;

        protected abstract void HandleRequest();
    }
}
