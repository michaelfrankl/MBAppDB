using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay("Frequency | {_id}")]
public partial class LineFrequency : Model
{
    public LineFrequency()
    {
        
    }

    public LineFrequency(string id, int frequency)
    {
        Id = id;
        Frequency = frequency;
    }

    public LineFrequency Clone()
    {
        return new LineFrequency()
        {
            Id = Id,
            Frequency = Frequency
        };
    }
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)] 
    private string _id;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _frequency;
}