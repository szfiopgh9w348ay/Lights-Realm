using wServer.realm;

namespace wServer.logic
{
    internal class OnDeath : ConditionalBehavior
    {
        private Behavior behav;

        public OnDeath(Behavior behav)
        {
            this.behav = behav;
        }

        public override BehaviorCondition Condition
        {
            get { return BehaviorCondition.OnDeath; }
        }

        protected override void BehaveCore(BehaviorCondition cond, RealmTime? time, object state)
        {
            behav.Tick(Host, time.Value);
        }
    }

    internal class OnHit : ConditionalBehavior
    {
        private Behavior behav;

        public OnHit(Behavior behav)
        {
            this.behav = behav;
        }

        public override BehaviorCondition Condition
        {
            get { return BehaviorCondition.OnHit; }
        }

        protected override void BehaveCore(BehaviorCondition cond, RealmTime? time, object state)
        {
            behav.Tick(Host, time.Value);
        }
    }
}
