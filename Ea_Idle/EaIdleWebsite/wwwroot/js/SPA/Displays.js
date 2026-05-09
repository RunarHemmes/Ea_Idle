import GameLogic from "../Game/GameLogic.js";

class Displays {
    gameState
    //app

    constructor(gameState) {
        this.gameState = gameState;
        //this.app = app;

        window.addEventListener("mining.html", this.MiningLoaded.bind(this));
        window.addEventListener("login.html", this.LoginLoaded.bind(this));
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
        document.getElementById("Settings_General").addEventListener("click", this.LoadGeneralSettings.bind(this))
        document.getElementById("Settings_Account").addEventListener("click", this.LoadAccountSettings.bind(this))
        document.getElementById("Settings_Parental").addEventListener("click", this.LoadParentSettings.bind(this))
        this.LoadGeneralSettings();
    }

    LoginLoaded() {
        this.RemoveNavBar();
        //document.getElementById("Login_Btn").addEventListener("click", app.Login.bind(app))
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