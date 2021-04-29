import React from 'react'
import { useState, useEffect } from 'react'
import { serviceConfig } from '../../../AppSettings/serviceConfig'
import { NotificationManager } from 'react-notifications'
import { Spinner } from '../../Spinner'
import Table from 'react-bootstrap/Table'

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
        return actors.map((actor, index) => {

            return (
                <tr key={actor.id}>
                    <td>{index + 1}</td>
                    <td>{actor.firstName} </td>
                    <td>{actor.lastName}</td>
                    <td><button className='btn btn-danger' onClick={() => removeActor(actor.id)} >Obrisi</button></td>
                </tr>
            )
        })
    }
    return (

        <Table className='actor-table' striped bordered hover variant='white' >
            <thead>
                <tr>
                    <th>#</th>
                    <th>Ime</th>
                    <th>Prezime</th>
                    <th>↓</th>
                </tr>
            </thead>
            <tbody>
                {showActors()}
            </tbody>
        </Table>

    )
}

export default ActorList
