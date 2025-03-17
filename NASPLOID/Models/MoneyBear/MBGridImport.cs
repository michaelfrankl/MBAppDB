using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay("ID/UserId | {_id}/{_userId} | {_month}/{_year} | Status: {_status}" )]
public partial class MBGridImport: Model
{
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _id;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _userId;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _month;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _year;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _status;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private DateOnly? _lastImport;
}