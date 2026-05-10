import Router from './Router.js';
import Displays from './Displays.js';
import GameLogic from '../Game/GameLogic.js';
import GameState from '../Models/GameState.js';
import AccountAPI from '../API/AccountAPI.js';
import ProgressAPI from '../API/ProgressAPI.js';
import Progress from '../Models/Progress.js';
import Authenticator from '../SPA/Authentication.js';
import User from '../Models/User.js';
import Settings from '../SPA/Settings.js';

class App {
    router
    gameLogic
    gameState
    displays
    accountAPI
    progressAPI
    authenticator
    user
    settings
     
    constructor() {
        this.user = new User(null, null);
        this.accountAPI = new AccountAPI(this.user);
        this.progressAPI = new ProgressAPI(this.user);
        this.authenticator = new Authenticator(this.accountAPI);
        this.settings = new Settings(this.accountAPI, this.user);
        this.router = new Router(this.user);
        this.gameState = new GameState();
        this.gameLogic = new GameLogic(this.gameState, this.progressAPI);
        this.displays = new Displays(this.gameState);

        this.router.Init();
        //this.LogIn();
        window.addEventListener("LoggedIn", this.LoadProgress.bind(this));
    }

    //async LogIn() {
    //    await this.accountAPI.Login();
    //    await this.LoadProgress();
    //}

    async LoadProgress() {
        await this.accountAPI.GetConnection();
        window.dispatchEvent(new CustomEvent("UpdateSpDisplay"));
        const progress = await this.progressAPI.GetProgress();
        this.gameState.ImportProgress(progress);
        this.router.NavTo("/Mining");
        this.gameLogic.StartGame();
    }
}

const app = new App();
