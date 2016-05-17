import React from 'react'
import { Router, Route, Redirect } from 'react-router'

import App from './app'
import UserOverview from './users/UserOverview'
import SingleUserView from './users/SingleUserView'
import RoleOverview from './roles/RoleOverview'
import SingleRoleView from './roles/SingleRoleView'
import PermissionOverview from './permissions/PermissionOverview'

const AppRouter = ({ history }) => (
  <Router history={history}>
    <Redirect from="/" to="users" />
    <Route path="/" component={App}>
      <Route path="users" component={UserOverview}/>
      <Route path="users/:key" component={SingleUserView} />
      <Route path="roles" component={RoleOverview} />
      <Route path="roles/:key" component={SingleRoleView} />
      <Route path="permissions" component={PermissionOverview} />
    </Route>
  </Router>
)

export default AppRouter
