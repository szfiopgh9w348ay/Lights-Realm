namespace wServer.networking.cliPackets
{
    public abstract class ClientPacket : Packet
    {
        public override byte[] Crypt(Client client, byte[] dat, int len)
        {
            return client.ReceiveKey.Crypt(dat, len);
        }
    }
}
