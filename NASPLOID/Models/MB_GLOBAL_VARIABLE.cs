using System;
using System.Collections.Generic;

namespace NASPLOID.Models;

public partial class MB_GLOBAL_VARIABLE
{
    public string ID { get; set; } = null!;

    public int USERID { get; set; }

    public string? VALUEASSTRING { get; set; }

    public decimal? VALUEASDECIMAL { get; set; }

    public string? URL { get; set; }

    public virtual MB_USER USER { get; set; } = null!;
}
