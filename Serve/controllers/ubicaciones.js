'use strict';

const ubicacionesModel = require('../modelos/ubicaciones')

function getTotalUbicaciones(req, res){
	ubicacionesModel.find({}, (error, Coordenadas) => {
		if(error)
			return res.status(500).send({message: 'Proble interno del servidor'})
		if(!Coordenadas)
			return res.status(400).send({message: 'No existen ubicaciones disponibles.'})
		res.status(200).send({Coordenadas})
	})
}

function postUbicaiones(req, res){
	let ubicacion = new ubicacionesModel();
	ubicacion.latitud = req.body.latitud
	ubicacion.longitud = req.body.longitud
	ubicacion.nombre = req.body.nombre
	ubicacion.detalle = req.body.detalle
	ubicacion.save((error, ubicacionGuardada) => {
		if(error)
			return res.status(500).send({message: 'Problema interno del servidor.'})
		res.status(200).send({ubicacionGuardada})
	})
}

function deleteUbicaciones(req, res){
	let ubicacionesId = req.params.idUbicaciones

	ubicacionesModel.findById(ubicacionesId, (error, responseIfExistProduct) => {
		if(error) 
			return res.status(500).send({message: 'Error al borrar el productStored ' + error})
		responseIfExistProduct.remove(error => {
			if(error) 
				return res.status(500).send({message: 'Error al borrar el productStored ' + error})
			})
		res.status(200).send({message: 'El producto ha sido eliminado'})
		})
}

module.exports = {
	getTotalUbicaciones,
	postUbicaiones
}