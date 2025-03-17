using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using NASPLOID.Models;
using NASPLOID.Models.MoneyBear;
using Org.BouncyCastle.Crypto.Macs;

namespace NASPLOID.Services.MoneyBear;

public static class MBUSerService
{
    public static async Task LoadAllUsers(this MBSessionContext session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        List<MBUser> users = (await context.MB_USERs.ToListAsync(cancellationToken).ConfigureAwait(false)).Select(ToUser)
            .ToList();
        session.Users = new ObservableCollection<MBUser>(users);
    }

    public static async Task LoadAllUserRoles(this MBSessionContext session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        List<MBRole> roles = (await context.MB_ROLEs.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToAccountRole).ToList();
        session.Roles = new ObservableCollection<MBRole>(roles);
    }

    public static async Task<int> AddUser(CancellationToken cancellationToken, MBUser newUser)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        
        MB_USER? user = await context.MB_USERs.Where(x => x.USERNAME == newUser.Name && x.EMAIL == newUser.Email).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        if (user != null)
            return (int)ErrorEnum.AlreadyExists;

        int newId = await context.MB_USERs.MaxAsync(x => x.ID, cancellationToken).ConfigureAwait(false) + 1;

        user = new MB_USER()
        {
            ID = newId,
            USERNAME = newUser.Name,
            EMAIL = newUser.Email,
            PASSWORD = newUser.Password,
            ROLE = newUser.Role
        };
        
        await context.MB_USERs.AddAsync(user, cancellationToken).ConfigureAwait(false);
        if (await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0)
        {
            return (int)ErrorEnum.Success;
        }
        else
        {
            return (int)ErrorEnum.Aborted;
        }
    }

    public static async Task<int> DeleteUser(CancellationToken cancellationToken, MBUser userToDelete)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        
        MB_USER? user = await context.MB_USERs.FindAsync(new object[]{userToDelete.Id}, cancellationToken).ConfigureAwait(false);
        if (user == null)
            return (int)ErrorEnum.NoMatchFound;
        context.MB_USERs.Remove(user);
        if (await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0)
            return (int)ErrorEnum.Success;
        return (int)ErrorEnum.Aborted;
    }

    public static async Task<int> EditUser(CancellationToken cancellationToken, MBUser uerToEdit, bool passwordChangeAllowed)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        
        MB_USER? user = await context.MB_USERs.FindAsync(new object[]{uerToEdit.Id}, cancellationToken).ConfigureAwait(false);
        if(user == null)
            return (int)ErrorEnum.NoMatchFound;

        int changeCounter = 0;
        if (user.USERNAME != uerToEdit.Name)
        {
            user.USERNAME = uerToEdit.Name;
            changeCounter++;
        }
        if (user.EMAIL != uerToEdit.Email)
        {
            user.EMAIL = uerToEdit.Email;
            changeCounter++;
        }
        if (user.PASSWORD != uerToEdit.Password && passwordChangeAllowed)
        {
            user.PASSWORD = uerToEdit.Password;
            changeCounter++;
        }
        if (user.ROLE != uerToEdit.Role)
        {
            user.ROLE = uerToEdit.Role;
            changeCounter++;
        }
        if (changeCounter == 0)
            return (int)ErrorEnum.ValueNotChanged;

        if (await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0)
            return (int)ErrorEnum.Success;
        
        return (int)ErrorEnum.Aborted;
    }

    private static MBUser ToUser(MB_USER value)
        => new MBUser()
        {
            Id = value.ID,
            Name = value.USERNAME,
            Email = value.EMAIL,
            Password = value.PASSWORD,
            Role =  value.ROLE
        };

    private static MBRole ToAccountRole(MB_ROLE value)
        => new MBRole()
        {
            RoleId = value.ROLE
        };
}