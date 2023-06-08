namespace SharedLib
{
    public class tarinaDTO
    {

        public long idtarina { get; set; }
        public DateTime? pvm { get; set; }
        public string? teksti { get; set; }
        public long idmatkakohde { get; set; }
       // public virtual matkakohdeDTO Matkakohde { get; set; } 
        public long idmatka { get; set; }
        //public virtual matkaDTO Matka { get; set; }

       // public virtual ICollection<kuvaDTO> Kuvat { get; set; }
    
    }
}
