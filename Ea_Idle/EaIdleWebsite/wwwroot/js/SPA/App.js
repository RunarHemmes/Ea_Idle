import Router from './Router.js'
import GameLogic from '../Game/Logic.js'

class App {
    #rootEl;
    router;
    game;
    constructor() {
        this.#rootEl = document.getElementById('app');
        this.router = new Router;
        this.router.init();
        //this.game = new GameLogic(this.#rootEl);
        //this.game.mainLoop();

        window.addEventListener("miningLoaded", this.setupGame)
    }

    setupGame() {
        this.game = new GameLogic();
        this.game.mainLoop();
    }
}

const app = new App();