using SharedLib;

namespace MyApi.Data;

public partial class Kuva
{
    public Kuva() { }

    internal Kuva(kuvaDTO kuvadto)
    {


    }
    internal kuvaDTO tokuvaDTO()
    {
        return new kuvaDTO
        {
            idkuva = this.Idkuva,
            kuva = this.Kuva1,
            idtarina = this.Idtarina

        };
    }


    public long Idkuva { get; set; }

    public string? Kuva1 { get; set; }

    public long Idtarina { get; set; }

    public virtual Tarina IdtarinaNavigation { get; set; } = null!;
}
