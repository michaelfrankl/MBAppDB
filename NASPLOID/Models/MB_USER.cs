using System;
using System.Collections.Generic;

namespace NASPLOID.Models;

public partial class MB_USER
{
    public int ID { get; set; }

    public string? USERNAME { get; set; }

    public string? EMAIL { get; set; }

    public string? PASSWORD { get; set; }

    public string? ROLE { get; set; }

    public virtual ICollection<CHART_CATEGORY_COLOR> CHART_CATEGORY_COLORs { get; set; } = new List<CHART_CATEGORY_COLOR>();

    public virtual ICollection<GRID_IMPORT_VALUE> GRID_IMPORT_VALUEs { get; set; } = new List<GRID_IMPORT_VALUE>();

    public virtual ICollection<GRID_IMPORT> GRID_IMPORTs { get; set; } = new List<GRID_IMPORT>();

    public virtual ICollection<GRID_LINE> GRID_LINEs { get; set; } = new List<GRID_LINE>();

    public virtual ICollection<GRID_STORAGE> GRID_STORAGEs { get; set; } = new List<GRID_STORAGE>();

    public virtual ICollection<MB_DEBT_LIST> MB_DEBT_LISTs { get; set; } = new List<MB_DEBT_LIST>();

    public virtual ICollection<MB_DEBT_USER> MB_DEBT_USERs { get; set; } = new List<MB_DEBT_USER>();

    public virtual ICollection<MB_GLOBAL_VARIABLE> MB_GLOBAL_VARIABLEs { get; set; } = new List<MB_GLOBAL_VARIABLE>();

    public virtual MB_ROLE? ROLENavigation { get; set; }
}
