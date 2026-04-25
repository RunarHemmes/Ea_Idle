import Router from './Router.js';
import Displays from './Displays.js';
import GameLogic from '../Game/GameLogic.js';
import GameState from '../Models/GameState.js';

class App {
    router
    gameLogic
    gameState
    displays

    constructor() {
        this.router = new Router();
        this.router.init();
        this.gameLogic = new GameLogic();
        this.gameState = new GameState();
        this.displays = new Displays(this.gameLogic);
    }
}

const app = new App();
