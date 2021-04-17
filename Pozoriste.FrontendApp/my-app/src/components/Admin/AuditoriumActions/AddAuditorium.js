import React, { useState, useEffect } from 'react'
import { NotificationManager } from 'react-notifications'
import { serviceConfig } from '../../../AppSettings/serviceConfig'

const AddAuditorium = () => {
    const [auditorium, setAuditorium] = useState({
        auditName: '',
        seatRows: '',
        numberOfSeats: '',
        theatreId: '',
        theatreName: '',
    })

    const [state, setState] = useState({
        theatres: []
    })

    useEffect(() => {
        getTheatres();
    }, [])

    const handleSubmit = (e) => {
        // e.preventDefault();
        if (auditorium.theatreId === '')
            return NotificationManager.error('Izaberite pozoriste!')
        else
            addAuditorium();
    }

    const getTheatres = () => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        }

        fetch(`${serviceConfig.baseURL}/api/theatres`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response)
                }
                return response.json();
            })
            .then((json) => {
                setState((prevState) => ({ ...prevState, theatres: json }))
            })
            .catch((response) => {
                NotificationManager.error('Nije moguce preuzeti podatke ! ')
            })
    }

    const addAuditorium = () => {
        const json = {
            auditName: auditorium.auditName,
            seatRows: +auditorium.seatRows,
            numberOfSeats: +auditorium.numberOfSeats,
            theatreName: auditorium.theatreName,
            theatreId: +auditorium.theatreId
        }

        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(json)
        }

        fetch(`${serviceConfig.baseURL}/api/Auditoriums/create`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response)
                }
                return response.json();
            })
            .then((json) => {
                NotificationManager.success('Kreirana nova sala!')
            })
            .catch((json) => {
                NotificationManager.error('Nije moguce kreirati salu!')
            })
    }

    return (
        <div className='add-auditorium-page'>
            <h2 style={{ textAlign: 'center' }}><strong>Dodaj novu salu: </strong></h2>
            <br />
            <form onSubmit={handleSubmit}>
                <select id="theatres" className="form-select" style={{ width: 115, height: 25 }} onChange={(e) => setAuditorium({ ...auditorium, theatreId: e.target.value })}>
                    <option value=''>Izaberi pozoriste</option>
                    {state.theatres.map((theatre) => {
                        return (
                            <option key={theatre.id} value={theatre.id}>{theatre.name}</option>
                        )
                    })}
                </select>
                <input
                    value={auditorium.auditName}
                    onChange={(e) => setAuditorium({ ...auditorium, auditName: e.target.value })}
                    placeholder='Naziv sale' type='text' maxLength='50' required
                    className='form-control'
                />
                <input
                    value={auditorium.seatRows}
                    onChange={(e) => setAuditorium({ ...auditorium, seatRows: e.target.value })} placeholder='Broj redova' type='number' min={1} max={20} required
                    className="form-control"
                />
                <input
                    value={auditorium.numberOfSeats}
                    onChange={(e) => setAuditorium({ ...auditorium, numberOfSeats: e.target.value })} placeholder='Broj sedista' type='number' min={1} max={20} required
                    className="form-control"
                />
                <button type='submit' className="btn btn-primary">Dodaj salu</button>
            </form>
        </div >
    )
}

export default AddAuditorium
