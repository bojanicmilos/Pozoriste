import React from 'react'
import { getRole } from '../../globalStorage/RoleCheck';

const TheatreItem = (props) => {
    const {
        id,
        name,
    } = props;

    const removeTheatre = props.removeTheatre;
    const getAuditoriumsForOneTheatre = props.getAuditoriumsForOneTheatre;

    const getDeleteBtn = (id) => {
        if (getRole() === 'admin')
            return <button className='btn btn-danger' onClick={() => removeTheatre(id)} >OBRISI</button>
    }

    return (
        <li className='theatre-container'>
            <p>Naziv pozorista:  {name}  {getDeleteBtn(id)} </p>
        </li>
    )
}

export default TheatreItem
