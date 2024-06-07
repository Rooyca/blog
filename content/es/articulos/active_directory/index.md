---
title: "Active Directory (Homelab)"
date: 2023-09-04
type: page
tags: 
- "active directory"
- "homelab"
- "windows"
description: Generalidades sobre active directory, cómo configurarlo, cómo usarlo, etc.
---

## Requisitos Homelab

- Virtualización: VMWare
- ISO: Windows Server 2022 & Windows 10 Pro

## Configuración

### VMWare

En este caso usaré VMWare Workstation PRO 17. Es importante ingresar una licencia válida para poder usar todas las funcionalidades, podemos usar una de las que se encuentran en [este gist](https://gist.github.com/PurpleVibe32/30a802c3c8ec902e1487024cdea26251).

### Windows Server 2022

#### Instalación

- Descargar la ISO de Windows Server 2022 desde [aquí](https://www.microsoft.com/es-es/evalcenter/evaluate-windows-server-2022)
- Crear una nueva máquina virtual en VMWare (Usar `BIOS` en caso de encontrar algún problema a la hora de bootear)
- Instalar Windows Server 2022
- Cambiar nombre del equipo
- Configurar la red (IP estática y/o `127.0.0.1` en el primer DNS)
- Instalar las actualizaciones
- Instalar las herramientas de VMWare
- Crear un snapshot

#### Configuración

- Instalar el rol de `Active Directory Domain Services`
- Configurar el dominio (en este caso `rooyca.local`)

### Windows 10 Pro

#### Configuración

- Crear una snapshot
- Unirse al dominio