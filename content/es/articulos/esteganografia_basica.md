---
title: "Como extraer la obra completa de Shakespeare de una Imagen"
date: 2021-09-23
type: page
tags: 
- "steganography"
- "linux"
description: ¿Te gustaría leer la obra completa de uno de los grandes de la literatura inglesa pero piensas que es demasiado extensa como para descargarla? Pues el día de hoy te tengo la solución, vamos a extraérla desde una pequeña imagen (2 Megabytes).
---

# Esteganografía
## ¿Qué es? 

A diferencia de la criptografía, donde es obvio que se está ocultando algo, la esteganografía oculta la información de tal forma que no se levante sospechas de que hay algo oculto. Esto se consigue por medio de diferentes técnicas que nos permiten ocultar archivos, imágenes, textos o incluso vídeos dentro de otros archivos.

## ¿Cómo funciona?  

Hay diferentes técnicas para ocultar información dentro de archivos. Una de las más usadas y quizás la más sencilla de entender es la comúnmente conocido como "Técnica del Bit menos significante" o LSB (por sus siglas en inglés).

Esta técnica lo que hace es cambiar algunos de los últimos bits en un byte para codificar el mensaje. Esto es útil en imágenes, donde el valor del rojo, verde y azul están representados por bits (un byte) en un rango de 0 a 255 (ver [RGB](https://en.wikipedia.org/wiki/RGB)) en decimales o de 00000000 a 11111111 en binario.

Cambiar el valor de los últimos dos bits en un pixel completamente rojo de: 11111111 a 11111101 unicamente cambia el valor de rojo de 255 a 253, lo cual a simple vista crea un cambio prácticamente imperceptible, pero que aún así nos permite ocultar información dentro de la imagen. 

![LSB](https://res.cloudinary.com/rooyca/image/upload/v1632442360/Blog/Imgs/steganography/lsb_drvccm.jpg)
El anterior diagrama muestra dos imágenes de 4 pixeles cada una, junto a su respectivo valor binario. ([Fuente](https://null-byte.wonderhowto.com/how-to/steganography-hide-secret-data-inside-image-audio-file-seconds-0180936/))

Como dijimos anteriormente, esta técnica funciona muy bien con archivos multimedia, pero no tanto con archivos de texto ASCII donde un simple bit puede cambiar los caracteres completamente. Sin mencionar que el LSB es la técnica más sencilla de detectar.

## ¿Cómo usarla?

Recuerda que hay muchas técnicas de esteganografía, unas mejores que otras. Por lo general lo mejor es evitar la LSB e ir por algo un poco más sofisticado. Incluso, ¿por qué no?, diseñar tu propio algoritmo de esteganografía.

También es importante tener presentes los conceptos de encriptar y comprimir. Si encriptamos la información antes de ocultarla le añadimos una capa más de seguridad, mientras que comprimir la información nos permitirá añadirle más información a nuestro archivo.  

Para el desarrollo de este tutorial, vamos a utilizar Steghide, una herramienta sencilla de usar pero no por ello menos eficaz. 

### 1. Ocultando información

Primeramente vamos a instalar Steghide, por medio de apt.

```bash
sudo apt-get install steghide
``` 

Una vez instalado procemos a ocultar nuestra información, con el siguiente comando:

```bash
steghide embed -ef archivoSecreto -cf archivoDePortada -sf archivoFinal -z nivelDeCompresion -e esquema
```

Ahora vamos a explicar los argumentos que acabamos de utilizar:

- **-ef** especifica la ruta del archivo que queremos ocultar. Podemos ocultar cualquier tipo de archivos dentro de nuestro archivo, incluyento scripts en Python.
- **-cf** este es el archivo en el cual ocultaremos nuestra información. Aquí si hay restricciones, solo se permiten archivos del tipo BMP, JPEG, WAV y AU.
- **-sf** es un argumento opcional que especifica el nombre del archivo resultante de esta "unión". Si no lo especificamos el archivo original será sobrescrito. 
- **-z** especifica el nivel de compresión, en un rango de 1 a 9. Si prefiere no comprimir su archivo use el argumente **-Z**
- **-e** especifica el tipo de encriptación. Steghide soporta una gran cantidad de esquemas de encriptación y si se omite este argumento Steghide usará la opción por defecto que es 128-bit AES. Si prefieres no usar encriptacion simplemtente escribe **-e none** 

En el siguiente ejemplo, ocultaré un archivo de texto dentro de una imagen de un tierno cachorro (para levantar menos sospecha :wink:). El archivo final se llamará "perrito-secreto.jpeg", no voy a hacer uso del compresor ni del encriptador, y el comando en la terminal luciría algo así:

`steghide embed -ef secret.txt -cf perrito.jpeg -sf perrito-secreto.jpeg -e none -Z` 

Después de ingresar nuestro comando Steghide nos pedirá que le asignemos una contraseña a nuestro archivo. Una vez ingresamos la passphrase tendremos nuestro archivo con información confidencial dentro de él :smile:.

![PERRITO-SEC](https://res.cloudinary.com/rooyca/image/upload/v1632446520/Blog/Imgs/steganography/info-ocul_d2mpa7.png)

El resultado sería una imagen común y corriente... Si deseas saber lo que hay dentro del archivo de texto, aquí te dejo la imagen (password: topsecret):

![PERRITO](https://res.cloudinary.com/rooyca/image/upload/v1632446478/Blog/Imgs/steganography/perrito-secreto_xgzjlc.jpg)

### 2. Extrayendo información

El proceso de extraer la información de nuestros archivos es incluso más sencillo. El comando que deberemos ejecutar es el siguiente:

`steghide extract -sf archivoSecreto -xf archivoFinal`

Cuando ejecutamos este comando nos pedirá la contraseña, la ingresamos y deberíamos tener todos los archivos que se encontraban dentro del `archivoSecreto`

Ahora bien, te estarás preguntando, ¿cómo hago para extraer información de un archivo si no tengo la contraseña? Pues actualmente existen infinidad de herramientas que nos pueden ayudar con eso. Pero el día de hoy vamos a hacer uso de [**binwalk**](https://github.com/ReFirmLabs/binwalk/tree/master).

Como lo prometí en un principio, vamos a extraer la obra completa de Shakespeare de la siguiente imagen:

![SHAKESPEARE](https://res.cloudinary.com/rooyca/image/upload/v1632447298/Blog/Imgs/steganography/shakespeare_hempxe.jpg)
> O si prefieres puedes visitar el post original en [Twitter](https://twitter.com/David3141593/status/1058124224798380032).

Al correr nuestro binwalk (`binwalk shakespeare.jpg`) nos indica que la imagen esta compuesta de 31 archivos RAR. 

![DATA-SP](https://res.cloudinary.com/rooyca/image/upload/v1632447713/Blog/Imgs/steganography/data-shake_ta3h6y.png)

Procedemos a extraer los archivos de la siguiente manera 
`binwalk  -e -C ./shake shakespeare.jpeg`

![FILEs](https://res.cloudinary.com/rooyca/image/upload/v1632448228/Blog/Imgs/steganography/final-html_ios0md.png)

Lo que creará una carpeta "shake" en nuestro directorio actual. Al ejecutar el comando nos extraerá los 31 archivos RAR, y al extraer el primero de ellos(`shakespeare.-part001.rar`) nos quedará un último archivo llamado `shakespeare.html` que al abrirlo, nos saldrá algo así:

![FILES-FINAL](https://res.cloudinary.com/rooyca/image/upload/v1632448228/Blog/Imgs/steganography/html1_az1vsv.png)

¿Increíble no?

Si quieres expander tus conocimientos sobre este tema te recomiendo leer [este](https://linuxhint.com/hide_files_inside_images_linux/) y [este](https://ostechnix.com/hide-files-inside-images-linux/) articulo. ¿Eso no es suficiente? Te recomiendo [este](https://academy.hoppersroppers.org/course/view.php?id=7#section-4) curso de CTF, que abarca estos temas y muchos más. 

{{< signature standard >}}