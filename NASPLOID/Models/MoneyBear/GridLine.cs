using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NASPLOID.Models.MoneyBear;

[DebuggerDisplay("{_id} | {_name}")]
public partial class GridLine : Model
{
    public GridLine(int id, string lineName, int lineNumber, string lineCategory, DateOnly validFrom, DateOnly validTo, int userId, 
        string frequency, int lineType, int lineColorId, string lineForegroundColor, string lineBackgroundColor, int lineStyleId, int lineCalculationId,
        decimal? januaryValue, decimal? februaryValue, decimal? marchValue, decimal? aprilValue, decimal? mayValue, decimal? juneValue, decimal? julyValue, decimal? augustValue, decimal? septemberValue,
        decimal? octoberValue, decimal? novemberValue, decimal? decemberValue, bool isReadOnly)
    {
        Id = id;
        Name = lineName;
        Number = lineNumber;
        LineCategoryId = lineCategory;
        ValidFrom = validFrom;
        ValidTo = validTo;
        Userid = userId;
        FrequencyId = frequency;
        LineTypId = lineType;
        LineColorId = lineColorId;
        LineForegroundColor = lineForegroundColor;
        LineBackgroundColor = lineBackgroundColor;
        LineStyleId = lineStyleId;
        LineCalculationId = lineCalculationId;
        JanuarValue = januaryValue;
        FebruarValue = februaryValue;
        MärzValue = marchValue;
        AprilValue = aprilValue;
        MaiValue = mayValue;
        JuniValue = juneValue;
        JuliValue = julyValue;
        AugustValue = augustValue;
        SeptemberValue = septemberValue;
        OktoberValue = octoberValue;
        NovemberValue = novemberValue;
        DezemberValue = decemberValue;
        IsReadOnly = isReadOnly;
    }

    public GridLine()
    {
        
    }

    public GridLine Clone()
    {
        return new GridLine()
        {
            Id = Id,
            Name = Name,
            Number = Number,
            LineCategoryId = LineCategoryId,
            ValidFrom = ValidFrom,
            ValidTo = ValidTo,
            Userid = Userid,
            FrequencyId = FrequencyId,
            LineTypId = LineTypId,
            LineColorId = LineColorId,
            LineForegroundColor = LineForegroundColor,
            LineBackgroundColor = LineBackgroundColor,
            LineStyleId = LineStyleId,
            LineCalculationId = LineCalculationId,
            JanuarValue = JanuarValue,
            FebruarValue = FebruarValue,
            MärzValue = MärzValue,
            AprilValue = AprilValue,
            MaiValue = MaiValue,
            JuniValue = JuniValue,
            JuliValue = JuliValue,
            AugustValue = AugustValue,
            SeptemberValue = SeptemberValue,
            OktoberValue = OktoberValue,
            NovemberValue = NovemberValue,
            DezemberValue = DezemberValue,
            IsReadOnly = IsReadOnly,
        };
    }
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)] 
    private int _id;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string _name;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int _number;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)] 
    private string? _lineCategoryId;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)] 
    private LineCategory? _lineCategory;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private DateOnly _validFrom;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private DateOnly _validTo;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int _userid;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? _frequencyId;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private LineFrequency? _frequency;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _lineTypId;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private LineTyp? _lineTyp;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _lineColorId;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? _lineForegroundColor;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string? _lineBackgroundColor;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _lineStyleId;

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int? _lineCalculationId;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private LineCalculation? _lineCalculation;

    #region Grid Month Value Selection

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private decimal? _januarValue;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private decimal? _februarValue;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private decimal? _märzValue;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private decimal? _aprilValue;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private decimal? _maiValue;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private decimal? _juniValue;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private decimal? _juliValue;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private decimal? _augustValue;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private decimal? _septemberValue;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private decimal? _oktoberValue;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private decimal? _novemberValue;
    
    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private decimal? _dezemberValue;

    #endregion

    [ObservableProperty] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private bool _isReadOnly;
}