using System.ComponentModel;
using System.Runtime.CompilerServices;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using XPTracker.Mediator;

public class Player : INotifyPropertyChanged
{
    private string _name;
    private int _xp;

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    public int XP
    {
        get => _xp;
        set
        {
            int oldXP = _xp;
            _xp = Math.Max(0, value);
            OnPropertyChanged();
            _mediator?.NotifyXPChanged(this, oldXP, _xp);
        }
    }



    private IXPMediator? _mediator;
    public void SetMediator(IXPMediator mediator)
    {
        _mediator = mediator;
    }

    public Player()
    {
        _name = "Player";
        _xp = 0;
    }


    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
