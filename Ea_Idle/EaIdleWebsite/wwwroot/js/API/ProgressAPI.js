import Progress from '../Models/Progress.js'

class ProgressAPI {
    constructor() {

    }

    async GetProgress() {
        const token = sessionStorage.token;
        
        const response = await fetch("https://localhost:3000/api/GameProgress/Get1", {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            }
        });

        console.log(response);

        if (!response.ok) {
            if (response.status == 400) {
                const newSave = this.NewProgress();
                return newSave;
            }
            return null;
        }
        const data = await response.json();
        const spAmount = parseInt(data.silverPennies);
        const progress = new Progress(spAmount);
        return progress;
    }

    async NewProgress() {
        const token = sessionStorage.token;

        const response = await fetch("https://localhost:3000/api/GameProgress/NewSave1", {
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
        const spAmount = parseInt(data.SilverPennies);
        progress = new Progress(spAmount);
        progress;
        return progress;

    }

    async saveProgress(progress) {
        const token = sessionStorage.token;

        const response = await fetch("https://localhost:3000/api/GameProgress/Update1", {
            method: "PUT",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                Id: 1,
                AccountId: 1,
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