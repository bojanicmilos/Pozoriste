import React, { useState, useEffect } from 'react'
import { serviceConfig } from '../../AppSettings/serviceConfig'
import ShowItem from './ShowItem'


const ShowList = () => {
    const [shows, setShowList] = useState([])
    const [isLoading, setIsLoading] = useState(true)


    useEffect(() => {
        getShows();
    }, [])

    const getShows = () => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Contetnt-Type': 'application/json',
            }
        };

        fetch(`${serviceConfig.baseURL}/api/shows`)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response);
                }
                return response.json();
            })
            .then((data) => {
                if (data) {
                    setShowList([...shows, ...data])
                    setIsLoading(false)
                }
            })
            .catch((response) => {
                setIsLoading(false);
            })
    }

    const fillPageWithShows = () => {
        return shows.map((show) => {
            return (
                <>
                    <ShowItem key={show.id} {...show} />
                </>
            )

        })

    }

    return (
        <>
            { isLoading ? fillPageWithShows() : <p>loading...</p>}
        </>
    )
}

export default ShowList



