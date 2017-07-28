'use strict';
const express = require('express');
const app = express();
const bodyParser = require('body-parser');
const port = process.env.PORT || 3000;


app.use(bodyParser.urlencoded({extended:false}));
app.use(bodyParser.json());
app.get('/all/:latitud/:longitud',(req,res)=>{
    res.send({message:'descripcion'})
});

app.listen(port,()=>{
    console.log(`api rest corriendo en http://localhost:${port}`)
});