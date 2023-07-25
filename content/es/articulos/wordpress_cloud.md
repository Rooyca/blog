---
title: "Alojar Wordpress en la nube (Google Cloud)"
date: 2022-07-01
type: page
tags: 
- "Wordpress"
- "Google Cloud"
- "Migración"
- "php"
- "Base de Datos"
description: ¿Has pensado en alojar tu sitio con WordPress en la nube, pero no sabes cómo? Hoy aprenderemos juntos el proceso para correr nuestro sitio en Google Cloud, pero un proceso similar podría utilizarse para correrlo en cualquier otra plataforma como AWS o Azure.
topic: cloud,wordpress
---

![Wordpress](https://upload.wikimedia.org/wikipedia/commons/2/20/WordPress_logo.svg)

# Creando la Máquina Virtual

"WordPress es un sistema de gestión de contenidos lanzado el 27 de mayo de 2003, enfocado a la creación de cualquier tipo de página web". Según estas veinte [estadísticas](https://blog.hubspot.com/website/wordpress-stats#:~:text=(W3Techs%2C%202022),every%20five%20websites%20use%20WordPress.) sobre WordPress para el 2022 encontramos que WordPress es usado por el 43.2% de todas las páginas en Internet.

Para poder correr WordPress en Google Cloud (GCloud de ahora en adelante), primeramente debemos crear una máquina virtual con una de las imágenes con las que cuenta GCloud. Para ello vamos a la parte superior izquierda, damos clic en "Más Productos" y buscamos "Marketplace". 

![Una](https://res.cloudinary.com/rooyca/image/upload/v1656712908/Blog/Imgs/hosting%20wordpress/1_hjegaj.png)

Se nos abrirá una ventana en la que buscaremos WordPress y le daremos a la primera opción (openlitespeed-wordpress)

![Dos](https://res.cloudinary.com/rooyca/image/upload/v1656710344/Blog/Imgs/hosting%20wordpress/3_o7rjwl.png)

A continuación vamos a darle a "Iniciar", después de esto se nos abrirá una ventana en la que podremos modificar las características que tendrá nuestra máquina virtual, mi recomendación es que la dejemos tal como se ve en la siguiente imagen (por supuesto escogiendo un nombre diferente). Todo lo demás que no se alcanza a ver en la imagen es porque deberemos dejarlo tal cual está. Presionamos, aceptar Términos y Condiciones y luego crear.


![Tres](https://res.cloudinary.com/rooyca/image/upload/v1656710344/Blog/Imgs/hosting%20wordpress/5_lbdvbs.png)



Y listo, eso sería todo, ya tenemos configurada nuestra máquina virtual. El resultado final debería ser algo como lo siguiente:



![Cuatro](https://res.cloudinary.com/rooyca/image/upload/v1656711028/Blog/Imgs/hosting%20wordpress/6_tcnjcy.png)



# Iniciando sesión en nuestra MV

Para poder completar la instalación de WordPress y demás complemento en nuestra máquina virtual, es necesario completar una serie de pasos.

Primeramente, debemos instalar Google Cloud CLI, en el siguiente [link](https://cloud.google.com/sdk/docs/install) encontrarás una guía completa con todo el proceso, pero si lo que deseas es únicamente instalarlo puedes abrir una Power Shell como Administrador y ejecutar el siguiente comando:

```
(New-Object Net.WebClient).DownloadFile("https://dl.google.com/dl/cloudsdk/channels/rapid/GoogleCloudSDKInstaller.exe", "$env:Temp\GoogleCloudSDKInstaller.exe")

& $env:Temp\GoogleCloudSDKInstaller.exe
    
```
Una vez hecho esto e instalado el CLI vamos a Inicio y buscamos "gcloud" abrimos el primer resultado y ejecutamos:

```
gcloud auth login
```

Se abrirá una ventana del navegador en la que tendremos que escoger nuestra cuenta de Google con la que creamos la máquina virtual. Damos aceptar a todo y listo.

Una vez la consola nos diga que estamos logeados de manera exitosa tendremos que correr el siguiente comando:

```
gcloud compute ssh --zone "TU_ZONA" "NOMBRE_MAQUINA_VIRTUAL"  --project "NOMBRE_PROYECTO"
```

Nos abrirá una ventana en la que deberemos ingresar nuestro dominio y un correo para poder configurar el SSL.


![Cinco](https://res.cloudinary.com/rooyca/image/upload/v1656711850/Blog/Imgs/hosting%20wordpress/7_se6twg.png)


Después de realizar este proceso nos dirigimos a:

**http://NUESTRA_IP/wp-admin**

Para instalar WordPress.


![Seis](https://res.cloudinary.com/rooyca/image/upload/v1656712691/Blog/Imgs/hosting%20wordpress/8_dmaxmo.png)


# "Aumentando" la memoria RAM

Es muy poca la memoria RAM que configuramos para nuestra máquina virtual, por lo que es altamente recomendable hacer Swap. "El Swap es el espacio que el disco duro tiene para intercambiar la memoria física con la memoria virtual". Para ello vamos a ejecutar los siguientes comandos, en orden:

```
sudo fallocate -l 1GB /swapfile
sudo chmod 600 /swapfile
sudo mkswap /swapfile
sudo swapon /swapfile
free -m

#Para que cuando se reinicie el servidor conservemos el archivo SWAP
echo '/swapfile none swap sw 00' | sudo tee -a /etc/fstab
```

Y eso sería todo, así de sencillo es correr WordPress en Google Cloud.

Recuerden que si tienen alguna duda pueden escribirme por: 

-    Discord: rooyca#6075
-    Telegram: @seiseiseis
-	 Mastodon: @rooyca@mas.to

Hasta la próxima. Que tengan un excelente día.