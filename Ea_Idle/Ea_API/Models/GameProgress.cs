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
                    { "Current_Bonus", 1000},
                    { "Bonus_mult", 98}
                } },
                { "Miners_Count", new Dictionary<string, int> {
                    { "Lvl", 0},
                    { "Price", 250},
                    { "Current_Bonus", 1000},
                    { "Bonus_mult", 50}
                } },
                { "Ore_Purity", new Dictionary<string, int> {
                    { "Lvl", 0},
                    { "Price", 20},
                    { "Current_Bonus", 100},
                    { "Bonus_add", 1}
                } },
                { "Ore_Price", new Dictionary<string, int> {
                    { "Lvl", 0},
                    { "Price", 20},
                    { "Current_Bonus", 100},
                    { "Bonus_add", 1}
                } },
            };
        }
    }
}
