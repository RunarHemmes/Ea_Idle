class User {
    name
    id
    role
    connectedName
    connectedId
    connectedTimeLimit

    constructor(username, id) {
        this.name = username;
        this.id = id;
    }
}

export default User;