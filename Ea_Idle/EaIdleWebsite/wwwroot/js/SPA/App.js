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
        this.game = new GameLogic();
    }


    
}

const app = new App();