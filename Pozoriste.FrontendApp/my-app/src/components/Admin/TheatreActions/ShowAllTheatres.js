import React, { useState, useEffect } from 'react'
import { NotificationManager } from 'react-notifications'
import { serviceConfig } from '../../../AppSettings/serviceConfig'
import Spinner from '../../Spinner'
import AuditoriumItem from '../Items/AuditoriumItem'
import TheatreItem from '../Items/TheatreItem'
import Table from 'react-bootstrap/Table'

const ShowAllTheatres = () => {
    const [theatres, setShowAllTheatres] = useState([])
    const [isLoading, setIsLoading] = useState(true)

    useEffect(() => {
        getAllTheatres();
    }, [])

    const getAllTheatres = () => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        };

        fetch(`${serviceConfig.baseURL}/api/Theatres`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response);
                }
                return response.json();
            })
            .then((data) => {
                if (data) {
                    setShowAllTheatres(prevState => ([...prevState, ...data]))
                    setIsLoading(false)
                }
            })
            .catch((response) => {
                NotificationManager.error('Greska prilikom prikazivanja pozorista!');
            })
    }

    const removeTheatre = (id) => {
        const requestOptions = {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            },
        };
        fetch(`${serviceConfig.baseURL}/api/Theatres/${id}`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return Promise.reject(response);
                }

                let theatresFiltered = theatres;
                theatresFiltered = theatresFiltered.filter((theatre) => theatre.id !== id);
                setShowAllTheatres(theatresFiltered);
                NotificationManager.success('Uspesno obrisano pozoriste!');
            })
            .catch((response) => {
                NotificationManager.error('Nije moguce obrisati teatar!')
            })
    }

    const fillPageWithTheatres = () => {
        return theatres.map((theatre, index) => {
            return (
                <TheatreItem key={theatre.id} {...theatre} index={index} removeTheatre={removeTheatre} />
            )
        })
    };

    return (
        <React.Fragment>
            {isLoading ? <Spinner></Spinner> : <Table className='white-table' striped bordered hover variant='white' >
                <thead>
                    <tr>
                        <th>#</th>
                        <th>Ime pozorista</th>
                        <th>↓</th>
                    </tr>
                </thead>
                <tbody>
                    {fillPageWithTheatres()}
                </tbody>
            </Table>}
        </React.Fragment>

    )
}

export default ShowAllTheatres
