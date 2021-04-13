import React from 'react'
import { useState } from 'react'
import { NotificationManager } from 'react-notifications'
import { serviceConfig } from '../../../AppSettings/serviceConfig'

const AddActor = () => {
    const [actor, setActor] = useState({
        firstName: '',
        lastName: ''
    })

    const handleSubmit = (e) => {
        e.preventDefault();

        if (actor.firstName !== '' && actor.lastName !== '') {
            addActor();
        }
        else {
            NotificationManager.error('Unesite sve neophodne podatke');
        }
    }

    const addActor = () => {

        const json = {
            firstName: actor.firstName,
            lastName: actor.lastName
        }

        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(json)
        }

        fetch(`${serviceConfig.baseURL}/api/actors`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response)
                }
                return response.json();
            })
            .then((json) => {
                console.log(json)
                NotificationManager.success("Glumac uspesno upisan !");

            })
            .catch((response) => {
                NotificationManager.error("Nije moguce dodati glumca !");
            })
    }




    return (
        <div className='add-actor-page'>
            <h2 style={{ textAlign: 'center' }}><strong>Dodaj glumca:</strong> </h2>
            <br />
            <form onSubmit={handleSubmit} >
                <input

                    maxLength='50'
                    className='form-control'
                    value={actor.firstName}
                    onChange={(e) => setActor({ ...actor, firstName: e.target.value })} placeholder='Ime' type='text' />
                <input

                    className='form-control'
                    maxLength='50'
                    value={actor.lastName}
                    onChange={(e) => setActor({ ...actor, lastName: e.target.value })} placeholder='Prezime' type='text' />

                <button className='btn btn-primary' type='submit'>Dodaj glumca</button>
            </form>

        </div>
    )
}

export default AddActor



