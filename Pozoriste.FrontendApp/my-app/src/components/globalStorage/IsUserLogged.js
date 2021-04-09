export const isUserLogged = () => {
    let userLogged = localStorage.getItem('userLoggedIn');

    if (userLogged === 'true') {
        return true
    }
    else return false
}