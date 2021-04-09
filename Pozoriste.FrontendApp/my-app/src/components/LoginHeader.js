import React from 'react'
import '../style/style.css'
import { useState } from 'react'
import { serviceConfig } from '../AppSettings/serviceConfig'

const LoginHeader = () => {
    const [username, setUsername] = useState('')
    const [isLogoutHidden, setIsLogoutHidden] = useState(true)
    const [isInputHidden, setIsInputHidden] = useState(false)
    const [isLoginHidden, setIsLoginHidden] = useState(false)

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
        setIsLogoutHidden(true)
        setIsLoginHidden(false)
        setIsInputHidden(false)
        setUsername('')
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
                    localStorage.setItem("userLoggedIn", "true")
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
            <div className='flex-container'>
                <strong className='title'>Pozorište</strong>
                <form type='text'>
                    {!isInputHidden && <>
                        <label className='label' htmlFor='username'>Korisničko ime &nbsp;</label>
                        <input onChange={handleChange} value={username} className='username-input' type='text' id='username' /></>}
                    {!isLoginHidden && <button id='login' type='submit' onClick={handleSubmit} className='flex-item btn btn-warning'>
                        Login
                    </button>}
                    {!isLogoutHidden && <button type='submit' onClick={handleSubmitLogout} className='btn btn-warning'>
                        Logout
                    </button>}
                </form>
            </div>
        </>

    )
}

export default LoginHeader
