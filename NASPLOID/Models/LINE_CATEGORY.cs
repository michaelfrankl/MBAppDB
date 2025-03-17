using System;
using System.Collections.Generic;

namespace NASPLOID.Models;

public partial class LINE_CATEGORY
{
    public string ID { get; set; } = null!;

    public virtual ICollection<CHART_CATEGORY_COLOR> CHART_CATEGORY_COLORs { get; set; } = new List<CHART_CATEGORY_COLOR>();

    public virtual ICollection<GRID_LINE> GRID_LINEs { get; set; } = new List<GRID_LINE>();
}
