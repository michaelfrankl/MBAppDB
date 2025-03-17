using System;
using System.Collections.Generic;

namespace NASPLOID.Models;

public partial class LINE_STYLE
{
    public int ID { get; set; }

    public string? STYLE { get; set; }

    public virtual ICollection<GRID_LINE> GRID_LINEs { get; set; } = new List<GRID_LINE>();
}
