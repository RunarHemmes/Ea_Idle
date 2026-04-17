using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models
{
    public class GameProgress
    {
        [Key]
        public int Id { get; set; }

        public string SilverPennies { get; set; }
    }
}
