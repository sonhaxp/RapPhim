namespace BanVeXemPhim.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GheNgoi")]
    public partial class GheNgoi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GheNgoi()
        {
            VeBan = new HashSet<VeBan>();
        }

        [Key]
        [StringLength(20)]
        public string MaGheNgoi { get; set; }

        [Required]
        [StringLength(10)]
        public string ViTriDay { get; set; }

        public int ViTriCot { get; set; }

        public int TrangThai { get; set; }

        [Required]
        [StringLength(20)]
        public string MaPhongChieu { get; set; }

        public virtual PhongChieu PhongChieu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VeBan> VeBan { get; set; }
    }
}
