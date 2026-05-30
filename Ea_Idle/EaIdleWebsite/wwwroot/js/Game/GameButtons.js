class GameButtons {
    logic

    constructor(gameLogic) {
        this.logic = gameLogic;

        window.addEventListener("mining.html", this.MiningLoaded.bind(this));
    }

    MiningLoaded() {
        document.getElementById("MU_Equipment").addEventListener("click", this.logic.EquipmentBought.bind(this.logic));
        document.getElementById("MU_Miners").addEventListener("click", this.logic.MinerBought.bind(this.logic));
        document.getElementById("MU_Purity").addEventListener("click", this.logic.OrePurityBought.bind(this.logic));
        document.getElementById("MU_Price").addEventListener("click", this.logic.OrePriceBought.bind(this.logic));
    }


}

export default GameButtons;