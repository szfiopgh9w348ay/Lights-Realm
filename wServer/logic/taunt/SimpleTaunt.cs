using wServer.networking.svrPackets;
using wServer.realm;
using wServer.realm.entities;

namespace wServer.logic.taunt
{
    internal abstract class TauntBase : Behavior
    {
        protected void Taunt(string taunt, bool all)
        {
            if (taunt.Contains("{PLAYER}"))
            {
                float dist = 10;
                Entity player = GetNearestEntity(ref dist, null);
                if (player == null) return;
                taunt = taunt.Replace("{PLAYER}", player.Name);
            }
            taunt = taunt.Replace("{HP}", (Host as Enemy).HP.ToString());
            try
            {
                Host.Self.Owner.BroadcastPacket(new TextPacket()
                {
                    Name = "#" + (Host.Self.ObjectDesc.DisplayId ?? Host.Self.ObjectDesc.ObjectId),
                    ObjectId = Host.Self.Id,
                    Stars = -1,
                    BubbleTime = 5,
                    Recipient = "",
                    Text = taunt,
                    CleanText = ""
                }, null);
            }
            catch
            {
                Program.logger.Info("Crash halted - Nobody likes death...");
            }
        }

        protected abstract override bool TickCore(RealmTime time);
    }

    internal class SimpleTaunt : TauntBase
    {
        private string taunt;

        public SimpleTaunt(string taunt)
        {
            this.taunt = taunt;
        }

        protected override bool TickCore(RealmTime time)
        {
            Taunt(taunt, false);
            return true;
        }
    }
}
