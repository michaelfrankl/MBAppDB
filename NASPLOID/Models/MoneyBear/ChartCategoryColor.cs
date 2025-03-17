using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay("Id | {_id} | Category: {_categoryId} | R:{_r} | G:{_g} | B:{_b}")]
public partial class ChartCategoryColor : Model
{
    public ChartCategoryColor(int id, string categoryId, int userId, int r, int g, int b)
    {
        Id = id;
        CategoryId = categoryId;
        UserId = userId;
        R = r;
        G = g;
        B = b;

    }
    public ChartCategoryColor()
    {
        
    }

    public ChartCategoryColor Clone()
    {
        return new ChartCategoryColor()
        {
            Id = Id,
            CategoryId = CategoryId,
            UserId = UserId,
            R = R,
            G = G,
            B = B
        };
    }
    
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int _id;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? _categoryId;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _userId;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int _r;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int g;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int b;

}