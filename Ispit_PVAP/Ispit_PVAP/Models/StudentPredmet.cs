using System;
using System.Collections.Generic;

namespace Ispit_PVAP.Models;

public partial class StudentPredmet
{
    public int IdStudenta { get; set; }

    public short IdPredmeta { get; set; }

    public string SkolskaGodina { get; set; } = null!;

    public virtual Predmet IdPredmetaNavigation { get; set; } = null!;

    public virtual Student IdStudentaNavigation { get; set; } = null!;
}
