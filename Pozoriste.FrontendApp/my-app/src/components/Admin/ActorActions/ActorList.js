import React from 'react'
import { useState, useEffect } from 'react'
import { serviceConfig } from '../../../AppSettings/serviceConfig'
import { NotificationManager } from 'react-notifications'
import { Spinner } from '../../Spinner'

const ActorList = () => {
    const [actors, setActors] = useState([])
    const [isLoading, setIsLoading] = useState(true)

    useEffect(() => {
        getActors();
    }, [])

    const getActors = () => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application-json',
            }
        }

        fetch(`${serviceConfig.baseURL}/api/actors`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response)
                }
                return response.json();
            })
            .then((json) => {
                setActors(json);
                setIsLoading(false);
            })
            .catch((response) => {
                NotificationManager.error('Nije moguce prikazati glumce.')
            })
    }

    const removeActor = (id) => {
        const requestOptions = {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application-json'
            }
        }

        fetch(`${serviceConfig.baseURL}/api/actors/${id}`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return response.reject(response);
                }

                let actorsFiltered = actors;
                actorsFiltered = actorsFiltered.filter((actor) => actor.id !== id);
                setActors(actorsFiltered);
                NotificationManager.success('Glumac uspesno obrisan ! ');
            })
            .catch((response) => {
                NotificationManager.error('Nije moguce obrisati glumca ! ');
            })
    }

    const showActors = () => {
        return actors.map((actor) => {

            return (
                <>
                    <li key={actor.id}>
                        Glumac &nbsp;
                    <strong>Ime: {actor.firstName} </strong>
                        <strong> Prezime: {actor.lastName}</strong>
                    </li>
                    <button onClick={() => removeActor(actor.id)} >Obrisi</button>
                </>
            )
        })
    }
    return (

        <ul className='actor-container'>
            {showActors()}
        </ul >

    )
}

export default ActorList
