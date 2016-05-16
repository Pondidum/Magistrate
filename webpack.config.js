var path = require('path')
var webpack = require('webpack')

module.exports = {
  devtool: 'cheap-module-eval-source-map',
  entry: [
    './Magistrate/client/index'
  ],
  output: {
    path: path.join(__dirname, '/Magistrate/client/static'),
    filename: 'bundle.js',
    publicPath: '/Magistrate/client/static/'
  },
  plugins: [
    new webpack.optimize.OccurenceOrderPlugin(),
    new webpack.NoErrorsPlugin()
  ],
  module: {
    loaders: [
      {
        test: /\.js$/,
        loaders: [ 'babel' ],
        exclude: /node_modules/,
        include: __dirname
      }
    ]
  }
}


// When inside Redux repo, prefer src to compiled version.
// You can safely delete these lines in your project.
var reduxSrc = path.join(__dirname, '..', '..', 'src')
var reduxNodeModules = path.join(__dirname, '..', '..', 'node_modules')
var fs = require('fs')
if (fs.existsSync(reduxSrc) && fs.existsSync(reduxNodeModules)) {
  // Resolve Redux to source
  module.exports.resolve = { alias: { 'redux': reduxSrc } }
  // Our root .babelrc needs this flag for CommonJS output
  process.env.BABEL_ENV = 'commonjs'
  // Compile Redux from source
  module.exports.module.loaders.push({
    test: /\.js$/,
    loaders: [ 'babel' ],
    include: reduxSrc
  })
}
