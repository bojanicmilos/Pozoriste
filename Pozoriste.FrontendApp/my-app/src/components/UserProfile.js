import React from 'react'
import { useState, useEffect } from 'react'
import serviceConfig from '../AppSettings/serviceConfig.js'
import { getUserName } from './globalStorage/GetUserName'


const UserProfile = () => {
    const [user, setUser] = useState({})
    const [reservations, setReservations] = useState([])

    useEffect(() => {
        getUserByUsername();

    }, [])

    const getUserByUsername = () => {
        let userName = getUserName();

        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        }
    }
    return (
        <div>
            User profile
        </div>
    )
}

export default UserProfile