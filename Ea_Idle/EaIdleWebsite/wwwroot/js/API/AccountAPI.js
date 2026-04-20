class AccountAPI {
    constructor() {

    }

    async Login() {
        const response = await fetch("https://localhost:3000/api/Account/Login", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                Username: "Harold",
                Password: "passwordHarold"
            })

        })
            .then(response => response.json())
            .then(data => {
                console.log(data);
                sessionStorage.setItem("token", data.token)
            })
            .catch(error => console.error(error));
        console.log(response);
        window.dispatchEvent(new CustomEvent("tokenReceived"));
    }
}

export default AccountAPI;