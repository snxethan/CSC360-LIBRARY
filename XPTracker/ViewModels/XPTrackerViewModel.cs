using System.Collections.ObjectModel;
using System.Windows.Input;
using XPTracker.Services;
using XPTracker.Mediator;

namespace XPTracker.ViewModels
{
    public class XPTrackerViewModel : BindableObject
    {
        private int _playerCount = 1;
        private string? _currentFilePath;
        private readonly IXPMediator _mediator = new XPMediator();

        public ObservableCollection<Player> Players
        {
            get => _players;
            set
            {
                _players = value;
                OnPropertyChanged(nameof(Players));
            }
        }
        private ObservableCollection<Player> _players = new();

        public ICommand AddPlayerCommand { get; }
        public ICommand RemovePlayerCommand { get; }
        public ICommand RenamePlayerCommand { get; }
        public ICommand ModifyXPCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand LoadCommand { get; }
        public ICommand NewCommand { get; }

        public XPTrackerViewModel()
        {
            AddPlayerCommand = new Command(AddPlayer);
            RemovePlayerCommand = new Command<Player>(RemovePlayer);
            ModifyXPCommand = new Command<(Player, int)>(ModifyXP);
            RenamePlayerCommand = new Command<Player>(player => player.Name = $"Player {_playerCount++}");
            SaveCommand = new Command(async () => await SavePlayers());
            SaveAsCommand = new Command(async () => await SavePlayersAs());
            LoadCommand = new Command(async () => await LoadPlayers());
            NewCommand = new Command(NewButton);
        }

        private void NewButton()
        {
            Players.Clear();
            _playerCount = 0;
            _currentFilePath = null;
            PlayerDataManager.ClearSession();
            _mediator.NotifyNewSessionStarted();
        }

        private void AddPlayer()
        {
            var player = new Player();
            player.SetMediator(_mediator);
            Players.Add(player);
            _mediator.NotifyPlayerAdded(player);
            OnPropertyChanged(nameof(Players));
        }

        private void RemovePlayer(Player player)
        {
            if (player != null)
            {
                Players.Remove(player);
                _mediator.NotifyPlayerRemoved(player);
                OnPropertyChanged(nameof(Players));
            }
        }

        private void ModifyXP((Player player, int amount) data)
        {
            if (data.player != null)
                data.player.XP += data.amount;
        }

        private async Task SavePlayers()
        {
            if (!PlayerDataManager.HasActiveFile)
            {
                await SavePlayersAs();
                return;
            }

            await PlayerDataManager.SaveToFile(Players);
            _mediator.NotifyFileSaved(PlayerDataManager.CurrentFilePath);
        }

        private async Task SavePlayersAs()
        {
            var filePath = await PlayerDataManager.PromptSaveFilePath();
            if (filePath == null) return;

            bool success = await PlayerDataManager.SaveToFile(Players, filePath);
            if (success)
            {
                await Application.Current.MainPage.DisplayAlert("Success", $"File saved at:\n{filePath}", "OK");
                _mediator.NotifyFileSaved(filePath);
            }
        }

        private async Task LoadPlayers()
        {
            var loadedPlayers = await PlayerDataManager.LoadFromFile();
            if (loadedPlayers == null) return;

            Players.Clear();
            foreach (var player in loadedPlayers)
            {
                player.SetMediator(_mediator);
                Players.Add(player);
            }

            _mediator.NotifyFileLoaded(loadedPlayers);
            await Application.Current.MainPage.DisplayAlert("Success", "Players loaded successfully.", "OK");
        }
    }
}
