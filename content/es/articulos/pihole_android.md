---
title: "Pi-hole para Android - [ROOT]"
date: 2023-05-06
type: page
tags: 
- "Pi-Hole"
- "Linux"
- "Android"
description: Instala Pi-Hole para monitorear tu red en tu dispositivo android (rooteado).
topic: pi-hole,android,linux
---

- Post original: [Aquí](https://github.com/DesktopECHO/Pi-hole-for-Android)

**Nota**: No hay ningún respaldo o asociación entre esta página y [Pi-hole© LLC](https://pi-hole.net/). Si encuentras esto útil, ellos [merecen tu apoyo](https://pi-hole.net/donate/).

Pi-hole es una aplicación para el bloqueo de anuncios y rastreadores para Linux que actúa como un "agujero negro" de DNS destinado mayoritariamente para el uso en redes privadas. Está diseñado para dispositivos integrados de baja potencia con capacidad de red, enfocándose en la Raspberry Pi como su plataforma de hardware de referencia.

`Pi-hole para Android` es una imagen de disco de Raspbian ajustada para funcionar con el instalador de Pi-hole en Pi Deploy (un fork de Linux Deploy). Se puede utilizar en cualquier dispositivo Android rooteado con una CPU `ARMv7` o más reciente, que ejecute Android 5.0 (Lollipop) o más reciente. El dispositivo como tal no es reelevante; podría ser un teléfono, una tableta, un stick HDMI o cualquier dispositivo que ejecute Android. Para dispositivos muy antiguos que ejecutan Android 4.x, consulta la rama [Legacy](https://github.com/DesktopECHO/Pi-hole-for-Android/tree/legacy).

## Requisitos: 

- Dispositivo Android que haya sido rooteado. 

## Nota para usuarios de versiones anteriores: 

- Desinstala cualquier versión anterior de Linux Deploy o Pi Deploy y reinicia tu dispositivo. 
- ¡No hacer caso de este consejo causará problemas!

## Instalación:

- Instala la última versión de [APK de Pi Deploy](https://github.com/DesktopECHO/Pi-hole-for-Android/releases/latest/download/pideploy.apk).
- Toca el menú principal (tres puntos en la parte superior derecha de la pantalla).
- Presiona "**Instalar**".
- En unos minutos, la [imagen de Raspbian Pi-hole](https://github.com/DesktopECHO/Pi-hole-for-Android/releases/latest/download/raspbian.tgz) se descargará e instalará en tu dispositivo.
- Cuando se complete el despliegue, toca [ ▷ **INICIAR** ] para lanzar la instancia.
- La instancia te proporcionará una contraseña para iniciar sesión en el panel de administración web de Pi-hole o vía SSH/RDP (Nombre de usuario: android).
- **Nota**: La contraseña aparece solo una vez cuando se despliega la imagen, asegúrate de registrar esta información.
- **Concejo**: El texto de la contraseña se puede resaltar y copiar en tu portapapeles.

![PASSWORD_PI_HOLE](https://user-images.githubusercontent.com/33142753/196851777-e46b145f-4c99-4b6f-9add-ed2f009dae4b.png)

---

**INSTALACIÓN COMPLETA    ·    Pi-Hole está corriendo en tu dispositivo Android**

---

La dirección IP del dispositivo Android se muestra en la parte superior de la ventana principal de Pi Deploy. Puedes interactuar con Pi-hole de varias maneras. Los ejemplos a continuación usan la dirección IP **10.73.0.31**.

- Desde un escritorio con Windows, conéctate a través de **RDP** -> `mstsc.exe /v:10.73.0.31`

- Desde una computadora que ejecute Linux, conéctate a través de SSH -> `ssh android@10.73.0.31`

- La interfaz administrativa de Pi-hole es accesible desde cualquier navegador en tu red -> `http://10.73.0.31/admin`

- Si tu dispositivo Android tiene una pantalla, puedes conectarte a la instancia de Pi-hole a través de RDP (como localhost) instalando el cliente de [Escritorio Remoto de Microsoft](https://play.google.com/store/apps/details?id=com.microsoft.rdc.androidx), o cualquier otro cliente de RDP.

![SSH_PI_HOLE](https://user-images.githubusercontent.com/33142753/196856874-72c307e3-2227-4ef1-a7b5-401e745f918f.png)

---

## Información Adicional:

- Es recomendable que la primera vez que inicies sesión en tu instancia, por SSH o RDP, ejecutes en la terminal los siguientes comandos para actualizar tu sistema y remover los paquetes innecesarios:

	```bash
	sudo apt-get update
	sudo apt-get upgrade -y
	sudo apt autoremove
	```
  
- Las sesiones de RDP ejecuta el gestor de ventanas Openbox con QTerminal en modo pantalla completa. Para abrir una nueva pestaña, presiona [**Ctrl-Shift-T**], y para mostrar la barra de menú, presiona [**Ctrl-Shift-M**].

- Puedes reiniciar (o "rebootear") la instancia de Pi-hole en Pi Deploy presionando [ ■ STOP ] y esperando unos segundos para que la instancia indique que todos los servicios se han detenido. Reinicia la instancia presionando [ ▸ START ].

- Cuando se inicia una instancia de Pi-hole, la configuración predeterminada es permitir que se configure automáticamente la red. Si cambias de red en el dispositivo Android, simplemente reinicia la instancia para que Pi-hole recoja la nueva configuración.
  Alternativamente, establece una asignación estática comentando dos líneas en `/etc/init.d/android-init` (verás cuáles son cuando abras el archivo en un editor). Después de que las líneas estén comentadas con un hash "#", puedes agregar manualmente tu dirección IP, sub-red y nombre de interfaz a `/etc/pihole/setupVars.conf`.

- Se ha agregado la última versión de [Unbound 1.17](https://www.nlnetlabs.nl/projects/unbound/about) para proporcionar DNS cifrados por defecto. No es necesaria ninguna configuración adicional, pero puedes personalizarlo según tus preferencias.

- Ajusta la escala de visualización de QT: `~/startwm.sh`

- Cambia el tamaño de fuente en QTerminal: `~/.config/qterminal.org/qterminal.ini`

---


Y eso sería todo, así de fácil es instalar Pi-hole en Android. Recuerda que si tienes alguna duda, queja, sugerencia o reclamo puedes hacérmela saber.

---