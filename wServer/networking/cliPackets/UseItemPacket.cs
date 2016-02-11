namespace wServer.networking.cliPackets
{
    public class UseItemPacket : ClientPacket
    {
        public int Time { get; set; }
        public ObjectSlot Slot { get; set; }
        public Position Position { get; set; }

        public override PacketID ID { get { return PacketID.UseItem; } }

        public override Packet CreateInstance()
        {
            return new UseItemPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            Time = rdr.ReadInt32();
            Slot = ObjectSlot.Read(rdr);
            Position = Position.Read(rdr);
        }

        protected override void Write(Client client, NWriter wtr)
        {
            wtr.Write(Time);
            Slot.Write(wtr);
            Position.Write(wtr);
        }
    }
}
