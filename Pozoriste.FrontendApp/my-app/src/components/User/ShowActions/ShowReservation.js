import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { getUserName } from '../../globalStorage/GetUserName'
import { serviceConfig } from '../../../AppSettings/serviceConfig'
import { NotificationManager } from 'react-notifications'

const ShowReservation = () => {
    const { id } = useParams()
    const [state, setState] = useState()


    useEffect(() => {
        getShow();
        getAuditoriumSeats();
        getReservedSeats();

        getUser();
    }, [])

    const getShow = () => {

    }

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
                response.json()
            })
            .then((json) => {

            })
            .catch((response) => {
                NotificationManager.error('Ulogujte se kako biste rezervisali sedista ! ')
            })

    }

    const getReservedSeats = () => {
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
            })
            .then((json) => {

            })
    }

    const getAuditoriumSeats = () => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'Application/json'
            }
        }
    }


    return (
        <div className='show-reservation'>

        </div>
    )
}

export default ShowReservation
