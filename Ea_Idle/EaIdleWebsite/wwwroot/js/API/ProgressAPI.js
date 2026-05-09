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
        const spAmount = parseInt(data.SilverPennies);
        const progress = new Progress(spAmount);
        progress;
        return progress;
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
                Id: 1,
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