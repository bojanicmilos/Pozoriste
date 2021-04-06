import React from 'react'

const PieceItem = (props) => {
    const {
        id,
        title,
        description,
        year,
        isActive,
        genre,
    } = props;

    return (
        <li key={id} className='pieceItem'>
            <p> Naziv komada: {title} </p>
            <p> Zanr: {genre} </p>
            <p> O delu: {description} </p>
            <p> Godina: {year} </p>
        </li>
    )
}

export default PieceItem