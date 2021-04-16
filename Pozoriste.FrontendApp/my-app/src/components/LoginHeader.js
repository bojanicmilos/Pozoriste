import React from 'react'
import '../style/style.css'
import { useState } from 'react'
import { serviceConfig } from '../AppSettings/serviceConfig'
import { Link } from 'react-router-dom'
import { isUserLogged } from './globalStorage/IsUserLogged'
import { useHistory } from "react-router-dom";
import { useContext } from 'react'
import { Context } from '../App'
import { NotificationManager } from 'react-notifications'

const LoginHeader = () => {
    const [username, setUsername] = useState('')
    const [isLogoutHidden, setIsLogoutHidden] = useState(true)
    const [isInputHidden, setIsInputHidden] = useState(false)
    const [isLoginHidden, setIsLoginHidden] = useState(false)

    let history = useHistory();

    const [context, setContext] = useContext(Context)


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
        localStorage.removeItem('userLoggedIn');
        localStorage.removeItem('userId')
        localStorage.removeItem('role')
        localStorage.removeItem('username')
        setIsLogoutHidden(true)
        setIsLoginHidden(false)
        setIsInputHidden(false)
        setUsername('')
        setContext(false)
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
                    setContext(true)
                }
            })
            .catch((response) => {
                NotificationManager.error('Pogresno korisnicko ime, molimo pokusajte ponovo');
            });
    };

    return (
        <>
            <div className='fixed-container'>
                <div className='flex-container'>
                    <Link to='/showlist' className='title-header'> Pozoriste</Link>
                    {isUserLogged() && <Link to='/userprofile' className='user-profile-header'>Profil</Link>}
                    <form type='text'>
                        {!isUserLogged() && <>
                            <label className='label' htmlFor='username'></label>
                            <input placeholder='Korisnicko ime' onChange={handleChange} value={username} className='username-input' type='text' id='username' /></>}
                        {!isUserLogged() && <button id='login' type='submit' onClick={handleSubmit} className='flex-item btn btn-outline-light'>
                            Prijavite se
                    </button>}
                        {isUserLogged() && <button type='submit' onClick={handleSubmitLogout} className='btn btn-outline-light'>
                            Odjavite se
                    </button>}
                    </form>
                </div>
            </div>

        </>

    )
}

export default LoginHeader
