using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Basketball7.Models
{
    public class Player
    {
        public long Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("No.")]
        public int Jersey { get; set; }
        [DisplayName("Age")]
        public int Age { get; set; }
        [DisplayName("Height")]
        public int Height { get; set; }
        [DisplayName("Weight")]
        public int Weight { get; set; }
        [DisplayName("HighSchool")]
        public string HighSchool { get; set; }
        [DisplayName("College")]
        public string College { get; set; }
        [DisplayName("Draft year")]
        public int DraftYear { get; set; }
        [DisplayName("Position")]
        public string Position { get; set; }
        [DisplayName("Image")]
        public string ImageUrl { get; set; }

        [NotMapped]
        public List<Season> Seasons { get; set; } = new List<Season>();
    }
}
