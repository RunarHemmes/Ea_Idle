class Router {
    routes;
    notFound;

    constructor() {
        this.routes = {
            "/": '../../views/mining.html',
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