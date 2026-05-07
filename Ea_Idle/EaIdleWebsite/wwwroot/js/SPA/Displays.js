import GameLogic from "../Game/GameLogic.js";

class Displays {
    gameState
    //app

    constructor(gameState) {
        this.gameState = gameState;
        //this.app = app;

        window.addEventListener("mining.html", this.MiningLoaded.bind(this));
        window.addEventListener("login.html", this.LoginLoaded.bind(this));
    }

    RemoveNavBar() {
        document.getElementById("Nav_Bar").hidden = true;
        document.getElementById("Nav_Bar").classList.add("Hidden");
        document.getElementById("App").classList.add("Fullscreen");
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

    LoginLoaded() {
        this.RemoveNavBar();
        //document.getElementById("Login_Btn").addEventListener("click", app.Login.bind(app))
    }

    UpdateSpDisplay() {
        document.getElementById("Sp_Display").innerText = this.gameState.sp;
    }
}

export default Displays;