using System;
using System.Collections.Generic;

namespace NASPLOID.Models;

public partial class GRID_IMPORT_VALUE
{
    public int ID { get; set; }

    public int? GZ_ID { get; set; }

    public int? USER_ID { get; set; }

    public decimal? VALUE { get; set; }

    public virtual GRID_LINE? GZ { get; set; }

    public virtual MB_USER? USER { get; set; }
}
