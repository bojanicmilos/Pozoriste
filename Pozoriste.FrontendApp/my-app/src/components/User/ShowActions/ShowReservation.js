import React, { useCallback, useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { getUserName } from '../../globalStorage/GetUserName'
import { serviceConfig } from '../../../AppSettings/serviceConfig'
import { NotificationManager } from 'react-notifications'
import { getRole } from '../../globalStorage/RoleCheck'
import { Container, Row, Col, Card } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faShoppingCart, faCouch } from "@fortawesome/free-solid-svg-icons";

const ShowReservation = () => {
    const { id } = useParams();
    const [state, setState] = useState({
        show: {},
        user: {},
        seats: [],
        maxRow: 0,
        maxNumberOfRow: 0,
        reservedSeats: [],
        currentReservationSeats: []
    });

    let allButtons;

    const getReservedSeats = useCallback(() => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'Application/json'
            }
        }

        fetch(`${serviceConfig.baseURL}/api/reservations/getbyshowid/${id}`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return response.reject(response);
                }
                return response.json();
            })
            .then((json) => {
                setState(prevState => ({ ...prevState, reservedSeats: json }))
            })
            .catch((response) => {
                NotificationManager.error('Bezuspesno ucitani podaci !')
            })
    }, [id])


    const getShow = useCallback(() => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'Application/json'
            }
        }

        fetch(`${serviceConfig.baseURL}/api/shows/${id}`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return response.reject(response);
                }
                return response.json();
            })
            .then((json) => {
                setState((prevState) => ({ ...prevState, show: json }))
                getReservedSeats();
            })
            .catch(() => {
                NotificationManager.error('Bezuspesno ucitani podaci !')
            })
    }, [id, getReservedSeats])



    const getAuditoriumSeats = useCallback(() => {

        if (state.show.auditoriumId === undefined) {
            return;
        }
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'Application/json'
            }
        }

        fetch(`${serviceConfig.baseURL}/api/seats/numberofseats/${state.show.auditoriumId}`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return response.reject(response);
                }
                return response.json();
            })
            .then((json) => {
                setState(prevState => ({ ...prevState, seats: json.seats, maxRow: json.maxRow, maxNumberOfRow: json.maxNumber }))
            })
            .catch((response) => {
                NotificationManager.error('Bezuspesno ucitani podaci !')
            })
    }, [state.show.auditoriumId])

    useEffect(() => {
        getUser();
        getShow();
    }, [getShow])

    useEffect(() => {
        getAuditoriumSeats();
    }, [getAuditoriumSeats])


    const getUser = () => {
        const uName = getUserName();

        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'Application/json'
            }
        }

        fetch(`${serviceConfig.baseURL}/api/users/username/${uName}`, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    return response.reject(response);
                }
                return response.json()
            })
            .then((json) => {
                setState(prevState => ({ ...prevState, user: json }))
            })
            .catch((response) => {
                NotificationManager.error('Ulogujte se kako biste rezervisali sedista ! ')
            })
    }

    const makeReservation = (
        e
    ) => {
        e.preventDefault();

        if (
            getRole() === "user" ||
            getRole() === "admin"
        ) {
            console.log('IN MAKE RESERVATION');
            const showId = id;

            const { currentReservationSeats } = state;

            const data = {
                showId: +showId,
                seatIds: currentReservationSeats,
                userId: state.user.id,
            };

            console.log('DATA IN MAKE RESERVATION ', data);

            const requestOptions = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(data),
            };

            fetch(`${serviceConfig.baseURL}/api/reservations/MakeReservation`, requestOptions)
                .then((response) => {
                    if (!response.ok) {
                        return Promise.reject(response);
                    }
                    return response.statusText;
                })
                .then((result) => {
                    NotificationManager.success(
                        "Uspesno ste rezervisali sedista !"
                    );
                    setTimeout(() => {
                        window.location.reload();
                    }, 1500);
                })
                .catch((response) => {
                    NotificationManager.error(response.message || response.statusText);
                });
        } else {
            NotificationManager.error("Ulogujte se kako biste izvrsili rezervaciju sedista.");
        }
    };
    const renderRows = (rows, seats) => {
        const rowsRendered = [];
        if (state.seats.length > 0) {
            for (let i = 0; i < rows; i++) {
                const startingIndex = i * seats;
                const maxIndex = (i + 1) * seats;

                rowsRendered.push(
                    <tr key={i}>{renderSeats(seats, i, startingIndex, maxIndex)}</tr>
                );
            }
        }
        return rowsRendered;
    };

    const checkIfSeatIsTaken = (currentSeatId) => {
        for (let i = 0; i < state.reservedSeats.length; i++) {
            if (state.reservedSeats[i].id === currentSeatId) {
                return true;
            }
        }
        return false;
    };

    const checkIfSeatIsCurrentlyReserved = (currentSeatId) => {
        return state.currentReservationSeats.some(item => item.id === currentSeatId);
    };

    const getSeatByPosition = (row, number) => {
        for (let i = 0; i < state.seats.length; i++) {
            if (state.seats[i].number === number && state.seats[i].row === row) {
                return state.seats[i];
            }
        }
    };

    const getSeatById = (seatId) => {
        console.log(seatId)
        for (let i = 0; i < state.seats.length; i++) {
            if (state.seats[i].id === seatId) {
                return state.seats[i];
            }
        }
    };

    const getAllButtons = () => {
        if (!allButtons) {
            allButtons = document.getElementsByClassName("seat");
            for (let i = 0; i < allButtons.length; i++) {
                let seat = getSeatById(allButtons[i].value);
            }
        }
    };

    const markSeatAsGreenish = (seatId) => {
        getAllButtons();
        for (let i = 0; i < allButtons.length; i++) {
            if (seatId.toString() === allButtons[i].value) {
                allButtons[i].className = "seat nice-green-color";
            }
        }
    };

    const getButtonBySeatId = (seatId) => {
        getAllButtons();
        for (let i = 0; i < allButtons.length; i++) {
            if (seatId.toString() === allButtons[i].value) {
                return allButtons[i];
            }
        }
    };

    const markSeatAsBlue = (seatId) => {
        getAllButtons();
        for (let i = 0; i < allButtons.length; i++) {
            if (seatId.toString() === allButtons[i].value) {
                allButtons[i].className = "seat";
            }
        }
    };

    const markWholeRowSeatsAsBlue = () => {
        getAllButtons();
        for (let i = 0; i < allButtons.length; i++) {
            if (allButtons[i].className !== "seat seat-taken") {
                allButtons[i].className = "seat";
            }
        }
    };

    const renderSeats = (
        seats,
        row,
        startingIndex,
        maxIndex
    ) => {
        let renderedSeats = [];
        let seatIndex = startingIndex;
        if (state.seats.length > 0) {
            for (let i = 0; i < seats; i++) {
                let currentSeatId = state.seats[seatIndex].id;
                let currentlyReserved =
                    state.currentReservationSeats.filter(
                        (seat) => seat.id === currentSeatId
                    ).length > 0;
                let seatTaken =
                    state.reservedSeats.filter((seat) => seat.id === currentSeatId)
                        .length > 0;

                renderedSeats.push(
                    <button
                        onClick={(e) => {
                            let currentRow = row;
                            let currentNumber = i;
                            let currSeatId = currentSeatId;

                            let leftSeatIsCurrentlyReserved = false;
                            let leftSeatIsTaken = false;
                            let rightSeatIsCurrentlyReserved = false;
                            let rightSeatIsTaken = false;
                            let leftSeatProperties = getSeatByPosition(
                                currentRow + 1,
                                currentNumber
                            );
                            console.log('LEFT SEAT PROPERTIES', leftSeatProperties);
                            let rightSeatProperties = getSeatByPosition(
                                currentRow + 1,
                                currentNumber + 2
                            );
                            console.log('RIGHT SEAT PROPERTIES', rightSeatProperties);
                            let currentReservationSeats = state.currentReservationSeats;

                            if (leftSeatProperties) {
                                leftSeatIsCurrentlyReserved = checkIfSeatIsCurrentlyReserved(
                                    leftSeatProperties.id
                                );
                                console.log('LEFT SEAT BOOL', leftSeatIsCurrentlyReserved);
                                leftSeatIsTaken = checkIfSeatIsTaken(leftSeatProperties.id);
                            }

                            if (rightSeatProperties) {
                                rightSeatIsCurrentlyReserved = checkIfSeatIsCurrentlyReserved(
                                    rightSeatProperties.id
                                );
                                console.log('RIGHT SEAT BOOL', rightSeatIsCurrentlyReserved);
                                rightSeatIsTaken = checkIfSeatIsTaken(rightSeatProperties.id);
                            }

                            if (!checkIfSeatIsCurrentlyReserved(currSeatId)) {
                                if (
                                    state.currentReservationSeats.length !== 0 &&
                                    getButtonBySeatId(currentSeatId).className !==
                                    "seat nice-green-color"
                                ) {
                                    NotificationManager.error("Nije moguce rezervisati sedista ! Sedista moraju biti jedna pored drugog !");
                                    return;
                                }
                                if (
                                    !leftSeatIsCurrentlyReserved &&
                                    !leftSeatIsTaken &&
                                    leftSeatProperties
                                ) {
                                    markSeatAsGreenish(leftSeatProperties.id);
                                }
                                if (
                                    !rightSeatIsCurrentlyReserved &&
                                    !rightSeatIsTaken &&
                                    rightSeatProperties
                                ) {
                                    markSeatAsGreenish(rightSeatProperties.id);
                                }
                                if (
                                    state.currentReservationSeats.some(item => item.id === currentSeatId) === false
                                ) {
                                    currentReservationSeats.push({
                                        id: currentSeatId,
                                    });
                                }
                            } else {
                                if (
                                    leftSeatIsCurrentlyReserved ||
                                    rightSeatIsCurrentlyReserved
                                ) {
                                    markWholeRowSeatsAsBlue();
                                    currentReservationSeats = [];
                                } else {
                                    currentReservationSeats.splice(
                                        currentReservationSeats.indexOf({
                                            id: currentSeatId,
                                        }),
                                        1
                                    );
                                    if (
                                        !leftSeatIsCurrentlyReserved &&
                                        !leftSeatIsTaken &&
                                        leftSeatProperties
                                    ) {
                                        markSeatAsBlue(leftSeatProperties.id);
                                    }
                                    if (
                                        !rightSeatIsCurrentlyReserved &&
                                        !rightSeatIsTaken &&
                                        rightSeatProperties
                                    ) {
                                        markSeatAsBlue(rightSeatProperties.id);
                                    }

                                    if (
                                        leftSeatIsCurrentlyReserved ||
                                        rightSeatIsCurrentlyReserved
                                    ) {
                                        setTimeout(() => {
                                            markSeatAsGreenish(currentSeatId);
                                        }, 150);
                                    }
                                }
                                setState({
                                    ...state,
                                    currentReservationSeats: currentReservationSeats,
                                });
                            }
                            setState({
                                ...state,
                                currentReservationSeats: currentReservationSeats,
                            });
                        }}
                        className={
                            seatTaken
                                ? "seat seat-taken"
                                : currentlyReserved
                                    ? "seat seat-current-reservation"
                                    : "seat"
                        }
                        value={currentSeatId}
                        key={`row${row}-seat${i}`}
                    >
                        <FontAwesomeIcon className="fa-2x couch-icon" icon={faCouch} />
                    </button>
                );
                if (seatIndex < maxIndex) {
                    seatIndex += 1;
                }
            }
        }

        return renderedSeats;
    };

    const fillTableWithData = () => {
        let auditorium = renderRows(state.maxRow, state.maxNumberOfRow);
        return (
            <Card.Body>
                <Card.Title>
                    <span className="card-title-font">{state.show.pieceTitle}</span>
                    <span className="float-right">

                    </span>
                </Card.Title>
                <hr />
                <Card.Subtitle className="mb-2 text-muted">
                    Godina: {state.show.pieceYear}
                    <span className="float-right">
                        Vreme predstave: {state.show.showTime}h
              </span>
                </Card.Subtitle>
                <hr />
                <Card.Text>
                    <Row className="mt-2">
                        <Col className="justify-content-center align-content-center">
                            <form>
                                <div className="payment">
                                    <h4 className="text-center">Izaberite sedista:</h4>
                                    <table className="payment-table">
                                        <thead className="payment-table__head">
                                            <tr className="payment-table__row">
                                                <th className="payment-table__cell">Ulaznice</th>
                                                <th className="payment-table__cell">Cena</th>
                                                <th className="payment-table__cell">Ukupno</th>
                                            </tr>
                                        </thead>
                                        <tbody className="payment-table__row">
                                            <tr>
                                                <td className="payment-table__cell">
                                                    <span>{state.currentReservationSeats.length}</span>
                                                </td>
                                                <td className="payment-table__cell">{state.show.ticketPrice}</td>
                                                <td className="payment-table__cell">
                                                    {state.currentReservationSeats.length * state.show.ticketPrice},00 RSD
                            </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <button
                                        onClick={(e) => makeReservation(e)}
                                        className="btn-payment"
                                    >
                                        Potvrdi
                        <FontAwesomeIcon
                                            className="text-primary mr-2 fa-1x btn-payment__icon"
                                            icon={faShoppingCart}
                                        />
                                    </button>
                                </div>
                            </form>
                            <div>
                                <Row className="justify-content-center">
                                    <table className="table-cinema-auditorium">
                                        <tbody>{auditorium}</tbody>
                                    </table>
                                    <div className="text-center text-white font-weight-bold cinema-screen">
                                        POZORISTE
                      </div>
                                </Row>
                            </div>
                        </Col>
                    </Row>
                    <hr />
                </Card.Text>
            </Card.Body>
        );
    };

    const showTable = fillTableWithData();
    return (
        <Container className='reservation-big-card'>
            <Row className="justify-content-center">
                <Col>
                    <Card className="mt-5 card-width">{showTable}</Card>
                </Col>
            </Row>
        </Container>
    );
};




export default ShowReservation
