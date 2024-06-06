---
title: "Crea tu primera API con FLASK"
date: 2021-09-21
type: page
tags: 
- "flask"
- "api"
description: El día de hoy vamos a ver cómo crear una sencilla API con Flask. Este es un proyecto que a primera vista parece complicado, pero ya cuando iniciamos nos damos cuenta de que es todo lo contrario; muy sencillo y divertido de realizar, así que te invito a alistar tu IDE y manos a la obra.
---

Antes que nada, aqui te dejo el [link](https://github.com/Rooyca/API-LoRFinder) por si deseas clonar directamente el repositorio de Github.

# Desarollando nuestra API 

Para este tutorial vamos a utilizar principalmente el "Mini"-Framework Flask y algunas extenciones del mismo. Para la instalación de todos los paquetes me gustaria dejarte dos opciones:
1. Instalar los siguientes paquetes con el comando `pip install x-paquete` 
- flask
- flask_restful
- flask_sqlalchemy

2. Descargar el el archivo [`requirements.txt`](https://github.com/Rooyca/API-LoRFinder/blob/main/requirements.txt) e instalar todo de una vez con el comando `pip install -r requirements.txt`

La primera opcion nos permite ir familiarizandonos poco a poco con las herramientas que vamos utilizando a lo largo de este tutorial y la segunda nos agiliza el trabajo. 

¿Cuál usar? Depende de... 
![YOU-LOR](https://res.cloudinary.com/rooyca/image/upload/v1632278025/Blog/Imgs/Website%20and%20Api%20with%20Flask%20%28LoR%29/lor_gi6hgv.jpg)

...*ti*


## Estructura de nuestra API

Vamos a explicar de manera súper básica la estructura de nuestra API. Para mayor informacion te invito a visitar la documentacion oficial de [FlaskRESTful](https://flask-restful.readthedocs.io/en/latest/). 

``` python
1. from flask import Flask
2. from flask_restful import Api, Resource, marshal_with, fields
3. from flask_sqlalchemy import SQLAlchemy

4. app = Flask(__name__)
5. api = Api(app)
6. app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///Players.db'
7. db = SQLAlchemy(app)
```

En las lineas uno, dos y tres importamos Flask, algunas clases desde flask_restful y SQLAlchemy, que será la encargada de realizar las conexiones con nuestra base de datos. Depués, de la linea cuatro a siete realizamos la configuracion de nuestra aplicacion y establecemos una conexion a través de `sqlite` con nuestra base de datos.

```python
class Players(db.Model):
    __tablename__ = "players"
    id = db.Column(db.Integer, primary_key=True)
    region = db.Column(db.String, nullable=False)
    name = db.Column(db.String, nullable=False)
    puuid = db.Column(db.String, nullable=False)


resource_fields = {
    'id':fields.Integer,
    'region':fields.String,
    'name':fields.String,
    'puuid':fields.String
}
```
Aquí primero creamos la clase `Players` que será la encargada de manejar la "extructura" de nuestra base de datos. Como podemos ver, creamos una tabla llamada `players` y cuatro columnas. Por último creamos un diccionario llamado `resource_fields` para serializar nuestra informacion de tal manera que podamos mostrarla en formato JSON. (¿Qué es [JSON](https://es.wikipedia.org/wiki/JSON)?)

```python
class Player(Resource):
    @marshal_with(resource_fields)
    def get(self, name):
        result = Players.query.filter_by(name=name).first()
        return result

api.add_resource(Player, "/<string:name>")


if __name__ == "__main__":
    app.run(host='0.0.0.0')
```
Por último, creamos la clase Player que organizará los datos de nuestra base de datos de la forma en que le especificamos en nuestro diccionario `resource_fields`. Luego definimos una función **get** que tomará como argumantos *self* y el nombre del jugador del cual queremos conocer la información. Con estos datos hacemos un `query` a nuestra base de datos, diciéndole que nos muestre el primer el primer jugador llamado `name`. 

`api.add_resource(Player, "/<string:name>")` es una de las partes fundamentales de nuestro código, pues es aquí donde especificamos la ruta a la cual se hará la request, o dicho de otra forma, es aquí donde definimos nuestro Endpoint. En este caso sería simplemente a la ruta de inicio, es decir:

```bash
https://0.0.0.0:8000/Bluegod
```
Por último, la parte de `if __name__ == "__main__":` sencillamente evita que se ejecute la aplicación cuando es importada por otro script y en la parte de `app.run(host='0.0.0.0')` podriamos añadirle `debug=True` para que cada que realizamos un cambio y tenemos nuestra aplicación corriendo este cambio se aplica inmediatamente después de guardar, esto ayuda mucho cuando estamos testeando o añadiendo nuevas funciones, pero se recomienda quitarlo a la hora de hacer *deploy* de nuestra App.

Y... ya, eso sería todo, si hemos seguido correctamente todos los pasos hasta aquí deberiamos tener nuestra API funcionando. ¡Vamos a probarla! 

![RESULTADO-JSON](https://res.cloudinary.com/rooyca/image/upload/v1632289004/Blog/Imgs/Website%20and%20Api%20with%20Flask%20%28LoR%29/json_vvz4hc.png)

¡Excelente! Sí funciona. 

Bueno, eso sería todo por el día de hoy. Si quieres probar esta API [AQUÍ](https://lor-finder.herokuapp.com/) te dejo un link en el cual podras hacerlo. Y recuerda, si tienes alguna duda, queja, sugerencia o reclamo no dudes en hacérmelo saber.


