import React from 'react'
import { useState, useEffect } from 'react'
import { useHistory } from 'react-router-dom'


const ShowItem = (props) => {
    const [toggleInfo, setToggleInfo] = useState(false)
    const history = useHistory()

    const {
        id,
        showTime,
        ticketPrice,
        pieceTitle,
        pieceDescription,
        pieceYear,
        genre,
        auditoriumName,
        theatreName,
        actors
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
        history.push(`/showreservation/${id}`)
    }

    return (

        <li className='showItem'>
            <div className="showItem-left">
                <p> {showTime} </p>
                <p> cena karte: {ticketPrice} </p>
                <p> Komad: {pieceTitle} </p>
                <button onClick={() => goToShowReservation(id)}>Rezervisite predstavu</button>
            </div>
            <div className="showItem-right">
                <p> Godina: {pieceYear} </p>
                <p> Zanr: {genre} </p>
                <p> Sala: {auditoriumName} </p>
                <p> Pozoriste: {theatreName}</p>
                <p> Glumci:</p>
                {showActors()}
                <button onClick={() => setToggleInfo(!toggleInfo)}>Vise informacija</button>
                <p>  {toggleInfo && pieceDescription} </p>
            </div>
        </li>
    )
}

export default ShowItem
