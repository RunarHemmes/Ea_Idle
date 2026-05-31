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

        public Dictionary<string, Dictionary<string, float>> MiningUpgrades { get; set; }

        public GameProgress(int accountId)
        {
            AccountId = accountId;
            SetDefault();
        }

        public void SetDefault()
        {
            SilverPennies = "0";
            MiningUpgrades = new Dictionary<string, Dictionary<string, float>>
            {
                { "Equipment", new Dictionary<string, float> {
                    { "Lvl", 0},
                    { "Price", 10},
                    { "Current_Bonus", 10},
                    { "Bonus_mult", 0.98f}
                } },
                { "Miners_Count", new Dictionary<string, float> {
                    { "Lvl", 0},
                    { "Price", 250},
                    { "Current_Bonus", 10},
                    { "Bonus_mult", 0.5f}
                } },
                { "Ore_Purity", new Dictionary<string, float> {
                    { "Lvl", 0},
                    { "Price", 20},
                    { "Current_Bonus", 1},
                    { "Bonus_add", 1}
                } },
                { "Ore_Price", new Dictionary<string, float> {
                    { "Lvl", 0},
                    { "Price", 20},
                    { "Current_Bonus", 1},
                    { "Bonus_add", 1}
                } },
            };
        }
    }
}
