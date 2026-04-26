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
            "/Credits": '../../views/notImplemented.html',
        };
        this.notFound = "<h1>404</h1><p>Not found.</p>";
    }

    Init() {
        window.addEventListener("popstate", () => this.ToRoute());
        this.ToRoute()

        window.navigateTo = (path) => {
            this.NavTo(path);
        };
    }

    NavTo(path) {
        window.history.pushState({}, "", path);
        this.ToRoute();
    }

    async ToRoute() {
        const path = window.location.pathname;
        const route = this.routes[path];
        const response = await fetch(route);
        const html = await response.text();
        const content = html || this.notFound;
        document.getElementById("app").innerHTML = content;
        debugger;
        const routeParts = route.split("/");
        const eventName = routeParts[routeParts.length - 1];
        window.dispatchEvent(new CustomEvent(eventName));
    }
}

export default Router;