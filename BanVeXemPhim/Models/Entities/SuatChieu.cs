namespace BanVeXemPhim.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SuatChieu")]
    public partial class SuatChieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SuatChieu()
        {
            VeBan = new HashSet<VeBan>();
        }

        [Key]
        [StringLength(20)]
        public string MaSuatChieu { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThoiGianChieu { get; set; }

        [Required]
        [StringLength(20)]
        public string MaPhim { get; set; }

        [Required]
        [StringLength(20)]
        public string MaPhongChieu { get; set; }

        [StringLength(10)]
        public string GioChieu { get; set; }

        public virtual Phim Phim { get; set; }

        public virtual PhongChieu PhongChieu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VeBan> VeBan { get; set; }
    }
}
