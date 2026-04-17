using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ea_API.Models
{
    public class GameProgress
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Account.Id))]
        public int AccountId { get; set; }

        public string SilverPennies { get; set; }

        public GameProgress(int id, int accountId, string silverPennies)
        {
            Id = id;
            AccountId = accountId;
            SilverPennies = silverPennies;
        }
    }
}
