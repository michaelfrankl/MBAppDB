using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay(("{_roleId}"))]
public partial class MBRole: Model
{
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _roleId;

    public MBRole(string roleName)
    {
        RoleId = roleName;
    }
    public MBRole()
    {
        
    }

    public MBRole Clone()
    {
        return new MBRole()
        {
            RoleId = RoleId,
        };
    }
}
