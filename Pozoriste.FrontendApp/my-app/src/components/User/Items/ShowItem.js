import React from 'react'

const ShowItem = (props) => {
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

    return (
        <li key={id} className='showItem'>
            <p> {showTime} </p>
            <p> cena karte: {ticketPrice} </p>
            <p> Komad: {pieceTitle} </p>
            <p> O delu: {pieceDescription} </p>
            <p> Godina: {pieceYear} </p>
            <p> Zanr: {genre} </p>
            <p> Sala: {auditoriumName} </p>
            <p> Pozoriste: {theatreName}</p>
            <p> Glumci:</p>
            {showActors()}
        </li>
    )
}

export default ShowItem
