namespace wServer.networking.svrPackets
{
    public class PlaySoundPacket : ServerPacket
    {
        public int OwnerId { get; set; }
        public byte SoundId { get; set; }

        public override PacketID ID { get { return PacketID.PlaySound; } }

        public override Packet CreateInstance()
        {
            return new PlaySoundPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            OwnerId = rdr.ReadByte();
            SoundId = rdr.ReadByte();
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(OwnerId);
            wtr.Write(SoundId);
        }
    }
}
