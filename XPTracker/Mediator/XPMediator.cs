using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace XPTracker.Mediator
{
    public class XPMediator : IXPMediator
    {
        public async void NotifyXPChanged(Player player, int oldXP, int newXP)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player), "Player cannot be null.");
            }

            if (oldXP < 0 || newXP < 0)
            {
                throw new ArgumentOutOfRangeException("XP values cannot be negative.");
            }

            if (newXP > 100 && oldXP <= 100)
            {
                Console.WriteLine($"🎉 {player.Name} has reached 100 XP!");
                await Application.Current.MainPage.DisplayAlert("Milestone", $"{player.Name} has reached 100 XP!",
                    "OK");
            }

            if (newXP > 500 && oldXP <= 500)
            {
                Console.WriteLine($"🎉 {player.Name} has reached 500 XP!");
                await Application.Current.MainPage.DisplayAlert("Milestone", $"{player.Name} has reached 500 XP!",
                    "OK");
            }

            if (newXP >= 1000 && oldXP < 1000)
            {
                Console.WriteLine($"🎉 {player.Name} has reached 1000 XP!");
                await Application.Current.MainPage.DisplayAlert("Milestone", $"{player.Name} has reached 1000 XP!",
                    "OK");
            }

            if (newXP >= 5000 && oldXP < 5000)
            {
                Console.WriteLine($"🏆 {player.Name} has reached 5000 XP!");
                await Application.Current.MainPage.DisplayAlert("Milestone", $"{player.Name} has reached 5000 XP!",
                    "OK");
            }

            if (newXP < oldXP)
            {
                Console.WriteLine($"⚠️ {player.Name} lost XP.");
                await Application.Current.MainPage.DisplayAlert("Warning", $"{player.Name} lost XP.", "OK");
            }
        }

        public async void NotifyPlayerAdded(Player player)
        {
            Console.WriteLine($"➕ {player.Name} was added.");
            await Application.Current.MainPage.DisplayAlert("Player Added", $"{player.Name} was added.", "OK");
        }

        public async void NotifyPlayerRemoved(Player player)
        {
            Console.WriteLine($"❌ {player.Name} was removed.");
            await Application.Current.MainPage.DisplayAlert("Player Removed", $"{player.Name} was removed.", "OK");
        }

        public async void NotifyFileSaved(string? filePath)
        {
            Console.WriteLine($"💾 File saved at: {filePath}");
            await Application.Current.MainPage.DisplayAlert("Saved", $"File saved at:\n{filePath}", "OK");
        }

        public async void NotifyFileLoaded(ObservableCollection<Player> players)
        {
            Console.WriteLine($"📂 File loaded with {players.Count} player(s).");
            await Application.Current.MainPage.DisplayAlert("File Loaded", $"Loaded {players.Count} player(s).", "OK");
        }

        public async void NotifyNewSessionStarted()
        {
            Console.WriteLine("🆕 New session started.");
            await Application.Current.MainPage.DisplayAlert("New Session", "A new session has started.", "OK");
        }
    }
}
