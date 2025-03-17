using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

public partial class MBSessionContext : Model
{
    [ObservableProperty] private ObservableCollection<GridLine>? _gridLines;
    [ObservableProperty] private ObservableCollection<GridStorage>? _gridStorages;
    [ObservableProperty] private ObservableCollection<LineCategory>? _lineCategories;
    [ObservableProperty] private ObservableCollection<ChartCategoryColor>? _chartCategoryColors;
    [ObservableProperty] private ObservableCollection<LineFrequency>? _lineFrequencies;
    [ObservableProperty] private ObservableCollection<LineTyp>? _lineTyps;
    [ObservableProperty] private ObservableCollection<LineColor>? _lineColors;
    [ObservableProperty] private ObservableCollection<LineStyle>? _lineStyles;
    [ObservableProperty] private ObservableCollection<LineCalculation>? _lineCalculations;
    [ObservableProperty] private ObservableCollection<MBRole>? _roles;
    [ObservableProperty] private ObservableCollection<MBUser>? _users;
    [ObservableProperty] private ObservableCollection<MBErrorCodes>? _errorCodes;
    [ObservableProperty] private ObservableCollection<MBGridImportValues>? _gridImportValues;
    [ObservableProperty] private ObservableCollection<MBGridImport>? _gridImports;
    [ObservableProperty] private MBUser? _user;
    [ObservableProperty] private ObservableCollection<MBGlobalVariables>? _globalVariables;
    [ObservableProperty] private ObservableCollection<MBDebtUser>? _debtUSer;
    [ObservableProperty] private ObservableCollection<MBDebtList>? _debtList;
    [ObservableProperty] private ObservableCollection<MBDebtType>? _debtTypes;
    [ObservableProperty] private string? _loginError;
}