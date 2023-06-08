using SharedLib;
namespace MyApi.Data;


public partial class Matkaaja
{
    public Matkaaja() { }

    internal Matkaaja(matkaajaDTO matkaajadto)
    {
        Idmatkaaja = matkaajadto.idmatkaaja;
        Etunimi = matkaajadto.etunimi;
        Sukunimi = matkaajadto.sukunimi;
        Nimimerkki = matkaajadto.nimimerkki;
        Paikkakunta = matkaajadto.paikkakunta;
        Esittely = matkaajadto.esittely;
        Kuva = matkaajadto.kuva;
        Email = matkaajadto.email;
        Password = matkaajadto.password;

    }
    internal matkaajaDTO toMatkaajaDTO()
    {
        return new matkaajaDTO
        {
            idmatkaaja = this.Idmatkaaja,
            etunimi = this.Etunimi,
            sukunimi = this.Sukunimi,
            nimimerkki = this.Nimimerkki,
            paikkakunta = this.Paikkakunta,
            esittely = this.Esittely,
            kuva = this.Kuva,
            email = this.Email,
            password = this.Password
        };
    }




    public long Idmatkaaja { get; set; }

    public string? Etunimi { get; set; }

    public string? Sukunimi { get; set; }

    public string? Nimimerkki { get; set; }

    public string? Paikkakunta { get; set; }

    public string? Esittely { get; set; }

    public string? Kuva { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Matka> Matkas { get; } = new List<Matka>();


}
