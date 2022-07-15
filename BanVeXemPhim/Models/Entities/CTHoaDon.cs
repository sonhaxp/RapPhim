namespace BanVeXemPhim.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CTHoaDon")]
    public partial class CTHoaDon
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaHoaDon { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MaDoAn { get; set; }

        public int SoLuong { get; set; }

        public long ThanhTien { get; set; }

        public virtual DoAn DoAn { get; set; }

        public virtual HoaDon HoaDon { get; set; }
    }
}
