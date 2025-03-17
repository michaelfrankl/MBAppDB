using System;
using System.Collections.Generic;

namespace NASPLOID.Models;

public partial class CHART_CATEGORY_COLOR
{
    public int ID { get; set; }

    public string? CATEGORYID { get; set; }

    public int? USERID { get; set; }

    public int R { get; set; }

    public int G { get; set; }

    public int B { get; set; }

    public virtual LINE_CATEGORY? CATEGORY { get; set; }

    public virtual MB_USER? USER { get; set; }
}
