using SharedLib;

namespace MyApi.Data;

public partial class Matkakohde
{
    public Matkakohde() { }

    internal Matkakohde(matkakohdeDTO Matkakohdedto)
    {


    }
    internal matkakohdeDTO toMatkakohdeDTO()
    {
        return new matkakohdeDTO
        {
            idmatkakohde = this.Idmatkakohde,
            kohdenimi = this.Kohdenimi,
            maa = this.Maa,
            paikkakunta = this.Paikkakunta,
            kuvausteksti = this.Kuvausteksti,
            kuva = this.Kuva
        };
    }
    public long Idmatkakohde { get; set; }

    public string? Kohdenimi { get; set; }

    public string? Maa { get; set; }

    public string? Paikkakunta { get; set; }

    public string? Kuvausteksti { get; set; }

    public string? Kuva { get; set; }

    public virtual ICollection<Tarina>? Tarinas { get; } = new List<Tarina>();
}
