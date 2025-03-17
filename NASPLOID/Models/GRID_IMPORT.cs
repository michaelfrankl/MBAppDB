using System;
using System.Collections.Generic;

namespace NASPLOID.Models;

public partial class GRID_IMPORT
{
    public int ID { get; set; }

    public int? USER_ID { get; set; }

    public int MONTH { get; set; }

    public int YEAR { get; set; }

    public int STATUS { get; set; }

    public DateOnly LAST_IMPORT { get; set; }

    public virtual MB_USER? USER { get; set; }
}
