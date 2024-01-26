using System;
using System.Collections.Generic;

namespace Ispit_PVAP.Models;

public partial class Student
{
    public int IdStudenta { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string Smer { get; set; } = null!;

    public short Broj { get; set; }

    public string GodinaUpisa { get; set; } = null!;

    public virtual ICollection<StudentPredmet> StudentPredmets { get; set; } = new List<StudentPredmet>();

    public virtual ICollection<Zapisnik> Zapisniks { get; set; } = new List<Zapisnik>();
}
public partial class NoviStudent
{
    public int IdStudenta { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string Indeks { get; set; } = null!;

    public virtual ICollection<StudentPredmet> StudentPredmets { get; set; } = new List<StudentPredmet>();

    public virtual ICollection<Zapisnik> Zapisniks { get; set; } = new List<Zapisnik>();
}
public partial class StudentIzmena
{
    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

}
