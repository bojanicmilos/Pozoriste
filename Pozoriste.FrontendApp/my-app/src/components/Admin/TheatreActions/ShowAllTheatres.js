import React, { useState, useEffect } from 'react'
import { NotificationManager } from 'react-notifications'
import { serviceConfig } from '../../../AppSettings/serviceConfig'
import Spinner from '../../Spinner'
import TheatreItem from '../Items/TheatreItem'

const ShowAllTheatres = () => {
    const [theatres, setShowAllTheatres] = useState([])
    const [isLoading, setIsLoading] = useState(true)

    useEffect(() => {
        getAllTheatres();
    }, [])

    const getAllTheatres = () => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        };

        fetch(`${serviceConfig.baseURL}/api/Theatres`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response);
                }
                return response.json();
            })
            .then((data) => {
                if (data) {
                    setShowAllTheatres(prevState => ([...prevState, ...data]))
                    setIsLoading(false)
                }
            })
            .catch((response) => {
                NotificationManager.error('Greska prilikom prikazivanja pozorista!');
            })
    }

    const fillPageWithPieces = () => {
        return theatres.map((theatre) => {
            return (
                <TheatreItem key={theatre.id} {...theatre} />
            )
        })
    }

    return (
        <ul>
            { isLoading ? <Spinner></Spinner> : fillPageWithPieces()}
        </ul>
    )
}

export default ShowAllTheatres
