import GameLogic from "../Game/GameLogic.js";

class Displays {
    gameState
    user

    constructor(gameState, user) {
        this.gameState = gameState;
        this.user = user;

        window.addEventListener("mining.html", this.MiningLoaded.bind(this));
        window.addEventListener("login.html", this.LoginLoaded.bind(this));
        window.addEventListener("registration.html", this.RegistrationLoaded.bind(this))
        window.addEventListener("tharni.html", this.RemoveAllListeners.bind(this));
        window.addEventListener("castar.html", this.RemoveAllListeners.bind(this));
        window.addEventListener("silmaril.html", this.RemoveAllListeners.bind(this));
        window.addEventListener("help.html", this.RemoveAllListeners.bind(this));
        window.addEventListener("settings.html", this.SettingsLoaded.bind(this));
        window.addEventListener("credits.html", this.RemoveAllListeners.bind(this));
    }

    RemoveNavBar() {
        document.getElementById("Nav_Bar").hidden = true;
        document.getElementById("Nav_Bar").classList.add("Hidden");
        document.getElementById("App").classList.add("Fullscreen");
    }

    AddNavBar() {
        document.getElementById("Nav_Bar").hidden = false;
        document.getElementById("Nav_Bar").classList.remove("Hidden");
        document.getElementById("App").classList.remove("Fullscreen");
    }

    RemoveAllListeners() {
        window.removeEventListener("UpdateSpDisplay", this.UpdateSpDisplay.bind(this));
    }

    MiningLoaded() {
        this.AddNavBar();
        this.RemoveAllListeners();
        window.addEventListener("UpdateSpDisplay", this.UpdateSpDisplay.bind(this));
        this.UpdateSpDisplay();
    }

    SettingsLoaded() {
        this.RemoveAllListeners();
        this.LoadGeneralSettings();
        document.getElementById("Settings_General").addEventListener("click", this.LoadGeneralSettings.bind(this))
        document.getElementById("Settings_Account").addEventListener("click", this.LoadAccountSettings.bind(this))
        document.getElementById("Settings_Parental").addEventListener("click", this.LoadParentSettings.bind(this))
    }

    LoginLoaded() {
        this.RemoveNavBar();
    }

    RegistrationLoaded() {
        this.RemoveNavBar();
    }

    LoadGeneralSettings() {
        this.HideSettingsPages();
        document.getElementById("General_Page").hidden = false;
        document.getElementById("General_Page").classList.remove("Hidden");
    }

    LoadAccountSettings() {
        this.HideSettingsPages();
        document.getElementById("Account_Page").hidden = false;
        document.getElementById("Account_Page").classList.remove("Hidden");
    }

    LoadParentSettings() {
        this.HideSettingsPages();
        document.getElementById("Parent_Page").hidden = false;
        document.getElementById("Parent_Page").classList.remove("Hidden");
        this.SetTimeOptions();
        document.getElementById("Hour_Select").addEventListener("change", this.SetTimeLimitBtn.bind(this));
        document.getElementById("Min_Select").addEventListener("change", this.SetTimeLimitBtn.bind(this));
        document.getElementById("Sec_Select").addEventListener("change", this.SetTimeLimitBtn.bind(this));
    }

    SetTimeOptions() {
        for (let i = 0; i <= 23; i++) {
            document.querySelector("#Hour_Select").add(new Option(i, i), undefined);
        }
        for (let i = 0; i <= 59; i++) {
            document.querySelector("#Min_Select").add(new Option(i, i), undefined);
            document.querySelector('#Sec_Select').add(new Option(i, i), undefined);
        }
        const times = this.user.connectedTimeLimit.split(":");

        document.getElementById("Hour_Select").value = parseInt(times[0]);
        document.getElementById("Min_Select").value = parseInt(times[1]);
        document.getElementById("Sec_Select").value = parseInt(times[2]);
        this.SetTimeLimitBtn();
    }

    SetTimeLimitBtn() {
        const hour = document.getElementById("Hour_Select").value;
        const min = document.getElementById("Min_Select").value;
        const sec = document.getElementById("Sec_Select").value;
        const text = `${hour}:${min}:${sec}`;
        document.getElementById("Time_Span").innerText = text;
    }

    HideSettingsPages() {
        document.getElementById("General_Page").hidden = true;
        document.getElementById("General_Page").classList.add("Hidden");
        document.getElementById("Account_Page").hidden = true;
        document.getElementById("Account_Page").classList.add("Hidden");
        document.getElementById("Parent_Page").hidden = true;
        document.getElementById("Parent_Page").classList.add("Hidden");
    }

    UpdateSpDisplay() {
        document.getElementById("Sp_Display").innerText = this.gameState.sp;
    }
}

export default Displays;