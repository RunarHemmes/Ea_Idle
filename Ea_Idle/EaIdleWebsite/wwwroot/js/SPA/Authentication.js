class Authenticator {
    accountAPI
    btn
    passwordField
    usernameField
    errorLbl

    constructor(accountAPI) {
        this.accountAPI = accountAPI;
        window.addEventListener("login.html", this.LoginLoaded.bind(this));
    }

    LoginLoaded() {
        this.btn = document.getElementById("Login_Btn");
        this.passwordField = document.getElementById("Password_Input");
        this.usernameField = document.getElementById("Username_Input");
        this.errorLbl = document.getElementById("Login_Error");

        this.btn.addEventListener("click", (e) => {
            e.preventDefault();
            this.Login();
        })
    }

    async Login() {
        
        const username = this.usernameField.value;
        const password = this.passwordField.value;
        let inputStatus = this.ValidateInput(username, password);

        if (inputStatus != "Ok") {
            this.errorLbl.innerText = inputStatus;
            return;
        }
        let result = await this.accountAPI.Login(username, password);
        if (result != null) {
            this.errorLbl.innerText = result;
        }
    }

    ValidateInput(name, pass) {
        if (name == "" || pass == "") {
            return "Please fill in all fields";
        }
        return "Ok";
    }
}

export default Authenticator;