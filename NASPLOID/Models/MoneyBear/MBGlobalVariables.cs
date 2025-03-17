using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay("{_id}/{_userId} | {_valueAsString}/{_valueAsDecimal} | URL: {_url}" )]
public partial class MBGlobalVariables : Model
{
    public MBGlobalVariables()
    {
        
    }

    public MBGlobalVariables(string id, int userId, string valueAsString, int valueAsDecimal, string url)
    {
        Id = id;
        UserId = userId;
        ValueAsString = valueAsString;
        ValueAsDecimal = valueAsDecimal;
        Url = url;
    }
    
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _id;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _userId;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? _valueAsString;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private decimal? _valueAsDecimal;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? _url;
}