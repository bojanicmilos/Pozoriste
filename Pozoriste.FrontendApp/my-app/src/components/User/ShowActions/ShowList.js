import React, { useState, useEffect } from 'react'
import { serviceConfig } from '../../../AppSettings/serviceConfig'
import ShowItem from '../Items/ShowItem'
import '../../../style/style.css'
import Spinner from '../../Spinner'
import '../../../style/spinner.css'



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

        fetch(`${serviceConfig.baseURL}/api/shows`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response);
                }
                return response.json();
            })
            .then((data) => {
                if (data) {
                    setShowList(prevState => ([...prevState, ...data]))
                    setIsLoading(false)
                }
            })
            .catch((response) => {
                console.log(response)
                setIsLoading(false);
            })
    }

    const fillPageWithShows = () => {
        return shows.map((show) => {
            return (
                <ShowItem key={show.id} {...show} />
            )
        })
    }

    return (
        <ul className='show-container'>
            { isLoading ? <Spinner></Spinner> : fillPageWithShows()}
        </ul>
    )
}

export default ShowList



