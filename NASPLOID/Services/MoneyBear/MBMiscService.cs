using System.Collections.ObjectModel;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using NASPLOID.Models;
using NASPLOID.Models.MoneyBear;

namespace NASPLOID.Services.MoneyBear;

public static class MBMiscService
{
    public static async Task LoadAllErrorCodesAsync(this MBSessionContext session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        List<MBErrorCodes> errorCodes =
            (await context.MB_ERROR_CODEs.ToListAsync(cancellationToken).ConfigureAwait(false)).Select(ToErrorCodes)
            .ToList();

        session.ErrorCodes = new ObservableCollection<MBErrorCodes>(errorCodes);
    }

    public static async Task LoadAllGridImportValuesAsync(this MBSessionContext session,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        List<MBGridImportValues> gridImportValues =
            (await context.GRID_IMPORT_VALUEs.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToGridImportValues).ToList();

        session.GridImportValues = new ObservableCollection<MBGridImportValues>(gridImportValues);
    }

    public static async Task LoadAllGridImportsAsync(this MBSessionContext session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        List<MBGridImport> gridImports =
            (await context.GRID_IMPORTs.ToListAsync(cancellationToken).ConfigureAwait(false)).Select(ToGridImport)
            .ToList();

        session.GridImports = new ObservableCollection<MBGridImport>(gridImports);
    }

    public static async Task<List<MBGridImport>?> LoadLatestGridAutoImportAsync(CancellationToken cancellationToken, int month, int year, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        var latestGridImports = (await context.GRID_IMPORTs.Where(x => x.USER_ID == userId)
                .ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToGridImport).ToList();

        if (latestGridImports.Count <= 0)
            return null;

        return latestGridImports;
    }

    public static async Task LoadAllChartColorsAsync(this MBSessionContext session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        List<ChartCategoryColor> chartCategoryColors =
            (await context.CHART_CATEGORY_COLORs.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToChartCategoryColor).ToList();
        
        session.ChartCategoryColors = new ObservableCollection<ChartCategoryColor>(chartCategoryColors);
    }

    public static async Task LoadAllGlobalVariablesAsync(this MBSessionContext session,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        
        List<MBGlobalVariables> globalVariables = (await context.MB_GLOBAL_VARIABLEs.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToGlobalVariables).ToList();

        session.GlobalVariables = new ObservableCollection<MBGlobalVariables>(globalVariables);
    }

    public static async Task LoadAllMBDebtUserAsync(this MBSessionContext session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        
        List<MBDebtUser> debtUsers = (await context.MB_DEBT_USERs.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToMBDebtUser).ToList();

        session.DebtUSer = new ObservableCollection<MBDebtUser>(debtUsers);
    }

    public static async Task LoadAllMBDebtListAsync(this MBSessionContext session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        List<MBDebtList> debtLists = (await context.MB_DEBT_LISTs.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToMBDebtList).ToList();

        session.DebtList = new ObservableCollection<MBDebtList>(debtLists);
    }

    public static async Task LoadAllMBDebtTypesAsync(this MBSessionContext session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        
        List<MBDebtType> debtTypes = (await context.MB_DEBT_TYPEs.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToMBDebtType).ToList();
        
        session.DebtTypes = new ObservableCollection<MBDebtType>(debtTypes);
    }

    public static async Task<int> CheckGridImport(CancellationToken cancellationToken, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        DateTime currentDate = DateTime.Now;
        GRID_IMPORT import = await context.GRID_IMPORTs.FirstOrDefaultAsync(
            x => x.MONTH == currentDate.Month && x.YEAR == currentDate.Year && x.USER_ID == userId, cancellationToken);
        if (import == null)
            return (int)ErrorEnum.NoImportFound;
        if (import.STATUS == 0)
            return (int)ErrorEnum.NoImportFound;
        return (int)ErrorEnum.AlreadyImported;
    }

