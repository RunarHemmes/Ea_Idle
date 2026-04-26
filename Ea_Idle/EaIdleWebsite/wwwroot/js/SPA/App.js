import Router from './Router.js';
import Displays from './Displays.js';
import GameLogic from '../Game/GameLogic.js';
import GameState from '../Models/GameState.js';
import AccountAPI from '../API/AccountAPI.js';
import ProgressAPI from '../API/ProgressAPI.js';
import Progress from '../Models/Progress.js';

class App {
    router
    gameLogic
    gameState
    displays
    accountAPI
    progressAPI
     
    constructor() {
        this.accountAPI = new AccountAPI();
        this.progressAPI = new ProgressAPI();
        this.router = new Router();
        this.gameState = new GameState();
        this.gameLogic = new GameLogic(this.gameState, this.progressAPI);
        this.displays = new Displays(this.gameState);

        this.router.init();
        this.LogIn();
    }

    async LogIn() {
        await this.accountAPI.Login();
        await this.LoadProgress();
    }

    async LoadProgress() {
        window.dispatchEvent(new CustomEvent("UpdateSpDisplay"));
        const progress = await this.progressAPI.GetProgress();
        this.gameState.ImportProgress(progress);
        this.gameLogic.StartGame();
    }
}

const app = new App();
