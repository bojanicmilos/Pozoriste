export const getUserName = () => {
    let userName = localStorage.getItem('username')
    return userName
}