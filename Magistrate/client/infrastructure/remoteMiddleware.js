const remoteMiddleware = socket => store => next => action => {

  if (action.meta && action.meta.remote)
    socket.send(JSON.stringify(action));


  next(action);
}

export default remoteMiddleware
