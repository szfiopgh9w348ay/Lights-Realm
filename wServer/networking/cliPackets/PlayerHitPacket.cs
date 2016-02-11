namespace wServer.networking.cliPackets
{
    public class PlayerHitPacket : ClientPacket
    {
        public byte BulletId { get; set; }
        public int ObjectId { get; set; }

        public override PacketID ID { get { return PacketID.PlayerHit; } }

        public override Packet CreateInstance()
        {
            return new PlayerHitPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            BulletId = rdr.ReadByte();
            ObjectId = rdr.ReadInt32();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(BulletId);
            wtr.Write(ObjectId);
        }
    }
}
