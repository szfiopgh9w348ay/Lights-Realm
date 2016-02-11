using db;
using System.Collections.Generic;
using wServer.networking.cliPackets;
using wServer.networking.svrPackets;

namespace wServer.realm.entities
{
    partial class Player
    {
        private const int LOCKED_LIST_ID = 0;
        private const int IGNORED_LIST_ID = 1;

        public void SendAccountList(List<int> list, int id)
        {
            client.SendPacket(new AccountListPacket()
            {
                AccountListId = id,
                AccountIds = list.ToArray()
            });
        }

        public void EditAccountList(RealmTime time, EditAccountListPacket pkt)
        {
            List<int> list;
            if (pkt.AccountListId == LOCKED_LIST_ID)
                list = Locked;
            else if (pkt.AccountListId == IGNORED_LIST_ID)
                list = Ignored;
            else return;
            if (list == null)
                list = new List<int>();
            Player player = Owner.GetEntity(pkt.ObjectId) as Player;
            if (player == null) return;
            int accId = player.client.Account.AccountId;
            var db = new Database();
            //if (pkt.Add && list.Count < 6)
            //    list.Add(accId);
            //else
            //    list.Remove(accId);

            if (pkt.Add)
            {
                list.Add(accId);
                if (pkt.AccountListId == LOCKED_LIST_ID)
                    db.AddLock(client.Account.AccountId, accId);
                if (pkt.AccountListId == IGNORED_LIST_ID)
                    db.AddIgnore(client.Account.AccountId, accId);
            }
            else
            {
                list.Remove(accId);
                if (pkt.AccountListId == LOCKED_LIST_ID)
                    db.RemoveLock(client.Account.AccountId, accId);
                if (pkt.AccountListId == IGNORED_LIST_ID)
                    db.RemoveIgnore(client.Account.AccountId, accId);
            }

            SendAccountList(list, pkt.AccountListId);
        }
    }
}
