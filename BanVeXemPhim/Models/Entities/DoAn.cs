namespace BanVeXemPhim.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoAn")]
    public partial class DoAn
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DoAn()
        {
            CTHoaDon = new HashSet<CTHoaDon>();
        }

        [Key]
        [StringLength(20)]
        public string MaDoAn { get; set; }

        [Required]
        [StringLength(100)]
        public string TenDoAn { get; set; }

        public long GiaBan { get; set; }

        [StringLength(50)]
        public string Anh { get; set; }

        [StringLength(50)]
        public string ChiTiet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHoaDon> CTHoaDon { get; set; }
    }
}
