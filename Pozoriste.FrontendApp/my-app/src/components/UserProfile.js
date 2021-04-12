import React from 'react'
import { useState, useEffect } from 'react'
import { serviceConfig } from '../AppSettings/serviceConfig.js'
import { getUserName } from './globalStorage/GetUserName'
import { getRole } from './globalStorage/RoleCheck'
import { isUserLogged } from './globalStorage/IsUserLogged'
import Spinner from './Spinner'

const UserProfile = () => {
    const [userReservations, setUserReservations] = useState({
        user: {

        },
        reservations: [

        ]
    })
    const [isLoading, setIsLoading] = useState(true)

    useEffect(() => {
        getUserByUsername();

    }, []);
    const getUserByUsername = () => {
        let userName = localStorage.getItem('username');
        let userId = localStorage.getItem('userId')
        const requestOptions = {
            method: 'GET',
            headers: {
                'Contetnt-Type': 'application/json',
            }
        };

        fetch(`${serviceConfig.baseURL}/api/users/username/${userName}`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    Promise.reject(response);
                }
                return response.json();
            })
            .then((json) => {
                if (json) {
                    setUserReservations(prevState => ({ ...prevState, user: json }));
                    getUserReservations(userId)
                }
            })
            .catch((response) => {

            })
    }

    const getUserReservations = (userId) => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Contetnt-Type': 'application/json',
            }
        };

        fetch(`${serviceConfig.baseURL}/api/Reservations/byuserid/${userId}`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    Promise.reject(response);
                }
                return response.json();
            })
            .then((json) => {
                if (json) {
                    setUserReservations(prevState => ({ ...prevState, reservations: json }));
                    setIsLoading(false)
                }
            })
            .catch((response) => {

            })
    }

    const showUserReservations = () => {
        return userReservations.reservations.map((reservation) => {

            return (
                <div>
                    <ul>
                        <li key={reservation.id}>
                            {reservation.showTime} &nbsp;
                            {reservation.theatreName} &nbsp;
                            {reservation.auditoriumName} &nbsp;
                            {reservation.pieceTitle} &nbsp;
                            <ul>Sedista: &nbsp;
                                {reservation.reservedSeats.map((seat) => {
                                return (

                                    <li key={seat.id}>
                                        Red: &nbsp;
                                        {seat.row} &nbsp;
                                            Broj: &nbsp;
                                        {seat.number}
                                    </li>
                                )
                            })}
                            </ul>

                        </li>
                    </ul>
                </div>

            )


        });
    }

    return (
        <>
            <div className='user-page'>
                {isLoading ? <Spinner></Spinner> : <>
                    <p>Ime: {userReservations.user.firstName} </p>
                    <p>Prezime: {userReservations.user.lastName}</p>
                    <p>Korisnicko ime: {userReservations.user.userName} </p>
                    <p>Uloga: {userReservations.user.userRole ? "Admin" : "Korisnik"}</p>

                    <div>
                        <li className="list-group-item list-group-item-primary"><strong>Vase rezervacije: </strong></li>
                        {showUserReservations()}
                    </div>
                </>
                }
            </div>
        </>
    )
}

export default UserProfile