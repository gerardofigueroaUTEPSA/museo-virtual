'use strict';

const mongoose = require('mongoose'),
	  app = require('./app'),
	  config = require('./config');

mongoose.connect(config.db, (error, response) => {
	if(error)
		return console.log('Error al conectar con la base de datos ' + error)
	console.log('Conexion a la base de datos establecida...')
	app.listen(config.port, () => {
		console.log('API REST corriendo en http://localhost:' + config.port)
	})
})