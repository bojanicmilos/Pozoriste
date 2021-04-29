import React from 'react'
import { useState, useEffect } from 'react'
import { serviceConfig } from '../AppSettings/serviceConfig.js'
import { getUserName } from './globalStorage/GetUserName'
import { getRole } from './globalStorage/RoleCheck'
import { isUserLogged } from './globalStorage/IsUserLogged'
import Spinner from './Spinner'
import img from '../images/user.png'


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
                <li className="reservation-components" key={reservation.id}>
                    <p id='resid' className='reservation-item'> <strong>Vreme i datum:</strong><span>{reservation.showTime} </span>
                        <strong>Pozoriste:</strong> <span>{reservation.theatreName} </span>
                        <strong>Sala:</strong> <span>{reservation.auditoriumName} </span>
                        <strong>Komad:</strong> <span>{reservation.pieceTitle}</span>  </p>
                    <ul id='seats' className='reservation-item'><strong>Sedista:</strong>
                        {reservation.reservedSeats.map((seat) => {
                            return (
                                <li key={seat.id}>
                                    Red:
                                    {seat.row} &nbsp;
                                    Broj:
                                    {seat.number}
                                </li>
                            )
                        })}
                    </ul>

                </li>
            )


        });
    }

    return (
        <>
            <div className='user-page'>
                {isLoading ? <Spinner></Spinner> : <>
                    <div class="left">
                        <img className="img-user" src={img} alt="user" style={{ height: '150px', width: '150px' }} />
                        <h4>{userReservations.user.firstName}</h4>
                        <h4>{userReservations.user.lastName}</h4>
                    </div>

                    <div class="right">
                        <div class="info">
                            <h3>Informacije</h3>
                            <div class="info_data">
                                <div class="info_data_item">
                                    <h4>Korisnicko ime:</h4>
                                    <p>{userReservations.user.userName}</p>
                                </div>
                                <div class="info_data_item">
                                    <h4>Uloga:</h4>
                                    <p class="profile-role">{userReservations.user.userRole ? "Admin" : "Korisnik"}</p>
                                </div>
                            </div>
                        </div>

                        <div class="reservations">
                            <h3>Rezervacije</h3>
                            <div class="reservations_data">
                                <div class="reservations_data_item">
                                    <h4>Vase rezervacije:</h4>
                                    <p>
                                        {showUserReservations()}
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>

                </>

                }

            </div>



        </>
    )
}

export default UserProfile