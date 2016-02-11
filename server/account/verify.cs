using db;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace server.account
{
    internal class verify : RequestHandler
    {
        protected override void HandleRequest()
        {
            using (var db = new Database())
            {
                var acc = db.Verify(Query["guid"], Query["password"]);
                if (acc == null)
                {
                    var status = Encoding.UTF8.GetBytes("<Error>Bad login</Error>");
                    Context.Response.OutputStream.Write(status, 0, status.Length);
                }
                else
                {
                    XmlSerializer serializer = new XmlSerializer(acc.GetType(), new XmlRootAttribute(acc.GetType().Name) { Namespace = "" });

                    XmlWriterSettings xws = new XmlWriterSettings();
                    xws.OmitXmlDeclaration = true;
                    xws.Encoding = Encoding.UTF8;
                    XmlWriter xtw = XmlWriter.Create(Context.Response.OutputStream, xws);
                    serializer.Serialize(xtw, acc, acc.Namespaces);
                }
            }
        }
    }
}
