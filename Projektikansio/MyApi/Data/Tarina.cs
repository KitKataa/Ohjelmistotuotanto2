using SharedLib;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Data;

public partial class Tarina
{

    public Tarina() { }

    internal Tarina(tarinaDTO tarinadto)
    {
    }
    internal tarinaDTO toTarinaDTO()
    {
        return new tarinaDTO
        {
            idtarina = this.Idtarina,
            pvm = this.Pvm,
            teksti = this.Teksti,
            idmatkakohde = this.Idmatkakohde,
            idmatka = this.Idmatka


        };
    }

    public long Idtarina { get; set; }

    public DateTime? Pvm { get; set; } = null!;

    public string? Teksti { get; set; }

    public long Idmatkakohde { get; set; }
    public Matkakohde IdmatkakohdeNavigation { get; set; }

    public long Idmatka { get; set; }

    public Matka IdmatkaNavigation { get; set; } 

    public virtual ICollection<Kuva> Kuvas { get; } = new List<Kuva>();
}
