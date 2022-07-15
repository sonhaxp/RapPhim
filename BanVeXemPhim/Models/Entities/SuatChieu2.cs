namespace BanVeXemPhim.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SuatChieu2
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaSuatChieu { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MaPhim { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string TenPhim { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ThoiGianChieu { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string MaPhongChieu { get; set; }

        [StringLength(20)]
        public string TenPhong { get; set; }

        [StringLength(10)]
        public string GioChieu { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(20)]
        public string MaRap { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string Anh { get; set; }

        public int? Tuoi { get; set; }

        [StringLength(100)]
        public string GhiChu { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string TenRap { get; set; }
    }
}
