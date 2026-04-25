import GameLogic from "../Game/GameLogic.js";

class Displays {
    gamelogic

    constructor(gameLogic) {
        this.gameLogic = gameLogic;

        window.addEventListener("MiningPageLoaded", this.MiningLoaded);
    }

    RemoveAllListeners() {
        window.removeEventListener("UpdateSpDisplay", this.gameLogic.UpdateSpDisplay);
    }

    MiningLoaded() {
        this.RemoveAllListeners();
        window.addEventListener("UpdateSpDisplay", this.gameLogic.UpdateSpDisplay);
    }


}

export default Displays;