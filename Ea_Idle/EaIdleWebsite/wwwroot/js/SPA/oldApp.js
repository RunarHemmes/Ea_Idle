import Router from './Router.js'
import GameLogic from '../Game/Logic.js'
import AccountAPI from '../API/AccountAPI.js'

class App {
    #rootEl;
    router;
    game;
    accountApi;
    constructor() {
        this.#rootEl = document.getElementById('app');
        this.accountApi = new AccountAPI();
        this.accountApi.Login();

        this.router = new Router;
        this.router.init();
        
        window.addEventListener("miningLoaded", this.setupGame);
    }

    setupGame() {
        this.game = new GameLogic();
        this.game.mainLoop();
    }
}

//const app = new App();