import 'babel-polyfill'

import React from 'react'
import { render } from 'react-dom'

import { createStore, applyMiddleware } from 'redux';
import { Provider } from 'react-redux'

import rootReducer from './reducers'


import App from './components/app'

var socket = new WebSocket(`ws://${location.hostname}:8090`);
socket.onopen = () => console.log("opened");
socket.onclose = () => console.log("closed");

const store = createStore(rootReducer);

socket.onmessage = (e) => {
  var state = JSON.parse(e.data);
  console.log(state);
}

render(
  <Provider store={store}>
    <App />
  </Provider>,
  document.getElementById('container')
)
