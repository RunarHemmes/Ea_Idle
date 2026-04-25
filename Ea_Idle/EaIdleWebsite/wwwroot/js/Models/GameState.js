import Progress from './Progress.js';

class GameState {
    sp
    spGain
    spCooldown
    lastSpTime
    tickSpeed
    saveCooldown
    lastSaveTime

    constructor(tickSpeed = 1000, saveCooldown = 30000, sp = 0, spGain = 1, spCooldown = 5000) {
        this.tickSpeed = tickSpeed;
        this.sp = sp;
        this.spGain = spGain;
        this.spCooldown = spCooldown;
        this.saveCooldown = saveCooldown;
        this.lastSpTime = Date.now();
        this.lastSaveTime = Date.now();

    }

    ImportProgress(progress) {
        this.sp = progress.silverPennies;
    }

    ExportProgress() {
        const progress = new Progress(this.sp);
        return progress;
    }
}

export default GameState;