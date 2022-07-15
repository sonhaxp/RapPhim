namespace BanVeXemPhim.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Phim2
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaPhim { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string TenPhim { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ThoiLuong { get; set; }

        public DateTime? NgayCongChieu { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string NgonNgu { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(100)]
        public string DienVien { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(50)]
        public string QuocGia { get; set; }

        [Key]
        [Column(Order = 6)]
        [StringLength(50)]
        public string NhaSanXuat { get; set; }

        [StringLength(4000)]
        public string TomTat { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TrangThai { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(20)]
        public string MaTheLoai { get; set; }

        [StringLength(50)]
        public string TenTheLoai { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(50)]
        public string Anh { get; set; }

        [StringLength(100)]
        public string GhiChu { get; set; }

        public int? Tuoi { get; set; }
    }
}
