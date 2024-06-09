---
title: "Build y publica tus paquetes en COPR | Fedora"
date: 2023-08-18
type: page
tags: 
- "fedora"
- "linux"
- "build"
description: "Aprende a construir paquetes RPM y publicarlos en COPR"
image: "https://logowik.com/content/uploads/images/fedora8487.logowik.com.webp"
---

{{< box info >}}
Link original: [RPM Packaging](https://developer.fedoraproject.org/deployment/rpm/about.html)
{{< /box >}}

## Empaquetando RPM

Ejecuta:

```bash
sudo dnf install fedora-packager rpmdevtools gcc
rpmdev-setuptree
cd ~/rpmbuild/SOURCES
wget http://ftp.gnu.org/gnu/hello/hello-2.10.tar.gz
cd ~/rpmbuild/SPECS
rpmdev-newspec --macros hello.spec
```

Ahora puedes abrir el archivo `hello.spec` y modificarlo. Coloca el siguiente contenido:

```rpm
Name:     hello
Version:  2.10
Release:  1%{?dist}
Summary:  El programa "Hola Mundo" de GNU
License:  GPLv3+
URL:      http://ftp.gnu.org/gnu/hello
Source0: https://ftp.gnu.org/gnu/hello/hello-%{version}.tar.gz

BuildRequires: gettext

Requires(post): info
Requires(preun): info

%description
El programa "Hola Mundo", realizado con todas las características de un proyecto FOSS adecuado, incluyendo configuración, construcción, internacionalización, archivos de ayuda, etc.

%prep
%autosetup

%build
%configure
%make_build

%install
%make_install
%find_lang %{name}
rm -f %{buildroot}/%{_infodir}/dir

%post
/sbin/install-info %{_infodir}/%{name}.info %{_infodir}/dir || :

%preun
if [ $1 = 0 ] ; then
/sbin/install-info --delete %{_infodir}/%{name}.info %{_infodir}/dir || :
fi

%files -f %{name}.lang
%{_mandir}/man1/hello.1.*
%{_infodir}/hello.info.*
%{_bindir}/hello

%doc AUTHORS ChangeLog NEWS README THANKS TODO
%license COPYING

%changelog
* Mar Sep 06 2011 El Coon de Ty <Ty@coon.org> - 2.8-1
- Versión inicial del paquete
```

Una vez que hayas terminado, puedes ejecutar:

```bash
rpmbuild -ba hello.spec
```

Tu paquete debería estar listo en `~/rpmbuild/RPMS/x86_64/hello-2.10-1.fc27.x86_64.rpm`.

### Recursos
Puedes aprender más sobre todas las secciones y etiquetas del archivo SPEC en la documentación de Fedora ["Cómo crear un paquete RPM"](https://fedoraproject.org/wiki/How_to_create_an_RPM_package) o en la [Guía de Empaquetado RPM](https://rpm-packaging-guide.github.io/).

### Empaquetado para otras distribuciones
El comando `rpmbuild` crea un paquete solo para la arquitectura y distribución de tu propia estación de trabajo. Si deseas crear un RPM para otra distribución y otra arquitectura, necesitas usar [Mock](https://github.com/rpm-software-management/mock/wiki). Puede construir paquetes para diferentes arquitecturas y diferentes versiones de Fedora o RHEL. Mock crea *chroots* y construye paquetes en ellos. Su única tarea es crear un *chroot* e intentar construir un paquete ahí.

Para instalar Mock debemos ejecutar:

```bash
sudo dnf install mock mock-scm
sudo usermod -a -G mock TuNombreDeUsuario
newgrp mock
```

### Contruyendo paquetes con Mock
Antes de empezar a usar Mock, debes tener un SRC.RPM (también llamado SRPM). Puedes crearlo usando `rpmbuild`:

```bash
rpmbuild -bs hello.spec
```

Luego puedes ejecutar:

```bash
mock -r /etc/mock/epel-6-i386.cfg ~/rpmbuild/SRPMS/hello-2.10-1.fc27.src.rpm
```

Esto crea un paquete CentOS/RHEL para la arquitectura i386 incluso cuando la arquitectura de tu estación de trabajo es x86_64 y tu estación de trabajo es, por ejemplo, Fedora. La lista de configuraciones disponibles se puede encontrar en el directorio `/etc/mock/`.

Puedes crear un RPM directamente desde git (u cualquier otro SCM) usando el complemento `mock-scm`. Puedes probar con:

```bash
mock -r fedora-22-x86_64 --scm-enable --scm-option method=git --scm-option package=PKG --scm-option git_get=set --scm-option spec=YOUR.SPEC --scm-option branch=master --scm-option write_tar=True --scm-option git_get='git clone git@git_ip_address:SCM_PKG.git SCM_PKG'
```

{{< box info >}}
Para más información dirígete a la [documentación de mock-scm](https://fedoraproject.org/wiki/Projects/Mock/Plugin/Scm).
{{< /box >}}

{{< box tip >}}
Si estás construyendo un paquete de código abierto, puedes usar el servicio **Copr**, que automatiza la parte de `mock` y `createrepo_c` y proporciona una agradable interfaz web.
{{< /box >}}

## Usando Copr

Puedes encontrar un [tutorial con imagenes](https://docs.pagure.org/copr.copr/screenshots_tutorial.html#screenshots-tutorial) en la documentación.

### Creando un repositorio

1. Crear una cuenta en [Copr](https://copr.fedorainfracloud.org/).
2. Haz clic en "Crear nuevo repositorio".
3. Añadimos nombre, descripción e instrucciones.
4. Selecionamos los `chroots` que queremos usar.
5. Damos clic en "Create".

### Building el paquete

1. Vamos a la pestaña `Builds`
2. Damos clic en `New Build`
3. Seleccionamos el *source*
4. Añadimos la url del [spec](https://fedoraproject.org/wiki/How_to_create_an_RPM_package#Create_a_spec_file) del paquete o subimos el `src.rpm`
5. Seleccionamos los `chroots` que queremos usar.
6. Damos clic en `Build`

### Instalando el repositorio

Para comprobar que el repositorio se ha creado correctamente, ejecutamos:

```bash
sudo dnf copr enable TuNombreDeUsuario/NombreDelRepositorio
```

Y después:

```bash
sudo dnf install NombreDelPaquete
```

## Bonus

### Archivos SPEC

- Python package: [hmta.spec](https://github.com/Rooyca/hmta/blob/master/hmta.spec)
- Python package: [requests.spec](https://src.fedoraproject.org/rpms/python-requests//blob/rawhide/f/python-requests.spec)




