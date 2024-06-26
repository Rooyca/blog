---
title: "Subir archivos a Nextcloud desde la línea de comandos"
date: 2023-07-29
type: page
tags: 
- "nextcloud"
- "cli"
- "python"
- "windows"
- "linux"
description: Sube archivos a Nextcloud desde la línea de comandos de manera rápida y sencilla.
---

{{< box info >}}
Github repo: https://github.com/Rooyca/upload-nextcloud-cli
{{< /box >}}

## Configuración

1. Crear un link compartido para una carpeta de Nextcloud. Recordar seleccionar la opción "File Drop (upload only)", o "Allow upload and editing" al crear el link compartido.

![Crear link compartido](upload_nextcloud_cli.png)

> [Imagen tomada de https://cylab.be](https://cylab.be/blog/33/how-to-upload-your-files-to-nextcloud-file-drop-using-curl)

2. Copiar el link compartido. El link debe tener el siguiente formato:

```bash
https://nextcloud.example.com/index.php/s/xXxXxXxXx
```

## Ejecución

En el repositorio podemos encontrar tres formas de ejecutar el script, dependiendo del sistema operativo que estemos utilizando o de si tenemos instalado Python en nuestro sistema.

### Windows

Esta es una version minimalista hecha por mi y para correrla correctamente es necesario tener instalado `CURL` en nuestro sistema. (Lo más probable es que ya lo tengas instalado). Puedes comprobarlo ejecutando el siguiente comando en la terminal:

```bash
curl --version
```

Si no lo tienes puedes intalarlo usando [Chocolatey](https://chocolatey.org/install). Para ello abrimos una PowerShell como administrador y ejecutamos el siguiente comando:

```bash
Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))
```

Una vez instalado `Chocolatey` ejecutamos el siguiente comando para instalar `CURL`:

```bash
choco install curl
```

Una vez instalado `CURL` ya podemos ejecutar el script. Para ello abrimos una terminal en la carpeta donde tengamos el script y ejecutamos el siguiente comando:

```bash
upload_file.bat
```

El script nos pedirá los siguientes datos:

1. Nombre del archivo que queremos subir.
2. Nombre de usuario (La parte del link que va después de `index.php/s/`).
3. IP o dominio de Nextcloud.
4. Nombre del archivo que queremos subir (OPCIONAL).

Una vez hayamos introducido todos los datos podremos ingresar la opción `5` para subir el archivo, una vez subido podremos regresar al menu y subir otro archivo o salir del script.

{{< box info >}}
El script automaticamente guarda la `IP` y el `Nombre usuario` en un **txt** para que no tengamos que volver a introducirlos.
{{< /box >}}

### Linux

{{< box important >}}
Este script NO es de mi autoría. El original lo puedes encontrar aquí: https://github.com/tavinus/cloudsend.sh
{{< /box >}}

Para usarlo solo tenemos que ejecutar el siguiente comando:

```bash
curl -O 'https://raw.githubusercontent.com/tavinus/cloudsend.sh/master/cloudsend.sh' && chmod +x cloudsend.sh
```

Una vez descargado el script, podemos ejecutarlo de la siguiente forma:

```bash
./cloudsend.sh <file> <folderLink>
```

Si tenemos en cuenta la url del ejemplo anterior, el comando sería el siguiente:

```bash
./cloudsend.sh test.txt https://nextcloud.example.com/index.php/s/xXxXxXxXx
```

### Python

{{< box warning >}}
Si tu sistema operativo es **Linux** te recomiendo usar el script de Linux, ya que es muchísimo más completo y tiene más opciones. Esta version en **Python** fue escrita por mí, por lo que puede contener ERRORES o no funcionar correctamente.
{{< /box >}}

Para usarlo solo tenemos que ejecutar el siguiente comando:

```bash
python upload_file.py <folderLink> <file>
```

Si tenemos en cuenta la url del ejemplo anterior, el comando sería el siguiente:

```bash
python upload_file.py https://nextcloud.example.com/index.php/s/xXxXxXxXx test.txt
```

## Conclusión

Existen diversas formas de enviar archivos a nuestro NextCloud desde la terminal, depende de ti escoger la opción que más se acomode a tus necesidades.

{{< signature standard >}}