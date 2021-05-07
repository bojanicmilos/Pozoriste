import React, { useState, useEffect } from 'react'
import { serviceConfig } from '../../../AppSettings/serviceConfig'
import ShowItem from '../Items/ShowItem'
import '../../../style/style.css'
import Spinner from '../../Spinner'
import '../../../style/spinner.css'
import { NotificationManager } from 'react-notifications'
import img from '../../../images/404.png'

const ShowList = () => {
    const [state, setState] = useState({
        shows: [],
        search: ''
    })
    const [isLoading, setIsLoading] = useState(true)



    useEffect(() => {
        getShows();
    }, [])


    const filterShows = (searchString) => {
        getShows(searchString);
    }

    const getShows = (searchString = '') => {
        console.log(searchString)
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            }
        };

        fetch(`${serviceConfig.baseURL}/api/shows?Search=${searchString}`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response);
                }
                return response.json();
            })
            .then((data) => {
                if (data) {
                    setState(prevState => ({ ...prevState, search: searchString }));
                    setState(prevState => ({ ...prevState, shows: data }))
                    setIsLoading(false)

                }
            })
            .catch((response) => {
                console.log(response)
                setIsLoading(false);
            })
    }

    const fillPageWithShows = () => {
        const showsLenght = state.shows.length;

        if (state.shows.length === 0) {
            return (
                <div className='not-found'>
                    <img className='not-found-img' src={img} alt='404 not found'>
                    </img>
                </div>

            )
        }

        return state.shows.map((show, index) => {
            return (
                <ShowItem index={index} len={showsLenght} key={show.id} {...show} />
            )
        })
    }

    return (
        <React.Fragment>
            <input
                value={state.search}
                onChange={(e) => filterShows(e.target.value)}
                placeholder='Pretrazivanje' type='text'
                className='form-control search'
            />
            <ul className='show-container'>
                {isLoading ? <Spinner></Spinner> : fillPageWithShows()}
            </ul>
        </React.Fragment>
    )
}

export default ShowList



