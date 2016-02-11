namespace wServer.networking.cliPackets
{
    public class InvDropPacket : ClientPacket
    {
        public ObjectSlot Slot { get; set; }

        public override PacketID ID { get { return PacketID.InvDrop; } }

        public override Packet CreateInstance()
        {
            return new InvDropPacket();
        }

        protected override void Read(Client client, NReader rdr)
        {
            Slot = ObjectSlot.Read(rdr);
        }

        protected override void Write(Client client, NWriter wtr)
        {
            Slot.Write(wtr);
        }
    }
}
