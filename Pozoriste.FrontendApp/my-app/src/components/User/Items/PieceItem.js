import React, { useState } from 'react'
import { Button } from 'react'
import { getRole } from '../../globalStorage/RoleCheck'
import { NotificationManager } from 'react-notifications'
import { serviceConfig } from '../../../AppSettings/serviceConfig'
import img from '../../../images/delete-removebg-preview.png'

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
                    NotificationManager.success("Uspesno deaktivirana predstava!");
                }
                else {
                    NotificationManager.success("Uspesno aktivirana predstava!");
                }

                setIsActive(!isActive);
            })
            .catch((response) => {
                NotificationManager.error("Ne moze se deaktivirati komad koji ima buduce predstava!");
            });
    };



    const getBtn = (isActive, id) => {
        if (isActive) {
            if (getRole() === "admin")
                return <button className="btn btn-danger" style={{ marginBottom: 15 }} onClick={() => activateDeactivatePiece(id)}>DEAKTIVIRAJ</button>
        }
        else if (getRole() === "admin")
            return <button className="btn btn-success" style={{ marginBottom: 15 }} onClick={() => activateDeactivatePiece(id)}>AKTIVIRAJ</button>
    }

    const getDeleteBtn = (id) => {
        if (getRole() === "admin")
            return <button id='delete-btn-piece' className="btn btn-danger" style={{ marginLeft: 10, marginBottom: 15 }} onClick={() => removePiece(id)}><img src={img} alt='' style={{ height: '25px', width: '25px', cursor: 'pointer' }} /></button>
    }

    return (
        <li className='pieceItem'>
            <h4> Naziv komada:</h4> <p>{title} </p>
            <h4> Zanr:</h4> <p>{genre} </p>
            <h4> Godina:</h4> <p>{year} </p>
            <h4>O delu:</h4>
            <p className='pieceItem-description'> {description} </p>
            {getBtn(isActive, id)}
            {getDeleteBtn(id)}
        </li>
    )
}

export default PieceItem