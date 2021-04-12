import React from 'react'
import { BrowserRouter as Router, Route, Switch, Redirect } from 'react-router-dom'
import LoginHeader from './components/LoginHeader'
import ShowList from './components/User/ShowActions/ShowList'
import UserProfile from './components/UserProfile'
import SideMenu from './components/SideMenu'
import PieceActiveList from './components/User/PieceActions/ShowAllActivePieces'
import PieceAllList from './components/User/PieceActions/ShowAllPieces'
import { NotificationContainer } from 'react-notifications';
function App() {
  return (
    <>
      <Router>
        <LoginHeader />
        <SideMenu />
        <Switch>

          <Redirect exact from="/" to="/showlist" />
          <Route path="/userprofile" component={UserProfile} />
          <Route path="/showlist" component={ShowList} />
          <Route path="/pieceactivelist" component={PieceActiveList} />
          <Route path="/piecealllist" component={PieceAllList} />

          {/* <Route path="/userprofile" component={UserProfile} /> */}
          {/* <Route path="/showlist" component={ShowList} /> */}


        </Switch>
        <NotificationContainer />
      </Router>
    </>
  );
}

export default App;
