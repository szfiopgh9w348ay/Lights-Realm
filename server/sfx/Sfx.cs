using System;
using System.IO;

namespace server.sfx
{
    internal class Sfx : RequestHandler
    {
        protected override void HandleRequest()
        {
            string file = AppDomain.CurrentDomain.BaseDirectory + (Context.Request.Url.LocalPath.StartsWith("/music") ? "sfx" + Context.Request.Url.LocalPath : Context.Request.Url.LocalPath.Remove(0, 1));

            if (File.Exists(file))
            {
                using (FileStream i = File.OpenRead(file))
                {
                    byte[] buff = new byte[i.Length];
                    int c;
                    while ((c = i.Read(buff, 0, buff.Length)) > 0)
                        Context.Response.OutputStream.Write(buff, 0, c);
                }
            }
            else
                Context.Response.Redirect("http://realmofthemadgodhrd.appspot.com/" + (file.Split('/')[1].Contains("music") ? file.Replace("sfx/", String.Empty) : file));
        }
    }
}
