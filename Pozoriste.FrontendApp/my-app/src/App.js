import React from 'react'
import { BrowserRouter as Router, Route, Switch, Redirect } from 'react-router-dom'
import LoginHeader from './components/LoginHeader'
import ShowList from './components/User/ShowActions/ShowList'
import UserProfile from './components/UserProfile'
import SideMenu from './components/SideMenu'
import PieceActiveList from './components/User/PieceActions/ShowAllActivePieces'
import PieceAllList from './components/User/PieceActions/ShowAllPieces'
import { NotificationContainer } from 'react-notifications';
import AddActor from './components/Admin/ActorActions/AddActor'
import { getRole } from './components/globalStorage/RoleCheck'
import { useState } from 'react'
import AddShow from './components/Admin/ShowActions/AddShow'
import ActorList from './components/Admin/ActorActions/ActorList'
import AddTheatre from './components/Admin/TheatreActions/AddTheatre'
import ShowAllTheatres from './components/Admin/TheatreActions/ShowAllTheatres'
import AddPiece from './components/Admin/PieceActions/AddPiece'
import { isUserLogged } from './components/globalStorage/IsUserLogged'
import ShowReservation from './components/User/ShowActions/ShowReservation'
import ShowAllAuditoriums from './components/Admin/AuditoriumActions/ShowAllAuditoriums'
import AddAuditorium from './components/Admin/AuditoriumActions/AddAuditorium'
import 'react-notifications/lib/notifications.css';

export const Context = React.createContext();

function App() {
  const [context, setContext] = useState(false)
  return (
    <Context.Provider value={[context, setContext]}>
      <Router>
        <LoginHeader />
        <SideMenu />
        <Switch>
          <Redirect exact from="/" to="/showlist" />
          <Route path="/showlist" component={ShowList} />
          {isUserLogged() && <Route path="/showreservation/:id" children={<ShowReservation />} />}
          <Route path="/pieceactivelist" component={PieceActiveList} />
          {getRole() === 'admin' && <Route path={"/piecealllist"} component={PieceAllList} />}
          {isUserLogged() && <Route path="/userprofile" component={UserProfile} />}
          {getRole() === 'admin' && <Route path={"/addpiece"} component={AddPiece} />}
          {getRole() === 'admin' && <Route path={"/addactor"} component={AddActor} />}
          {getRole() === 'admin' && <Route path={"/actorlist"} component={ActorList} />}
          {getRole() === 'admin' && <Route path={"/addtheatre"} component={AddTheatre} />}
          {getRole() === 'admin' && <Route path={"/showalltheatres"} component={ShowAllTheatres} />}
          {getRole() === 'admin' && <Route path={"/addshow"} component={AddShow} />}
          {getRole() === 'admin' && <Route path={"/showallauditoriums"} component={ShowAllAuditoriums} />}
          {getRole() === 'admin' && <Route path={"/addauditorium"} component={AddAuditorium} />}

          <Redirect exact from="*" to="/showlist" />
        </Switch>
        <NotificationContainer />
      </Router>
    </Context.Provider>
  );
}

export default App;
