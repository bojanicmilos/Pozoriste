import React, { useState, useEffect } from 'react'
import { serviceConfig } from '../../../AppSettings/serviceConfig'
import Spinner from '../../Spinner'
import '../../../style/spinner.css'
import PieceItem from '../Items/PieceItem'

const ShowAllActivePieces = () => {
    const [pieces, setShowAllActivePieces] = useState([])
    const [isLoading, setIsLoading] = useState(true)

    useEffect(() => {
        getAllActivePieces();
    }, [])

    const getAllActivePieces = () => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            }
        };

        fetch(`${serviceConfig.baseURL}/api/Pieces/active`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response);
                }
                return response.json();
            })
            .then((data) => {
                if (data) {
                    setShowAllActivePieces(prevState => ([...prevState, ...data]))
                    setIsLoading(false)
                }
            })
            .catch((response) => {
                console.log(response)
                setIsLoading(false);
            })
    }

    const fillPageWithPieces = () => {
        return pieces.map((piece) => {
            return (
                <PieceItem key={piece.id} {...piece} />
            )
        })
    }

    return (
        <ul className='piece-container'>
            { isLoading ? <Spinner></Spinner> : fillPageWithPieces()}
        </ul>
    )
}

export default ShowAllActivePieces