import React, { useState, useEffect } from 'react'
import { serviceConfig } from '../../../AppSettings/serviceConfig'
import Spinner from '../../Spinner'
import '../../../style/spinner.css'
import AuditoriumItem from '../Items/AuditoriumItem'
import 'react-notifications/lib/notifications.css'
import { NotificationManager } from 'react-notifications'

function ShowAllAuditoriums() {
    const [auditoriums, setShowAllAuditoriums] = useState([])
    const [isLoading, setIsLoading] = useState(true)

    useEffect(() => {
        getAllAuditoriums();
    }, [])

    const getAllAuditoriums = () => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            }
        };

        fetch(`${serviceConfig.baseURL}/api/Auditoriums/getAll`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response);
                }
                return response.json();
            })
            .then((data) => {
                if (data) {
                    setShowAllAuditoriums(prevState => ([...prevState, ...data]))
                    setIsLoading(false)
                }
            })
            .catch((response) => {
                setIsLoading(false);
            })
    }

    const removeAuditorium = (id) => {
        const requestOptions = {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            },
        };
        fetch(`${serviceConfig.baseURL}/api/Auditoriums/${id}`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response);
                }

                let auditoriumsFiltered = auditoriums;
                auditoriumsFiltered = auditoriumsFiltered.filter((auditorium) => auditorium.id !== id);
                setShowAllAuditoriums(auditoriumsFiltered);
                NotificationManager.success('Uspesno obrisana sala!');
            })
            .catch((response) => {
                NotificationManager.error('Nije moguce obrisati auditorijum!')
            })
    }

    const fillPageWithAuditoriums = () => {
        return auditoriums.map((auditorium) => {
            return (
                <AuditoriumItem key={auditorium.id} {...auditorium} removeAuditorium={removeAuditorium} />
            )
        })
    }

    return (
        <div className='auditorium-container'>
            { isLoading ? <Spinner></Spinner> : fillPageWithAuditoriums()}
        </div>
    )
}

export default ShowAllAuditoriums
