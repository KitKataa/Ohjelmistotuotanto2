namespace SharedLib
{
    public class matkakohdeDTO
    {
        public long idmatkakohde { get; set; }
        public string kohdenimi { get; set; }
        public string maa { get; set; }
        public string paikkakunta { get; set; }
        public string kuvausteksti { get; set; }
        public string kuva { get; set; }
       // public virtual ICollection<tarinaDTO> Tarinas { get; set; }
    }
}
