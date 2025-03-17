using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay("ID | {_id} | LineId {_lineId} | Import-Values: {_value}" )]
public partial class MBGridImportValues: Model
{
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _id;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _lineId;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _userId;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private decimal? _value;
}