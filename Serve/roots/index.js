'use strict';

const express = require('express'),
	  ubicacionesController = require('../controllers/ubicaciones'),
	  api = express.Router()

api.get('/ubicaciones', ubicacionesController.getTotalUbicaciones)
api.post('/ubicaciones', ubicacionesController.postUbicaiones)


module.exports = api