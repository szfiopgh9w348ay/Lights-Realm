using wServer.networking.cliPackets;

namespace wServer.realm.entities
{
    partial class Player
    {
        public void TextBoxButton(TextBoxButtonPacket pkt)
        {
            int button = pkt.Button;
            string type = pkt.Type;
            if (type == "NewClient")
            {
                client.Disconnect();
            }
        }
    }
}
