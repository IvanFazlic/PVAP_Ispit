namespace Ispit_PVAP.Models
{
    public class Prijava_brojIndeksa
    {
        public int IdPrijave { get; set; }
        public int IdIspita { get; set; }
        public int IdStudneta { get; set; }
        public virtual Ispit IdIspitaNavigation { get; set; } = null!;
        public virtual Student IdStudentaNavigation { get; set; } = null!;
    }
    public class PrijavaIspita
    {
        public int IdStudneta { get; set; }
        public int IdPredmeta { get; set; }
    }
    public class Test
    {
        public int IdIspita { get; set; }
        public int Count { get; set; }
    }
}
