import React from 'react'
import { useState } from 'react'
import { NotificationManager } from 'react-notifications'
import { serviceConfig } from '../../../AppSettings/serviceConfig'

const AddTheatre = () => {
    const [theatre, setTheatre] = useState({
        name: '',
        auditName: '',
        seatRows: '',
        numberOfSeats: '',
        cityName: '',
        streetName: ''
    })

    const handleSubmit = (e) => {
        e.preventDefault();
        addTheatre();
        setTheatre({ name: '', auditName: '', seatRows: '', numberOfSeats: '', cityName: '', streetName: '' })
    }


    const addTheatre = () => {
        const json = {
            name: theatre.name,
            auditName: theatre.auditName,
            seatRows: +theatre.seatRows,
            numberOfSeats: +theatre.numberOfSeats,
            cityName: theatre.cityName,
            streetName: theatre.streetName
        }

        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(json)
        }

        fetch(`${serviceConfig.baseURL}/api/theatres`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response)
                }
                return response.json();
            })
            .then((json) => {
                NotificationManager.success("Kreirano novo pozoriste !");
            })
            .catch((json) => {
                NotificationManager.error("Nije moguce kreirati pozoriste !");
            })
    }

    return (
        <div className='add-theatre-page'>
            <h2 style={{ textAlign: 'center' }}><strong>Dodaj novo pozoriste: </strong></h2>
            <br />
            <form onSubmit={handleSubmit}>
                <input
                    value={theatre.name}
                    onChange={(e) => setTheatre({ ...theatre, name: e.target.value })} placeholder='Naziv pozorista' type='text' maxLength='50' required
                    className="form-control"
                />
                <input
                    value={theatre.auditName}
                    onChange={(e) => setTheatre({ ...theatre, auditName: e.target.value })} placeholder='Naziv auditoriuma' type='text' maxLength='50' required
                    className="form-control"
                />
                <input
                    value={theatre.seatRows}
                    onChange={(e) => setTheatre({ ...theatre, seatRows: e.target.value })} placeholder='Broj redova' type='number' min={1} max={20} required
                    className="form-control"
                />
                <input
                    value={theatre.numberOfSeats}
                    onChange={(e) => setTheatre({ ...theatre, numberOfSeats: e.target.value })} placeholder='Broj sedista' type='number' min={1} max={20} required
                    className="form-control"
                />
                <input
                    value={theatre.cityName}
                    onChange={(e) => setTheatre({ ...theatre, cityName: e.target.value })} placeholder='Naziv grada' type='text' maxLength='50' required
                    className="form-control"
                />
                <input
                    value={theatre.streetName}
                    onChange={(e) => setTheatre({ ...theatre, streetName: e.target.value })} placeholder='Ulica i broj' type='text' maxLength='50' required
                    className="form-control"
                />
                <button type='submit' className="btn btn-primary">Dodaj pozoriste</button>
            </form>
        </div >
    )
}

export default AddTheatre
