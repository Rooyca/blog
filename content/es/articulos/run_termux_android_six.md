---
title: "Instalar Termux en Android 5 o 6 | Samsung J2 Prime"
date: 2024-06-08
type: page
tags: 
- "termux"
- "android"
description: "Guía paso a paso para instalar Termux en Samsung J2 Prime... o cualquier otro celular con Android 5 o 6."
image: "https://miro.medium.com/v2/resize:fit:1400/1*5R38Aa6bT9pLLYwL3fwDBA.png"
---

{{< box info >}}
**Este guía está inspirada en el siguiente vídeo**
{{< /box >}}

{{< youtube ErQA1vK0rHg >}}


# Pasos a seguir


## Descargar la APK legacy de Termux

Nos dirigimos a [**archive.org**](https://archive.org/download/termux-repositories-legacy) y descargamos [termux-v0.79-offline-bootstraps.apk](https://archive.org/download/termux-repositories-legacy/termux-v0.79-offline-bootstraps.apk)

## Agregar repositorios para Android 5/6

Después de instalar, abrimos Termux y ejecutamos:

```bash
termux-setup-storage
```
Aceptamos los permisos y después:

```bash
apt update; apt upgrade
cd ../usr/etc/apt
```

Ahora copiamos el `Main` repo para la versión de android 5/6 desde [termux.dev](https://packages.termux.dev/)

```bash
echo "deb https://packages.termux.dev/termux-main-21/ stable main" > sources.list
```
Después añadimos los repositorios `game` y `science` para android 5/6:


```bash
cd sources.list.d
echo "deb https://termux.dev/game-packages-21-bin games stable" > game.list
echo "deb https://termux.dev/science-packages-21-bin science stable" > science.list
```

## Actualizar los repositorios

```bash
pkg upgrade
apt upgrade -y
#type N then Y
```

## Conclusión

Con estos sencillos pasos, ya tendremos Termux instalado en nuestro celular. 

{{< signature standard >}}



