namespace BanVeXemPhim.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            CTHoaDon = new HashSet<CTHoaDon>();
            VeBan = new HashSet<VeBan>();
        }

        [Key]
        [StringLength(20)]
        public string MaHoaDon { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayLap { get; set; }

        public long? TongTien { get; set; }

        [Required]
        [StringLength(20)]
        public string MaKhachHang { get; set; }

        public int? SoVe { get; set; }

        [StringLength(100)]
        public string Ghe { get; set; }

        [StringLength(100)]
        public string Rap { get; set; }

        [StringLength(50)]
        public string Phim { get; set; }

        [StringLength(200)]
        public string DoAn { get; set; }

        [StringLength(50)]
        public string SuatChieu { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHoaDon> CTHoaDon { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VeBan> VeBan { get; set; }
    }
}
