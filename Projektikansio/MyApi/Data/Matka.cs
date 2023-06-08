using SharedLib;

namespace MyApi.Data;

public partial class Matka
{
    public Matka() { }

    internal Matka(matkaDTO Matkadto)
    {


    }
    internal matkaDTO toMatkaDTO()
    {
        return new matkaDTO
        {
            idmatkaaja = this.Idmatkaaja,
            alkupvm = this.Alkupvm,
            loppupvm = this.Loppupvm,
            yksityinen = this.Yksityinen,
            idmatka = this.Idmatka
        };
    }

    public long Idmatkaaja { get; set; }

    public DateTime Alkupvm { get; set; }

    public DateTime Loppupvm { get; set; }

    public long Yksityinen { get; set; }

    public long Idmatka { get; set; }

    public virtual Matkaaja IdmatkaajaNavigation { get; set; } = null!;

    public virtual ICollection<Tarina> Tarinas { get; } = new List<Tarina>();
}
