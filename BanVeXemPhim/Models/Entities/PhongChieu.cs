namespace BanVeXemPhim.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhongChieu")]
    public partial class PhongChieu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhongChieu()
        {
            GheNgoi = new HashSet<GheNgoi>();
            SuatChieu = new HashSet<SuatChieu>();
        }

        [Key]
        [StringLength(20)]
        public string MaPhongChieu { get; set; }

        public int soluonghang { get; set; }

        public int soluongcot { get; set; }

        [Required]
        [StringLength(20)]
        public string MaRap { get; set; }

        [StringLength(20)]
        public string TenPhong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GheNgoi> GheNgoi { get; set; }

        public virtual Rap Rap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuatChieu> SuatChieu { get; set; }
    }
}
