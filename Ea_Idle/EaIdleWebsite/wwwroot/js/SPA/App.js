import Router from './Router.js';
import Displays from './Displays.js';
import GameLogic from '../Game/GameLogic.js';
import GameState from '../Models/GameState.js';

export class App {
    constructor() {
        this.router = new Router();
        this.gameLogic = new GameLogic();
        this.gameState = new GameState();
        this.displays = new Displays(this.gameLogic);
    }
}