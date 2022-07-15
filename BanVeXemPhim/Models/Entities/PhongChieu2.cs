namespace BanVeXemPhim.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PhongChieu2
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaPhongChieu { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int soluonghang { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int soluongcot { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string MaRap { get; set; }

        [StringLength(20)]
        public string TenPhong { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(50)]
        public string TenRap { get; set; }

        [StringLength(200)]
        public string DiaChi { get; set; }
    }
}
