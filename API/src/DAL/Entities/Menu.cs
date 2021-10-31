using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DAL.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        public int Weight { get; set; }
        public int Meat { get; set; }
        public string About { get; set; }
        public List<Restaurant> Restaurants { get; set; }
    }
}
