using wServer.logic.movement;

namespace wServer.logic
{
    partial class BehaviorDb
    {
        private static _ Pets = Behav()
            .InitMany(0x1600, 0x1641, Behaves("Pet",
                If.Instance(new PetBehaves(), PetChasing.Instance(8, 10, 3))
            ));
    }
}
