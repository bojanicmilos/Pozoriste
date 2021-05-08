import React from 'react'
import { useEffect, useState } from 'react'
import { NotificationManager } from 'react-notifications'
import { serviceConfig } from '../../../AppSettings/serviceConfig'

const AddPiece = () => {
    const [piece, setPiece] = useState({
        title: '',
        year: '',
        genre: '',
        isActive: '',
        description: ''
    })

    const handleSubmit = (e) => {
        e.preventDefault();
        if (piece.genre === '' || piece.isActive === '') {
            NotificationManager.error('Popunite sva prazna polja!')
        }
        else {
            addPiece();
            setPiece({title: '', year: '', genre: '', isActive: '', description: ''})
            document.getElementById('genre').selectedIndex = 0
            document.getElementById('isActive').selectedIndex = 0
        }

    }

    const addPiece = () => {
        const json = {
            title: piece.title,
            year: +piece.year,
            description: piece.description,
            genre: +piece.genre,
            isActive: (+piece.isActive) ? true : false,
        }

        const requestOptions = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(json)
        }
        console.log(json);
        fetch(`${serviceConfig.baseURL}/api/Pieces/create`, requestOptions)
            .then((response) => {
                return response.json();
            })
            .then((json) => {
                if (json.errorMessage) {
                    NotificationManager.error(json.errorMessage)
                }
                else {
                    NotificationManager.success('Komad uspesno dodat!')
                }
            })
            .catch(() => {
                NotificationManager.error('Greska')
            })
    }

    return (
        <div className='add-piece-page'>
            <h2 style={{ textAlign: 'center' }}><strong>Dodaj novi komad: </strong></h2>
            <br />
            <form onSubmit={handleSubmit}>
                <input
                    value={piece.title}
                    onChange={(e) => setPiece({ ...piece, title: e.target.value })}
                    placeholder='Naziv komada' type='text' maxLength='70' required
                    className='form-control'
                />
                <input
                    value={piece.year}
                    onChange={(e) => setPiece({ ...piece, year: e.target.value })}
                    placeholder='Godina izdanja' type='number' min={1000} max={2050} required
                    className='form-control'
                />
                <select onChange={(e) => setPiece({ ...piece, genre: e.target.value })}
                    id='genre' className="form-select"
                    style={{ width: 115, height: 25 }}>
                    <option value=''>Izaberi zanr</option>
                    <option value='0'>DRAMA</option>
                    <option value='1'>KOMEDIJA</option>
                    <option value='2'>TRAGEDIJA</option>
                </select>
                <select onChange={(e) => setPiece({ ...piece, isActive: e.target.value })} id='isActive' className="form-select" style={{ width: 115, height: 25 }}>
                    <option value=''>Da li je komad aktivan/neaktivan</option>
                    <option value='1'>Aktivan</option>
                    <option value='0'>Neaktivan</option>
                </select>
                <textarea className="form-control" id="exampleFormControlTextarea1" value={piece.description} onChange={(e) => setPiece({ ...piece, description: e.target.value })} rows="3" maxLength='450' placeholder='Deskripcija' required></textarea>
                <button className='btn btn-primary' type='submit'>Dodaj komad</button>
            </form>
        </div>
    )


}

export default AddPiece