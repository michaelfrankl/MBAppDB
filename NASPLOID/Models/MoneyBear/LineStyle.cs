using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay("Style | {_id} ")]
public partial class LineStyle : Model
{
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int _id;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? _style;
}