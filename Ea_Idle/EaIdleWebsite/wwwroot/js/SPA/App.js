import Router from './Router.js'

class App {
    #rootEl;
    router;
    constructor() {
        this.#rootEl = document.getElementById('app');
        this.router = new Router;
        this.router.init();
    }

    
}

const app = new App();