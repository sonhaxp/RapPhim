namespace BanVeXemPhim.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VeBan")]
    public partial class VeBan
    {
        [Key]
        [StringLength(20)]
        public string MaVe { get; set; }

        [Required]
        [StringLength(20)]
        public string MaSuatChieu { get; set; }

        [Required]
        [StringLength(20)]
        public string MaGheNgoi { get; set; }

        [StringLength(20)]
        public string MaHoaDon { get; set; }

        public virtual GheNgoi GheNgoi { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual SuatChieu SuatChieu { get; set; }
    }
}
