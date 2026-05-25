using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ea_API.Models
{
    public class GameProgress
    {
        [Key]
        [ForeignKey(nameof(Account.Id))]
        public int AccountId { get; set; }

        public string SilverPennies { get; set; }

        public GameProgress(int accountId)
        {
            AccountId = accountId;
            SetDefault();
        }

        public void SetDefault()
        {
            SilverPennies = "0";
        }
    }
}
