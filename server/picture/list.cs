using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace server.picture
{
    internal class list : RequestHandler
    {
        private byte[] buff = new byte[0x10000];

        protected override void HandleRequest()
        {
            Pics pics = new Pics()
            {
                SearchOffset = Convert.ToInt32(Query["offset"]),
                Pictures = new List<Pic>() {
                    new Pic() {
                        PicName = "Dark Fire",
                        PictureId = "dark_fire",
                        DataType = 2,
                        Tags = new List<string>() {
                            "test",
                            "test2",
                            "dark",
                            "fire"
                        }
                    }
                }
            };

            XmlSerializer serializer = new XmlSerializer(pics.GetType(), new XmlRootAttribute(pics.GetType().Name) { Namespace = "" });

            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Encoding = Encoding.UTF8;
            XmlWriter xtw = XmlWriter.Create(Context.Response.OutputStream, xws);
            serializer.Serialize(xtw, pics, pics.Namespaces);
        }
    }
}
