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

## Instalación de `rclone`

![rclone](https://res.cloudinary.com/rooyca/image/upload/v1691629511/Blog/Imgs/guia_backups/logo_on_light__horizontal_color_nvxrkk.svg)

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

### Configuración de `rclone`

Hay muchas maneras de configurar `rclone`. Todas ellas se pueden encontrar en la [documentacion oficial](https://rclone.org/docs/). Hoy vamos a utilizar `Google drive`.

Es importante tener en cuenta que para poder configurarlo correctamente son necesarias un `Client Id` y un `Client Secret`. Para ello debemos configurar una nueva aplicación en la [consola de desarrolladores de Google](https://console.developers.google.com/), para esto debemos seguir los siguientes pasos:

1. Crear un nuevo proyecto.
2. Habilitar la API de Google Drive.
3. Crear credenciales.

![cre](https://res.cloudinary.com/rooyca/image/upload/v1691642523/Blog/Imgs/guia_backups/2023-08-09_20-35_lk5vxf.png)

>> IMPORTANTE: Se debe añadir el correo de la cuenta de Google que se desea utilizar para almacenar los backups como tester del proyecto.

Para ello vamos a seguir [esta guía](https://rclone.org/drive/), es muy sencillo. Pero antes es importante tener en cuenta:

1. Tenemos que añadir `local` como un `remote`.
2. Es recomendable añadir una contraseña robusta para nuestra configuración local.
3. Tenemos que añadir nuestra unidad y si queremos cifrarla tenemos que añadir otro `remote` llamado `encryp/decryp`.
4. Hay muchas opciones que podemos usar: `copy`, `move`, `sync`, etc. En este caso usaremos `sync`.

Para descargar archivos podemos usar:

```bash
rclone copy bipbop:enc/2023-05-16-CTF.zip arch:/home/rooyca
```
Donde `bipbop:enc` es nuestro remoto encriptado. El truco está en usar el nombre original del fichero, en este caso `2023-05-16-CTF.zip`, en lugar del encriptado que muestra nuestra carpeta Drive. La segunda parte `arch:/home/rooyca` es nuesto `local`.

O tambíen podemos copiar toda la carpeta:

```bash
rclone copy bipbop:enc arch:/home/rooyca
```

En este ejemplo estamos copiando toda la carpeta `enc` de nuestro remoto `bipbop` a nuestro remoto local `arch` en la ruta `/home/rooyca`.

## Instalación y configuración de `rooykup`

Como podemos ver es menianamente sencillo configurar `rclone`, pero para facilitar aún más la tarea he creado un script que automatiza todo el proceso.

![rooykup](https://raw.githubusercontent.com/Rooyca/rooykup-backup-and-sync/master/rooykup_example.gif)

### Instalación

- Clonamos [este repositorio](https://github.com/Rooyca/rooykup-backup-and-sync/)
- Creamos un archivo `config.toml` en `~/.config/rooykup` con la siguiente estructura:

```toml
[config]
workingDirectory = "/path/to/working/directory"
shutDownAfterBackup = false
alwaysCompress = false
remote = ["remote:folder", "remote2:"]
local = "local:"
configPass = "here your super secure passphrase for rclone config" 

[exclude]
directories = [".git", "node_modules"] # If none leave it empty 

[[pathAndDirName]]
path = "/path/to/folder/to/backup"
zipName = "NameOfTheZipFile

[[pathAndDirName]]
path = "/path/to/folder/to/backup2"
zipName = "AnotherNameOfTheZipFile"
```

Puedes añadir todos los `[[pathAndDirName]]` que necesites.

### Uso

- Ejectua `python rooykup.py` para iniciar el proceso de respaldo.
	- Si deseas ejecutarlo periódicamente, puedes usar `cron` o `systemd`
- También puedes crear un alias en tu archivo `.bashrc` o `.zshrc`.

```bash
alias rooykup="python /path/to/rooykup.py"
```

- Si desea apagar el ordenador después del backup, cambia `shutDownAfterBackup` a `true` en tu fichero `config.toml` o ejecútalo con la opción `-s`.

- Si quieres comprimir siempre los ficheros, cambia `alwaysCompress` a `true` en tu fichero `config.toml` o ejecútalo con la opción `-c`. Esto comprimirá incluso si el directorio ya tiene un fichero comprimido el día de hoy.


## Respaldar dotfiles y demás en GitHub

![git](https://res.cloudinary.com/rooyca/image/upload/v1691699867/Blog/Imgs/guia_backups/git_repo_backups_hgrkho.png)

Hay muchas formas de hacer esto, pero en esta ocación haremos uso del template que he creado para este propósito: [tamplate-backups](https://github.com/Rooyca/template-backups). Ahí encontraremos una lista de checkeo y un script que nos ayudará a automatizar el proceso.

### Uso

1. Clonamos [este repositorio](https://github.com/Rooyca/template-backups)
2. Exportamos las variables de entorno (Opcional)

```bash
export DONT_BACKUP="Trash,pnpm,gem"
export BACKUP_DIR="/home/user/mybackup"
```

3. Ejecutamos los comandos:

```bash
cd template-backups
chmod +x rokup.sh
./rokup.sh
```

---

ULTIMA ACTUALIZACIÓN: 2023-08-10