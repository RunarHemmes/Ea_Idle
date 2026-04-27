class GameLogic {
    gameState
    tickInterval
    progressAPI
    constructor(gameState, progressAPI) {
        this.gameState = gameState;
        this.progressAPI = progressAPI;
    }

    StartGame() {
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
        window.alert("Your game has been saved.");
    }

    GainSp() {
        this.gameState.lastSpTime = Date.now();
        this.gameState.sp += this.gameState.spGain;
        window.dispatchEvent(new CustomEvent("UpdateSpDisplay"));
    }
}

export default GameLogic;