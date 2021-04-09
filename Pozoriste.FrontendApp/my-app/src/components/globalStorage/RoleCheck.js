export const getRole = () => {
    const roleEnum = localStorage.getItem('role');

    let role = '';

    if (roleEnum == '0') {
        role = 'user';
    }
    else if (roleEnum == '1') {
        role = 'admin'
    }
    return role;
}