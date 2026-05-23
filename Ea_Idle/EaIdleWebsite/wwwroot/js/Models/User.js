class User {
    name
    id
    role
    connectedName
    connectedId
    connectedTimeLimit
    code

    constructor(username, id) {
        this.name = username;
        this.id = id;
    }
}

export default User;