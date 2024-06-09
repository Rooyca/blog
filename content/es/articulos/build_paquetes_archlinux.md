---
title: "Build y publica tus paquetes en AUR | Archlinux"
date: 2023-08-16
type: page
tags: 
- "arch"
- "linux"
- "build"
description: "En este artículo aprenderás a publicar paquetes en el repositorio de usuarios (AUR) de Arch."
image: "https://i0.wp.com/discovery.endeavouros.com/wp-content/uploads/2021/03/AUR.png"
---

{{< box warning >}}
El contenido de este artículo se basa en mi experiencia personal, por lo que pueden haber errores.
{{< /box >}}

{{< box important >}}
Este artículo no profundiza en muchos temas, por lo que es recomendable leer la [documentación](https://wiki.archlinux.org/title/Arch_User_Repository) oficial de Arch.
{{< /box >}}

{{< box tip >}}
Resultado final: [hmta](https://aur.archlinux.org/packages/hmta/)
{{< /box >}}


## Introducción

El repositorio de usuarios de Arch (AUR) es un repositorio impulsado por la comunidad y para la comunidad. Contiene descripciones de paquetes (**PKGBUILD**) que te permiten compilar un paquete desde el código fuente con **makepkg** y luego instalarlo a través de **pacman**. El AUR se creó para organizar y compartir nuevos paquetes de la comunidad y para ayudar a acelerar la inclusión de paquetes populares en el repositorio adicional.

## Requisitos

- Tener una cuenta en [AUR](https://aur.archlinux.org/).
- Tener instalado [**base-devel**](https://archlinux.org/packages/?name=base-devel).
- Tener instalado [**git**](https://archlinux.org/packages/?name=git).
- Tener el sistema actualizado.

## Creando y publicando el paquete

### Script

En este ejemplo usaré **hmta**, un script en python el cual nos permite conocer cuánto tiempo tardariamos en ver un anime. Puedes ver el código fuente en [GitHub](https://github.com/Rooyca/hmta).


```bash
git clone https://github.com/Rooyca/hmta.git
```

### SSH

Para poder subir el paquete a AUR, necesitamos configurar una llave SSH. Aunque ya tengas una configurada, es recomendable crear una nueva.

```bash
ssh-keygen -f ~/.ssh/aur
```

{{< box tip >}}
Puedes añadir multiples llaves públicas a tu perfil separandolas con un salto de linea.
{{< /box >}}

### Creando un repositorio para el paquete

```bash
git -c init.defaultbranch=master clone ssh://aur@aur.archlinux.org/PKGNOMBRE.git
```

Si el paquete no existe, se creará un nuevo repositorio. En caso de que ya exista, se clonará el repositorio. El output cuando se crea un nuevo repositorio es el siguiente:

```bash
Cloning into 'PKGNOMBRE'...
warning: You appear to have cloned an empty repository.
Checking connectivity... done.
```

### Publicando el paquete

En el repositorio que acabamos de clonar, creamos un archivo llamado **PKGBUILD** con el siguiente contenido:

```bash
# Maintainer: rooyca <rooyca.f9rnz at aleeas dot com>
pkgname=hmta
pkgver=1.0.1
pkgrel=1
pkgdesc="Do you wanna know how much time would take you to watch an anime? Well, this is your tool."
arch=('i686' 'x86_64')
url="https://github.com/Rooyca/hmta"
license=('MIT')
depends=('python' 'python-urllib3')
source=("https://github.com/Rooyca/hmta/archive/refs/tags/v$pkgver.tar.gz")
sha256sums=('590f76d949efecaebcdc9ce818e80e1d3f5fa6dd13467ee458a1bfb5dafe29f9')

build() {
    cd "$srcdir/hmta-$pkgver"
}

package() {
    cd "$srcdir/hmta-$pkgver"
    install -Dm755 hmta.py "$pkgdir/usr/bin/hmta"
}
```

{{< box info >}}
Puedes encontrar más información sobre el archivo **PKGBUILD** en la [documentación oficial](https://wiki.archlinux.org/title/PKGBUILD).
{{< /box >}}

Podemos testear que el paquete se construye correctamente con el comando **makepkg**.

```bash
makepkg -si
```

Si todo sale bien, podemos continuar con el proceso de publicación.

Ahora nos aseguramos de crear el archivo **.SRCINFO**.

```bash
makepkg --printsrcinfo > .SRCINFO
git add PKGBUILD .SRCINFO
git commit -m "Initial commit"
git push
```

### Después de publicar el paquete

Una vez publicado el paquete, podemos instalarlo con **yay**.

```bash
yay -S hmta
```

Otra cosa que podemos hacer después de publicar el paquete es añadirle *palabras claves*, para ello nos dirigimos a la página del paquete y en la parte inferior derecha encontraremos el apartado **Keywords**.