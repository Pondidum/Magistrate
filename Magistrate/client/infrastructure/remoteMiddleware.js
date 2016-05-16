const remoteMiddleware = store => next => action => {

  const meta = action.meta;

  if (meta && meta.remote) {

    var options = {}

    options.url = meta.url;

    if (meta.method)
      options.method = meta.method

    if (meta.data)
      options.data = JSON.stringify(action[meta.data])

    if (meta.success)
      options.success = data => meta.success(store, data)

    if (meta.error)
      options.error = (xhr, status, ex) => meta.error(store, xhr, status, ex)

    $.ajax(options);
  }

  next(action);
}

export default remoteMiddleware
