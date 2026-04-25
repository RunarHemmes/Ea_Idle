class GameLogic {
    gameState
    tickInterval
    constructor(gameState) {
        this.gameState = gameState;
        
    }

    StartGame() {
        this.tickInterval = setInterval(this.MainGameLoop.bind(this), this.gameState.tickSpeed);
    }

    StopGame() {
        clearInterval(this.tickInterval);
    }

    MainGameLoop() {
        const now = Date.now();
        const n = now - this.gameState.lastSpTime;
        if (n >= this.gameState.spCooldown) {
            this.GainSp();
        }

        if (Date.now() - this.gameState.lastSpTime >= this.gameState.spCooldown) {
            this.GainSp();
        }
    }

    GainSp() {
        this.gameState.lastSpTime = Date.now();
        this.gameState.sp += this.gameState.spGain;
        window.dispatchEvent(new CustomEvent("UpdateSpDisplay"));
    }
}

export default GameLogic;