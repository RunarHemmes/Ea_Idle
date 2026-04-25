import ProgressAPI from '../API/ProgressAPI.js'
import Progress from '../Models/Progress.js'

class GameLogic {
    sp;
    spGain;
    spDisplay;
    spTime;
    api;
    constructor() {
        this.sp = 0;
        this.spGain = 1;
        this.spDisplay = document.getElementById("sp_Display");
        this.spTime = 5000;

        window.addEventListener("tokenReceived", this.setupAPI.bind(this))
        setTimeout(this.saveGame.bind(this), 30000);
        window.dispatchEvent(new CustomEvent("UpdateSP"));
    }

    setupAPI() {
        this.api = new ProgressAPI();
        this.retrieveSave();
    }

    async retrieveSave() {
        const progress = await this.api.GetProgress();
        this.sp = progress.silverPennies;
        window.dispatchEvent(new CustomEvent("UpdateSP"));
    }

    async saveGame() {
        const progress = await this.api.saveProgress(new Progress(this.sp));
    }

    mainLoop() {
        setTimeout(this.gainSP.bind(this), this.spTime);
        window.addEventListener("UpdateSP", this.updateSpDisplay.bind(this));
    }

    updateSpDisplay() {
        this.spDisplay.textContent = this.sp;
    }

    gainSP() {
        this.sp += this.spGain;
        window.dispatchEvent(new CustomEvent("UpdateSP"));
        this.mainLoop();
    }
}

//export default GameLogic;