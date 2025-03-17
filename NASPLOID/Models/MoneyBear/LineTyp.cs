using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay("Typ | {_id}")]
public partial class LineTyp : Model
{
    public LineTyp(int id, string type)
    {
        Id = id;
        Typ = type;
    }

    public LineTyp()
    {
        
    }

    public LineTyp Clone()
    {
        return new LineTyp()
        {
            Id = Id,
            Typ = Typ
        };
    }
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int _id;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? _typ;
}