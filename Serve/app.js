const express = require('express'),
	  bodyParser = require('body-parser'),
	  app = express(),
	  api = require('./roots/index')

app.all('/*', function(req, res, next){
	res.header("Access-Control-Allow-Origin", "*");
  	res.header("Access-Control-Allow-Headers", "X-Requested-With");
  	res.header("Access-Control-Allow-Methods", "GET, POST","PUT");
  	next();
})

app.use(bodyParser.urlencoded({extended: true}))
app.use(bodyParser.json())
app.use('/api', api)

module.exports = app