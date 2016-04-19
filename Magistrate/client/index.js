import 'babel-polyfill'

import React from 'react'
import { render } from 'react-dom'

import { createStore, applyMiddleware } from 'redux';
import { Provider } from 'react-redux'
import remoteMiddleware from './infrastructure/remoteMiddleware'
import { Router, Route, hashHistory } from 'react-router'
import { syncHistory, routerReducer } from 'react-router-redux'

import { setState, setTileSize } from './actions'
import rootReducer from './reducers'

import App from './components/app'
import UserOverview from './components/users/UserOverview'
import SingleUserView from './components/users/SingleUserView'
import RoleOverview from './components/roles/RoleOverview'
import SingleRoleView from './components/roles/SingleRoleView'
import PermissionOverview from './components/permissions/PermissionOverview'

var socket = new WebSocket(`ws://${location.hostname}:8090`);
socket.onopen = () => console.log("opened");
socket.onclose = () => console.log("closed");

const reduxRouterMiddleware = syncHistory(hashHistory);
const createStoreWithMiddleware = applyMiddleware(remoteMiddleware(socket), reduxRouterMiddleware)(createStore);
const store = createStoreWithMiddleware(rootReducer);

socket.onmessage = (e) => {
  var state = JSON.parse(e.data);
  store.dispatch(setState(state));
}

var tileSize = reactCookie.load('tileSize') || Tile.sizes.large
store.dispatch(setTileSize(tileSize));

render(
  <Provider store={store}>
    <Router history={hashHistory}>
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
