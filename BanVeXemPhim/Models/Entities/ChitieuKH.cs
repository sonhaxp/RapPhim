namespace BanVeXemPhim.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChitieuKH")]
    public partial class ChitieuKH
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaKhachHang { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string HoTen { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string Email { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(100)]
        public string DiaChi { get; set; }

        public long? Chitieu { get; set; }
    }
}
