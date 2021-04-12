import React from 'react'
import '../style/style.css'
import { useState } from 'react'
import { serviceConfig } from '../AppSettings/serviceConfig'
import { Link } from 'react-router-dom'
import { isUserLogged } from './globalStorage/IsUserLogged'
import { useHistory } from "react-router-dom";

const LoginHeader = () => {
    const [username, setUsername] = useState('')
    const [isLogoutHidden, setIsLogoutHidden] = useState(true)
    const [isInputHidden, setIsInputHidden] = useState(false)
    const [isLoginHidden, setIsLoginHidden] = useState(false)

    let history = useHistory();


    const handleChange = (e) => {
        setUsername(e.target.value)
    }

    const handleSubmit = (e) => {
        document.getElementById('login').blur()
        e.preventDefault();

        if (username !== "") {
            login();
        }
    }

    const handleSubmitLogout = (e) => {
        e.preventDefault();
        localStorage.removeItem('userLoggedIn');
        localStorage.removeItem('userId')
        localStorage.removeItem('role')
        localStorage.removeItem('username')
        setIsLogoutHidden(true)
        setIsLoginHidden(false)
        setIsInputHidden(false)
        setUsername('')
        history.push('/')
    };

    const login = () => {
        const requestOptions = {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${localStorage.getItem("jwt")}`,
            },
        };

        fetch(
            `${serviceConfig.baseURL}/api/users/username/${username}`,
            requestOptions
        )
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response)
                }
                return response.json();
            })
            .then((data) => {
                if (data) {
                    localStorage.setItem('userId', data.id)
                    localStorage.setItem('role', data.userRole)
                    localStorage.setItem('username', data.userName)
                    localStorage.setItem('userLoggedIn', "true")
                    setIsLogoutHidden(false)
                    setIsInputHidden(true)
                    setIsLoginHidden(true)
                    console.log('Uspesno logovanje...NOTIFICATION MANAGER...')
                }

            })
            .catch((response) => {
                console.log('Pogresno korisnicko ime... Notification manager dodaj.')
            });
    };

    return (
        <>
            <div className='fixed-container'>
                <div className='flex-container'>
                    <Link to='/showlist' className='title-header'>Pozoriste</Link>
                    {isUserLogged() && <Link to='/userprofile' className='user-profile-header'>Profil</Link>}
                    <form type='text'>
                        {!isInputHidden && <>
                            <label className='label' htmlFor='username'></label>
                            <input placeholder='Korisnicko ime' onChange={handleChange} value={username} className='username-input' type='text' id='username' /></>}
                        {!isLoginHidden && <button id='login' type='submit' onClick={handleSubmit} className='flex-item btn btn-warning'>
                            Login
                    </button>}
                        {!isLogoutHidden && <button type='submit' onClick={handleSubmitLogout} className='btn btn-warning'>
                            Logout
                    </button>}
                    </form>
                </div>
            </div>

        </>

    )
}

export default LoginHeader
