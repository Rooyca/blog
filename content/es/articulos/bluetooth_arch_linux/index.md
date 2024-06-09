---
title: "Conecta tus dispositivos Bluetooth en Arch Linux"
date: 2024-05-18
type: page
tags: 
- "arch"
- "linux"
- "bluetooth"
description: "Linux es todo diversion hasta que tienes que conectar un dispositivo bluetooth."
image: "https://thehackernews.com/images/-4cJPA2xc3uQ/X4lJatASkPI/AAAAAAAAA48/Hy5WY63rAWcvPjJi91C12tl81_EfKlsGACLcBGAsYHQ/s0/linux-bluetooth.jpg"
---

{{< box info >}}
En el presente artículo se presume que has seguido la guía: [Bluetooth - ArchWiki](https://wiki.archlinux.org/title/bluetooth).
{{< /box >}}

### Error

Al evaluar el estado mediante `systemctl status bluetooth.service`, se encontró el siguiente error:

![Estado](status.png)

Se intentó resolver el error actualizando a la versión más reciente, dado que se trata de un problema documentado en la [versión 5.75](https://github.com/bluez/bluez/issues/821). No obstante, esto no funcionó.

### Solución

La solución hallada consistió en establecer la conexión del dispositivo Bluetooth mediante el comando `bluetoothctl`.

Inicialmente, se procedió a iniciar el servicio:

```bash
bluetoothctl
```

Posteriormente, se habilitó el Bluetooth:

```bash
power on
```

Se llevó a cabo un scan de los dispositivos:

```bash
scan on
```

Acto seguido, se procedió a conectar el dispositivo en cuestión:

```bash
connect MAC
```

> Donde `MAC` representa la dirección MAC del dispositivo.

De esta manera, se logró establecer la conexión con éxito de mi altavoz Bluetooth en el entorno de Arch Linux. 

En caso de alguna inquietud o problema, no dudes en dejar tu comentario. 

¡Hasta la próxima!
