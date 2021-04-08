import React from 'react'
import { useState, useEffect } from 'react'
import serviceConfig from '../../AppSettings/serviceConfig'


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
                'Content-Type': 'application/json',
                //Authorization: `Bearer ${localStorage.getItem("role")}`
            }
        }


    }

    return (
        <div>

        </div>
    )
}

export default UserProfile

