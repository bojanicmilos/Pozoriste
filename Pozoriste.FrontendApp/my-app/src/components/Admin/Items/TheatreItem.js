import React from 'react'

const TheatreItem = (props) => {
    const {
        id,
        name,
    } = props;

    return (
        <li className='theatre-container'>
            <p>Naziv pozorista:  {name} </p>
        </li>
    )
}

export default TheatreItem
