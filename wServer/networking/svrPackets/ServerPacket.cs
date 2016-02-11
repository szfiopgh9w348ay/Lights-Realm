namespace wServer.networking.svrPackets
{
    public abstract class ServerPacket : Packet
    {
        public override byte[] Crypt(Client client, byte[] dat, int len)
        {
            return client.SendKey.Crypt(dat, len);
        }
    }
}
