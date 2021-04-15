import React, { useCallback, useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { getUserName } from '../../globalStorage/GetUserName'
import { serviceConfig } from '../../../AppSettings/serviceConfig'
import { NotificationManager } from 'react-notifications'

const ShowReservation = () => {
    const { id } = useParams();
    const [state, setState] = useState({
        show: {},
        user: {},
        seats: [],
        maxRow: 0,
        maxNumberOfRow: 0,
        reservedSeats: [],
        currentClickedSeats: [],

    });

    const getReservedSeats = useCallback(() => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'Application/json'
            }
        }

        fetch(`${serviceConfig.baseURL}/api/reservations/getbyshowid/${id}`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return response.reject(response);
                }
                return response.json();
            })
            .then((json) => {
                setState(prevState => ({ ...prevState, reservedSeats: json }))
            })
            .catch((response) => {
                NotificationManager.error('Bezuspesno ucitani podaci !')
            })
    }, [id])


    const getShow = useCallback(() => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'Application/json'
            }
        }

        fetch(`${serviceConfig.baseURL}/api/shows/${id}`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return response.reject(response);
                }
                return response.json();
            })
            .then((json) => {
                setState((prevState) => ({ ...prevState, show: json }))
                getReservedSeats();
            })
            .catch(() => {
                NotificationManager.error('Bezuspesno ucitani podaci !')
            })
    }, [id, getReservedSeats])



    const getAuditoriumSeats = useCallback(() => {

        if (state.show.auditoriumId === undefined) {
            return;
        }
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'Application/json'
            }
        }

        fetch(`${serviceConfig.baseURL}/api/seats/numberofseats/${state.show.auditoriumId}`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return response.reject(response);
                }
                return response.json();
            })
            .then((json) => {
                setState(prevState => ({ ...prevState, seats: json.seats, maxRow: json.maxRow, maxNumberOfRow: json.maxNumber }))
            })
            .catch((response) => {
                NotificationManager.error('Bezuspesno ucitani podaci !')
            })
    }, [state.show.auditoriumId])

    useEffect(() => {
        getUser();
        getShow();
    }, [getShow])

    useEffect(() => {
        getAuditoriumSeats();
    }, [getAuditoriumSeats])

    const getUser = () => {
        const uName = getUserName();

        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'Application/json'
            }
        }

        fetch(`${serviceConfig.baseURL}/api/users/username/${uName}`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return response.reject(response);
                }
                return response.json()
            })
            .then((json) => {
                setState(prevState => ({ ...prevState, user: json }))
            })
            .catch((response) => {
                NotificationManager.error('Ulogujte se kako biste rezervisali sedista ! ')
            })
    }




    return (
        <div className='show-reservation'>

        </div>
    )
}

export default ShowReservation
