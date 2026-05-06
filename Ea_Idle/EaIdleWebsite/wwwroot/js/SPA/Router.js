class Router {
    routes;
    notFound;

    constructor() {
        // A list of URL routes, with the html filepaths, and whether the user has to be logged in to access the page.
        //this.routes = {
        //    "/": ['../../views/login.html', false],
        //    "/Login": ['../../views/login.html', false],
        //    "/Mining": ['../../views/mining.html', true],
        //    "/Tharni": ['../../views/notImplemented.html', true],
        //    "/Castar": ['../../views/notImplemented.html', true],
        //    "/Silmaril": ['../../views/notImplemented.html', true],
        //    "/Help": ['../../views/notImplemented.html', true],
        //    "/Settings": ['../../views/notImplemented.html', true],
        //    "/Credits": ['../../views/notImplemented.html', true],
        //};
        this.routes = {
            "/": { filePath: '../../views/login.html', requiresAuth: false },
            "/Login": { filePath: '../../views/login.html', requiresAuth: false },
            "/Mining": { filePath: '../../views/mining.html', requiresAuth: true },
            "/Tharni": { filePath: '../../views/notImplemented.html', requiresAuth: true },
            "/Castar": { filePath: '../../views/notImplemented.html', requiresAuth: true },
            "/Silmaril": { filePath: '../../views/notImplemented.html', requiresAuth: true },
            "/Help": { filePath: '../../views/notImplemented.html', requiresAuth: true },
            "/Settings": { filePath: '../../views/notImplemented.html', requiresAuth: true },
            "/Credits": { filePath: '../../views/notImplemented.html', requiresAuth: true },
        }
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
        let filePath;

        if (route.requiresAuth == true) {
            filePath = this.routes["/Login"].filePath;
        } else {
            filePath = route.filePath;
            debugger;
        }

        const response = await fetch(filePath);
        const html = await response.text();
        const content = html || this.notFound;

        document.getElementById("App").innerHTML = content;

        const routeParts = filePath.split("/");
        const eventName = routeParts[routeParts.length - 1];
        window.dispatchEvent(new CustomEvent(eventName));
    }
}

export default Router;