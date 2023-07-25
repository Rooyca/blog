---
title: "Una guía para compartir archivos en la Blockchain con IPFS"
date: 2021-10-05
tags: 
- "IPFS"
- "blockchain"
description: IPFS es un protocolo hypermedia peer-to-peer diseñado para preservar y hacer crecer el conocimiento de la humanidad haciendo la web más abierta, más resiliente y mejor.
topic: ipfs,blockchain
---

![IPFS-IMG](https://cdn-images-1.medium.com/v2/resize:fit:1200/1*lGh_L0ldPWz1kFMcKvj4Qw.png)

IPFS, o Interplanetary File System, creado por Protocol Labs, es un protocolo peer-to-peer donde cada nodo almacena una colección de archivos *hasheados*. Un usuario que desee alguno de esos archivos se conecta a una capa de este protocolo y por medio del Hash, IPFS se encarga "recolectar" el archivo que está distribuido a través de los nodos y finalmente envía el archivo al usuario que lo solicitó.

¿Recuerdan BitTorrent? Pues es parecido, solo que con IPFS tienes más control de tus archivos y se hace referencia a tus archivos por medio de hashes.

### ¿Y la seguridad?
Como habrán podido deducir, en IPFS "cualquiera" puede tener acceso a nuestros archivos, lo único que se necesita es un Hash del tipo: *QmYqSCWuzG8Cyo4MFQzqKcC14ct4ybA*... para descargar nuestro archivo. Lo que nos llevaría a concluir que IPFS no es el protocolo indicado para archivos "sensibles"... ¿o sí?

### Con ustedes, Encriptación Asimétrica (EA)
Por suerte, contamos con herramientas que nos permiten asegurar nuestros archivos antes de subirlos a IPFS. La EA nos permite encriptar el archivo con la clave publica del destinatario, lo que garantiza que terceros no puedan hacer nada con el archivo. Para este tutorial usaremos [GPG](https://www.gnupg.org/).

### Manos a la obra

En este tutorial, haremos lo siguiente:

- Configurar GPG.
- Configurar IPFS.
- Encriptar un archivo con la clave publica de alguien.
- Subir un archivo encriptado a IPFS.
- Descargar dicho archivo desde otra máquina y asegurarnos de que solo el destinatario pueda ver su contenido.

### Instalación de lo que usaremos

#### GPG
Tendremos que descargar GPG en ambas maquinas, la que envía y la que recibe, para ello nos dirigimos a [este](https://blog.ghostinthemachines.com/2015/03/01/how-to-use-gpg-command-line/) link y seguimos las instrucciones dependiendo de nuestro sistema operativo. Una vez instalado vamos a generar una clave en ambos equipos, para ello usamos el siguiente comando:

```bash
gpg --gen-key
```

Luego nos pedirá nuestro nombre, correo, un comentario y una contraseña, que entre más robusta mejor.

![IMG-KEY](https://res.cloudinary.com/rooyca/image/upload/v1633438176/Blog/Imgs/IPFS/publickey_rxodzo.png)

Ahora, en nuestro segundo equipo, después de generada la clave, la vamos a exportar con el siguiente comando:

```bash
gpg --export --armor > clavePublica.asc
``` 
Pasamos nuestro archivo a nuestro segundo pc, e importamos la clave con el siguiente comando:

```bash
gpg --import clavePublica.asc
```

Podemos revisar nuestras claves con el siguiente comando:

```bash
gpg --list-keys
```

Listo, eso sería todo en cuanto a GPG, ahora vamos a configurar nuestro nodo.

#### IPFS
Existen varias alternativas a la hora de descargar IPFS, en el siguiente [enlace](https://ipfs.io/#install) podremos usar la que nos parezca mejor. En mi caso usaré la terminal, una vez instalado podremos iniciar nuestro nodo con el siguiente comando:
```bash
ipfs init
```
Recuerda que esto debes hacerlo en ambos computadores. Después de esto deberemos iniciar nuestro programa (*daemon*: Disk And Execution MONitor)
```bash
ipfs daemon
```
![IPFS-DAEMON](https://res.cloudinary.com/rooyca/image/upload/v1633439020/Blog/Imgs/IPFS/ipfsdaemon_tbrs3o.png)

Y listo, ya tendríamos nuestros nodos preparados para subir y bajar archivos.
### Encriptando nuestro archivo con GPG
Ahora vamos a encripar nuestro archivo para que solo pueda ser desencriptado por el receptor, para ello vamos a correr el siguiente comando:
```bash
gpg --encrypt --recipient "Nombre del que recibe" archivo.pdf
```
En el que podrán notar dos variables, el nombre de la persona que lo recibe (que debe ser el mismo que pusimos en la clave pública que generamos anteriormente), en mi caso seria "Rooyca" y después pondremos nuestro archivo, que en mi caso es un pdf. Nos quedará un archivo con extensión .gpg.

![ENCRYP](https://res.cloudinary.com/rooyca/image/upload/v1633439717/Blog/Imgs/IPFS/encrypt_gqawej.png)
### Subiéndolo a IPFS
Una vez que tenemos todo preparado podemos abrir nuestra terminal y subir nuestro archivo encriptado a IPFS con el siguiente comando:
```bash
ipfs add archivo.pdf.gpg
```

![IPFS-ADD](https://res.cloudinary.com/rooyca/image/upload/v1633440055/Blog/Imgs/IPFS/ipfsadd_diqry9.png)

Podemos comprobar que subimos correctamente nuestro archivo con el comando:
```bash
ipfs pin ls
```
![IPFS-LS](https://res.cloudinary.com/rooyca/image/upload/v1633440758/Blog/Imgs/IPFS/Untitlead_esz4mb.png)

Aquí debemos buscar el Hash que coincida con el resultado de nuestro comando anterior, en mi caso seria:
> QmazcLvRjPMLY5UAGEDVLrJCKX3ySqb8TZxLmvt7pfbxgf

### Descargando de IPFS
Ahora, para descargar nuestro archivo es igual de sencillo, en nuestro segundo computador ingresamos `ipfs get` junto al hash del archivo.
```bash
ipfs get QmazcLvRjPMLY5UAGEDVLrJCKX3ySqb8TZxLmvt7pfbxgf
```
Ya tendriamos nuestro PDF, pero si intentamos abrirlo nos daremos cuenta que no es posible ver su contenido, por lo que procedemos a desencriptarlo.
### Desencriptando
Para ello vamos a ingresar el siguiente comando, en el que le pasamos el nombre del archivo y ">" para cambiarle el nombre a "archivodes.pdf".
```bash
gpg --decrypt QmazcLvRjPMLY5UAGEDVLrJCKX3ySqb8TZxLmvt7pfbxgf > archivodes.pdf
```
Y listo, si ahora lo volvemos a abrir podremos acceder de manera normal a nuestro pdf. Así de sencillo es mandar archivos confidenciales o sensibles a través de una excelente plataforma como es IPFS. 

Bueno, eso es todo por hoy, espero que esta pequeña guía los haya animado a mantener su propio nodo y ha hacer uso de este grandioso protocolo. Como siempre, si tienen alguna duda o simplemente desean decirme algo no duden en contactarme.

---

-    Discord: rooyca#6075
-    Telegram: @seiseiseis
-	 Mastodon: @rooyca@mas.to

##### Publicación original

[*Blockgeni.com*](https://blockgeni.com/a-guide-to-securely-share-files-on-the-blockchain-with-ipfs/)
