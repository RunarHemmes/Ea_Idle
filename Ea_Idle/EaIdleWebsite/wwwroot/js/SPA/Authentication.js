class Authenticator {
    accountAPI
    btn
    passwordField
    usernameField
    errorLbl
    emailField
    confirmField
    roleSelect

    constructor(accountAPI) {
        this.accountAPI = accountAPI;
        window.addEventListener("login.html", this.LoginLoaded.bind(this));
        window.addEventListener("registration.html", this.RegistrationLoaded.bind(this));
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
        const inputStatus = this.ValidateLoginInput(username, password);

        if (inputStatus != "Ok") {
            this.errorLbl.innerText = inputStatus;
            return;
        }
        let result = await this.accountAPI.Login(username, password);
        if (result != null) {
            this.errorLbl.innerText = result;
        }
        window.dispatchEvent(new CustomEvent("LoggedIn"));
    }

    ValidateLoginInput(name, pass) {
        if (name == "" || pass == "") {
            return "Please fill in all fields";
        }
        return "Ok"; 
    }

    RegistrationLoaded() {
        this.btn = document.getElementById("Registration_Btn");
        this.usernameField = document.getElementById("Username_Input");
        this.emailField = document.getElementById("Email_Input");
        this.passwordField = document.getElementById("Password_Input");
        this.confirmField = document.getElementById("Confirm_Input");
        this.roleSelect = document.getElementById("Role_Select");
        this.errorLbl = document.getElementById("Registration_Error");

        this.btn.addEventListener("click", (e) => {
            e.preventDefault();
            this.Register();
        })
    }

    async Register() {
        const username = this.usernameField.value;
        const email = this.emailField.value;
        const password = this.passwordField.value;
        const confirm = this.confirmField.value;
        const role = this.roleSelect.value;
        const inputStatus = this.ValidateRegisterInput(username, email, password, confirm);

        if (inputStatus != "Ok") {
            this.errorLbl.innerText = inputStatus;
        }
        
    }

    ValidateRegisterInput(name, mail, pass, confirm) {
        if (name == "" || mail == "" || pass == "" || confirm == "") {
            return "Please fill in all fields";
        }
        if (7 < pass.lenght < 16) {
            return "Your password is the wrong length, it should be 8-5 characters";
        }
        if (pass != confirm) {
            return "The passwords are not the same";
        }
        let mailCheck = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
        if (mailCheck.test(mail)) {
            return "This email is not valid, please check for mistakes";
        }
        return "Ok";
    }
}

export default Authenticator;