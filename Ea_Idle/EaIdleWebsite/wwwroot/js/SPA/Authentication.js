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

        this.btn.addEventListener("click", this.Login.bind(this));
    }

    Login() {
        let inputStatus = this.ValidateInput();
        if (inputStatus != "Ok") {
            this.errorLbl.innerText = inputStatus;
        }
        await this.accountAPI.Login();
    }

    ValidateInput() {

    }
}