import 'babel-polyfill'

import React from 'react'
import { render } from 'react-dom'

import { createStore, applyMiddleware } from 'redux';
import { Provider } from 'react-redux'
import remoteMiddleware from './infrastructure/remoteMiddleware'
import { hashHistory } from 'react-router'
import { syncHistory, routerReducer } from 'react-router-redux'

import { updateUsers, updateRoles, updatePermissions, setTileSize } from './actions'
import rootReducer from './reducers'
import AppRouter from './components/AppRouter'

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
    <AppRouter history={hashHistory} />
  </Provider>,
  document.getElementById('container')
)
