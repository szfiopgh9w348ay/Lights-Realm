using wServer.networking.cliPackets;

namespace wServer.realm.entities
{
    partial class Player
    {
        public void Buy(RealmTime time, BuyPacket pkt)
        {
            SellableObject obj = Owner.GetEntity(pkt.ObjectId) as SellableObject;
            if (obj != null)
                obj.Buy(this);
        }

        public void CheckCredits(RealmTime t, CheckCreditsPacket pkt)
        {
            client.Database.ReadStats(client.Account);
            Credits = client.Account.Credits;
            UpdateCount++;
        }
    }
}
