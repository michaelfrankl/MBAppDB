using System;
using System.Collections.Generic;

namespace NASPLOID.Models;

public partial class GRID_LINE
{
    public int ID { get; set; }

    public string LINE_NAME { get; set; } = null!;

    public int LINE_NUMBER { get; set; }

    public string? LINE_CATEGORYID { get; set; }

    public DateOnly LINE_VALIDFROM { get; set; }

    public DateOnly LINE_VALIDTO { get; set; }

    public int? LINE_USERID { get; set; }

    public string? LINE_FREQUENCYID { get; set; }

    public int? LINE_TYPID { get; set; }

    public int? LINE_STYLEID { get; set; }

    public int? LINE_COLORID { get; set; }

    public int? LINE_CALCULATIONID { get; set; }

    public virtual ICollection<GRID_IMPORT_VALUE> GRID_IMPORT_VALUEs { get; set; } = new List<GRID_IMPORT_VALUE>();

    public virtual ICollection<GRID_STORAGE> GRID_STORAGEs { get; set; } = new List<GRID_STORAGE>();

    public virtual LINE_CALCULATION? LINE_CALCULATION { get; set; }

    public virtual LINE_CATEGORY? LINE_CATEGORY { get; set; }

    public virtual LINE_FREQUENCY? LINE_FREQUENCY { get; set; }

    public virtual LINE_STYLE? LINE_STYLE { get; set; }

    public virtual LINE_TYP? LINE_TYP { get; set; }

    public virtual MB_USER? LINE_USER { get; set; }
}
