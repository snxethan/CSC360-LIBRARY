using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// The IPlayerXPState interface defines the contract for player XP state management.
namespace XPTracker.States
{
    public interface IPlayerXPState
    {
        void ModifyXP(Player player, int amount);
        string Name { get; }
    }

}
