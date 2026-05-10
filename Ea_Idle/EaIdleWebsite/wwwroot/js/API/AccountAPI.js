class AccountAPI {
    user
    constructor(user) {
        this.user = user;
    }

    async Login(username, password) {
        const response = await fetch("https://localhost:3000/api/Account/Login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                Username: username,
                Role: "Player",
                Password: password
            })
        });
        const data = await response.json();
        console.log(data);
        if (!response.ok) {
            return data.errMsg;
        }
        this.user.name = data.user.username;
        this.user.id = data.user.id;
        this.user.role = data.user.role;

        sessionStorage.setItem("token", data.token);
        return null;
    }

    async SetTimeLimit(hour, min, sec) {
         const token = sessionStorage.token;
        const response = await fetch(`https://localhost:3000/api/Account/SetTimeLimit${this.user.id}-${hour}:${min}:${sec}`, {
             method: "PATCH",
             headers: {
                 "Authorization": `Bearer ${token}`,
                 "Content-Type": "application/json"
             },
             //body: JSON.stringify({
             //    timeLimit: `${hour}:${min}:${sec}`
             //})
         });
         const data = await response.json();
         console.log(data);
         if (!response.ok) {
             return data.errMsg;
         }
         return "Ok";
    }

    async GetConnection() {
        const token = sessionStorage.token;
        const response = await fetch(`https://localhost:3000/api/Account/GetConnect${this.user.id}`, {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            }
        });
        const data = await response.json();
        console.log(data);
        if (!response.ok) {
            return data.errMsg;
        }
        if (this.user.role == "Parent") {
            this.user.connectedName = data.childName;
            this.user.connectedId = data.childId;
        } else {
            this.user.connectedName = data.parentName;
            this.user.connectedId = data.parentId;
        }
        this.user.connectedTimeLimit = data.timeLimit;
        return null;
    }
}

export default AccountAPI;