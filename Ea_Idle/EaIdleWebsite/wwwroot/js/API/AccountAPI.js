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
                Password: password
            })
        });
        const data = await response.json();
        console.log(data);
        if (!response.ok) {
            return data.errMsg;
        }
        this.user.name = data.user.username;

        sessionStorage.setItem("token", data.token);
        return null;
    }
}

export default AccountAPI;