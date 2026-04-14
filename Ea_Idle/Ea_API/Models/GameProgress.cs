using System.ComponentModel.DataAnnotations;

namespace Ea_API.Models
{
    public class GameProgress
    {
        [Key]
        public int Id { get; set; }

        public string SilverPennies { get; set; }
    }
}