    public static async Task<int> ImportGridDefaultValuesAsync(CancellationToken cancellationToken, int userId,
        ObservableCollection<GridLine> lines, ObservableCollection<MBGridImport> gridImports,
        ObservableCollection<MBGridImportValues> gridImportValues,
        DateTime dateToImport)
    {
        DateOnly date = new DateOnly(dateToImport.Year, dateToImport.Month, dateToImport.Day);
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        if (lines == null || gridImports == null || gridImportValues == null)
            return (int)ErrorEnum.NullReference;

        lines = new ObservableCollection<GridLine>(lines.Where(x =>
            x.Id == userId && x.ValidTo >= date && x.ValidFrom <= date));
        int errorCount = 0;

        foreach (GridLine gridLine in lines)
        {
            var importValue = gridImportValues.FirstOrDefault(x => x.LineId == gridLine.Id && x.UserId == userId);
            if (importValue != null)
            {
                gridLine.JanuarValue = importValue.Value;
                int result = await MBGridService.ImportNewGridLineValueAsync(cancellationToken, gridLine, userId,
                    dateToImport.Month, dateToImport.Year);
                if (result == (int)ErrorEnum.Aborted)
                    errorCount++;
            }
        }

        if (errorCount == 0)
        {
            var import =
                gridImports.FirstOrDefault(x => x.UserId == userId && x.Month == date.Month && x.Year == date.Year);
            if (import == null)
            {
                GRID_IMPORT newImport = new GRID_IMPORT()
                {
                    USER_ID = userId,
                    MONTH = date.Month,
                    YEAR = date.Year,
                    STATUS = 1,
                    LAST_IMPORT = date
                };
                await context.GRID_IMPORTs.AddAsync(newImport, cancellationToken);
                if (await context.SaveChangesAsync(cancellationToken) > 0)
                    return (int)ErrorEnum.Success;
            }
            else
            {
                GRID_IMPORT? importValue = await context.GRID_IMPORTs.FirstOrDefaultAsync(x =>
                    x.USER_ID == userId && x.MONTH == date.Month && x.YEAR == date.Year, cancellationToken);
                if (importValue == null)
                    return (int)ErrorEnum.NullReference;
                importValue.LAST_IMPORT = date;
                importValue.STATUS = 1;
                if (await context.SaveChangesAsync(cancellationToken) > 0)
                    return (int)ErrorEnum.Success;
            }
        }

        return (int)ErrorEnum.Aborted;
    }

    public static async Task<int> SetDefaultGridValuesAsync(CancellationToken cancellationToken, GridLine gridLine,
        int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        GRID_IMPORT_VALUE value = await context.GRID_IMPORT_VALUEs
            .Where(x => x.USER_ID == userId && x.GZ_ID == gridLine.Id).FirstOrDefaultAsync(cancellationToken);
        if (value == null)
        {
            GRID_IMPORT_VALUE newImportValue = new GRID_IMPORT_VALUE()
            {
                GZ_ID = gridLine.Id,
                USER_ID = userId,
                VALUE = gridLine.JanuarValue
            };
            await context.GRID_IMPORT_VALUEs.AddAsync(newImportValue, cancellationToken);
            if (await context.SaveChangesAsync(cancellationToken) > 0)
                return (int)ErrorEnum.Success;
            return (int)ErrorEnum.Aborted;
        }

        if (value.VALUE == gridLine.JanuarValue)
            return (int)ErrorEnum.ValueNotChanged;
        value.VALUE = gridLine.JanuarValue;
        if (await context.SaveChangesAsync(cancellationToken) > 0)
            return (int)ErrorEnum.Success;
        return (int)ErrorEnum.Aborted;
    }

    public static async Task<int> SetDefaultGridValuesAsync(CancellationToken cancellationToken, GridLine gridLine)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        GRID_IMPORT_VALUE value = await context.GRID_IMPORT_VALUEs
            .Where(x => x.USER_ID == gridLine.Userid && x.GZ_ID == gridLine.Id).FirstOrDefaultAsync(cancellationToken);
        if (value == null)
        {
            GRID_IMPORT_VALUE newImportValue = new GRID_IMPORT_VALUE()
            {
                GZ_ID = gridLine.Id,
                USER_ID = gridLine.Userid,
                VALUE = gridLine.JanuarValue
            };
            await context.GRID_IMPORT_VALUEs.AddAsync(newImportValue, cancellationToken);
            return (int)ErrorEnum.Success;
        }

