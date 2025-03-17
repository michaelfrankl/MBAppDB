using System;
using System.Collections.Generic;

namespace NASPLOID.Models;

public partial class MB_DEBT_TYPE
{
    public string ID { get; set; } = null!;

    public virtual ICollection<MB_DEBT_LIST> MB_DEBT_LISTs { get; set; } = new List<MB_DEBT_LIST>();
}
