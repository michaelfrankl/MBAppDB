using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay(("Name: {_debtName} Sum: {_debtSum} | UserId: {_userId}"))]
public partial class MBDebtList: Model
{
    public MBDebtList()
    {
        
    }

    public MBDebtList(int id, string debtName, DateOnly debtDate, decimal debtSum, string debtType, bool isDebtOpen, DateOnly debtPaidDate, 
        string debtNote, int userId, DateOnly editedDate, byte[] debtImage)
    {
        Id = id;
        DebtName = debtName;
        DebtDate = debtDate;
        DebtSum = debtSum;
        DebtType = debtType;
        IsDebtOpen = isDebtOpen;
        DebtPaidDate = debtPaidDate;
        DebtNote = debtNote;
        UserId = userId;
        EditedDate = editedDate;
        DebtImage = debtImage;
    }

    public MBDebtList Clone()
    {
        return new MBDebtList()
        {
            Id = Id,
            DebtName = DebtName,
            DebtDate = DebtDate,
            DebtSum = DebtSum,
            DebtType = DebtType,
            IsDebtOpen = IsDebtOpen,
            DebtPaidDate = DebtPaidDate,
            DebtNote = DebtNote,
            UserId = UserId,
            EditedDate = EditedDate,
            DebtImage = DebtImage
        };
    }
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int _id;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _debtName;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private DateOnly _debtDate;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private decimal? _debtSum;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? _debtType;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private bool? _isDebtOpen;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private DateOnly? _debtPaidDate;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? _debtNote;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _userId;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private DateOnly? _editedDate;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private byte[]? _debtImage;
}