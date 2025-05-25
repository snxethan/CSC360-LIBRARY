using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPTracker.Mediator
{
    public interface IXPMediator
    {
        void NotifyXPChanged(Player player, int oldXP, int newXP);
        void NotifyPlayerAdded(Player player);
        void NotifyPlayerRemoved(Player player);
        void NotifyFileSaved(string? filePath);
        void NotifyFileLoaded(ObservableCollection<Player> players);
        void NotifyNewSessionStarted();

    }
}
