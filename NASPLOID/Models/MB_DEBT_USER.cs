using System;
using System.Collections.Generic;

namespace NASPLOID.Models;

public partial class MB_DEBT_USER
{
    public string NAME { get; set; } = null!;

    public int USERID { get; set; }

    public virtual MB_USER USER { get; set; } = null!;
}
