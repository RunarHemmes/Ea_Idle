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

    constructor(tickSpeed = 50, saveCooldown = 30000, sp = 0, spGain = 1, spCooldown = 5000) {
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
        const copy = JSON.parse(JSON.stringify(this));
        const progress = new Progress(copy.sp, copy.mu);
        return progress;
    }
}

export default GameState;