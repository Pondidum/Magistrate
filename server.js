var webpack = require('webpack')
var webpackDevMiddleware = require('webpack-dev-middleware')
var webpackHotMiddleware = require('webpack-hot-middleware')
var config = require('./webpack.config')
var express = require('express')

var app = new express()
var port = 3000

var compiler = webpack(config)
var root = __dirname + '/Magistrate/client/';

app.use(webpackDevMiddleware(compiler, { noInfo: true, publicPath: '/static/' }))
app.use(webpackHotMiddleware(compiler))

app.use("/css", express.static(root + 'css'));
app.use("/js", express.static(root + 'js'));
app.use("/fonts", express.static(root + 'fonts'));

app.get("/", function(req, res) {
  res.sendFile(root + 'app.htm')
})

app.listen(port, function(error) {
  if (error) {
    console.error(error)
  } else {
    console.info("==> 🌎  Listening on port %s. Open up http://localhost:%s/ in your browser.", port, port)
  }
})
