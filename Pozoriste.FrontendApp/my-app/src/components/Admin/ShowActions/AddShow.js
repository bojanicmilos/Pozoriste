import React from 'react'
import { useEffect, useState } from 'react'
import { NotificationManager } from 'react-notifications'
import { serviceConfig } from '../../../AppSettings/serviceConfig'

const AddShow = () => {
    const [state, setState] = useState({
        actors: [],
        auditoriums: [],
        pieces: [],
        theatres: [],
        isLoading: true
    })

    const [sendState, setSendState] = useState({
        showTime: '',
        actorsList: [],
        theatreId: '',
        auditoriumId: '',
        pieceId: '',
        ticketPrice: '100'
    })

    useEffect(() => {
        getTheatres();
        getActors();
        getAuditoriums();
        getPieces();
    }, [])

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
                if (json.status === 400) {
                    NotificationManager.error('Pogresno upisan datum !')
                }
                else if (json.errorMessage) {
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
            setSendState({ ...sendState, actorsList: [...sendState.actorsList, { id: actorId }] })
        }
        else {
            setSendState({ ...sendState, actorsList: sendState.actorsList.filter((actor) => actor.id !== actorId) })
        }
    }


    return (
        <div className='add-show-page'>
            <form className='add-show-page-form' onSubmit={handleSubmit}>
                <div className='add-show-flex'>
                    <div className='flex-child add-show-left-flex'>

                        <h4 className='show-title'>Datum predstave: </h4>
                        <input
                            onChange={(e) =>
                                setSendState({ ...sendState, showTime: e.target.value })
                            }
                            name="showTime"
                            type="datetime-local"
                            style={{
                                borderRadius: '5px',
                                backgroundColor: '#fff',
                                padding: '3px 5px',
                                boxShadow: 'inset 0 3px 6px rgba(0,0,0,0.6)',
                                width: '235px'
                            }}
                            id="date"
                            className="input-date select-dropdown"
                        />
                        <br />
                        <h4 className='show-title-actors'>Glumci: </h4>
                        <div className='actors-checkbox'>
                            {state.actors.map((actor, index) => {
                                return (
                                    <React.Fragment key={actor.id}>
                                        {(index !== 0) && <br />}
                                        <input className='form-check-input'
                                            name={'actor' + actor.id}
                                            type='checkbox'
                                            onChange={(e) => updateActorsList(e, actor.id)} />
                                        <label className='form-check-label' htmlFor={'actor' + actor.id}>&nbsp; {actor.firstName} &nbsp;{actor.lastName}</label>
                                    </React.Fragment>
                                )
                            })}
                        </div>
                    </div>
                    <div className='flex-child add-show-right-flex'>
                        <div className='theatres-labels'>
                            <label htmlFor="theatres"><strong>Pozoriste: &nbsp;</strong></label><br />
                            <select id="theatres" onChange={(e) => setSendState({ ...sendState, theatreId: e.target.value })}>
                                <option value=''>Izaberite pozoriste</option>
                                {state.theatres.map((theatre) => {
                                    return (
                                        <option key={theatre.id} value={theatre.id}>{theatre.name}</option>
                                    )
                                })}
                            </select> <br />
                            <br />
                            <label htmlFor="auditoriums"><strong>Sala: &nbsp; </strong></label> <br />
                            <select disabled={(sendState.theatreId === '') ? true : false} onChange={(e) => setSendState({ ...sendState, auditoriumId: e.target.value })} id="auditoriums">
                                <option value=''>Izaberite salu</option>
                                {state.auditoriums.map((auditorium) => {
                                    return (
                                        <React.Fragment key={auditorium.id}>
                                            {
                                                (auditorium.theatreId.toString() === sendState.theatreId) &&
                                                <option value={auditorium.id}>{auditorium.name}</option>

                                            }
                                        </React.Fragment>
                                    )
                                })}
                            </select>
                            <br />
                            <br />

                            <label htmlFor="pieces"><strong>Pozorisni komad: &nbsp; </strong></label> <br />
                            <select disabled={(sendState.auditoriumId === '') ? true : false} onChange={(e) => setSendState({ ...sendState, pieceId: e.target.value })} id="pieces">
                                <option value=''>Izaberite pozorisni komad</option>
                                {state.pieces.map((piece) => {
                                    return (
                                        <option key={piece.id} value={piece.id}>{piece.title}</option>
                                    )
                                })}
                            </select>
                            <br />
                            <br />
                            <label htmlFor="ticket"><strong>Cena karte: &nbsp; </strong></label> <br />
                            <input disabled={(sendState.pieceId === '') ? true : false} id='ticket' min='100' max='5000' onChange={(e) => setSendState({ ...sendState, ticketPrice: e.target.value })} value={sendState.ticketPrice} type='number' />
                            <br />
                            <br />
                            <br />
                            <button className='btn btn-primary' type='submit'>Dodaj predstavu</button>
                        </div>
                    </div>
                </div>


            </form>

        </div>
    )
}

export default AddShow

