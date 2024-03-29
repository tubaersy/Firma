namespace FIRMA_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SAYFA")]
    public partial class SAYFA
    {
        [Key]
        public int SAYFA_REFNO { get; set; }

        [Required]
        [StringLength(100)]
        public string BASLIK { get; set; }

        [Required]
        public string ICERIK { get; set; }
    }
}
