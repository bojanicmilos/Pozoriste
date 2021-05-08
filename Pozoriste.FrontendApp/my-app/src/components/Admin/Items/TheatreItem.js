import React from 'react'
import { getRole } from '../../globalStorage/RoleCheck';
import img from '../../../images/delete-removebg-preview.png'

const TheatreItem = (props) => {
    const {
        id,
        name,
        cityName,
        streetName,
        index
    } = props;

    const removeTheatre = props.removeTheatre;

    const getDeleteBtn = (id) => {
        if (getRole() === 'admin')
            return <img src={img} alt='' style={{ height: '25px', width: '25px', cursor: 'pointer' }} onClick={() => removeTheatre(id)} />
    }

    return (
        <tr className='theatre-container'>
            <td>{index + 1}</td>
            <td>{name}</td>
            <td>{cityName}</td>
            <td>{streetName}</td>
            <td>{getDeleteBtn(id)}</td>
        </tr>
    )
}

export default TheatreItem
