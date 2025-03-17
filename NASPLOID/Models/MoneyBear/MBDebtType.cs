using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay(("DebtType: {_id}"))]
public partial class MBDebtType: Model
{

    public MBDebtType(string typeName)
    {
        Id = typeName;
    }

    public MBDebtType()
    {
        
    }

    public MBDebtType Clone()
    {
        return new MBDebtType()
        {
            Id = Id
        };
    }
    
    [ObservableProperty] private string _id;
}