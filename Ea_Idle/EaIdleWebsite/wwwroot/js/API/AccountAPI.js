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
        debugger;
        this.user = data.user;

        sessionStorage.setItem("token", data.token);
            //.then(response => response.json())s
            //.then(data => {
            //    console.log(data);
            //    sessionStorage.setItem("token", data.token)
            //})
            //.catch(error => console.error(error));
        debugger;
    }
}

export default AccountAPI;