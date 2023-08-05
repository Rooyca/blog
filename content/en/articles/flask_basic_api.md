---
title: "Create your first API with Flask"
date: 2021-09-21
type: page
tags: 
- "flask"
- "api"
description: Today we're going to learn how to create a simple API with Flask. At first glance, this may seem like a complex project, but once we get started, we realize it's actually the opposite - very straightforward and fun to do. So I invite you to get your IDE ready and let's get started
topic: Flask,api
---

First of all, here is the [link](https://github.com/Rooyca/API-LoRFinder) in case you want to clone directly the Github repository.

# Developing our API 

For this tutorial we are going to use mainly the "Mini"-Framework Flask and some extensions of it. For the installation of all the packages I would like to leave you with two options:
1. Install the following packages with the `pip install x-package` command. 
- flask
- flask_restful
- flask_sqlalchemy

2. Download the file [`requirements.txt`](https://github.com/Rooyca/API-LoRFinder/blob/main/requirements.txt) and install everything at once with the command `pip install -r requirements.txt`.

The first option allows us to familiarize ourselves little by little with the tools that we are going to use throughout this tutorial and the second one speeds up the work. 

Which one to use? It's up to...
![YOU-LOR](https://res.cloudinary.com/rooyca/image/upload/v1632278025/Blog/Imgs/Website%20and%20Api%20with%20Flask%20%28LoR%29/lor_gi6hgv.jpg)

...*you*


## Structure of our API

We are going to explain in a very basic way the structure of our API. For more information I invite you to visit the official documentation of [FlaskRESTful](https://flask-restful.readthedocs.io/en/latest/). 

``` python
1. from flask import Flask
2. from flask_restful import Api, Resource, marshal_with, fields
3. from flask_sqlalchemy import SQLAlchemy

4. app = Flask(__name__)
5. api = Api(app)
6. app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///Players.db'
7. db = SQLAlchemy(app)
```

In lines one, two and three we import Flask, some classes from flask_restful and SQLAlchemy, which will be in charge of making the connections with our database. Then, from line four to seven we make the configuration of our application and establish a connection through `sqlite` with our database.

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
Here we first create the `Players` class that will be in charge of managing the "structure" of our database. As we can see, we create a table called `players` and four columns. Finally we create a dictionary called `resource_fields` to serialize our information so that we can display it in JSON format. (What is [JSON](https://es.wikipedia.org/wiki/JSON)?)

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
Finally, we create the Player class that will organize the data in our database in the way we specify in our `resource_fields` dictionary. Then we define a **get** function that will take as argument *self* and the name of the player of which we want to know the information. With this data we do a `query` to our database, telling it to show us the first player named `name`. 

`api.add_resource(Player, "/<string:name>")` is one of the fundamental parts of our code, because it is here where we specify the route to which the request will be made, or in other words, it is here where we define our Endpoint. In this case it would be simply to the start path, that is:

```bash
https://0.0.0.0:8000/Bluegod
```
Finally, the `if __name__ == "__main__":` part simply prevents the application from running when it is imported by another script and in the `app.run(host='0.0.0. 0')` we could add `debug=True` so that every time we make a change and we have our application running this change is applied immediately after saving, this helps a lot when we are testing or adding new functions, but it is recommended to remove it when making *deploy* of our App.

And... now, that would be all, if we have followed correctly all the steps up to here we should have our API working, let's test it! 

![RESULTADO-JSON](https://res.cloudinary.com/rooyca/image/upload/v1632289004/Blog/Imgs/Website%20and%20Api%20with%20Flask%20%28LoR%29/json_vvz4hc.png)

Excellent! It does work. 

Well, that's all for today. If you want to try this API [HERE](https://lor-finder.herokuapp.com/) I leave you a link where you can do it. And remember, if you have any doubt, complaint, suggestion or claim don't hesitate to let me know.