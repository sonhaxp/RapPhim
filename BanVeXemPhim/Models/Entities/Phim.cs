namespace BanVeXemPhim.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Phim")]
    public partial class Phim
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phim()
        {
            SuatChieu = new HashSet<SuatChieu>();
        }

        [Key]
        [StringLength(20)]
        public string MaPhim { get; set; }

        [Required]
        [StringLength(50)]
        public string TenPhim { get; set; }

        public int ThoiLuong { get; set; }

        public DateTime? NgayCongChieu { get; set; }

        [Required]
        [StringLength(50)]
        public string NgonNgu { get; set; }

        [Required]
        [StringLength(100)]
        public string DienVien { get; set; }

        [Required]
        [StringLength(50)]
        public string QuocGia { get; set; }

        [Required]
        [StringLength(50)]
        public string NhaSanXuat { get; set; }

        [StringLength(4000)]
        public string TomTat { get; set; }

        public int TrangThai { get; set; }

        [Required]
        [StringLength(20)]
        public string MaTheLoai { get; set; }

        [Required]
        [StringLength(50)]
        public string Anh { get; set; }

        [StringLength(100)]
        public string GhiChu { get; set; }

        public int? Tuoi { get; set; }

        public virtual TheLoai TheLoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuatChieu> SuatChieu { get; set; }
    }
}
