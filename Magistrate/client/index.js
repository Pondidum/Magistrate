import 'babel-polyfill'
import React from 'react'
import { render } from 'react-dom'

import App from './components/app'

var socket = new WebSocket(`ws://${location.hostname}:8090`);
socket.onopen = () => console.log("opened");
socket.onclose = () => console.log("closed");

socket.onmessage = (e) => {
  var state = JSON.parse(e.data);
  console.log(state);
}

render(
  <App />,
  document.getElementById('container')
)
