using wServer.realm.entities;

namespace wServer.realm.commands
{
    internal interface ICommand
    {
        string Command { get; }
        int RequiredRank { get; }

        void Execute(Player player, string[] args);
    }
}
