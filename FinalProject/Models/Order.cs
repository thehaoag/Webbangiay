namespace FinalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int ID { get; set; }

        public int? CusID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public int? CatID { get; set; }

        public int? Price { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Created_date { get; set; }

        public int? Status { get; set; }

    }
}
