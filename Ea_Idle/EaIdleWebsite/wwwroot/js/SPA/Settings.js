class Settings {
    accountAPI
    user

    constructor(accountAPI, user) {
        this.accountAPI = accountAPI;
        this.user = user;
        window.addEventListener("settings.html", this.SettingsLoaded.bind(this));
    }

    SettingsLoaded() {
        document.getElementById("Limit_Btn").addEventListener("click", this.setTimeLimit.bind(this));
        document.getElementById("Connect_Btn").addEventListener("click", this.TryToConnect.bind(this));
        document.getElementById("Child_Span").innerText = this.user.connectedName;
        document.getElementById("Child_Lbl").innerText = this.user.connectedName;
        if (this.user.role != "Parent") {
            document.getElementById("Child_Sect").classList.add("Hidden");
            document.getElementById("Limit_Sect").classList.add("Hidden");
        }
        this.SetCurrentLimit();
    }

    TryToConnect() {
        const strCode = document.getElementById("Code_Input").value;
        const code = parseInt(strCode);
        const setResult = this.accountAPI.SetConnection(code);
        if (setResult != "Ok") {
            document.getElementById("Connect_Error").innerText = setResult;
            return;
        }
        const getResult = this.accountAPI.GetConnection();
        if (getResult == null) {
            window.alert(`Succesfully connected with ${this.user.connectedName}.`);
        } else if (getResult == "The connection for this account is still pending.") {
            window.alert("The connection is now pending.");
        }
        document.getElementById("Connect_Error").innerText = getResult;
        return;
    }

    async setTimeLimit() {
        const hour = document.getElementById("Hour_Select").value;
        const min = document.getElementById("Min_Select").value;
        const sec = document.getElementById("Sec_Select").value;
        const result =  await this.accountAPI.SetTimeLimit(hour, min, sec);
        if (result == "Ok") {
            window.alert(`Succesfully set the timelimit for ${this.user.connectedName} to ${hour}:${min}:${sec}`);
        } else {
            window.alert(result);    
        }
    }
}

export default Settings;