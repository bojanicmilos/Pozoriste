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
import { useContext } from 'react'
import { useState } from 'react'
import AddShow from './components/Admin/ShowActions/AddShow'
import ActorList from './components/Admin/ActorActions/ActorList'
import AddTheatre from './components/Admin/TheatreActions/AddTheatre'
import ShowAllTheatres from './components/Admin/TheatreActions/ShowAllTheatres'
import AddPiece from './components/Admin/PieceActions/AddPiece'
import { isUserLogged } from './components/globalStorage/IsUserLogged'

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
          <Route path="/pieceactivelist" component={PieceActiveList} />
          {getRole() === 'admin' && <Route path={"/piecealllist"} component={PieceAllList} />}
          {isUserLogged() && <Route path="/userprofile" component={UserProfile} />}
          {getRole() === 'admin' && <Route path={"/addpiece"} component={AddPiece} />}
          {getRole() === 'admin' && <Route path={"/addactor"} component={AddActor} />}
          {getRole() === 'admin' && <Route path={"/actorlist"} component={ActorList} />}
          {getRole() === 'admin' && <Route path={"/addtheatre"} component={AddTheatre} />}
          {getRole() === 'admin' && <Route path={"/showalltheatres"} component={ShowAllTheatres} />}
          {getRole() === 'admin' && <Route path={"/addshow"} component={AddShow} />}

          <Redirect exact from="*" to="/showlist" />
        </Switch>
        <NotificationContainer />
      </Router>
    </Context.Provider>
  );
}

export default App;
