import GameLogic from "../Game/GameLogic.js";

class Displays {
    gameState

    constructor(gameState) {
        this.gameState = gameState;

        window.addEventListener("mining.html", this.MiningLoaded.bind(this));
        window.addEventListener("login.html", this.RemoveNavBar.bind(this));
    }

    RemoveNavBar() {
        document.getElementById("Nav_Bar").hidden = true;
        document.getElementById("Nav_Bar").classList.add("Hidden");
        document.getElementById("App").classList.add("Fullscreen");
        document.getElementById("App").id
    }

    AddNavBar() {
        document.getElementById("Nav_Bar").hidden = false;
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
        document.getElementById("Sp_Display").innerText = this.gameState.sp;
    }
}

export default Displays;