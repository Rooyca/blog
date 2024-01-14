---
title: "Instalar PostgreSQL en Termux (Android)"
date: 2024-01-14
tags: 
- "termux"
- "postgres"
description: Guia sensilla para instalar PostgreSQL en Termux (Terminal de comando para Android)
topic: termux,postgres
---

## Instalación

Instalamos el paquete `postgresql`:

```bash
pkg install postgresql
```

Luego creamos el `skeleton` de la base de datos:

```bash
mkdir -p $PREFIX/var/lib/postgresql
initdb -D $PREFIX/var/lib/postgresql
```

Ahora iniciamos nuestra DB:

```bash
pg_ctl -D $PREFIX/var/lib/postgresql start
```

Cuando queramos detenerla podemos usar:

```bash
pg_ctl -D $PREFIX/var/lib/postgresql stop
```

Para conectarnos necesitamos un usuario. Podemos crear uno usando:

```bash
createuser --superuser --pwprompt NOMBRE_DE_USUARIO
```

Para crear una base de datos usamos:

```bash
createdb NOMBRE_DE_BASE_DE_DATOS
```

Nos conectamos usando:

```bash
psql NOMBRE_DE_BASE_DE_DATOS
```

## Acceder a la DB en Local

Para acceder a la base de datos desde otra terminal o cliente debemos configurar los archivos `pg_hba.conf` y `postgresql.conf`.

```bash
cd $PREFIX/var/lib/postgresql
nano pg_hba.conf
```

Y agregamos la siguiente linea al final:

```
host    all             all             192.168.0.1/24          md5
```

Donde `192.168.0.1/24` es el rango de IPs que pueden acceder a la base de datos. Guardamos los cambios y salimos del archivo. Ahora abrimos el archivo `postgresql.conf`:

```bash
nano postgresql.conf
```

Y buscamos la linea `listen_addresses` y la cambiamos por (o simplemente la descomentamos eliminando el `#` del inicio):

```
listen_addresses = '*'
```

Guardamos los cambios y salimos del archivo. Ahora reiniciamos la base de datos:

```bash
pg_ctl -D $PREFIX/var/lib/postgresql restart
```

>> **POST ORIGINAL**: [https://wiki.termux.com/wiki/Postgresql](https://wiki.termux.com/wiki/Postgresql)