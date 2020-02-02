"use strict";
const path = require('path');
const webpack = require('webpack');
const bundleFileName = 'bundle';


module.exports = {
  mode: "development",
  entry: ['./src/js/app.js', './src/sass/app.scss'],
  output: {
    filename: bundleFileName + '.js',
    path: path.resolve(__dirname, 'wwwroot/dist'),
    publicPath: '/'
  },
  module: {
    rules: [
      {
        test: /\.scss$/,
        use: [
          {
            loader: 'file-loader',
            options: {
              name: bundleFileName + '.css'
            }
          },
          {
            loader: 'extract-loader'
          },
          {
            loader: "css-loader",
            options: {
              minimize: true
            }
          },
          {
            loader: "sass-loader"
          }
        ]
      },
      {
        test: /\.css$/,
        use: ['style-loader', 'css-loader']
      }
    ]
  },
  plugins: [
    new webpack.ProvidePlugin({
      $: 'jquery',
      jQuery: 'jquery'
    }),
  ],
}