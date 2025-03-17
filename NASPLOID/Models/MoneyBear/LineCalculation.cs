using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay("Calculation | {_id} -> {_calculation}")]
public partial class LineCalculation : Model
{
    public LineCalculation(int id, string calculation)
    {
        Id = id;
        Calculation = calculation;
    }

    public LineCalculation()
    {
        
    }
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)] 
    private int _id;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)] 
    private string? _calculation;
}