using System;
using wServer.realm;

namespace wServer.logic.taunt
{
    internal class RandomTaunt : TauntBase
    {
        public RandomTaunt(double prob, string taunt)
        {
            this.prob = prob; this.taunt = taunt;
        }

        private double prob;
        private string taunt;

        private Random rand = new Random();

        protected override bool TickCore(RealmTime time)
        {
            if (rand.NextDouble() > prob) return false;
            Taunt(taunt, false);
            return true;
        }
    }
}
