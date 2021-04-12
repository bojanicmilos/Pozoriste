import React, { useState } from 'react'
import { Button } from 'react'
import { getRole } from '../../globalStorage/RoleCheck'
import { NotificationManager } from 'react-notifications'
import { serviceConfig } from '../../../AppSettings/serviceConfig'


const PieceItem = (props) => {
    const [isActive, setIsActive] = useState(props.isActive);
    const removePiece = props.removePiece;

    const {
        id,
        title,
        description,
        year,
        genre,
    } = props;

    const activateDeactivatePiece = (id) => {
        const requestOptions = {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            }
        };

        fetch(`${serviceConfig.baseURL}/api/Pieces/activateDeactivate/${id}`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response);
                }
                return response.json();
            })
            .then((response) => {
                if (isActive) {
                    NotificationManager.success("Successfully deactivated piece");
                }
                else {
                    NotificationManager.success("Successfully activated piece");
                }

                setIsActive(!isActive);
            })
            .catch((response) => {
                NotificationManager.error("Can't deactivate piece that have future shows");
            });
    };



    const getBtn = (isActive, id) => {
        if (isActive) {
            if (getRole() === "admin")
                return <button style={{ backgroundColor: 'red' }} onClick={() => activateDeactivatePiece(id)}>DEAKTIVIRAJ</button>
        }
        else if (getRole() === "admin")
            return <button style={{ backgroundColor: 'green' }} onClick={() => activateDeactivatePiece(id)}>AKTIVIRAJ</button>
    }

    const getDeleteBtn = (id) => {
        if (getRole() === "admin")
            return <button style={{ backgroundColor: 'red' }} onClick={() => removePiece(id)}>OBRISI</button>
    }

    return (
        <li key={id} className='pieceItem'>
            <p> Naziv komada: {title} </p>
            <p> Zanr: {genre} </p>
            <p> O delu: {description} </p>
            <p> Godina: {year} </p>
            {getBtn(isActive, id)}
            {getDeleteBtn(id)}
        </li>
    )
}

export default PieceItem