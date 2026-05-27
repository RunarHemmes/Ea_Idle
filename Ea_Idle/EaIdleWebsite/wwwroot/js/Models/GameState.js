import Progress from './Progress.js';

class GameState {
    sp
    spGain
    spCooldown
    lastSpTime
    tickSpeed
    saveCooldown
    lastSaveTime
    mu

    constructor(tickSpeed = 1000, saveCooldown = 3000, spGain = 1, spCooldown = 10000) {
        this.tickSpeed = tickSpeed;
        this.spGain = spGain;
        this.spCooldown = spCooldown;
        this.saveCooldown = saveCooldown;
        this.lastSpTime = Date.now();
        this.lastSaveTime = Date.now();
    }

    ImportProgress(progress) {
        this.sp = progress.silverPennies;
        this.mu = progress.miningUpgrades;
    }

    ExportProgress() {
        const progress = new Progress(this.sp, this.mu);
        return progress;
    }
}

export default GameState;