using System;
using System.Collections.Generic;

namespace NASPLOID.Models;

public partial class GRID_STORAGE
{
    public int ID { get; set; }

    public int GL_ID { get; set; }

    public decimal? VALUE { get; set; }

    public int LINE_DAY { get; set; }

    public int LINE_MONTH { get; set; }

    public int LINE_YEAR { get; set; }

    public int LINE_USERID { get; set; }

    public string? LINE_NOTE { get; set; }

    public DateTime? LINE_LASTEDIT { get; set; }

    public virtual GRID_LINE GL { get; set; } = null!;

    public virtual MB_USER LINE_USER { get; set; } = null!;
}
