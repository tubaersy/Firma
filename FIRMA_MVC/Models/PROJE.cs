namespace FIRMA_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PROJE")]
    public partial class PROJE
    {
        [Key]
        [Display(Name = "Proje Refno:")]
        public int PROJE_REFNO { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Proje Adý:")]
        public string PROJE_ADI { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Resim:")]
        public string RESIM { get; set; }

        [Required]
        [StringLength(1000)]
        [Display(Name = "Açýklama:")]
        public string ACIKLAMA { get; set; }
    }
}
