import React from 'react'
import { BrowserRouter as Router, Route, Switch, Redirect } from 'react-router-dom'
import LoginHeader from './components/LoginHeader'
import ShowList from './components/User/ShowActions/ShowList'
import UserProfile from './components/UserProfile'

function App() {
  return (
    <>
      <LoginHeader />
      <Router>
        <Switch>
          <Redirect exact from="/" to="/showlist" />
          <Route path="/userprofile" component={UserProfile} />
          <Route path="/showlist" component={ShowList} />
          {/* <Route path="/userprofile" component={UserProfile} /> */}
          {/* <Route path="/showlist" component={ShowList} /> */}
        </Switch>
      </Router>
    </>
  );
}

export default App;
