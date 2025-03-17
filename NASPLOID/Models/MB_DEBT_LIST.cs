using System;
using System.Collections.Generic;

namespace NASPLOID.Models;

public partial class MB_DEBT_LIST
{
    public int ID { get; set; }

    public string? NAME { get; set; }

    public DateOnly DEBTDATE { get; set; }

    public decimal? DEBTSUM { get; set; }

    public string? DEBTTYPE { get; set; }

    public bool? DEBTOPEN { get; set; }

    public DateOnly? DEBTPAIDDATE { get; set; }

    public string? DEBTNOTE { get; set; }

    public int? USERID { get; set; }

    public DateOnly? EDITEDATE { get; set; }

    public byte[]? DEBTIMAGE { get; set; }

    public virtual MB_DEBT_TYPE? DEBTTYPENavigation { get; set; }

    public virtual MB_USER? USER { get; set; }
}
