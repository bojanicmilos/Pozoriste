import React, { useEffect, useState } from 'react'
import { gerRole, getRole } from '../../globalStorage/RoleCheck'
import { NotificationManager } from 'react-notifications'
import { serviceConfig } from '../../../AppSettings/serviceConfig'

const AuditoriumItem = (props) => {
    const {
        id,
        name,
        theatreName,
        theatreId,
    } = props;

    const removeAuditorium = props.removeAuditorium;

    const getDeleteBtn = (id) => {
        if (getRole() === 'admin')
            return <button className='btn btn-danger' onClick={() => removeAuditorium(id)} >OBRISI</button>
    }

    return (
        <div className='auditorium-container'>
            <h3>Naziv sale: {name} {getDeleteBtn(id)}</h3>
            <h4>Naziv pozorista kojem pripada: {theatreName}</h4>
        </div >
    )
}

export default AuditoriumItem