        if (value.VALUE == gridLine.JanuarValue)
            return (int)ErrorEnum.ValueNotChanged;
        value.VALUE = gridLine.JanuarValue;
        if (await context.SaveChangesAsync(cancellationToken) > 0)
            return (int)ErrorEnum.Success;
        return (int)ErrorEnum.Aborted;
    }

    public static async Task<MBGridImportValues> GetGridImportValueAsync(CancellationToken cancellationToken,
        int lineId, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        GRID_IMPORT_VALUE? value = await context.GRID_IMPORT_VALUEs.Where(x => x.GZ_ID == lineId && x.USER_ID == userId)
            .FirstOrDefaultAsync(cancellationToken);
        if (value == null)
            return new MBGridImportValues()
            {
                Id = 0,
                LineId = 0,
                UserId = 0,
                Value = 0
            };

        return new MBGridImportValues()
        {
            Id = value.ID,
            LineId = value.GZ_ID,
            UserId = value.USER_ID,
            Value = value.VALUE,
        };
    }

    public static async Task<int> ImportValuesForSpecificLinesAndMonthAsync(CancellationToken cancellationToken,
        ObservableCollection<GridLine> lines, int month, int year, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        await using (var transaction = await context.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                List<GRID_STORAGE> gridStorages = new List<GRID_STORAGE>();
                foreach (GridLine gridLine in lines)
                {
                    GRID_STORAGE? lineStorage = await context.GRID_STORAGEs.FirstOrDefaultAsync(
                        x => x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year &&
                             x.LINE_USERID == userId, cancellationToken);
                    if (lineStorage == null)
                    {
                        GRID_STORAGE newLineStorage = new GRID_STORAGE()
                        {
                            GL_ID = gridLine.Id,
                            VALUE = 0,
                            LINE_DAY = gridLine.ValidFrom.Day,
                            LINE_MONTH = month,
                            LINE_YEAR = year,
                            LINE_USERID = userId,
                            LINE_NOTE = null,
                            LINE_LASTEDIT = DateTime.Now,
                        };
                        await context.GRID_STORAGEs.AddAsync(newLineStorage, cancellationToken);
                        gridStorages.Add(newLineStorage);
                    }
                    else
                        gridStorages.Add(lineStorage);
                }

                int importCounter = 0;

                foreach (var storage in gridStorages)
                {
                    GRID_LINE line = context.GRID_LINEs.First(x =>
                        x.ID == storage.GL_ID && x.LINE_USERID == storage.LINE_USERID);
                    if (line.LINE_VALIDFROM.Month > month && line.LINE_VALIDFROM.Year >= year)
                        continue;
                    if (line.LINE_VALIDTO.Month < month && line.LINE_VALIDTO.Year < year)
                        continue;

                    var validImport = await MBGridService.IsGridLineValidForImportAfterFrequencyCheckAsync(
                        cancellationToken, line, month,
                        year);

                    if (validImport)
                    {
                        MBGridImportValues? importValue =
                            await GetGridImportValueAsync(cancellationToken, storage.GL_ID, userId);
                        if (importValue.Id != 0)
                        {
                            storage.VALUE = importValue.Value;
                            storage.LINE_LASTEDIT = DateTime.Now;
                        }
                    }
                    else
                    {
                        storage.VALUE = 0;
                        storage.LINE_LASTEDIT = DateTime.Now;
                    }

                    importCounter++;
                    await context.SaveChangesAsync(cancellationToken);
                }

                if (importCounter == 0)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return (int)ErrorEnum.NoValidImport;
                }
                await context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return (int)ErrorEnum.Success;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync(cancellationToken);
                return (int)ErrorEnum.Aborted;
            }
        }
    }

    public static async Task<int> ImportValuesForSpecificMonthAsync(CancellationToken cancellationToken, int month,
        int year, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        await using (var transaction = await context.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                var gridStorageResult = await MBGridService.GenerateValidGridLineForSpecificMonthAsync(context,
                    cancellationToken, month,
                    year, userId);
                if (gridStorageResult == (int)ErrorEnum.Aborted || gridStorageResult == (int)ErrorEnum.NullReference)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return (int)ErrorEnum.Aborted;
                }

                List<GRID_STORAGE> gridStorages = await context.GRID_STORAGEs
                    .Where(x => x.LINE_USERID == userId && x.LINE_MONTH == month && x.LINE_YEAR == year)
                    .ToListAsync(cancellationToken);

                int importCounter = 0;
                foreach (var storage in gridStorages)
                {
                    GRID_LINE line = context.GRID_LINEs.First(x =>
                        x.ID == storage.GL_ID && x.LINE_USERID == storage.LINE_USERID);
                    if (line.LINE_VALIDFROM.Month > month && line.LINE_VALIDFROM.Year >= year)
                        continue;
                    if (line.LINE_VALIDTO.Month < month && line.LINE_VALIDTO.Year < year)
                        continue;

                    var validImport = await MBGridService.IsGridLineValidForImportAfterFrequencyCheckAsync(
                        cancellationToken, line, month,
                        year);

                    if (validImport)
                    {
                        MBGridImportValues? importValue =
                            await GetGridImportValueAsync(cancellationToken, storage.GL_ID, userId);
                        if (importValue.Id != 0)
                        {
                            storage.VALUE = importValue.Value;
                            storage.LINE_LASTEDIT = DateTime.Now;
                        }
                    }
                    else
                    {
                        storage.VALUE = 0;
                        storage.LINE_LASTEDIT = DateTime.Now;
                    }

                    importCounter++;
                    await context.SaveChangesAsync(cancellationToken);
                }

                if (importCounter == 0)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return (int)ErrorEnum.NoValidImport;
                }

                int importResult = await MarkMonthAsImportedAsync(cancellationToken, month, year, userId);
                if (importResult == (int)ErrorEnum.Aborted)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return (int)ErrorEnum.Aborted;
                }

                await context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return (int)ErrorEnum.Success;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return (int)ErrorEnum.Aborted;
            }
        }
    }

    public static async Task<bool> IsGridLineActiveAsync(CancellationToken cancellationToken, int lineId, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        GRID_LINE gridLine = context.GRID_LINEs.First(x => x.ID == lineId && x.LINE_USERID == userId);

        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
        if (gridLine.LINE_VALIDTO.Month < currentDate.Month && gridLine.LINE_VALIDTO.Year < currentDate.Year)
            return false;
        return true;
    }

    public static async Task<int> MarkMonthAsImportedAsync(CancellationToken cancellationToken, int month, int year,
        int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        GRID_IMPORT? lastImport = await context.GRID_IMPORTs
            .Where(x => x.USER_ID == userId && x.MONTH == month && x.YEAR == year)
            .FirstOrDefaultAsync(cancellationToken);
        if (lastImport == null)
        {
            GRID_IMPORT newImportEntry = new GRID_IMPORT()
            {
                USER_ID = userId,
                MONTH = month,
                YEAR = year,
                STATUS = 1,
                LAST_IMPORT = DateOnly.FromDateTime(DateTime.Now),
            };
            await context.GRID_IMPORTs.AddAsync(newImportEntry, cancellationToken);
            if (await context.SaveChangesAsync(cancellationToken) > 0)
                return (int)ErrorEnum.Success;
        }
        else
        {
            if (lastImport.STATUS == 1)
                return (int)ErrorEnum.Success;
            lastImport.STATUS = 1;
            lastImport.LAST_IMPORT = DateOnly.FromDateTime(DateTime.Now);
            if (await context.SaveChangesAsync(cancellationToken) > 0)
                return (int)ErrorEnum.Success;
        }

        return (int)ErrorEnum.Aborted;
    }

    public static async Task<int> CheckIfMonthAlreadyImported(CancellationToken cancellationToken, int month, int year,
        int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        GRID_IMPORT? import = await context.GRID_IMPORTs
            .Where(x => x.USER_ID == userId && x.MONTH == month && x.YEAR == year)
            .FirstOrDefaultAsync(cancellationToken);
        if (import == null)
            return (int)ErrorEnum.NoImportFound;
        if (import.STATUS == 1)
            return (int)ErrorEnum.AlreadyImported;
        return (int)ErrorEnum.NoImportFound;
    }

    public static async Task<int> ImportDebitLineValueForSpecificMonth(CancellationToken cancellationToken,
        GridLine line, decimal value, int month, int year, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        GRID_STORAGE? storage = await context.GRID_STORAGEs.FirstOrDefaultAsync(x => x.GL_ID == line.Id
            && x.LINE_USERID == userId && x.LINE_MONTH == month && x.LINE_YEAR == year, cancellationToken);
        if (storage == null)
        {
            GRID_STORAGE newStorageItem = new GRID_STORAGE()
            {
                GL_ID = line.Id,
                VALUE = value,
                LINE_DAY = line.ValidFrom.Day,
                LINE_MONTH = month,
                LINE_YEAR = year,
                LINE_USERID = userId,
                LINE_NOTE = null,
                LINE_LASTEDIT = DateTime.Now
            };
            await context.GRID_STORAGEs.AddAsync(newStorageItem, cancellationToken);
            if (await context.SaveChangesAsync(cancellationToken) > 0)
                return (int)ErrorEnum.Success;
            return (int)ErrorEnum.Aborted;
        }

        storage.VALUE = value;
        storage.LINE_LASTEDIT = DateTime.Now;
        if (await context.SaveChangesAsync(cancellationToken) > 0)
            return (int)ErrorEnum.Success;
        return (int)ErrorEnum.Aborted;
    }

    public static async Task<int> SaveChartColorForCategoryAsync(CancellationToken cancellationToken, int r, int g, int b,
        string category, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        CHART_CATEGORY_COLOR? savedColor = await context.CHART_CATEGORY_COLORs.FirstOrDefaultAsync(x => x.CATEGORYID.ToLower().Equals(category.ToLower()) && x.USERID == userId, cancellationToken);
        if (savedColor == null)
        {
            savedColor = new CHART_CATEGORY_COLOR()
            {
                CATEGORYID = category,
                USERID = userId,
                R = r,
                G = g,
                B = b,
            };
            await context.CHART_CATEGORY_COLORs.AddAsync(savedColor, cancellationToken);
        }
        else
        {
            savedColor.R = r;
            savedColor.G = g;
            savedColor.B = b;
        }
        if (await context.SaveChangesAsync(cancellationToken) > 0)
            return (int)ErrorEnum.Success;
        return (int)ErrorEnum.Aborted;
    }

    public static async Task<int> SavePieChartColorForHomeViewAsync(CancellationToken cancellationToken,
        string incomeColorPrefix, string outGoingColorPrefix, string resourceNameIncomeColor, string resourceNameOutgoingColor, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        MB_GLOBAL_VARIABLE? incomeColorSet =
            await context.MB_GLOBAL_VARIABLEs.FirstOrDefaultAsync(
                x => x.USERID == userId && x.ID.Equals(resourceNameIncomeColor), cancellationToken);
        MB_GLOBAL_VARIABLE? outgoingColorSet = await context.MB_GLOBAL_VARIABLEs.FirstOrDefaultAsync(
            x => x.USERID == userId && x.ID.Equals(resourceNameOutgoingColor), cancellationToken);
        
        string savedIncomePrefix = string.Empty;
        string savedOutgoingPrefix = string.Empty;

        if (incomeColorSet == null)
        {
            MB_GLOBAL_VARIABLE newIncomeColorSet = new MB_GLOBAL_VARIABLE()
            {
                ID = resourceNameIncomeColor,
                USERID = userId,
                VALUEASSTRING = incomeColorPrefix,
            };
            await context.MB_GLOBAL_VARIABLEs.AddAsync(newIncomeColorSet, cancellationToken);
        }
        else
        {
            if(!string.IsNullOrWhiteSpace(incomeColorSet.VALUEASSTRING))
                savedIncomePrefix = incomeColorSet.VALUEASSTRING;
            incomeColorSet.VALUEASSTRING = incomeColorPrefix;
        }

        if (outgoingColorSet == null)
        {
            MB_GLOBAL_VARIABLE newOutgoingColorSet = new MB_GLOBAL_VARIABLE()
            {
                ID = resourceNameOutgoingColor,
                USERID = userId,
                VALUEASSTRING = outGoingColorPrefix
            };
            await context.MB_GLOBAL_VARIABLEs.AddAsync(newOutgoingColorSet, cancellationToken);
        }
        else
        {
            if(!string.IsNullOrWhiteSpace(outgoingColorSet.VALUEASSTRING))
                savedOutgoingPrefix = outgoingColorSet.VALUEASSTRING;
            outgoingColorSet.VALUEASSTRING = outGoingColorPrefix;
        }

        if (incomeColorSet != null && outgoingColorSet != null)
        {
            if (savedIncomePrefix.Equals(incomeColorSet.VALUEASSTRING) &&
                savedOutgoingPrefix.Equals(outgoingColorSet.VALUEASSTRING))
                return (int)ErrorEnum.ValueNotChanged;
        }

        if (await context.SaveChangesAsync(cancellationToken) > 0)
            return (int)ErrorEnum.Success;
        
        return (int)ErrorEnum.Aborted;
    }

    public static async Task<int> SaveGridLinePrefixForHomeOverViewAsync(CancellationToken cancellationToken,
        string gridLinePrefix, string resourceName, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        
        string savedPrefix = string.Empty;
        MB_GLOBAL_VARIABLE? personPrefix = await context.MB_GLOBAL_VARIABLEs.FirstOrDefaultAsync(x => x.USERID == userId && x.ID == resourceName, cancellationToken);
        if (personPrefix == null)
        {
            MB_GLOBAL_VARIABLE newPersonPrefix = new MB_GLOBAL_VARIABLE()
            {
                ID = resourceName,
                USERID = userId,
                VALUEASSTRING = gridLinePrefix
            };
            await context.MB_GLOBAL_VARIABLEs.AddAsync(newPersonPrefix, cancellationToken);
        }
        else
        {
            if (personPrefix.VALUEASSTRING != null)
            {
                savedPrefix = personPrefix.VALUEASSTRING;
                if (savedPrefix == gridLinePrefix)
                    return (int)ErrorEnum.ValueNotChanged;
            }
            personPrefix.VALUEASSTRING = gridLinePrefix;
        }
        if(await context.SaveChangesAsync(cancellationToken) > 0)
            return (int)ErrorEnum.Success;
        return (int)ErrorEnum.Aborted;
    }

    public static async Task<MBGlobalVariables?> GetGlobalVariableAsync(CancellationToken cancellationToken,
        string resourceName, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        
        MB_GLOBAL_VARIABLE? value = await context.MB_GLOBAL_VARIABLEs.FirstOrDefaultAsync(x => x.USERID == userId && x.ID == resourceName, cancellationToken);
        if (value == null)
            return null;

        return new MBGlobalVariables()
        {
            Id = value.ID,
            UserId = value.USERID,
            ValueAsString = value.VALUEASSTRING,
            ValueAsDecimal = value.VALUEASDECIMAL,
            Url = value.URL
        };
    }

    public static async Task<int> SaveDebtListChangesAsync(CancellationToken cancellationToken,
        MBDebtList debtList, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        MB_DEBT_LIST? value = await context.MB_DEBT_LISTs.Where(x => x.ID == debtList.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (value == null)
            return (int)ErrorEnum.NullReference;

        value.NAME = debtList.DebtName;
        value.DEBTDATE = debtList.DebtDate;
        value.DEBTSUM = debtList.DebtSum;
        value.DEBTOPEN = debtList.IsDebtOpen;
        value.DEBTPAIDDATE = debtList.DebtPaidDate;
        value.DEBTNOTE = debtList.DebtNote;
        value.DEBTIMAGE = debtList.DebtImage;
        value.EDITEDATE = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        
        if(await context.SaveChangesAsync(cancellationToken) > 0)
            return (int)ErrorEnum.Success;
        
        return (int)ErrorEnum.Aborted;
    }

    public static async Task<int> SaveDebtValueChangesAsync(CancellationToken cancellationToken, MBDebtList debtList)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        MB_DEBT_LIST? value = await context.MB_DEBT_LISTs.FirstOrDefaultAsync(x => x.ID == debtList.Id, cancellationToken);
        if (value == null)
            return (int)ErrorEnum.NullReference;

        if (value.DEBTSUM == debtList.DebtSum)
            return (int)ErrorEnum.ValueNotChanged;
        
        value.DEBTSUM = debtList.DebtSum;
        if(await context.SaveChangesAsync(cancellationToken) > 0)
            return (int)ErrorEnum.Success;
        return (int)ErrorEnum.Aborted;
    }

    public static async Task<int> SaveDebtNoteChangesAsync(CancellationToken cancellationToken, MBDebtList debtList)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        
        MB_DEBT_LIST? value = await context.MB_DEBT_LISTs.FirstOrDefaultAsync(x => x.ID == debtList.Id, cancellationToken);
        if(value == null)
            return (int)ErrorEnum.NullReference;
        value.DEBTNOTE = debtList.DebtNote;
        value.EDITEDATE = DateOnly.FromDateTime(DateTime.Now);
        if (await context.SaveChangesAsync(cancellationToken) > 0)
            return (int)ErrorEnum.Success;
        return (int)ErrorEnum.Aborted;
    }

    public static async Task<int> AddNewDebtUserAsync(CancellationToken cancellationToken, string debtName,
        int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        
        MB_DEBT_USER? value = await context.MB_DEBT_USERs.FirstOrDefaultAsync(x => x.NAME == debtName && x.USERID == userId, cancellationToken);
        if (value == null)
        {
            MB_DEBT_USER newDebtUser = new MB_DEBT_USER()
            {
                NAME = debtName,
                USERID = userId
            };
            await context.MB_DEBT_USERs.AddAsync(newDebtUser, cancellationToken);
            if (await context.SaveChangesAsync(cancellationToken) > 0)
                return (int)ErrorEnum.Success;
            else
                return (int)ErrorEnum.Aborted;
        }
        return (int)ErrorEnum.AlreadyExists;
    }

    public static async Task<(int, MBDebtList?)> AddNewDebtListEntryAsync(CancellationToken cancellationToken,
        MBDebtList debtList, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        MB_DEBT_LIST newEntry = new MB_DEBT_LIST()
        {
            NAME = debtList.DebtName,
            DEBTDATE = debtList.DebtDate,
            DEBTSUM = debtList.DebtSum,
            DEBTTYPE = debtList.DebtType,
            DEBTOPEN = debtList.IsDebtOpen,
            DEBTPAIDDATE = debtList.DebtPaidDate,
            DEBTNOTE = debtList.DebtNote,
            USERID = userId,
            EDITEDATE = debtList.EditedDate,
        };

        newEntry.DEBTIMAGE = debtList.DebtImage;
        
        await context.MB_DEBT_LISTs.AddAsync(newEntry, cancellationToken);
        if (await context.SaveChangesAsync(cancellationToken) > 0)
        {
            debtList.Id = newEntry.ID;
            return ((int)ErrorEnum.Success, debtList);
        }
        return ((int)ErrorEnum.Aborted, null);
    }

    private static MBErrorCodes ToErrorCodes(MB_ERROR_CODE value)
        => new MBErrorCodes()
        {
            Id = value.ID,
            ErrorCode = value.ERROR
        };

    private static MBGridImportValues ToGridImportValues(GRID_IMPORT_VALUE value)
        => new MBGridImportValues()
        {
            Id = value.ID,
            LineId = value.GZ_ID,
            UserId = value.USER_ID,
            Value = value.VALUE
        };

    private static ChartCategoryColor ToChartCategoryColor(CHART_CATEGORY_COLOR value)
        => new ChartCategoryColor()
        {
            Id = value.ID,
            CategoryId = value.CATEGORYID,
            UserId = value.USERID,
            R = value.R,
            G = value.G,
            B = value.B
        };

    private static MBGlobalVariables ToGlobalVariables(MB_GLOBAL_VARIABLE value)
        => new MBGlobalVariables()
        {
            Id = value.ID,
            UserId = value.USERID,
            ValueAsString = value.VALUEASSTRING,
            ValueAsDecimal = value.VALUEASDECIMAL,
            Url = value.URL
        };

    private static MBDebtUser ToMBDebtUser(MB_DEBT_USER value)
        => new MBDebtUser()
        {
            Name = value.NAME,
            UserId = value.USERID
        };

    private static MBDebtList ToMBDebtList(MB_DEBT_LIST value)
        => new MBDebtList()
        {
            Id = value.ID,
            DebtName = value.NAME,
            DebtDate = value.DEBTDATE,
            DebtSum = value.DEBTSUM,
            DebtType = value.DEBTTYPE,
            IsDebtOpen = value.DEBTOPEN,
            DebtPaidDate = value.DEBTPAIDDATE,
            DebtNote = value.DEBTNOTE,
            UserId = value.USERID,
            EditedDate = value.EDITEDATE,
            DebtImage = value.DEBTIMAGE
        };

    private static MBDebtType ToMBDebtType(MB_DEBT_TYPE value)
        => new MBDebtType()
        {
            Id = value.ID
        };

    private static MBGridImport ToGridImport(GRID_IMPORT value)
        => new MBGridImport()
        {
            Id = value.ID,
            UserId = value.USER_ID,
            Month = value.MONTH,
            Year = value.YEAR,
            Status = value.STATUS,
            LastImport = value.LAST_IMPORT
        };
}