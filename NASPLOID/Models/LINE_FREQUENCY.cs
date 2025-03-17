using System;
using System.Collections.Generic;

namespace NASPLOID.Models;

public partial class LINE_FREQUENCY
{
    public string ID { get; set; } = null!;

    public int? FREQUENCY { get; set; }

    public virtual ICollection<GRID_LINE> GRID_LINEs { get; set; } = new List<GRID_LINE>();
}
