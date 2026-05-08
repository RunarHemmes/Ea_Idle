class Router {
    routes;
    notFound;
    user;

    constructor(user) {
        // A list of URL routes, with the html filepaths, and whether the user has to be logged in to access the page.
        this.user = user;
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

        if (route.requiresAuth == true && this.user.name == null) {
            filePath = this.routes["/Login"].filePath;
        } else {
            filePath = route.filePath;
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