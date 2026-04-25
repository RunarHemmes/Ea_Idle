import GameLogic from "../Game/GameLogic";

export class Displays {
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