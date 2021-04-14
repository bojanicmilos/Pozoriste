import React from 'react'
import { useEffect, useState } from 'react'
import { NotificationManager } from 'react-notifications'
import { serviceConfig } from '../../../AppSettings/serviceConfig'

const AddShow = () => {
    const [state, setState] = useState({
        actors: [],
        auditoriums: [],
        pieces: [],
        isLoading: true
    })

    const [sendState, setSendState] = useState({
        showTime: '',
        actorsList: [],
        auditoriumId: '',
        pieceId: '',
        ticketPrice: '100'
    })

    useEffect(() => {
        getActors();
        getAuditoriums();
        getPieces();
    }, [])

    const getActors = () => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        }

        fetch(`${serviceConfig.baseURL}/api/actors`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response)
                }
                return response.json()
            })
            .then((json) => {
                setState(prevState => ({ ...prevState, actors: json }))
            })
            .catch((response) => {
                NotificationManager.error('Nije moguce preuzeti podatke ! ')
            })
    }

    const getAuditoriums = () => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        }

        fetch(`${serviceConfig.baseURL}/api/auditoriums/getall`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response)
                }
                return response.json()
            })
            .then((json) => {
                setState(prevState => ({ ...prevState, auditoriums: json }))
            })
            .catch((response) => {
                NotificationManager.error('Nije moguce preuzeti podatke ! ')
            })

    }

    const getPieces = () => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        }

        fetch(`${serviceConfig.baseURL}/api/pieces/active`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response)
                }
                return response.json()
            })
            .then((json) => {
                setState(prevState => ({ ...prevState, pieces: json, isLoading: false }))
            })
            .catch((response) => {
                NotificationManager.error('Nije moguce preuzeti podatke ! ')
            })
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        if (sendState.pieceId.match(/\d+/g) === null || sendState.auditoriumId.match(/\d+/g) === null) {
            NotificationManager.error('Odaberite neke od ponudjenih opcija.')
            return;
        }

        if (sendState.showTime !== '' && sendState.ticketPrice !== '' && sendState.pieceId !== '' && sendState.auditoriumId !== '') {
            addShow();
        }
        else {
            NotificationManager.error('Unesite sve neophodne podatke ! ')
        }

    }

    const addShow = () => {
        const json = {
            showTime: sendState.showTime,
            ticketPrice: +sendState.ticketPrice,
            pieceId: +sendState.pieceId,
            auditoriumId: +sendState.auditoriumId,
            actorIds: sendState.actorsList
        }

        console.log(json);

        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(json)
        }

        fetch(`${serviceConfig.baseURL}/api/shows`, requestOptions)
            .then((response) => {
                return response.json();
            })
            .then((json) => {
                if (json.errorMessage) {
                    NotificationManager.error(json.errorMessage)
                }
                else {
                    NotificationManager.success('Predstava uspesno dodata !')
                }
            })
            .catch(() => {
                NotificationManager.error('Greska')
            })
    }

    const updateActorsList = (e, actorId) => {
        if (e.target.checked) {
            setSendState({ ...sendState, actorsList: sendState.actorsList.concat({ id: actorId }) })
        }
        else {
            setSendState({ ...sendState, actorsList: sendState.actorsList.filter((actor) => actor.id !== actorId) })
        }
    }


    return (
        <div className='add-show-page'>
            <strong className='date-show-title'>Datum predstave: </strong>
            <br />
            <form onSubmit={handleSubmit}>
                <input
                    onChange={(e) =>
                        setSendState({ ...sendState, showTime: e.target.value })
                    }
                    name="showTime"
                    type="datetime-local"
                    style={{
                        marginLeft: '330px',
                        borderRadius: '5px',
                        backgroundColor: '#fff',
                        padding: '3px 5px',
                        boxShadow: 'inset 0 3px 6px rgba(0,0,0,0.1)',
                        width: '235px'
                    }}
                    id="date"
                    className="input-date select-dropdown"
                />
                <br />
                <br />
                <strong>Glumci: </strong>
                <br />
                <div className='actors-checkbox'>
                    {state.actors.map((actor, index) => {
                        return (
                            <React.Fragment key={actor.id}>
                                {(index !== 0) && <br />}
                                <input className='form-check-input'
                                    name={'actor' + actor.id}
                                    type='checkbox'
                                    onChange={(e) => updateActorsList(e, actor.id)} />
                                <label className='form-check-label' htmlFor={'actor' + actor.id}>&nbsp; {actor.firstName} &nbsp; {actor.lastName}</label>
                            </React.Fragment>
                        )
                    })}
                </div>

                <br />

                <label htmlFor="auditoriums"><strong>Izaberi auditorium: &nbsp; </strong></label>
                <select onChange={(e) => setSendState({ ...sendState, auditoriumId: e.target.value })} id="auditoriums">
                    <option>Izaberite salu</option>
                    {state.auditoriums.map((auditorium) => {
                        return (
                            <option key={auditorium.id} value={auditorium.id}>{auditorium.name}</option>
                        )
                    })}
                </select>

                <br />

                <label htmlFor="pieces"><strong>Izaberi pozorisni komad: &nbsp; </strong></label>
                <select onChange={(e) => setSendState({ ...sendState, pieceId: e.target.value })} id="pieces">
                    <option>Izaberite pozorisni komad</option>
                    {state.pieces.map((piece) => {
                        return (
                            <option key={piece.id} value={piece.id}>{piece.title}</option>
                        )
                    })}
                </select>
                <br />
                <label htmlFor="ticket"><strong>Cena karte: &nbsp; </strong></label>
                <input id='ticket' min='100' onChange={(e) => setSendState({ ...sendState, ticketPrice: e.target.value })} value={sendState.ticketPrice} type='number' />
                <br />
                <button className='btn btn-primary' type='submit'>Dodaj predstavu</button>
            </form>

        </div>
    )
}

export default AddShow

