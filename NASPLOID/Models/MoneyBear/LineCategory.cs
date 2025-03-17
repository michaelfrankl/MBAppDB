using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay("Category | {_id}")]
public partial class LineCategory : Model
{
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)] 
    private string _id;

    public LineCategory(string _id)
    {
        Id = _id;
    }

    public LineCategory()
    {
        
    }

    public LineCategory Clone()
    {
        return new LineCategory()
        {
            Id = Id
        };
    }
}