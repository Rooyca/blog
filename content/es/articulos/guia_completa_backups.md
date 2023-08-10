---
title: "Hacer Backups usando rooykup, rclone y github (🚧 EN CONSTRUCCIÓN 🚧)"
date: 2023-08-09
type: page
tags: 
- "rclone"
- "github"
- "backups"
description: Las copias de seguridad son una parte importante de la administración de sistemas. En este artículo aprenderás todo lo que necesitas saber para implementar una estrategia de respaldo completa y automatizada en Linux.
topic: rclone,github,backups
---

## Introducción

Un backup es una copia de seguridad de los datos e información que se puede realizar a baja o alta escala. La programación y realización de una copia de seguridad resulta fundamental para el mantenimiento de un equipo o sistema, debido a que previene la pérdida de datos e información importante y permite tener un plan de acción en caso de presentar problemas como virus, fallos en los discos duro, accidentes, entre otros.

![backup](https://res.cloudinary.com/rooyca/image/upload/v1691629512/Blog/Imgs/guia_backups/696aba821d856b6e452815b12e98d97b_y5tqyj.png)

> [Fuente](https://keepcoding.io/blog/que-es-un-backup-y-por-que-es-tan-importante/#:~:text=La%20programaci%C3%B3n%20y%20realizaci%C3%B3n%20de,duro%2C%20accidentes%2C%20entre%20otros.)

## Tipos de Backups
Hay cuatro tipos principales de copias de seguridad: **completas**, **incrementales**, **diferenciales** y de **copia**.

* **Copia de seguridad completa:** Una copia de seguridad completa es una copia de todos los datos en un sistema. Es el tipo más completo de copia de seguridad y se utiliza para proteger contra la pérdida de datos completa. Las copias de seguridad completas suelen realizarse una vez al día, una vez a la semana o una vez al mes, según las necesidades.
* **Copia de seguridad incremental:** Una copia de seguridad incremental es una copia de los datos que han cambiado desde la última copia de seguridad completa. Las copias de seguridad incrementales suelen realizarse varias veces al día o varias veces a la semana. Son más pequeñas que las copias de seguridad completas y requieren menos tiempo para guardarlas.
* **Copia de seguridad diferencial:** Una copia de seguridad diferencial es similar a una copia de seguridad incremental, pero copia todos los datos que han cambiado desde la última copia de seguridad completa o diferencial. Las copias de seguridad diferenciales son más grandes que las copias de seguridad incrementales, pero requieren menos tiempo para guardarlas que las copias de seguridad completas.
* **Copia de seguridad de copia:** Una copia de seguridad de copia es una copia de todos los datos en un sistema que se guarda en un lugar diferente.

También es importante tener en cuenta que no existe una sola estrategia de copia de seguridad que sea perfecta para todos. Debe desarrollar una estrategia que sea adecuada para sus necesidades.

## Herramientas de Backup en Linux

Algunas de las herramientas de copia de seguridad más populares en Linux son:

* **rsync** es una herramienta de línea de comandos que se utiliza para copiar archivos y directorios de forma recursiva. Es una herramienta muy versátil que se puede utilizar para hacer copias de seguridad, sincronizar archivos y transferir archivos entre diferentes sistemas.

* **rsnapshot** es una herramienta de línea de comandos que se utiliza para realizar copias de seguridad incrementales de archivos y directorios. Rsnapshot es una herramienta muy eficiente que se puede utilizar para realizar copias de seguridad de grandes cantidades de datos.

* **Duplicity** es una herramienta de línea de comandos que se utiliza para hacer copias de seguridad de archivos y directorios en la nube. Duplicity es una herramienta muy versátil que se puede utilizar para hacer copias de seguridad de archivos en una variedad de servicios de almacenamiento en la nube, incluidos Amazon S3, Google Cloud Storage y Microsoft Azure Blob Storage.

* **rclone** es una herramienta de línea de comandos que se utiliza para sincronizar archivos entre diferentes sistemas, incluidos servidores locales, servidores remotos y servicios de almacenamiento en la nube. Rclone es una herramienta muy versátil que se puede utilizar para sincronizar archivos en una variedad de formatos, incluidos archivos de imagen, archivos de audio y archivos de video.

![rclone](https://res.cloudinary.com/rooyca/image/upload/v1691629511/Blog/Imgs/guia_backups/logo_on_light__horizontal_color_nvxrkk.svg)

En esta guía se hará uso de `rclone`, por lo que a continuación se mostrarán algunas características de `rclone`:

* Soporta una amplia variedad de ubicaciones, incluidas unidades locales, unidades externas, servidores remotos y servicios de almacenamiento en la nube.
* Es una herramienta muy eficiente que se puede utilizar para almacenar grandes cantidades de datos.
* Es una herramienta muy versátil que se puede utilizar para almacenar copias de seguridad de archivos en una variedad de formatos.
* Es una herramienta de código abierto que está disponible de forma gratuita.

`rclone` es una excelente opción para almacenar copias de seguridad de archivos. Es una herramienta poderosa, versátil y eficiente que se puede utilizar para almacenar copias de seguridad de archivos en una variedad de ubicaciones.

### Instalación y configuración de `rclone`

> [Guía de instalación](https://rclone.org/install/). 

- Script de instalación (Linux/macOS/BSD)

```bash
sudo -v ; curl https://rclone.org/install.sh | sudo bash
```

- Arch:

```bash
sudo pacman -S rclone
```

- Fedora

```bash
sudo dnf install rclone
```

### Configuración

Hay muchas maneras de configurar `rclone`. Todas ellas se pueden encontrar en la [documentacion oficial](https://rclone.org/docs/). Hoy vamos a utilizar `Google drive`.

Es importante tener en cuenta que para poder configurarlo correctamente son necesarias un `Client Id` y un `Client Secret`. Para ello debemos configurar una nueva aplicación en la [consola de desarrolladores de Google](https://console.developers.google.com/), para esto debemos seguir los siguientes pasos:

1. Crear un nuevo proyecto.
2. Habilitar la API de Google Drive.
3. Crear credenciales.

![cre](https://res.cloudinary.com/rooyca/image/upload/v1691642523/Blog/Imgs/guia_backups/2023-08-09_20-35_lk5vxf.png)

>> IMPORTANTE: Se debe añadir el correo de la cuenta de Google que se desea utilizar para almacenar los backups como tester del proyecto.

Para ello vamos a seguir [esta guía](https://rclone.org/drive/), es muy fácil. Después de que todo está configurado como en la instrucción podemos continuar. Pero antes es importante tener en cuenta:

1. Tenemos que añadir local como un remote.
2. Es recomendable añadir una contraseña fuerte para nuestra configuración local.
3. Tenemos que añadir nuestra unidad y, a continuación, si queremos establecer el cifrado tenemos que añadir otro remoto que llamado `encryp/decryp`, o algo así. (Establece la misma contraseña que estableciste al subir los archivos)
4. Hay muchas opciones que podemos usar `copy`, `move`, `sync`, etc. pero vamos a usar solo `sync` en este caso.
5. Al sincronizar local con remoto necesitamos pasar el remoto como el nuevo remoto (el encriptado que creamos en el paso 3). 
6. Para descargar archivos puedes usar:

```bash
rclone copy bipbop:enc/2023-05-16-CTF.zip arch:/home/rooyca
```
Where `bipbop:enc` its our remote encrypted, but the trick is to use the filename as the original filename, in this case `2023-05-16-CTF.zip`, instead of the encrypted one that its showing our Drive folder. `arch:/home/rooyca` its the local remote with the path.

That should do.

Or we can copy the entire folder:

```bash
rclone copy bipbop:enc arch:/home/rooyca
```

In this example we are copying the entire folder `enc` from our remote `bipbop` to our local remote `arch` in the path `/home/rooyca`.

