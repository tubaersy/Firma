namespace FIRMA_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("URUN")]
    public partial class URUN
    {
        [Key]
        public int URUN_REFNO { get; set; }

        [Required(ErrorMessage ="�r�n ad�n� giriniz !")]
        [StringLength(50, ErrorMessage ="�r�n ad� 3 ile 5 karakter aras�nda olmal�",
                          ErrorMessageResourceName ="",
                          ErrorMessageResourceType = null,
                          MinimumLength = 3)]
        public string URUN_ADI { get; set; }

        [Column(TypeName = "numeric")]
        [Required(ErrorMessage = "Fiyat� giriniz !")]
        public decimal FIYATI { get; set; }

        [Required(ErrorMessage = "�r�n durumunu belirtiniz")]
        public bool DURUMU { get; set; }

        [Required(ErrorMessage = "Bir kategori se�iniz")]
        public int KATEGORI_REFNO { get; set; }

        [Required(ErrorMessage = "KDV oran� giriniz")]
        public int KDV_ORANI { get; set; }

        [Required(ErrorMessage = "Bir marka se�iniz")]
        public int MARKA_REFNO { get; set; }

        [StringLength(4000)]
        public string ACIKLAMA { get; set; }

        [Required(ErrorMessage = "Bir resim se�iniz")]
        [StringLength(50)]
        public string RESIM1 { get; set; }

        [StringLength(50)]
        public string RESIM2 { get; set; }

        [StringLength(50)]
        public string RESIM3 { get; set; }

        [StringLength(50)]
        public string RESIM4 { get; set; }

        public virtual KATEGORI KATEGORI { get; set; }

        public virtual MARKA MARKA { get; set; }
    }
}
