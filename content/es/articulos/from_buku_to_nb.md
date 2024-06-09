---
title: "Migrar de buku a nb con Python"
date: 2024-06-06
type: page
tags: 
- "buku"
- "nb"
- "python"
description: "Transfiere todos tus marcadores de buku a nb usando Python."
image: "https://www.datanami.com/wp-content/uploads/2020/11/database_migration_shutterstock_hanss.jpg"
---

![img_migration](https://www.datanami.com/wp-content/uploads/2020/11/database_migration_shutterstock_hanss.jpg)

## Qué es buku

> ![Stars](https://img.shields.io/github/stars/jarun/buku)
> [![Latest release](https://img.shields.io/github/release/jarun/buku.svg?maxAge=600)](https://github.com/jarun/buku/releases/latest)


[buku](https://github.com/jarun/buku) es, según los mismos desarrolladores, "un potente gestor de marcadores y una miniweb personal". Uno de sus principales fuertes es que sus registros se guardan en una base de datos *SQL* por lo que la búsqueda es extremadamente rápida.

## Qué es nb

> ![Stars](https://img.shields.io/github/stars/xwmx/nb)
> [![Latest release](https://img.shields.io/github/v/tag/xwmx/nb)](https://github.com/xwmx/nb/tags)


[nb](https://github.com/xwmx/nb) es, según su descripción en GitHub, "una CLI para tomar notas, guardar marcadores, etiquetar archivos, filtrar, buscar, versionar y sincronizar con Git. También incluye conversión de archivos usando Pandoc, y mucho más, todo esto en un solo script". 

Para mí una clara ventaja de *nb* sobre *buku* es su multifuncionalidad, literalmente puedo usar una sola herramienta para manejar todas mis notas (desde *todos* hasta *bookmarks*). Es por eso que decidí pasarme por completo a *nb*. Y uno de los primeros retos que encontré fue cómo pasar los *marcadores* a archivos MD.

## Usando Python

La solución que encontré fue usar un script de Python que se encargara de agregar uno a uno los archivos `nb URL --tags TAG,TAG`, para ello primero exporté mis marcadores de buku.

```
buku --export bookmarks.md
```
Ya después simplemente corrí el script. 


```python
import re
import subprocess

def run_command(url, tags):
    command = f"nb {url} --tags {','.join(tags)}"
    subprocess.run(command, shell=True)

def main(filename):
    with open(filename, "r") as file:
        lines = file.readlines()
        for line in lines:
            match = re.search(r"\[.*\]\((.*)\).*TAGS: (.*)", line)
            if match:
                url = match.group(1)
                tags = match.group(2).replace(" -->","").split(",")
                run_command(url, tags)

if __name__ == "__main__":
    main("bookmarks.md")
```

Y listo, ya estoy a un paso más de tener todos mis archivos en un solo lugar.

Eso es todo por hoy, si te fue de utilidad, si tienes alguna inquietud o problema no dudes en dejarme un comentario. Hasta la próxima, ¡Gracias por leer!



