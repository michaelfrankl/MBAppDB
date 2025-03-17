using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay("Color | {_id} | fg:{_foreground} | bg:{_background}")]
public partial class LineColor: Model
{
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int _id;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? _foreground;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? _background;
}