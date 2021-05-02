import React, { useEffect, useState } from 'react'
import { gerRole, getRole } from '../../globalStorage/RoleCheck'
import { NotificationManager } from 'react-notifications'
import { serviceConfig } from '../../../AppSettings/serviceConfig'
import img from '../../../images/delete-removebg-preview.png'

const AuditoriumItem = (props) => {
    const {
        id,
        name,
        theatreName,
        theatreId,
        index
    } = props;

    const removeAuditorium = props.removeAuditorium;

    const getDeleteBtn = (id) => {
        if (getRole() === 'admin')
            return <img src={img} alt='' style={{ height: '25px', width: '25px', cursor: 'pointer' }} onClick={() => removeAuditorium(id)} />
    }

    return (
        <tr>
            <td>{index + 1}</td>
            <td>{name}</td>
            <td>{theatreName}</td>
            <td>{getDeleteBtn(id)}</td>
        </tr>
    )
}

export default AuditoriumItem
