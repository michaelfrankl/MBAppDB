using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using NASPLOID.Models;
using NASPLOID.Models.MoneyBear;

namespace NASPLOID.Services.MoneyBear;

public static class MBGridService
{
    public static async Task LoadAllGridLinesAsync(this MBSessionContext session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        List<GridLine> lines = (await context.GRID_LINEs.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToGridLine).ToList();

        session.GridLines = new ObservableCollection<GridLine>(lines);
    }

    public static async Task<ObservableCollection<GridLine>> LoadAllGridLinesAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        List<GridLine> lines = (await context.GRID_LINEs.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToGridLine).ToList();

        return new ObservableCollection<GridLine>(lines);
    }

    public static async Task LoadAllGridStorageAsync(this MBSessionContext session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        List<GridStorage> storages = (await context.GRID_STORAGEs.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToGridStorage).ToList();
        session.GridStorages = new ObservableCollection<GridStorage>(storages);
    }

    public static async Task<ObservableCollection<GridStorage>> LoadAllGridStorageAsync(
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        List<GridStorage> storages = (await context.GRID_STORAGEs.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToGridStorage).ToList();
        return new ObservableCollection<GridStorage>(storages);
    }

    public static async Task SetAllGridLinesValuesAsync(this MBSessionContext session,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        int startMonth = 1;
        int selectedPreviewMonths = 11;
        int selectedStartYear = DateTime.Now.Year;
        GridStorage stroage = new GridStorage();

        if (session.GridLines == null)
            return;
        if (session.GridStorages == null)
            return;

        foreach (GridLine gridLine in session.GridLines)
        {
            for (int i = startMonth; i <= (startMonth + selectedPreviewMonths); i++)
            {
                switch (i)
                {
                    case (int)MonthEnum.Januar:
                        stroage = session.GridStorages.FirstOrDefault(x =>
                            x.LineId == gridLine.Id && x.LineMonth == i && x.LineYear == selectedStartYear);
                        if (stroage == null)
                            gridLine.JanuarValue = 0;
                        else
                            gridLine.JanuarValue = stroage.Value;
                        break;
                    case (int)MonthEnum.Februar:
                        stroage = session.GridStorages.FirstOrDefault(x =>
                            x.LineId == gridLine.Id && x.LineMonth == i && x.LineYear == selectedStartYear);
                        if (stroage == null)
                            gridLine.FebruarValue = 0;
                        else
                            gridLine.FebruarValue = stroage.Value;
                        break;
                    case (int)MonthEnum.März:
                        stroage = session.GridStorages.FirstOrDefault(x =>
                            x.LineId == gridLine.Id && x.LineMonth == i && x.LineYear == selectedStartYear);
                        if (stroage == null)
                            gridLine.MärzValue = 0;
                        else
                            gridLine.MärzValue = stroage.Value;
                        break;
                    case (int)MonthEnum.April:
                        stroage = session.GridStorages.FirstOrDefault(x =>
                            x.LineId == gridLine.Id && x.LineMonth == i && x.LineYear == selectedStartYear);
                        if (stroage == null)
                            gridLine.AprilValue = 0;
                        else
                            gridLine.AprilValue = stroage.Value;
                        break;
                    case (int)MonthEnum.Mai:
                        stroage = session.GridStorages.FirstOrDefault(x =>
                            x.LineId == gridLine.Id && x.LineMonth == i && x.LineYear == selectedStartYear);
                        if (stroage == null)
                            gridLine.MaiValue = 0;
                        else
                            gridLine.MaiValue = stroage.Value;
                        break;
                    case (int)MonthEnum.Juni:
                        stroage = session.GridStorages.FirstOrDefault(x =>
                            x.LineId == gridLine.Id && x.LineMonth == i && x.LineYear == selectedStartYear);
                        if (stroage == null)
                            gridLine.JuniValue = 0;
                        else
                            gridLine.JuniValue = stroage.Value;
                        break;
                    case (int)MonthEnum.Juli:
                        stroage = session.GridStorages.FirstOrDefault(x =>
                            x.LineId == gridLine.Id && x.LineMonth == i && x.LineYear == selectedStartYear);
                        if (stroage == null)
                            gridLine.JuliValue = 0;
                        else
                            gridLine.JuliValue = stroage.Value;
                        break;
                    case (int)MonthEnum.August:
                        stroage = session.GridStorages.FirstOrDefault(x =>
                            x.LineId == gridLine.Id && x.LineMonth == i && x.LineYear == selectedStartYear);
                        if (stroage == null)
                            gridLine.AugustValue = 0;
                        else
                            gridLine.AugustValue = stroage.Value;
                        break;
                    case (int)MonthEnum.September:
                        stroage = session.GridStorages.FirstOrDefault(x =>
                            x.LineId == gridLine.Id && x.LineMonth == i && x.LineYear == selectedStartYear);
                        if (stroage == null)
                            gridLine.SeptemberValue = 0;
                        else
                            gridLine.SeptemberValue = stroage.Value;
                        break;
                    case (int)MonthEnum.Oktober:
                        stroage = session.GridStorages.FirstOrDefault(x =>
                            x.LineId == gridLine.Id && x.LineMonth == i && x.LineYear == selectedStartYear);
                        if (stroage == null)
                            gridLine.OktoberValue = 0;
                        else
                            gridLine.OktoberValue = stroage.Value;
                        break;
                    case (int)MonthEnum.November:
                        stroage = session.GridStorages.FirstOrDefault(x =>
                            x.LineId == gridLine.Id && x.LineMonth == i && x.LineYear == selectedStartYear);
                        if (stroage == null)
                            gridLine.NovemberValue = 0;
                        else
                            gridLine.NovemberValue = stroage.Value;
                        break;
                    case (int)MonthEnum.Dezember:
                        stroage = session.GridStorages.FirstOrDefault(x =>
                            x.LineId == gridLine.Id && x.LineMonth == i && x.LineYear == selectedStartYear);
                        if (stroage == null)
                            gridLine.DezemberValue = 0;
                        else
                            gridLine.DezemberValue = stroage.Value;
                        break;
                }
            }
        }
    }

    public static async Task CalculateAllGridSumLinesAsync(this MBSessionContext session,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        int startMonth = 1;
        int selectedPreviewMonths = 11;
        LineCalculation? lineCalculation = new LineCalculation();

        if (session.GridLines == null)
            return;
        if (session.LineCalculations == null)
            return;

        foreach (GridLine gridLine in session.GridLines)
        {
            if (gridLine.LineTypId != (int)LineTypeEnum.Summen)
                continue;
            for (int i = startMonth; i <= (startMonth + selectedPreviewMonths); i++)
            {
                switch (i)
                {
                    #region Months

                    case (int)MonthEnum.Januar:
                        lineCalculation =
                            session.LineCalculations.FirstOrDefault(x => x.Id == gridLine.LineCalculationId);
                        if (lineCalculation == null)
                            gridLine.JanuarValue = 0;
                        else
                            gridLine.JanuarValue =
                                await CalculateSumLinesForSpecificMonthAsnyc(session.GridLines, i,
                                    lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.Februar:
                        lineCalculation =
                            session.LineCalculations.FirstOrDefault(x => x.Id == gridLine.LineCalculationId);
                        if (lineCalculation == null)
                            gridLine.FebruarValue = 0;
                        else
                            gridLine.FebruarValue =
                                await CalculateSumLinesForSpecificMonthAsnyc(session.GridLines, i,
                                    lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.März:
                        lineCalculation =
                            session.LineCalculations.FirstOrDefault(x => x.Id == gridLine.LineCalculationId);
                        if (lineCalculation == null)
                            gridLine.MärzValue = 0;
                        else
                            gridLine.MärzValue =
                                await CalculateSumLinesForSpecificMonthAsnyc(session.GridLines, i,
                                    lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.April:
                        lineCalculation =
                            session.LineCalculations.FirstOrDefault(x => x.Id == gridLine.LineCalculationId);
                        if (lineCalculation == null)
                            gridLine.AprilValue = 0;
                        else
                            gridLine.AprilValue =
                                await CalculateSumLinesForSpecificMonthAsnyc(session.GridLines, i,
                                    lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.Mai:
                        lineCalculation =
                            session.LineCalculations.FirstOrDefault(x => x.Id == gridLine.LineCalculationId);
                        if (lineCalculation == null)
                            gridLine.MaiValue = 0;
                        else
                            gridLine.MaiValue =
                                await CalculateSumLinesForSpecificMonthAsnyc(session.GridLines, i,
                                    lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.Juni:
                        lineCalculation =
                            session.LineCalculations.FirstOrDefault(x => x.Id == gridLine.LineCalculationId);
                        if (lineCalculation == null)
                            gridLine.JuniValue = 0;
                        else
                            gridLine.JuniValue =
                                await CalculateSumLinesForSpecificMonthAsnyc(session.GridLines, i,
                                    lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.Juli:
                        lineCalculation =
                            session.LineCalculations.FirstOrDefault(x => x.Id == gridLine.LineCalculationId);
                        if (lineCalculation == null)
                            gridLine.JuliValue = 0;
                        else
                            gridLine.JuliValue =
                                await CalculateSumLinesForSpecificMonthAsnyc(session.GridLines, i,
                                    lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.August:
                        lineCalculation =
                            session.LineCalculations.FirstOrDefault(x => x.Id == gridLine.LineCalculationId);
                        if (lineCalculation == null)
                            gridLine.AugustValue = 0;
                        else
                            gridLine.AugustValue =
                                await CalculateSumLinesForSpecificMonthAsnyc(session.GridLines, i,
                                    lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.September:
                        lineCalculation =
                            session.LineCalculations.FirstOrDefault(x => x.Id == gridLine.LineCalculationId);
                        if (lineCalculation == null)
                            gridLine.SeptemberValue = 0;
                        else
                            gridLine.SeptemberValue =
                                await CalculateSumLinesForSpecificMonthAsnyc(session.GridLines, i,
                                    lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.Oktober:
                        lineCalculation =
                            session.LineCalculations.FirstOrDefault(x => x.Id == gridLine.LineCalculationId);
                        if (lineCalculation == null)
                            gridLine.OktoberValue = 0;
                        else
                            gridLine.OktoberValue =
                                await CalculateSumLinesForSpecificMonthAsnyc(session.GridLines, i,
                                    lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.November:
                        lineCalculation =
                            session.LineCalculations.FirstOrDefault(x => x.Id == gridLine.LineCalculationId);
                        if (lineCalculation == null)
                            gridLine.NovemberValue = 0;
                        else
                            gridLine.NovemberValue =
                                await CalculateSumLinesForSpecificMonthAsnyc(session.GridLines, i,
                                    lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.Dezember:
                        lineCalculation =
                            session.LineCalculations.FirstOrDefault(x => x.Id == gridLine.LineCalculationId);
                        if (lineCalculation == null)
                            gridLine.DezemberValue = 0;
                        else
                            gridLine.DezemberValue =
                                await CalculateSumLinesForSpecificMonthAsnyc(session.GridLines, i,
                                    lineCalculation.Calculation);
                        break;

                    #endregion
                }
            }
        }
    }

    private static async Task<decimal?> CalculateSumLinesForSpecificMonthAsnyc(ObservableCollection<GridLine> gridLines,
        int currentMonth, string expression)
    {
        if (expression == null) return 0;
        if (expression.Length <= 0) return 0;
        var matches = Regex.Matches(expression, @"(\d+)|([+-])");
        decimal? result = 0;
        string currentOperation = "+";

        foreach (Match match in matches)
        {
            if (int.TryParse(match.Value, out int id))
            {
                var line = gridLines.FirstOrDefault(i => i.Id == id);
                if (line != null)
                {
                    if (currentOperation == "+")
                    {
                        switch (currentMonth)
                        {
                            #region Months

                            case (int)MonthEnum.Januar:
                                result += line.JanuarValue;
                                break;
                            case (int)MonthEnum.Februar:
                                result += line.FebruarValue;
                                break;
                            case (int)MonthEnum.März:
                                result += line.MärzValue;
                                break;
                            case (int)MonthEnum.April:
                                result += line.AprilValue;
                                break;
                            case (int)MonthEnum.Mai:
                                result += line.MaiValue;
                                break;
                            case (int)MonthEnum.Juni:
                                result += line.JuniValue;
                                break;
                            case (int)MonthEnum.Juli:
                                result += line.JuliValue;
                                break;
                            case (int)MonthEnum.August:
                                result += line.AugustValue;
                                break;
                            case (int)MonthEnum.September:
                                result += line.SeptemberValue;
                                break;
                            case (int)MonthEnum.Oktober:
                                result += line.OktoberValue;
                                break;
                            case (int)MonthEnum.November:
                                result += line.NovemberValue;
                                break;
                            case (int)MonthEnum.Dezember:
                                result += line.DezemberValue;
                                break;

                            #endregion
                        }

                    }
                    else if (currentOperation == "-")
                    {
                        switch (currentMonth)
                        {
                            #region Months

                            case (int)MonthEnum.Januar:
                                result -= line.JanuarValue;
                                break;
                            case (int)MonthEnum.Februar:
                                result -= line.FebruarValue;
                                break;
                            case (int)MonthEnum.März:
                                result -= line.MärzValue;
                                break;
                            case (int)MonthEnum.April:
                                result -= line.AprilValue;
                                break;
                            case (int)MonthEnum.Mai:
                                result -= line.MaiValue;
                                break;
                            case (int)MonthEnum.Juni:
                                result -= line.JuniValue;
                                break;
                            case (int)MonthEnum.Juli:
                                result -= line.JuliValue;
                                break;
                            case (int)MonthEnum.August:
                                result -= line.AugustValue;
                                break;
                            case (int)MonthEnum.September:
                                result -= line.SeptemberValue;
                                break;
                            case (int)MonthEnum.Oktober:
                                result -= line.OktoberValue;
                                break;
                            case (int)MonthEnum.November:
                                result -= line.NovemberValue;
                                break;
                            case (int)MonthEnum.Dezember:
                                result -= line.DezemberValue;
                                break;

                            #endregion
                        }
                    }
                }
            }
            else if (match.Value == "+" || match.Value == "-")
            {
                currentOperation = match.Value;
            }
        }

        return result;
    }

    private static async Task<decimal?> CalculateSumLinesForSpecificMonthAsnycTwo(
        ObservableCollection<GridLine> gridLines, int currentMonth, string expression)
    {
        if (expression == null) return 0;
        if (expression.Length <= 0) return 0;
        var matches = Regex.Matches(expression, @"(\d+)|([+-])");
        decimal? result = 0;
        string currentOperation = "+";

        foreach (Match match in matches)
        {
            if (int.TryParse(match.Value, out int id))
            {
                var line = gridLines.FirstOrDefault(i => i.Id == id);
                if (line != null)
                {
                    if (currentOperation == "+")
                    {
                        switch (currentMonth)
                        {
                            #region Months

                            case (int)MonthEnum.Januar:
                                result += line.JanuarValue;
                                break;
                            case (int)MonthEnum.Februar:
                                result += line.FebruarValue;
                                break;
                            case (int)MonthEnum.März:
                                result += line.MärzValue;
                                break;
                            case (int)MonthEnum.April:
                                result += line.AprilValue;
                                break;
                            case (int)MonthEnum.Mai:
                                result += line.MaiValue;
                                break;
                            case (int)MonthEnum.Juni:
                                result += line.JuniValue;
                                break;
                            case (int)MonthEnum.Juli:
                                result += line.JuliValue;
                                break;
                            case (int)MonthEnum.August:
                                result += line.AugustValue;
                                break;
                            case (int)MonthEnum.September:
                                result += line.SeptemberValue;
                                break;
                            case (int)MonthEnum.Oktober:
                                result += line.OktoberValue;
                                break;
                            case (int)MonthEnum.November:
                                result += line.NovemberValue;
                                break;
                            case (int)MonthEnum.Dezember:
                                result += line.DezemberValue;
                                break;

                            #endregion
                        }

                    }
                    else if (currentOperation == "-")
                    {
                        switch (currentMonth)
                        {
                            #region Months

                            case (int)MonthEnum.Januar:
                                result -= line.JanuarValue;
                                break;
                            case (int)MonthEnum.Februar:
                                result -= line.FebruarValue;
                                break;
                            case (int)MonthEnum.März:
                                result -= line.MärzValue;
                                break;
                            case (int)MonthEnum.April:
                                result -= line.AprilValue;
                                break;
                            case (int)MonthEnum.Mai:
                                result -= line.MaiValue;
                                break;
                            case (int)MonthEnum.Juni:
                                result -= line.JuniValue;
                                break;
                            case (int)MonthEnum.Juli:
                                result -= line.JuliValue;
                                break;
                            case (int)MonthEnum.August:
                                result -= line.AugustValue;
                                break;
                            case (int)MonthEnum.September:
                                result -= line.SeptemberValue;
                                break;
                            case (int)MonthEnum.Oktober:
                                result -= line.OktoberValue;
                                break;
                            case (int)MonthEnum.November:
                                result -= line.NovemberValue;
                                break;
                            case (int)MonthEnum.Dezember:
                                result -= line.DezemberValue;
                                break;

                            #endregion
                        }
                    }
                }
            }
            else if (match.Value == "+" || match.Value == "-")
            {
                currentOperation = match.Value;
            }
        }

        return result;
    }

    public static async Task SaveCalculatedGridSumLineAsnyc(this MBSessionContext session,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        if (session.GridLines == null)
            return;
        if (session.GridStorages == null)
            return;

        int startMonth = 1;
        int selectedPreviewMonths = 11;
        int selectedYear = DateTime.Now.Year;
        GRID_STORAGE value = new GRID_STORAGE();

        foreach (GridLine line in session.GridLines)
        {
            if (line.LineTypId != (int)LineTypeEnum.Summen)
                continue;
            for (int i = startMonth; i <= startMonth + selectedPreviewMonths; i++)
            {
                switch (i)
                {
                    case (int)MonthEnum.Januar:
                        value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                            x.GL_ID == line.Id && x.LINE_MONTH == i && x.LINE_YEAR == selectedYear);
                        if (value != null)
                        {
                            value.VALUE = line.JanuarValue;
                            value.LINE_LASTEDIT = DateTime.Now;
                            await context.SaveChangesAsync(cancellationToken);
                        }

                        break;
                    case (int)MonthEnum.Februar:
                        value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                            x.GL_ID == line.Id && x.LINE_MONTH == i && x.LINE_YEAR == selectedYear);
                        if (value != null)
                        {
                            value.VALUE = line.FebruarValue;
                            value.LINE_LASTEDIT = DateTime.Now;
                            await context.SaveChangesAsync(cancellationToken);
                        }

                        break;
                    case (int)MonthEnum.März:
                        value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                            x.GL_ID == line.Id && x.LINE_MONTH == i && x.LINE_YEAR == selectedYear);
                        if (value != null)
                        {
                            value.VALUE = line.MärzValue;
                            value.LINE_LASTEDIT = DateTime.Now;
                            await context.SaveChangesAsync(cancellationToken);
                        }

                        break;
                    case (int)MonthEnum.April:
                        value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                            x.GL_ID == line.Id && x.LINE_MONTH == i && x.LINE_YEAR == selectedYear);
                        if (value != null)
                        {
                            value.VALUE = line.AprilValue;
                            value.LINE_LASTEDIT = DateTime.Now;
                            await context.SaveChangesAsync(cancellationToken);
                        }

                        break;
                    case (int)MonthEnum.Mai:
                        value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                            x.GL_ID == line.Id && x.LINE_MONTH == i && x.LINE_YEAR == selectedYear);
                        if (value != null)
                        {
                            value.VALUE = line.MaiValue;
                            value.LINE_LASTEDIT = DateTime.Now;
                            await context.SaveChangesAsync(cancellationToken);
                        }

                        break;
                    case (int)MonthEnum.Juni:
                        value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                            x.GL_ID == line.Id && x.LINE_MONTH == i && x.LINE_YEAR == selectedYear);
                        if (value != null)
                        {
                            value.VALUE = line.JuliValue;
                            value.LINE_LASTEDIT = DateTime.Now;
                            await context.SaveChangesAsync(cancellationToken);
                        }

                        break;
                    case (int)MonthEnum.Juli:
                        value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                            x.GL_ID == line.Id && x.LINE_MONTH == i && x.LINE_YEAR == selectedYear);
                        if (value != null)
                        {
                            value.VALUE = line.JuliValue;
                            value.LINE_LASTEDIT = DateTime.Now;
                            await context.SaveChangesAsync(cancellationToken);
                        }

                        break;
                    case (int)MonthEnum.August:
                        value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                            x.GL_ID == line.Id && x.LINE_MONTH == i && x.LINE_YEAR == selectedYear);
                        if (value != null)
                        {
                            value.VALUE = line.AugustValue;
                            value.LINE_LASTEDIT = DateTime.Now;
                            await context.SaveChangesAsync(cancellationToken);
                        }

                        break;
                    case (int)MonthEnum.September:
                        value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                            x.GL_ID == line.Id && x.LINE_MONTH == i && x.LINE_YEAR == selectedYear);
                        if (value != null)
                        {
                            value.VALUE = line.SeptemberValue;
                            value.LINE_LASTEDIT = DateTime.Now;
                            await context.SaveChangesAsync(cancellationToken);
                        }

                        break;
                    case (int)MonthEnum.Oktober:
                        value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                            x.GL_ID == line.Id && x.LINE_MONTH == i && x.LINE_YEAR == selectedYear);
                        if (value != null)
                        {
                            value.VALUE = line.OktoberValue;
                            value.LINE_LASTEDIT = DateTime.Now;
                            await context.SaveChangesAsync(cancellationToken);
                        }

                        break;
                    case (int)MonthEnum.November:
                        value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                            x.GL_ID == line.Id && x.LINE_MONTH == i && x.LINE_YEAR == selectedYear);
                        if (value != null)
                        {
                            value.VALUE = line.NovemberValue;
                            value.LINE_LASTEDIT = DateTime.Now;
                            await context.SaveChangesAsync(cancellationToken);
                        }

                        break;
                    case (int)MonthEnum.Dezember:
                        value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                            x.GL_ID == line.Id && x.LINE_MONTH == i && x.LINE_YEAR == selectedYear);
                        if (value != null)
                        {
                            value.VALUE = line.DezemberValue;
                            value.LINE_LASTEDIT = DateTime.Now;
                            await context.SaveChangesAsync(cancellationToken);
                        }

                        break;
                }
            }
        }
    }

    public static async Task<int> SaveCurrentCalculatedGridSumLineAsync(GridLine line, int month, int year,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        GRID_STORAGE? storage = await context.GRID_STORAGEs
            .Where(x => x.GL_ID == line.Id && x.LINE_MONTH == month && x.LINE_YEAR == year).FirstOrDefaultAsync(cancellationToken);
        if (storage == null)
            return (int)ErrorEnum.NullReference;

        var storedValue = storage.VALUE;

        storage.VALUE = month switch
        {
            (int)MonthEnum.Januar => line.JanuarValue,
            (int)MonthEnum.Februar => line.FebruarValue,
            (int)MonthEnum.März => line.MärzValue,
            (int)MonthEnum.April => line.AprilValue,
            (int)MonthEnum.Mai => line.MaiValue,
            (int)MonthEnum.Juni => line.JuniValue,
            (int)MonthEnum.Juli => line.JuliValue,
            (int)MonthEnum.August => line.AugustValue,
            (int)MonthEnum.September => line.SeptemberValue,
            (int)MonthEnum.Oktober => line.OktoberValue,
            (int)MonthEnum.November => line.NovemberValue,
            (int)MonthEnum.Dezember => line.DezemberValue,
        };

        if (storedValue == storage.VALUE)
            return (int)ErrorEnum.Success;

        if(await context.SaveChangesAsync(cancellationToken) > 0)
            return (int)ErrorEnum.Success;
        

        return (int)ErrorEnum.Aborted;
    }

    public static async Task SaveCalculatedGridSumLineForSpecificMonth(int month, int year,
        ObservableCollection<GridLine> lines, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        if (lines == null)
            return;
        GRID_STORAGE value = new GRID_STORAGE();

        foreach (GridLine gridLine in lines)
        {
            if (gridLine.LineTypId != (int)LineTypeEnum.Summen)
                continue;
            switch (month)
            {
                case (int)MonthEnum.Januar:
                    value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                        x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year);
                    if (value != null)
                    {
                        value.VALUE = gridLine.JanuarValue;
                        value.LINE_LASTEDIT = DateTime.Now;
                        await context.SaveChangesAsync(cancellationToken);
                    }

                    break;
                case (int)MonthEnum.Februar:
                    value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                        x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year);
                    if (value != null)
                    {
                        value.VALUE = gridLine.FebruarValue;
                        value.LINE_LASTEDIT = DateTime.Now;
                        await context.SaveChangesAsync(cancellationToken);
                    }

                    break;
                case (int)MonthEnum.März:
                    value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                        x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year);
                    if (value != null)
                    {
                        value.VALUE = gridLine.MärzValue;
                        value.LINE_LASTEDIT = DateTime.Now;
                        await context.SaveChangesAsync(cancellationToken);
                    }

                    break;
                case (int)MonthEnum.April:
                    value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                        x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year);
                    if (value != null)
                    {
                        value.VALUE = gridLine.AprilValue;
                        value.LINE_LASTEDIT = DateTime.Now;
                        await context.SaveChangesAsync(cancellationToken);
                    }

                    break;
                case (int)MonthEnum.Mai:
                    value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                        x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year);
                    if (value != null)
                    {
                        value.VALUE = gridLine.MaiValue;
                        value.LINE_LASTEDIT = DateTime.Now;
                        await context.SaveChangesAsync(cancellationToken);
                    }

                    break;
                case (int)MonthEnum.Juni:
                    value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                        x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year);
                    if (value != null)
                    {
                        value.VALUE = gridLine.JuniValue;
                        value.LINE_LASTEDIT = DateTime.Now;
                        await context.SaveChangesAsync(cancellationToken);
                    }

                    break;
                case (int)MonthEnum.Juli:
                    value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                        x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year);
                    if (value != null)
                    {
                        value.VALUE = gridLine.JuliValue;
                        value.LINE_LASTEDIT = DateTime.Now;
                        await context.SaveChangesAsync(cancellationToken);
                    }

                    break;
                case (int)MonthEnum.August:
                    value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                        x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year);
                    if (value != null)
                    {
                        value.VALUE = gridLine.AugustValue;
                        value.LINE_LASTEDIT = DateTime.Now;
                        await context.SaveChangesAsync(cancellationToken);
                    }

                    break;
                case (int)MonthEnum.September:
                    value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                        x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year);
                    if (value != null)
                    {
                        value.VALUE = gridLine.SeptemberValue;
                        value.LINE_LASTEDIT = DateTime.Now;
                        await context.SaveChangesAsync(cancellationToken);
                    }

                    break;
                case (int)MonthEnum.Oktober:
                    value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                        x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year);
                    if (value != null)
                    {
                        value.VALUE = gridLine.OktoberValue;
                        value.LINE_LASTEDIT = DateTime.Now;
                        await context.SaveChangesAsync(cancellationToken);
                    }

                    break;
                case (int)MonthEnum.November:
                    value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                        x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year);
                    if (value != null)
                    {
                        value.VALUE = gridLine.NovemberValue;
                        value.LINE_LASTEDIT = DateTime.Now;
                        await context.SaveChangesAsync(cancellationToken);
                    }

                    break;
                case (int)MonthEnum.Dezember:
                    value = await context.GRID_STORAGEs.FirstOrDefaultAsync(x =>
                        x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year);
                    if (value != null)
                    {
                        value.VALUE = gridLine.DezemberValue;
                        value.LINE_LASTEDIT = DateTime.Now;
                        await context.SaveChangesAsync(cancellationToken);
                    }

                    break;
            }
        }
    }

    public static async Task<int> CheckIfGridLineValueChanged(int lineId, decimal value, int month, int year, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();
        int result = 0;

        await Task.Run(async () =>
        {
            GRID_STORAGE? storage = await context.GRID_STORAGEs
                .Where(x => x.GL_ID == lineId && x.LINE_MONTH == month && x.LINE_YEAR == year)
                .FirstOrDefaultAsync(cancellationToken);
            if (storage == null)
            {
                result = (int)ErrorEnum.NullReference;
                return result;
            }

            if (value == storage.VALUE)
                result = (int)ErrorEnum.ValueNotChanged;
            else
                result = (int)ErrorEnum.ValueChanged;
            return result;
        }, cancellationToken);
        return result;
    }

    public static async Task<int> SaveValueForSelectedGridLineAsync(int id, decimal value, int month, int year,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (cancellationToken.IsCancellationRequested)
            return (int)ErrorEnum.Aborted;

        using var context = new DatabaseContext();

        await Task.Run(() =>
        {
            GRID_STORAGE storage =
                context.GRID_STORAGEs.FirstOrDefault(x =>
                    x.GL_ID == id && x.LINE_MONTH == month && x.LINE_YEAR == year);
            if (storage == null)
                return (int)ErrorEnum.NullReference;
            if (storage.VALUE == value)
                return (int)ErrorEnum.NullReference;
            storage.VALUE = value;
            storage.LINE_LASTEDIT = DateTime.Now;
            int result = context.SaveChanges();
            if (result <= 0)
                return (int)ErrorEnum.Aborted;
            return (int)ErrorEnum.Success;
        }, cancellationToken);
        return (int)ErrorEnum.Success;
    }

    public static async Task<int> RecalculateSumLinesForSpecificMonthAsync(int month, int year, int userId,
        ObservableCollection<GridLine> lines, ObservableCollection<LineCalculation> lineCalculations,
        CancellationToken cancellationToken)
    {
        ObservableCollection<GridLine> sumLines = new ObservableCollection<GridLine>(lines.Where(x => x.LineTypId == (int)LineTypeEnum.Summen && x.Userid == userId).ToList());

        await Task.Run(async () =>
        {
            foreach (GridLine gridLine in sumLines)
            {
                LineCalculation lineCalculation = lineCalculations.First(x => x.Id == gridLine.LineCalculationId);
                switch (month)
                {
                    case (int)MonthEnum.Januar:
                        gridLine.JanuarValue =
                            await CalculateSumLinesForSpecificMonthAsnycTwo(lines, month, lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.Februar:
                        gridLine.FebruarValue =
                            await CalculateSumLinesForSpecificMonthAsnycTwo(lines, month, lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.März:
                        gridLine.MärzValue =
                            await CalculateSumLinesForSpecificMonthAsnycTwo(lines, month, lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.April:
                        gridLine.AprilValue =
                            await CalculateSumLinesForSpecificMonthAsnycTwo(lines, month, lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.Mai:
                        gridLine.MaiValue =
                            await CalculateSumLinesForSpecificMonthAsnycTwo(lines, month, lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.Juni:
                        gridLine.JuniValue =
                            await CalculateSumLinesForSpecificMonthAsnycTwo(lines, month, lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.Juli:
                        gridLine.JuliValue =
                            await CalculateSumLinesForSpecificMonthAsnycTwo(lines, month, lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.August:
                        gridLine.AugustValue =
                            await CalculateSumLinesForSpecificMonthAsnycTwo(lines, month, lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.September:
                        gridLine.SeptemberValue =
                            await CalculateSumLinesForSpecificMonthAsnycTwo(lines, month, lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.Oktober:
                        gridLine.OktoberValue =
                            await CalculateSumLinesForSpecificMonthAsnycTwo(lines, month, lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.November:
                        gridLine.NovemberValue =
                            await CalculateSumLinesForSpecificMonthAsnycTwo(lines, month, lineCalculation.Calculation);
                        break;
                    case (int)MonthEnum.Dezember:
                        gridLine.DezemberValue =
                            await CalculateSumLinesForSpecificMonthAsnycTwo(lines, month, lineCalculation.Calculation);
                        break;
                }
                int saveResult = await SaveCurrentCalculatedGridSumLineAsync(gridLine, month, year, cancellationToken);
                //Console.WriteLine($"Folgende Berechnung ging durch: {gridLine.Name}");
                if (saveResult == (int)ErrorEnum.Aborted)
                {
                    //Console.WriteLine($"Abgebrochen bei: {gridLine.Name}");
                    return (int)ErrorEnum.Aborted;
                }
            }
            //Console.WriteLine("Erfolgreich berechnet!");
            return (int)ErrorEnum.Success;
        }, cancellationToken);

        return (int)ErrorEnum.Success;
    }

    public static async Task<ObservableCollection<GridLine>> SearchForCalculationLinesAsync(int startLineId,
        ObservableCollection<GridLine> lines, ObservableCollection<LineCalculation> calculations,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        ObservableCollection<GridLine> sumLines = new ObservableCollection<GridLine>();

        List<LineCalculation> linesToCalculate =
            calculations.Where(x => Regex.IsMatch(x.Calculation, @"(^|[+\-*/\s.,])6([+\-*/\s.,]|$)")).ToList();
        if (linesToCalculate.Count == 0)
            return sumLines;
        List<GridLine> startLines = lines.Where(x => x.LineCalculationId == startLineId).ToList();

        ObservableCollection<GridLine> helperLines = new ObservableCollection<GridLine>(startLines);
        
        ObservableCollection<GridLine> filteredLines =
            await SumLineFilterHelper(helperLines, lines, new ObservableCollection<GridLine>(), calculations);
        Console.WriteLine("Test");
        
        return sumLines;
    }

    private static async Task<ObservableCollection<GridLine>> SumLineFilterHelper(ObservableCollection<GridLine> lines, ObservableCollection<GridLine> allLines,
        ObservableCollection<GridLine> filteredLines, ObservableCollection<LineCalculation> calculations)
    {
        int saveLine = 0;
        GridLine currentLine = new GridLine();
        if (lines.Count() == 0)
        {
            return filteredLines;
        }

        foreach (GridLine gridLine in lines)
        {
            if (saveLine == 0)
            {
                currentLine = gridLine;
                saveLine++;
            }
            List<LineCalculation> lineCalculations = calculations.Where(x => Regex.IsMatch(x.Calculation, @"(^|[+\-*/\s.,])6([+\-*/\s.,]|$)")).ToList();
            foreach (LineCalculation calculation in lineCalculations)
            {
                filteredLines.Add(allLines.First(x => x.LineCalculationId == calculation.Id));
            }
        }

        lines.Remove(currentLine);
        return await SumLineFilterHelper(lines, allLines, filteredLines, calculations);
    }

public static async Task<int> SetAllGridLineValuesAsync(ObservableCollection<GridLine> lines, int month, int year, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (lines == null)
            return (int)ErrorEnum.Aborted;
        using var context = new DatabaseContext();
        
        GRID_STORAGE? storage = new GRID_STORAGE();
        
        await Task.Run(async () =>
        {
            foreach (GridLine gridLine in lines)
            {
                switch (month)
                {
                    case (int)MonthEnum.Januar:
                        storage = await context.GRID_STORAGEs.Where(x => x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year).FirstOrDefaultAsync(cancellationToken);
                        if (storage == null)
                            gridLine.JanuarValue = 0;
                        else
                            gridLine.JanuarValue = storage.VALUE;
                        break;
                    case (int)MonthEnum.Februar:
                        storage = await context.GRID_STORAGEs.Where(x => x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year).FirstOrDefaultAsync(cancellationToken);
                        if (storage == null)
                            gridLine.FebruarValue = 0;
                        else
                            gridLine.FebruarValue = storage.VALUE;
                        break;
                    case (int)MonthEnum.September:
                        storage = await context.GRID_STORAGEs.Where(x => x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year).FirstOrDefaultAsync(cancellationToken);
                        if (storage == null)
                            gridLine.SeptemberValue = 0;
                        else
                            gridLine.SeptemberValue = storage.VALUE;
                        break;
                }
            }
            return (int)ErrorEnum.Success;
        }, cancellationToken);
        
        return (int)ErrorEnum.Success;
    }
    

    public static async Task LoadAllLineCategoriesAsync(this MBSessionContext session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();
        
        List<LineCategory> categories = (await context.LINE_CATEGORies.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToLineCategory).ToList();
        session.LineCategories = new ObservableCollection<LineCategory>(categories);
    }

    public static async Task<int> AddNewLineCategoryAsync(CancellationToken cancellationToken, LineCategory category)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        if (category == null)
            return (int)ErrorEnum.NullReference;
        
        LINE_CATEGORY checkCategory = await context.LINE_CATEGORies.FirstOrDefaultAsync(x => x.ID == category.Id, cancellationToken);
        if (checkCategory != null)
            return (int)ErrorEnum.AlreadyExists;

        LINE_CATEGORY newCategory = new LINE_CATEGORY()
        {
            ID = category.Id
        };
        
        await context.LINE_CATEGORies.AddAsync(newCategory, cancellationToken);
        var result = await context.SaveChangesAsync(cancellationToken);
        if (result > 0)
            return (int)ErrorEnum.Success;
        
        return (int)ErrorEnum.Aborted;
    }

    public static async Task LoadAllLineTypesAsync(this MBSessionContext session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        List<LineTyp> types = (await context.LINE_TYPs.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToLineType).ToList();
        session.LineTyps = new ObservableCollection<LineTyp>(types);
    }

    public static async Task LoadAllLineStylesAsync(this MBSessionContext session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();
        
        List<LineStyle> styles = (await context.LINE_STYLEs.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToLineStyle).ToList();
        
        session.LineStyles = new ObservableCollection<LineStyle>(styles);
    }

    public static async Task LoadAllLineColorsAsync(this MBSessionContext session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        List<LineColor> colors = (await context.LINE_COLORs.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToLineColor).ToList();
        session.LineColors = new ObservableCollection<LineColor>(colors);
    }

    public static async Task LoadAllLineFrequencyAsync(this MBSessionContext session, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        List<LineFrequency> frequencies =
            (await context.LINE_FREQUENCies.ToListAsync(cancellationToken).ConfigureAwait(false))
            .Select(ToLineFrequency).ToList();
        session.LineFrequencies = new ObservableCollection<LineFrequency>(frequencies);
    }

    public static async Task LoadAllLineCalculationsAsync(this MBSessionContext session,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        List<LineCalculation> calculations =
            (await context.LINE_CALCULATIONs.ToListAsync(cancellationToken).ConfigureAwait(false)).Select(ToLineCalculation)
            .ToList();
        session.LineCalculations = new ObservableCollection<LineCalculation>(calculations);
    }
    
    public static async Task<int> GetAvailableIdForNewCalculationAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();
        
        int maxId = await context.LINE_CALCULATIONs.MaxAsync(x => x.ID, cancellationToken).ConfigureAwait(false);
        if (maxId != null)
            return maxId + 1;
        return (int)ErrorEnum.Aborted;
    }

    public static async Task<int> ImportNewGridLineValueAsync(CancellationToken cancellationToken, GridLine gridLine, int userId, int month, int year)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();
        
        GRID_STORAGE storage = await context.GRID_STORAGEs.FirstOrDefaultAsync(x => x.GL_ID == gridLine.Id && x.LINE_USERID == userId && x.LINE_MONTH == month && x.LINE_YEAR == year, cancellationToken);
        if (storage == null)
        {
            GRID_STORAGE newValue = new GRID_STORAGE()
            {
                GL_ID = gridLine.Id,
                VALUE = gridLine.JanuarValue,
                LINE_DAY = gridLine.ValidFrom.Day,
                LINE_MONTH = month,
                LINE_YEAR = year,
                LINE_USERID = userId,
                LINE_NOTE = string.Empty,
                LINE_LASTEDIT = new DateTime()
            };
            await context.GRID_STORAGEs.AddAsync(newValue, cancellationToken);
            if (await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0)
                return (int)ErrorEnum.Success;
            return (int)ErrorEnum.Aborted;
        }
        else
        {
            storage.VALUE = gridLine.JanuarValue;
            storage.LINE_LASTEDIT = new DateTime();
            if (await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false) > 0)
                return (int)ErrorEnum.Success;
            return (int)ErrorEnum.Aborted;
        }
    }

    public static async Task<(int, LineCalculation)> EditExistingGridLineAsync(CancellationToken cancellationToken,
        ObservableCollection<GridLine> lines, GridLine editGridLine)
    {
        cancellationToken.ThrowIfCancellationRequested();
        using var context = new DatabaseContext();

        await using (var transaction = await context.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                GRID_LINE? lineToEdit = await context.GRID_LINEs
                    .FirstOrDefaultAsync(x => x.ID == editGridLine.Id, cancellationToken).ConfigureAwait(false);
                if (lineToEdit == null)
                    return ((int)ErrorEnum.NullReference, null);
                int currentLineNumber = lineToEdit.LINE_NUMBER;

                lineToEdit.LINE_NAME = editGridLine.Name;
                lineToEdit.LINE_NUMBER = editGridLine.Number;
                lineToEdit.LINE_CATEGORYID = editGridLine.LineCategoryId;
                lineToEdit.LINE_VALIDFROM = editGridLine.ValidFrom;
                lineToEdit.LINE_VALIDTO = editGridLine.ValidTo;
                lineToEdit.LINE_FREQUENCYID = editGridLine.FrequencyId;
                lineToEdit.LINE_TYPID = editGridLine.LineTypId;
                lineToEdit.LINE_STYLEID = 1;
                lineToEdit.LINE_COLORID = 1;
                lineToEdit.LINE_CALCULATIONID = editGridLine.LineCalculationId;
                await context.SaveChangesAsync(cancellationToken);
                
                /*
                 * Der Zeilenwert wurde hier absichtlich nicht berücksichtigt durch folgende Szenarien:
                 * a) alle verfügbaren Zeilen auf den neuen Wert setzen (was Probleme bei Summenänderungen bewirken könnte)
                 * b) auf den aktuellen Monat referenzieren (wenn keine Zeile angelegt wurde erfolgt eine Null-Reference)
                 * c) nur den Ursprungswert also ValidFrom auf den neuen Wert setzen!
                 */

                LineCalculation editLineCalculation = new LineCalculation(0, string.Empty);
                if (editGridLine.LineTypId == (int)LineTypeEnum.Summen)
                {
                    var checkCalculation = await context.LINE_CALCULATIONs.FirstOrDefaultAsync(x => x.ID == lineToEdit.LINE_CALCULATIONID, cancellationToken);
                    if (checkCalculation != null)
                    {
                        checkCalculation.CALCULATION = editGridLine.LineCalculation.Calculation;
                        editLineCalculation.Id = checkCalculation.ID;
                        editLineCalculation.Calculation = checkCalculation.CALCULATION;
                    }
                    else
                    {
                        LINE_CALCULATION newCalculation = new LINE_CALCULATION()
                        {
                            ID = editGridLine.LineCalculation.Id,
                            CALCULATION = editGridLine.LineCalculation.Calculation,
                        };
                        lineToEdit.LINE_CALCULATIONID = newCalculation.ID;
                        await context.LINE_CALCULATIONs.AddAsync(newCalculation, cancellationToken);
                        editLineCalculation.Id = newCalculation.ID;
                        editLineCalculation.Calculation = newCalculation.CALCULATION;
                    }
                    await context.SaveChangesAsync(cancellationToken);
                }

                if (currentLineNumber != editGridLine.Number)
                {
                    foreach (GridLine line in lines)
                    {
                        GRID_LINE? storedLine =
                            await context.GRID_LINEs.FindAsync([line.Id], cancellationToken).ConfigureAwait(false);
                        if (storedLine != null)
                        {
                            storedLine.LINE_NUMBER = line.Number;
                        }
                    }
                    await context.SaveChangesAsync(cancellationToken);
                }
                await context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return ((int)ErrorEnum.Success, editLineCalculation);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return ((int)ErrorEnum.Aborted, null);
            }
        }
    }

    public static async Task<(int, int, LineCalculation)> AddNewGridLineAsync(CancellationToken cancellationToken,  ObservableCollection<GridLine> lines, GridLine newGridLine, bool defaultValue)
    {
        using var context = new DatabaseContext();
        cancellationToken.ThrowIfCancellationRequested();

        await using (var transaction = await context.Database.BeginTransactionAsync(cancellationToken))
        {
            try
            {
                GRID_LINE gridLine = new GRID_LINE()
                {
                    LINE_NAME = newGridLine.Name,
                    LINE_NUMBER = newGridLine.Number,
                    LINE_CATEGORYID = newGridLine.LineCategoryId,
                    LINE_VALIDFROM = newGridLine.ValidFrom,
                    LINE_VALIDTO = newGridLine.ValidTo,
                    LINE_USERID = newGridLine.Userid,
                    LINE_FREQUENCYID = newGridLine.FrequencyId,
                    LINE_TYPID = newGridLine.LineTypId,
                    LINE_STYLEID = 1,
                    LINE_COLORID = 1,
                    LINE_CALCULATIONID = newGridLine.LineCalculationId
                };
                await context.GRID_LINEs.AddAsync(gridLine, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                GRID_STORAGE gridStorage = new GRID_STORAGE()
                {
                    GL_ID = gridLine.ID,
                    VALUE = newGridLine.JanuarValue,
                    LINE_DAY = newGridLine.ValidFrom.Day,
                    LINE_MONTH = newGridLine.ValidFrom.Month,
                    LINE_YEAR = newGridLine.ValidFrom.Year,
                    LINE_USERID = newGridLine.Userid,
                    LINE_LASTEDIT = DateTime.Now
                };
                await context.GRID_STORAGEs.AddAsync(gridStorage, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
                
                if (defaultValue)
                {
                    GRID_IMPORT_VALUE? value = await context.GRID_IMPORT_VALUEs.Where(x => x.USER_ID == newGridLine.Userid && x.GZ_ID == newGridLine.Id).FirstOrDefaultAsync(cancellationToken);
                    if (value == null)
                    {
                        GRID_IMPORT_VALUE newImportValue = new GRID_IMPORT_VALUE()
                        {
                            GZ_ID = gridLine.ID,
                            USER_ID = gridLine.LINE_USERID,
                            VALUE = newGridLine.JanuarValue
                        };
                        await context.GRID_IMPORT_VALUEs.AddAsync(newImportValue, cancellationToken);
                    }
                    else
                    {
                        if (value.VALUE != newGridLine.JanuarValue)
                            value.VALUE = newGridLine.JanuarValue;
                    }
                    await context.SaveChangesAsync(cancellationToken);
                }

                LineCalculation returnCalculation = new LineCalculation();
                if (newGridLine.LineTypId == (int)LineTypeEnum.Summen)
                {
                    LINE_CALCULATION newCalculation = new LINE_CALCULATION()
                    {
                        ID = newGridLine.LineCalculation.Id,
                        CALCULATION = newGridLine.LineCalculation.Calculation,
                    };
                    await context.LINE_CALCULATIONs.AddAsync(newCalculation, cancellationToken);
                    gridLine.LINE_CALCULATIONID = newCalculation.ID;
                    await context.SaveChangesAsync(cancellationToken);
                    returnCalculation = new LineCalculation()
                    {
                        Id = newCalculation.ID,
                        Calculation = newCalculation.CALCULATION
                    };
                }
                await context.SaveChangesAsync(cancellationToken);

                foreach (GridLine line in lines)
                {
                    GRID_LINE? storedLine =
                        await context.GRID_LINEs.FindAsync([line.Id], cancellationToken).ConfigureAwait(false);
                    if (storedLine != null)
                    {
                        storedLine.LINE_NUMBER = line.Number;
                    }
                }
                await context.SaveChangesAsync(cancellationToken);
                
                await transaction.CommitAsync(cancellationToken);
                return ((int)ErrorEnum.Success, gridLine.ID, returnCalculation);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                return ((int)ErrorEnum.Aborted, 0, null);
            }
        }
    }

    public static async Task<int> GetAvailableGridLineMaxIdAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();

        int newId = context.GRID_LINEs.Max(x => x.ID) + 1;
        return newId;
    }

    public static async Task<string> GetCommentForGridLine(CancellationToken cancellationToken, int gridLineId,
        int month, int year, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        
        GRID_STORAGE? gridStorage = await context.GRID_STORAGEs.FirstOrDefaultAsync(x => x.GL_ID == gridLineId && x.LINE_MONTH == month && x.LINE_YEAR == year && x.LINE_USERID == userId, cancellationToken).ConfigureAwait(false);
        if (gridStorage == null)
            return "null";
        if (gridStorage.LINE_NOTE == null)
            return string.Empty;
        return gridStorage.LINE_NOTE;
    }

    public static async Task<int> SaveCommentForGridLine(CancellationToken cancellationToken, int gridLineId,
        string comment, int month, int year, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        
        GRID_STORAGE? gridStorage = await context.GRID_STORAGEs.FirstOrDefaultAsync(x => x.GL_ID == gridLineId && x.LINE_MONTH == month && x.LINE_YEAR == year && x.LINE_USERID == userId, cancellationToken).ConfigureAwait(false);
        if (gridStorage == null)
            return (int)ErrorEnum.NullReference;
        gridStorage.LINE_NOTE = comment;
        if (await context.SaveChangesAsync(cancellationToken) > 0)
            return (int)ErrorEnum.Success;
        return (int)ErrorEnum.Aborted;
    }

    public static async Task<int> GenerateValidGridLineForSpecificMonthAsync(DatabaseContext context, CancellationToken cancellationToken,
        int month, int year, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        List<GRID_LINE> validGridLines = await context.GRID_LINEs.Where(x => x.LINE_USERID == userId && x.LINE_VALIDTO.Month >= month && x.LINE_VALIDTO.Year >= year).ToListAsync(cancellationToken);
        if (validGridLines.Count == 0)
            return (int)ErrorEnum.NullReference;

        int createCounter = 0;
        
        foreach (GRID_LINE gridLine in validGridLines)
        {
            GRID_STORAGE? checkStroage = await context.GRID_STORAGEs.Where(x => x.GL_ID == gridLine.ID && x.LINE_USERID == userId && x.LINE_MONTH == month && x.LINE_YEAR == year).FirstOrDefaultAsync(cancellationToken);
            if (checkStroage == null)
            {
                GRID_STORAGE newStorageLine = new GRID_STORAGE()
                {
                    GL_ID = gridLine.ID,
                    VALUE = 0,
                    LINE_DAY = DateTime.Now.Day,
                    LINE_MONTH = month,
                    LINE_YEAR = year,
                    LINE_NOTE = null,
                    LINE_USERID = userId,
                    LINE_LASTEDIT = DateTime.Now
                };
                await context.AddAsync(newStorageLine, cancellationToken);
            }
            createCounter++;
        }
        if (createCounter != validGridLines.Count)
        {
            return (int)ErrorEnum.Aborted;
        }
        await context.SaveChangesAsync(cancellationToken);
        return (int)ErrorEnum.Success;
    }

    public static async Task<bool> IsGridLineValidForImportAfterFrequencyCheckAsync(CancellationToken cancellationToken,
        GRID_LINE gridLine, int month, int year)
    {
        await using var context = new DatabaseContext();
        string? frequencyId = gridLine.LINE_FREQUENCYID;
        LINE_FREQUENCY? lineFrequency = await context.LINE_FREQUENCies
            .FirstOrDefaultAsync(x => x.ID == gridLine.LINE_FREQUENCYID, cancellationToken).ConfigureAwait(false);
        if (lineFrequency == null)
            return false;
        if (lineFrequency.FREQUENCY == 1)
            return true;
        if (lineFrequency.FREQUENCY == 12 && gridLine.LINE_VALIDFROM.Year >= year &&
            gridLine.LINE_VALIDFROM.Month == month)
            return true;
        
        /*
         * TODO: Frequenz bestimmen
         * Wenn es sich um +6 Monate bzw. +12 Monate handelt hat man eine
         * Jahresverschiebung, des Weiteren ist die ValdidFrom die Vergangenheit ggf. mehrere Jahre alt
         */
        int? currentMonth = gridLine.LINE_VALIDFROM.Month;
        int iterate = 0;
        int ? startMonth = gridLine.LINE_VALIDFROM.Month;
        var frequencies = new Dictionary<int?, string>();
        frequencies.Add(startMonth, await GetMonthNameFromInt(startMonth));
        do
        {
            currentMonth += lineFrequency.FREQUENCY;
            if (currentMonth == startMonth && iterate > 0)
                break;
            if (currentMonth > 12)
                currentMonth -= 12;
            if(currentMonth != startMonth)
                frequencies.Add(currentMonth, await GetMonthNameFromInt(currentMonth));
            iterate++;
        } while (currentMonth != startMonth);

        if (frequencies.TryGetValue(month, out string? frequency))
        {
            Console.WriteLine("Monat wurde gefunden");
            return true;
        }
        return false;
    }

    public static async Task<ObservableCollection<GridLine>> RefreshGridLinesAfterImportForSpecificMonth(
        CancellationToken cancellationToken, ObservableCollection<GridLine> lines, int month, int year, int userId)
    {
        cancellationToken.ThrowIfCancellationRequested();
        await using var context = new DatabaseContext();
        foreach (GridLine gridLine in lines)
        {
            GRID_STORAGE? storage = await context.GRID_STORAGEs.Where(x => x.GL_ID == gridLine.Id && x.LINE_MONTH == month && x.LINE_YEAR == year && x.LINE_USERID == userId).FirstOrDefaultAsync(cancellationToken);
            if (storage != null)
            {
                switch (month)
                {
                    case (int)MonthEnum.Januar:
                        gridLine.JanuarValue = storage.VALUE;
                        break;
                    case (int)MonthEnum.Februar:
                        gridLine.FebruarValue = storage.VALUE;
                        break;
                    case (int)MonthEnum.März:
                        gridLine.MärzValue = storage.VALUE;
                        break;
                    case (int)MonthEnum.April:
                        gridLine.AprilValue = storage.VALUE;
                        break;
                    case (int)MonthEnum.Mai:
                        gridLine.MaiValue = storage.VALUE;
                        break;
                    case (int)MonthEnum.Juni:
                        gridLine.JuniValue = storage.VALUE;
                        break;
                    case (int)MonthEnum.Juli:
                        gridLine.JuliValue = storage.VALUE;
                        break;
                    case (int)MonthEnum.August:
                        gridLine.AugustValue = storage.VALUE;
                        break;
                    case (int)MonthEnum.September:
                        gridLine.SeptemberValue = storage.VALUE;
                        break;
                    case (int)MonthEnum.Oktober:
                        gridLine.OktoberValue = storage.VALUE;
                        break;
                    case (int)MonthEnum.November:
                        gridLine.NovemberValue = storage.VALUE;
                        break;
                    case (int)MonthEnum.Dezember:
                        gridLine.DezemberValue = storage.VALUE;
                        break;
                }
            } 
        }
        await context.SaveChangesAsync(cancellationToken);
        return lines;
    }

    private static async Task<string> GetMonthNameFromInt(int? month)
    {
        return month switch
        {
            1 => "Januar",
            2 => "Februar",
            3 => "März",
            4 => "April",
            5 => "Mai",
            6 => "Juni",
            7 => "Juli",
            8 => "August",
            9 => "September",
            10 => "Oktober",
            11 => "November",
            12 => "Dezember",
            _ => "Monat"
        };
    }


    private static GridLine ToGridLine(GRID_LINE value)
        => new GridLine()
        {
            Id = value.ID,
            Name = value.LINE_NAME,
            Number = value.LINE_NUMBER,
            LineCategoryId = value.LINE_CATEGORYID,
            ValidFrom = value.LINE_VALIDFROM,
            ValidTo = value.LINE_VALIDTO,
            Userid = value.LINE_USERID?? 0,
            FrequencyId = value.LINE_FREQUENCYID,
            LineTypId = value.LINE_TYPID,
            LineStyleId = value.LINE_STYLEID,
            LineCalculationId = value.LINE_CALCULATIONID,
            LineColorId = value.LINE_COLORID
        };

    private static GridStorage ToGridStorage(GRID_STORAGE value)
        => new GridStorage()
        {
            Id = value.ID,
            LineId = value.GL_ID,
            Value = value.VALUE,
            LineDay = value.LINE_DAY,
            LineMonth = value.LINE_MONTH,
            LineYear = value.LINE_YEAR,
            UserId = value.LINE_USERID,
            Note = value.LINE_NOTE,
            LastEdited = value.LINE_LASTEDIT
        };
    
    private static LineCategory ToLineCategory(LINE_CATEGORY value)
        => new LineCategory()
        {
            Id = value.ID
        };

    private static LineTyp ToLineType(LINE_TYP value)
        => new LineTyp()
        {
            Id = value.ID,
            Typ = value.TYP
        };

    private static LineStyle ToLineStyle(LINE_STYLE value)
        => new LineStyle()
        {
            Id = value.ID,
            Style = value.STYLE
        };

    private static LineColor ToLineColor(LINE_COLOR value)
        => new LineColor()
        {
            Id = value.ID,
            Foreground = value.FOREGROUND,
            Background = value.BACKGROUND
        };

    private static LineFrequency ToLineFrequency(LINE_FREQUENCY value)
        => new LineFrequency()
        {
            Id = value.ID,
            Frequency = value.FREQUENCY
        };

    private static LineCalculation ToLineCalculation(LINE_CALCULATION value)
        => new LineCalculation()
        {
            Id = value.ID,
            Calculation = value.CALCULATION
        };
    
    private static MBUser ToUser(MB_USER value)
        => new MBUser()
        {
            Id = value.ID,
            Name = value.USERNAME,
            Email = value.EMAIL,
            Password = value.PASSWORD
        };
}