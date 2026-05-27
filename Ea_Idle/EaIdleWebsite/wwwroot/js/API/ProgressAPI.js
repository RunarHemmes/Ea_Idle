import Progress from '../Models/Progress.js'

class ProgressAPI {
    user;

    constructor(user) {
        this.user = user;
    }

    async GetProgress() {
        const token = sessionStorage.token;
        
        const response = await fetch(`https://localhost:3000/api/GameProgress/Get${this.user.id}`, {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            }
        });

        console.log(response);

        if (!response.ok) {
            if (response.status == 400) {
                var data = await this.NewProgress();
                // return newSave;
            } else {
            return null;
            }
        } else {
        var data = await response.json();
        }

        let spAmount = parseInt(data.silverPennies);
        if (isNaN(spAmount)) {
            spAmount = 0;
        }
        var mu = data.miningUpgrades;
        mu.Equipment.Bonus_mult /= 100;
        mu.Miners_Count.Bonus_mult /= 100;
        mu.Equipment.Extra = (mu.Equipment.Current_Bonus * mu.Equipment.Bonus_mult) - mu.Equipment.Current_Bonus;
        mu.Miners_Count.Extra = (mu.Miners_Count.Current_Bonus * mu.Miners_Count.Bonus_mult) - mu.Miners_Count.Current_Bonus;
        mu.Ore_Purity.Extra = (mu.Ore_Purity.Current_Bonus + mu.Ore_Purity.Bonus_add) - mu.Ore_Purity.Current_Bonus;
        mu.Ore_Price.Extra = (mu.Ore_Price.Current_Bonus + mu.Ore_Price.Bonus_add) - mu.Ore_Price.Current_Bonus;
        mu.Equipment.Extra = parseFloat(mu.Equipment.Extra.toFixed(2)); 
        mu.Miners_Count.Extra = parseFloat(mu.Miners_Count.Extra.toFixed(2));
        // mu.Ore_Price.Extra = parseInt(mu.Ore_Price.Extra.toFixed(2));
        // mu.Ore_Purity.Extra = parseInt(mu.Ore_Purity.Extra.toFixed(2));



        const progress = new Progress(spAmount, mu);
        return progress;
    }

    async NewProgress() {
        const token = sessionStorage.token;

        const response = await fetch(`https://localhost:3000/api/GameProgress/NewSave${this.user.id}`, {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            }
        });

        console.log(response);

        if (!response.ok) {
            return null;
        }
        const data = await response.json();
        return data;
    }

    async saveProgress(progress) {
        const token = sessionStorage.token;

        const response = await fetch(`https://localhost:3000/api/GameProgress/Update${this.user.id}`, {
            method: "PUT",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                //Id: 1,
                AccountId: this.user.id,
                SilverPennies: `${progress.silverPennies}`
            })
        });

        console.log(response);

        if (!response.ok) {
            return null;
        }
        const data = await response.json();
        const spAmount = parseInt(data.silverPennies);
        const newProgress = new Progress(spAmount);
        return newProgress;
    }
}

export default ProgressAPI;