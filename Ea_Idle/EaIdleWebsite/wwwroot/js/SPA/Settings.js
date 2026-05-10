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
        document.getElementById("Child_Span").innerText = this.user.connectedName;
        document.getElementById("Child_Lbl").innerText = this.user.connectedName;
        if (this.user.role != "Parent") {
            document.getElementById("Child_Sect").classList.add("Hidden");
            document.getElementById("Limit_Sect").classList.add("Hidden");
        }
        this.SetCurrentLimit();
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