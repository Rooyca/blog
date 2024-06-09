---
title: "Creando nuestros propios comandos en Linux | Alias"
date: 2021-09-20
type: page
tags: 
- "linux"
description: ¿Cansando de tener que escribir veinte variables para ejecutar un simple comando? Pues hoy aprenderemos a mapear nuestros comandos haciendo uso de Alias. 
---

# Creando nuestros Aliases

Alias es una forma sencilla de mapper, bindear, unir(?) comandos. Esta es la mejor manera de ahorrarnos tiempo a la hora de escribir cadenas larguísimas de comandos. Si nosotros escribimos "alias" en nuestra terminal debería aparecernos los aliases predeterminados de nuestro sistema, algo así:
![ALIASES-DEFAULT](https://res.cloudinary.com/rooyca/image/upload/v1632189896/Blog/Imgs/Commands-Linux/alias-predeterminados_lhjdfr.png)

Como podran notar, en mi caso tengo dos alias customizados: **price** y **subl**; el primero es un script en python que uso para ver el comportamiento del mercado crypto y el segundo es simplemtente para abrir SublimeText. 

### Modificando nuestro archivo .bashrc

Para poder lograr que nuestros alias sean permanentes y no solo durante una seccion de la terminal debemos modificar un archivo llamado .bashrc, que por lo general se encuentra en */home/usuario/* para esto debemos abrirlo con el editor de texto de nuestra preferencia, en mi caso voy a utilizar vim:

    vim ~/.bashrc

![VIM-ALIASES](https://res.cloudinary.com/rooyca/image/upload/v1632190837/Blog/Imgs/Commands-Linux/vim-aliases_wgtx0d.png)

Bajamos hasta encontrar la seccion de Aliases y añadimos nuestro comando de la siguiente manera:

    alias nuestro-comando='comando-a-mapear'

Un ejemplo más claro sería el siguiente:
    
    alias price='python3 ~/Desktop/get_price.py'

En donde "price" es el comando que le quiero asignar a la cadena: "python3 ~/Desktop/get_price.py". Ahora lo último que nos queda es guardar los cambias y listo, así de sencillo tenemos nuestro comando personalizado. 

![GET-PRICE](https://res.cloudinary.com/rooyca/image/upload/v1632191334/Blog/Imgs/Commands-Linux/get-price_mntpwd.png)

{{< signature standard >}}

