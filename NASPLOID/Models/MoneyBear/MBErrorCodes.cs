using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay("ID | {_id} | {_errorCode}" )]
public partial class MBErrorCodes : Model
{
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int _id;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? _errorCode;
}