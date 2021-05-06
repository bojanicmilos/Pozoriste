import React from 'react'
import { useState, useEffect } from 'react'
import { useHistory } from 'react-router-dom'
import { isUserLogged } from '../../globalStorage/IsUserLogged'
import { NotificationManager } from 'react-notifications'

const ShowItem = (props) => {
    const history = useHistory()

    const {
        id,
        showTime,
        ticketPrice,
        pieceTitle,
        pieceYear,
        genre,
        auditoriumName,
        theatreName,
        actors,
        len,
        index
    } = props;

    const showActors = () => {
        return actors.map((actor) => {
            return (
                <p key={actor.id}>
                    <strong>{actor.firstName} {actor.lastName}</strong>
                </p>
            )
        })
    }

    const goToShowReservation = (id) => {
        if (isUserLogged()) {
            history.push(`/showreservation/${id}`)
        }
        else {
            NotificationManager.error('Molim ulogujte se kako biste izvrsili rezervaciju!')
        }

    }

    return (
        <React.Fragment>
            <li className='showItem'>
                <div className="showItem-left">
                    <h4>Datum<br /><div className='and-symbol'>&</div>vreme:</h4>
                    <p>{showTime} </p>
                    <h4> Cena karte:</h4>
                    <p>{ticketPrice} din</p>
                    <h4> Komad:</h4>
                    <p className='showItem-left-lastp'>{pieceTitle} </p>
                    <button onClick={() => goToShowReservation(id)}>Rezervisite predstavu</button>
                </div>
                <div className="showItem-right">
                    <div className='showItem-right-p'>
                        <h4> Godina:</h4>
                        <p>{pieceYear} </p>
                        <h4> Zanr:</h4>
                        <p>{genre} </p>
                        <h4> Sala:</h4>
                        <p>{auditoriumName} </p>
                        <h4> Pozoriste:</h4>
                        <p>{theatreName}</p>
                    </div>
                    <h4 className='showItem-h4'> Glumci:</h4>
                    <p className='showItem-showActors'>{showActors()} </p>
                </div>
            </li>
            {(index + 1 !== len) && <p className="arrow down"></p>}
        </React.Fragment>
    )
}

export default ShowItem
