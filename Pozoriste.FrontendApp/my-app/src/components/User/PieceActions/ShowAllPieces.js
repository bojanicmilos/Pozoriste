import React, { useState, useEffect, Button } from 'react'
import { serviceConfig } from '../../../AppSettings/serviceConfig'
import Spinner from '../../Spinner'
import '../../../style/spinner.css'
import PieceItem from '../Items/PieceItem'
import { getRole } from '../../globalStorage/RoleCheck'
import 'react-notifications/lib/notifications.css';
import { NotificationContainer, NotificationManager } from 'react-notifications';

const ShowAllPieces = () => {
    const [pieces, setShowAllPieces] = useState([])
    const [isLoading, setIsLoading] = useState(true)

    useEffect(() => {
        getAllPieces();
    }, [])

    const getAllPieces = () => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            }
        };

        fetch(`${serviceConfig.baseURL}/api/Pieces/all`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response);
                }
                return response.json();
            })
            .then((data) => {
                if (data) {
                    setShowAllPieces(prevState => ([...prevState, ...data]))
                    setIsLoading(false)
                }
            })
            .catch((response) => {
                setIsLoading(false);
            })
    }

    const removePiece = (id) => {
        const requestedOptions = {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json"
            },
        };

        fetch(`${serviceConfig.baseURL}/api/Pieces/${id}`, requestedOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response);
                }

                let piecesFiltered = pieces;
                piecesFiltered = piecesFiltered.filter((piece) => piece.id !== id);
                setShowAllPieces(piecesFiltered);
                NotificationManager.success("Uspesno obrisan komad!");
            })
            .catch((response) => {
                NotificationManager.error("Ne moze se obrisati komad koji ima buduce predstave !");
            })
    }

    const fillPageWithPieces = () => {
        return pieces.map((piece) => {
            return (
                <PieceItem key={piece.id} {...piece} removePiece={removePiece} />

            )
        })
    }

    return (
        <ul className='piece-container'>
            { isLoading ? <Spinner></Spinner> : fillPageWithPieces()}
        </ul>
    )
}

export default ShowAllPieces