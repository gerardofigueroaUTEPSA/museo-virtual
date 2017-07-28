'use strict';

const mongoose = require('mongoose'),
	  schema = mongoose.Schema;

const ubicacionesSchema = schema({
	latitud: String,
	longitud: String,
	nombre: String,
	detalle: String
})

module.exports = mongoose.model('Detalles', ubicacionesSchema);