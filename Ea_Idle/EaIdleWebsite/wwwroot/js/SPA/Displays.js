import GameLogic from "../Game/GameLogic.js";

class Displays {
    gameState

    constructor(gameState) {
        this.gameState = gameState;

        window.addEventListener("mining.html", this.MiningLoaded.bind(this));
    }

    RemoveAllListeners() {
        window.removeEventListener("UpdateSpDisplay", this.UpdateSpDisplay.bind(this));
    }

    MiningLoaded() {
        this.RemoveAllListeners();
        window.addEventListener("UpdateSpDisplay", this.UpdateSpDisplay.bind(this));
        this.UpdateSpDisplay();
    }

    UpdateSpDisplay() {
        document.getElementById("sp_Display").innerText = this.gameState.sp;
    }
}

export default Displays;