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
import ActorList from './components/Admin/ActorActions/ActorList'
import AddTheatre from './components/Admin/TheatreActions/AddTheatre'
import ShowAllTheatres from './components/Admin/TheatreActions/ShowAllTheatres'

export const AddActorContext = React.createContext();
export const AddTheatreContext = React.createContext();

function App() {
  const [context, setContext] = useState(false)
  return (
    <AddActorContext.Provider value={[context, setContext]}>
      <Router>
        <LoginHeader />
        <SideMenu />
        <Switch>

          <Redirect exact from="/" to="/showlist" />
          <Route path="/userprofile" component={UserProfile} />
          <Route path="/showlist" component={ShowList} />
          <Route path="/pieceactivelist" component={PieceActiveList} />
          <Route path="/piecealllist" component={PieceAllList} />
          {getRole() === 'admin' && <Route path={"/addactor"} component={AddActor} />}
          {getRole() === 'admin' && <Route path={"/actorlist"} component={ActorList} />}
          {getRole() === 'admin' && <Route path={"/addtheatre"} component={AddTheatre} />}
          {getRole() === 'admin' && <Route path={"/showalltheatres"} component={ShowAllTheatres} />}

          <Redirect exact from="*" to="/showlist" />
        </Switch>
        <NotificationContainer />
      </Router>
    </AddActorContext.Provider>
  );
}

export default App;
