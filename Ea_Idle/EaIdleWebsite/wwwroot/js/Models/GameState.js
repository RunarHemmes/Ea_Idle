class GameState {
    sp
    spGain
    spCooldown
    lastSpTime
    tickSpeed

    constructor(tickSpeed = 1000, sp = 0, spGain = 1, spCooldown = 5000) {
        this.tickSpeed = tickSpeed;
        this.sp = sp;
        this.spGain = spGain;
        this.spCooldown = spCooldown;
        this.lastSpTime = Date.now();
    }
}

export default GameState;