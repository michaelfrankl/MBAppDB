using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay(("{_id} | {_name}"))]
public partial class MBUser : Model
{
    [ObservableProperty] [DebuggerBrowsable((DebuggerBrowsableState.Never))]
    private int _id;
    [ObservableProperty] [DebuggerBrowsable((DebuggerBrowsableState.Never))]
    private string? _name;
    [ObservableProperty] [DebuggerBrowsable((DebuggerBrowsableState.Never))]
    private string? _email;
    [ObservableProperty] [DebuggerBrowsable((DebuggerBrowsableState.Never))]
    private string? _password;
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? _role;
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private bool isAdministrator;

    public MBUser()
    {
        
    }

    public MBUser(int id, string? name, string? email, string? password, string? role, bool isAdministrator)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        Role = role;
        IsAdministrator = CheckAdmin(role);
    }
    private bool CheckAdmin(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            return false;

        if (role.ToLower().Equals("administrator"))
            return true;
        else
            return false;
    }

    public MBUser Clone()
    {
        return new MBUser()
        {
            Id = Id,
            Name = Name,
            Email = Email,
            Password = Password,
            Role = Role,
            IsAdministrator = CheckAdmin(Role)
        };
    }

    public MBRole ToRole(string role)
    {
        if (role == null)
            return new MBRole("Default");
        switch (role.ToLower())
        {
            case "administrator":
                return new MBRole("Administrator");
            case "tester":
                return new MBRole("Tester");
            case "nutzer":
                return new MBRole("Nutzer");
            case "demo":
                return new MBRole("Demo");
            default:
                return new MBRole("Default");
        }
    }
}