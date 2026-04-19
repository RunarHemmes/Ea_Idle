class GameLogic {
    sp;
    spGain;
    spDisplay;
    spTime;
    //app;
    constructor() {
        //this.app = app;
        this.sp = 0;
        this.spGain = 1;
        this.spDisplay = document.getElementById("sp_Display");
        //this.spDisplay = 
        this.spTime = 5000;
        //this.updateSpDisplay();
        window.dispatchEvent(new CustomEvent("UpdateSP"));
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
        //this.updateSpDisplay();
        window.dispatchEvent(new CustomEvent("UpdateSP"));
        this.mainLoop();
    }
}

export default GameLogic;