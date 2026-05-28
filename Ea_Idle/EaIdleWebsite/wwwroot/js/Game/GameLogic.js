class GameLogic {
    gameState
    tickInterval
    progressAPI
    constructor(gameState, progressAPI) {
        this.gameState = gameState;
        this.progressAPI = progressAPI;
    }

    // The main methods, for the main workings of the game:
    StartGame() {
        this.UpdateStats();
        this.tickInterval = setInterval(this.MainGameLoop.bind(this), this.gameState.tickSpeed);
    }

    StopGame() {
        clearInterval(this.tickInterval);
    }

    MainGameLoop() {
        if (Date.now() - this.gameState.lastSpTime >= this.gameState.spCooldown) {
            this.GainSp();
        }
        if (Date.now() - this.gameState.lastSaveTime >= this.gameState.saveCooldown) {
            this.SaveGame();
        }
    }

    async SaveGame() {
        this.gameState.lastSaveTime = Date.now();
        await this.progressAPI.saveProgress(this.gameState.ExportProgress());
        //window.alert("Your game has been saved.");
    }

    UpdateStats() {
        this.gameState.spCooldown = this.gameState.mu.Equipment.Current_Bonus * 1000;
        this.gameState.spGain = this.gameState.mu.Ore_Price.Current_Bonus;
    }


    // The methods for gaining all kinds of points:
    GainSp() {
        this.gameState.lastSpTime = Date.now();
        this.gameState.sp += this.gameState.spGain;
        window.dispatchEvent(new CustomEvent("UpdateSpDisplay"));
    }


    // The methods for dealing with all upgrades:
    EquipmentBought() {
        if (this.gameState.sp < this.gameState.mu.Equipment.Price) {
            return;
        }
        this.gameState.sp -= this.gameState.mu.Equipment.Price;
        this.gameState.mu.Equipment.Lvl += 1;
        this.gameState.mu.Equipment.Price *= 1.2;
        this.gameState.mu.Equipment.Price = parseFloat(this.gameState.mu.Equipment.Price.toFixed(0));
        var newSpeed = this.gameState.mu.Equipment.Current_Bonus * this.gameState.mu.Equipment.Bonus_mult;
        newSpeed = parseFloat(newSpeed.toFixed(2));
        this.gameState.mu.Equipment.Current_Bonus = newSpeed;
        this.gameState.mu.Miners_Count.Current_Bonus = newSpeed;
        this.UpdateStats();
        this.gameState.mu.Equipment.Extra = (this.gameState.mu.Equipment.Current_Bonus * this.gameState.mu.Equipment.Bonus_mult) - this.gameState.mu.Equipment.Current_Bonus;
        this.gameState.mu.Miners_Count.Extra = (this.gameState.mu.Miners_Count.Current_Bonus * this.gameState.mu.Miners_Count.Bonus_mult) - this.gameState.mu.Miners_Count.Current_Bonus;
        this.gameState.mu.Equipment.Extra = parseFloat(this.gameState.mu.Equipment.Extra.toFixed(2));
        this.gameState.mu.Miners_Count.Extra = parseFloat(this.gameState.mu.Miners_Count.Extra.toFixed(2));
        window.dispatchEvent(new CustomEvent("MU_Equipment_Bought"));
        window.dispatchEvent(new CustomEvent("MU_Miners_Bought"));
        window.dispatchEvent(new CustomEvent("UpdateSpDisplay"));
    }

    MinerBought() {
        if (this.gameState.sp < this.gameState.mu.Miners_Count.Price) {
            return;
        }
        this.gameState.sp -= this.gameState.mu.Miners_Count.Price;
        this.gameState.mu.Miners_Count.Lvl += 1;
        this.gameState.mu.Miners_Count.Price *= 1.8;
        this.gameState.mu.Miners_Count.Price = parseFloat(this.gameState.mu.Miners_Count.Price.toFixed(0));
        var newSpeed = this.gameState.mu.Miners_Count.Current_Bonus * this.gameState.mu.Miners_Count.Bonus_mult;
        newSpeed = parseFloat(newSpeed.toFixed(2));
        this.gameState.mu.Miners_Count.Current_Bonus = newSpeed;
        this.gameState.mu.Equipment.Current_Bonus = newSpeed;
        this.UpdateStats();
        this.gameState.mu.Equipment.Extra = (this.gameState.mu.Equipment.Current_Bonus * this.gameState.mu.Equipment.Bonus_mult) - this.gameState.mu.Equipment.Current_Bonus;
        this.gameState.mu.Miners_Count.Extra = (this.gameState.mu.Miners_Count.Current_Bonus * this.gameState.mu.Miners_Count.Bonus_mult) - this.gameState.mu.Miners_Count.Current_Bonus;
        this.gameState.mu.Equipment.Extra = parseFloat(this.gameState.mu.Equipment.Extra.toFixed(2));
        this.gameState.mu.Miners_Count.Extra = parseFloat(this.gameState.mu.Miners_Count.Extra.toFixed(2));
        window.dispatchEvent(new CustomEvent("MU_Miners_Bought"));
        window.dispatchEvent(new CustomEvent("MU_Equipment_Bought"));
        window.dispatchEvent(new CustomEvent("UpdateSpDisplay"));
    }

    OrePriceBought() {
        if (this.gameState.sp < this.gameState.mu.Ore_Price.Price) {
            return;
        }
        this.gameState.sp -= this.gameState.mu.Ore_Price.Price;
        this.gameState.mu.Ore_Price.Lvl += 1;
        this.gameState.mu.Ore_Price.Price *= 1.2;
        this.gameState.mu.Ore_Price.Price = parseFloat(this.gameState.mu.Ore_Price.Price.toFixed(0));
        var newGain = this.gameState.mu.Ore_Price.Current_Bonus + this.gameState.mu.Ore_Price.Bonus_add;
        newGain = parseFloat(newGain.toFixed(2));
        this.gameState.mu.Ore_Price.Current_Bonus = newGain;
        this.gameState.mu.Ore_Purity.Current_Bonus = newGain;
        this.UpdateStats();
        this.gameState.mu.Ore_Purity.Extra = (this.gameState.mu.Ore_Purity.Current_Bonus + this.gameState.mu.Ore_Purity.Bonus_add) - this.gameState.mu.Ore_Purity.Current_Bonus;
        this.gameState.mu.Ore_Price.Extra = (this.gameState.mu.Ore_Price.Current_Bonus + this.gameState.mu.Ore_Price.Bonus_add) - this.gameState.mu.Ore_Price.Current_Bonus;
        window.dispatchEvent(new CustomEvent("MU_Price_Bought"));
        window.dispatchEvent(new CustomEvent("MU_Purity_Bought"));
        window.dispatchEvent(new CustomEvent("UpdateSpDisplay"));
    }

    OrePurityBought() {
        if (this.gameState.sp < this.gameState.mu.Ore_Purity.Price) {
            return;
        }
        this.gameState.sp -= this.gameState.mu.Ore_Purity.Price;
        this.gameState.mu.Ore_Purity.Lvl += 1;
        this.gameState.mu.Ore_Purity.Price *= 1.2;
        this.gameState.mu.Ore_Purity.Price = parseFloat(this.gameState.mu.Ore_Purity.Price.toFixed(0));
        var newGain = this.gameState.mu.Ore_Purity.Current_Bonus + this.gameState.mu.Ore_Purity.Bonus_add;
        newGain = parseFloat(newGain.toFixed(2));
        this.gameState.mu.Ore_Purity.Current_Bonus = newGain;
        this.gameState.mu.Ore_Price.Current_Bonus = newGain;
        this.UpdateStats();
        this.gameState.mu.Ore_Purity.Extra = (this.gameState.mu.Ore_Purity.Current_Bonus + this.gameState.mu.Ore_Purity.Bonus_add) - this.gameState.mu.Ore_Purity.Current_Bonus;
        this.gameState.mu.Ore_Price.Extra = (this.gameState.mu.Ore_Price.Current_Bonus + this.gameState.mu.Ore_Price.Bonus_add) - this.gameState.mu.Ore_Price.Current_Bonus;
        window.dispatchEvent(new CustomEvent("MU_Purity_Bought"));
        window.dispatchEvent(new CustomEvent("MU_Price_Bought"));
        window.dispatchEvent(new CustomEvent("UpdateSpDisplay"));
    }
}

export default GameLogic;