namespace FinalProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        public int ID { get; set; }

        public int? CatID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Detail { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        public int? Price { get; set; }

        [StringLength(50)]
        public string Color { get; set; }

        [StringLength(50)]
        public string Size { get; set; }
    }
}
