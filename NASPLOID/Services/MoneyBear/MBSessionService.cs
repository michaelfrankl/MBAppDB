using System.Collections.ObjectModel;
using Microsoft.AspNetCore.DataProtection;
using NASPLOID.Foundation;
using NASPLOID.Models;
using NASPLOID.Models.MoneyBear;

namespace NASPLOID.Services.MoneyBear;

public class MBSessionService
{
    public static async Task<MBSessionContext> CreateMBSessionContext(CancellationToken cancellationToken)
    {
        MBSessionContext session = CreateSessionContext();
        await LoadSessionDataAsync(session, cancellationToken);
        await CalculateSessionDataAsync(session, cancellationToken);
        await SaveCalculatedSessionDataAsync(session, cancellationToken);
        return session;
    }
    
    public static async Task<MBSessionContext> CreateMBSessionContext(string username, string password, IDataProtector protector, CancellationToken cancellationToken)
    {
        MBSessionContext session = CreateSessionContext();
        await LoadSessionDataAsync(session, cancellationToken);
        await CalculateSessionDataAsync(session, cancellationToken);
        //await SaveCalculatedSessionDataAsync(session, cancellationToken);

        MBUser user = await GetUserFromSessionContext(session, username, password, protector, cancellationToken);
        session.User = user;
        return session;
    }

    public static async Task<(int, string, MBUser)> IsUserListedInDataBase(string username, string password, IDataProtector protector,
        CancellationToken cancellationToken)
    {
        string login = string.Empty;
        MBUser user;
        (login, user) = await GetUserFromDatabase(username, password, protector, cancellationToken);
        if (login == Resources.Success)
            return (0, Resources.Success, user);
        if (login == Resources.UserNotFound)
            return (1, Resources.UserNotFound, user);
        if (login == Resources.Credentials)
            return (2, Resources.Credentials, user);
        return (3, Resources.MiscError, user);
    }


    private static async Task<(string, MBUser)> GetUserFromDatabase(string username, string password, IDataProtector protector,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        MB_USER user = context.MB_USERs.FirstOrDefault(x => x.USERNAME == username);
        if (user == null)
            return (Resources.UserNotFound, null);
        string storedPassword = protector.Unprotect(user.PASSWORD);
        if (storedPassword.Equals(password))
        {
            MBUser newUser = new MBUser()
            {
                Id = user.ID,
                Name = user.USERNAME,
                Email = user.EMAIL,
                Password = user.PASSWORD,
                Role = user.ROLE,
            };
            return (Resources.Success, newUser);
        }
        else
            return (Resources.Credentials, null);
    }
    
    private static async Task<MBUser> GetUserFromSessionContext(MBSessionContext session, string username, string password, IDataProtector protector,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        MBUser user = session.Users.FirstOrDefault(x => x.Name == username);
        if (user == null)
            session.LoginError = Resources.UserNotFound;
        string storedPassword = protector.Unprotect(user.Password);
        if (storedPassword.Equals(password))
        {
            session.LoginError = string.Empty;
            return user;
        }
        else
        {
            session.LoginError = Resources.Credentials;
        }
        return user;
    }

    public static async Task<(ObservableCollection<GridLine> lines, ObservableCollection<GridStorage> storages)>
        RefreshGridDataAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ObservableCollection<GridLine> lines = await MBGridService.LoadAllGridLinesAsync(cancellationToken);
        ObservableCollection<GridStorage> storages = await MBGridService.LoadAllGridStorageAsync(cancellationToken);
        return (lines, storages);
    }

    private static async Task LoadSessionDataAsync(MBSessionContext session, CancellationToken cancellationToken)
    {
        var dataTask = Task.WhenAny(
            session.LoadAllGridLinesAsync(cancellationToken),
            session.LoadAllGridStorageAsync(cancellationToken),
            session.LoadAllLineCategoriesAsync(cancellationToken),
            session.LoadAllChartColorsAsync(cancellationToken),
            session.LoadAllLineTypesAsync(cancellationToken),
            session.LoadAllLineStylesAsync(cancellationToken),
            session.LoadAllLineColorsAsync(cancellationToken),
            session.LoadAllLineFrequencyAsync(cancellationToken),
            session.LoadAllUserRoles(cancellationToken),
            session.LoadAllUsers(cancellationToken),
            session.LoadAllLineCalculationsAsync(cancellationToken),
            session.LoadAllErrorCodesAsync(cancellationToken),
            session.LoadAllGridImportsAsync(cancellationToken),
            session.LoadAllGridImportValuesAsync(cancellationToken),
            session.LoadAllGlobalVariablesAsync(cancellationToken),
            session.LoadAllMBDebtUserAsync(cancellationToken),
            session.LoadAllMBDebtListAsync(cancellationToken),
            session.LoadAllMBDebtTypesAsync(cancellationToken)
            );
        await dataTask.ConfigureAwait(true);
    }

    private static async Task CalculateSessionDataAsync(MBSessionContext session, CancellationToken cancellationToken)
    {
        var dataTask = Task.WhenAny(
            session.SetAllGridLinesValuesAsync(cancellationToken),
            session.CalculateAllGridSumLinesAsync(cancellationToken));
        await dataTask.ConfigureAwait(true);
    }

    private static async Task SaveCalculatedSessionDataAsync(MBSessionContext session,
        CancellationToken cancellationToken)
    {
        var dataTask = Task.WhenAny(
            session.SaveCalculatedGridSumLineAsnyc(cancellationToken));
        await dataTask.ConfigureAwait(true);
    }
    
    public static MBSessionContext CreateSessionContext()
        => new MBSessionContext();
}