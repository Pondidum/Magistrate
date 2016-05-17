import 'babel-polyfill'

import React from 'react'
import { render } from 'react-dom'

import { createStore, applyMiddleware } from 'redux';
import { Provider } from 'react-redux'
import remoteMiddleware from './infrastructure/remoteMiddleware'
import { Router, Route, Redirect, hashHistory } from 'react-router'
import { syncHistory, routerReducer } from 'react-router-redux'

import { updateUsers, updateRoles, updatePermissions, setTileSize } from './actions'
import rootReducer from './reducers'

import App from './components/app'
import UserOverview from './components/users/UserOverview'
import SingleUserView from './components/users/SingleUserView'
import RoleOverview from './components/roles/RoleOverview'
import SingleRoleView from './components/roles/SingleRoleView'
import PermissionOverview from './components/permissions/PermissionOverview'

const reduxRouterMiddleware = syncHistory(hashHistory);
const createStoreWithMiddleware = applyMiddleware(remoteMiddleware, reduxRouterMiddleware)(createStore);
const store = createStoreWithMiddleware(rootReducer);

var tileSize = reactCookie.load('tileSize') || Tile.sizes.large
store.dispatch(setTileSize(tileSize));
store.dispatch(updateUsers())
store.dispatch(updateRoles())
store.dispatch(updatePermissions())

render(
  <Provider store={store}>
    <Router history={hashHistory}>
      <Redirect from="/" to="users" />
      <Route path="/" component={App}>
        <Route path="users" component={UserOverview}/>
        <Route path="users/:key" component={SingleUserView} />
        <Route path="roles" component={RoleOverview} />
        <Route path="roles/:key" component={SingleRoleView} />
        <Route path="permissions" component={PermissionOverview} />
      </Route>
    </Router>
  </Provider>,
  document.getElementById('container')
)
