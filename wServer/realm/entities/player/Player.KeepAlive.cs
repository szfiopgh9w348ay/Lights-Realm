using System.Collections.Generic;
using wServer.networking.cliPackets;
using wServer.networking.svrPackets;

namespace wServer.realm.entities
{
    public partial class Player
    {
        private long lastPong = -1;
        private int? lastTime = null;
        private long tickMapping = 0;
        private Queue<long> ts = new Queue<long>();

        private bool sentPing = false;

        private bool KeepAlive(RealmTime time)
        {
            if (lastPong == -1) lastPong = time.tickTimes - 1500;
            if (time.tickTimes - lastPong > 1500 && !sentPing)
            {
                sentPing = true;
                ts.Enqueue(time.tickTimes);
                client.SendPacket(new PingPacket());
            }
            else if (time.tickTimes - lastPong > 3000)
            {
                ;//psr.Disconnect();
                return false;
            }
            return true;
        }

        public void Pong(PongPacket pkt)
        {
            if (lastTime != null && (pkt.Time - lastTime.Value > 3000 || pkt.Time - lastTime.Value < 0))
                ;//psr.Disconnect();
            else
                lastTime = pkt.Time;
            tickMapping = ts.Dequeue() - pkt.Time;
            lastPong = pkt.Time + tickMapping;
            sentPing = false;
        }
    }
}
