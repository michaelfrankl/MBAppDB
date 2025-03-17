using System;
using System.Collections.Generic;

namespace NASPLOID.Models;

public partial class MB_ROLE
{
    public string ROLE { get; set; } = null!;

    public virtual ICollection<MB_USER> MB_USERs { get; set; } = new List<MB_USER>();
}
