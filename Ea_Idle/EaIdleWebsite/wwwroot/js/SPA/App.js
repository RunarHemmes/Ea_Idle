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
        this.gameState = new GameState();
        this.gameLogic = new GameLogic(this.gameState);
        this.displays = new Displays(this.gameLogic, this.gameState);

        this.router.init();
        this.gameLogic.StartGame();
    }
}

const app = new App();
