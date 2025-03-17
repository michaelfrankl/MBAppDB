using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay(("Name: {_name} || From User: {_userId}"))]
public partial class MBDebtUser: Model
{
    public MBDebtUser()
    {
        
    }

    public MBDebtUser(string name, int userId)
    {
        Name = name;
        UserId = userId;
    }

    public MBDebtUser Clone()
    {
        return new MBDebtUser()
        {
            Name = Name,
            UserId = UserId
        };
    }
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _name;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int _userId;
}