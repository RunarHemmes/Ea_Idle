class Router {
    routes;
    notFound;

    constructor() {
        this.routes = {
            "/Mining": '../../views/mining.html',
            "/Tharni": '../../views/notImplemented.html',
            "/Castar": '../../views/notImplemented.html',
            "/Silmaril": '../../views/notImplemented.html',
            "/Help": '../../views/notImplemented.html',
            "/Settings": '../../views/notImplemented.html',
            "/Credits": '../../views/credits.html',

        };
        this.notFound = "<h1>404</h1><p>Not found.</p>";
    }

    init() {
        window.addEventListener("popstate", () => this.toRoute());
        this.toRoute()

        // I know that having both the window.navigateTo and eventlisteners for every button is doing things double,
        // but having only 1 implemented kept sometimes working and sometimes not working. For now, this works, though I do
        // want to come up with a better solution in the future.
        document.getElementById("Nav_Mining").addEventListener("click", this.navTo("/Mining"));
        document.getElementById("Nav_Tharni").addEventListener("click", this.navTo("/Tharni"));
        document.getElementById("Nav_Castar").addEventListener("click", this.navTo("/Castar"));
        document.getElementById("Nav_Silmaril").addEventListener("click", this.navTo("/Silmaril"));
        document.getElementById("Nav_Help").addEventListener("click", this.navTo("/Help"));
        document.getElementById("Nav_Settings").addEventListener("click", this.navTo("/Settings"));
        document.getElementById("Nav_Credits").addEventListener("click", this.navTo("/Credits"));

        window.navigateTo = (path) => {
            this.navTo(path);
        };
    }

    navTo(path) {
        window.history.pushState({}, "", path);
        this.toRoute();
    }

    async toRoute() {
        debugger;
        const path = window.location.pathname;
        const route = this.routes[path];
        const response = await fetch(route);
        const html = await response.text();

        const content = html || this.notFound;
        document.getElementById("app").innerHTML = content;
    }
}

export default Router;