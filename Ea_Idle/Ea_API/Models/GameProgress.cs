using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ea_API.Models
{
    public class GameProgress
    {
        [Key]
        [ForeignKey(nameof(Account.Id))]
        public int AccountId { get; set; }

        public string SilverPennies { get; set; }

        public Dictionary<string, Dictionary<string, int>> MiningUpgrades { get; set; }

        public GameProgress(int accountId)
        {
            AccountId = accountId;
            SetDefault();
        }

        public void SetDefault()
        {
            SilverPennies = "0";
            MiningUpgrades = new Dictionary<string, Dictionary<string, int>>
            {
                { "Equipment", new Dictionary<string, int> {
                    { "Lvl", 0},
                    { "Price", 10},
                    { "Current bonus", 0},
                    { "Bonus mult", 98}
                } },
                { "Miners Count", new Dictionary<string, int> {
                    { "Lvl", 0},
                    { "Price", 250},
                    { "Current bonus", 0},
                    { "Bonus mult", 50}
                } },
                { "Ore Purity", new Dictionary<string, int> {
                    { "Lvl", 0},
                    { "Price", 20},
                    { "Current bonus", 0},
                    { "Bonus add", 1}
                } },
                { "Ore Price", new Dictionary<string, int> {
                    { "Lvl", 0},
                    { "Price", 20},
                    { "Current bonus", 0},
                    { "Bonus add", 1}
                } },
            };
        }
    }
}
