---
title: "Crea tu blog con HUGO y publicalo en Cloudflare Pages"
date: 2023-07-27
type: page
tags: 
- "cloudflare"
- "blog"
- "hugo"
description: Crear un blog con HUGO (un generador de sitios estáticos) y públicalo en Cloudflare Pages usando GitHub.
---

{{< box info >}}
[**POST Original**](https://developers.cloudflare.com/pages/framework-guides/deploy-a-hugo-site/)
{{< /box >}}

## Instalar Hugo

Instala el CLI de Hugo, con las instrucciones adecuadas para tu sistema operativo:

### Linux

Hugo puede venir incluido en el `package manager` de tu distro. Si ese es el caso, puedes instalarlo directamente usando el `package manager`. - por ejemplo, en Ubuntu, usa el siguiente comando:

```bash
sudo apt-get install hugo
```

Si, por el contrario, tu `package manager` no incluye Hugo o deseas descargarlo directamente, dirigete a la parte de [Instalación Manual](#instalación-manual).

### Homebrew (macOS)

Si usas el `package manager` [Homebrew](https://brew.sh/), puedes correr el comando `brew install` para instalar Hugo:

```bash
brew install hugo
```

### Windows (Chocolatey)

Si usas el `package manager` de [Chocolatey](https://chocolatey.org/), puedes correr el comando `choco install` para instalar Hugo:

```bash
choco install hugo --confirm
```

### Windows (Scoop)


Si usas el `package manager` de [Scoop](https://scoop.sh/), puedes correr el comando `scoop install` para instalar Hugo:

```bash
scoop install hugo
```

### Instalación Manual

El repositorio de GitHub de Hugo contiene versiones `pre-built` de la `CLI` para varios sistemas operativos, puedes revisar en la [página de `Rreleases`](https://github.com/gohugoio/hugo/releases).

Para conocer cómo instalar estas `releases` puedes dirigirte a la [Documentación de Hugo](https://gohugo.io/getting-started/installing/).

## Crea un nuevo proyecto

Con Hugo ya instalado, puedes dirigirte a ["Comenzar con Hugo"](https://gohugo.io/getting-started/quick-start/) para crear tu proyecto o simplemente ejecutas el comando `hugo new` en tu terminal:

```bash
hugo new site my-hugo-site
```

En Hugo se usan temas para personalizar la apariencia y la "sensación" de los sitios estaticos. Existe un gran número de temas disponibles en [themes.gohugo.io](https://themes.gohugo.io/) – por ahora puedes usar el [Tema Terminal](https://themes.gohugo.io/hugo-theme-terminal/) para ello corre el siguiente comando:

```bash
$ cd my-hugo-site
$ git init
$ git submodule add https://github.com/panr/hugo-theme-terminal.git themes/terminal
$ git submodule update --init --recursive
```

También deberías copiar a tu archivo `config.toml` la configuración por defecto que te ofrece el tema, este archivo lo encuentras en el directorio principal de tu proyecto. Toma la siguiente información y adaptala a las necesidades de tu proyecto:

```bash
baseurl = "/"
languageCode = "es-es"
theme = "terminal"
paginate = 5

[params]
  # nombre del directorio principal del contenido (por defecto es `content/posts`)
  # la lista del contenido aparecera en tu página principal (baseurl)
  contentTypeName = "posts"

  # ["orange", "blue", "red", "green", "pink"]
  themeColor = "orange"

  # si configuras este valor a 0, solo el submenu será visible
  showMenuItems = 2

  # mostar el seleccionador de idiomas
  showLanguageSelector = false

  # ancho del tema en pantalla completa
  fullWidthTheme = false

  # centrar el tema con el ancho por defecto
  centerTheme = false

  # favicon personalizado (por defecto es un `themeColor` square)
  # favicon = "favicon.ico"

  # mostrar la última actualización
  # Si usas git, puedes poner `enableGitInfo` como `true` y las publicaciones automaticamente obtienen la fecha de la última actualización 
  showLastUpdated = false
  # Da una `string` como prefijo para la fecha de la última actualización. Por defecto, luce así: 2020-xx-xx [Updated: 2020-xx-xx] :: Author
  # updatedDatePrefix = "Updated"

  # configura todos los `headings` al tamaño por defecto (depende de la configuración del navegador)
  # está como `true` por defecto
  # oneHeadingSize = false

[params.twitter]
  # configura Twitter handles para las targetas de Twitter
  # ver https://developer.twitter.com/en/docs/tweets/optimize-with-cards/guides/getting-started#card-and-content-attribution
  # NO incluir @
  creator = ""
  site = ""

[languages]
  [languages.en]
    languageName = "Spanisg"
    title = "Terminal"
    subtitle = "Un sencillo tema retro para Hugo"
    owner = ""
    keywords = ""
    copyright = ""
    menuMore = "Ver más"
    readMore = "Leer más"
    readOtherPosts = "Leer los otros posts"
    missingContentMessage = "Página no encontrada..."
    missingBackButtonLabel = "Regresar a la página de inicio"

    [languages.en.params.logo]
      logoText = "Terminal"
      logoHomeLink = "/"

    [languages.en.menu]
      [[languages.en.menu.main]]
        identifier = "about"
        name = "About"
        url = "/about"
      [[languages.en.menu.main]]
        identifier = "showcase"
        name = "Showcase"
        url = "/showcase"
```

## Crear un nuevo post

Crea un nuevo post para darle algo de contenido a tu nuevo sitio. Corre el comando `hugo new` para generar un nuevo post:

```bash
hugo new posts/hola-mundo.md
```

Dentro de `hola-mundo.md`, añade algo de contenido para crear tu post. CUando estes listo para publicar elimina la linea que dice `draft`, la encuentras en el `frontmatter` (lo que va en medio de los `---`) de tu post. Cualquier Post con `draft: true` no se tendrá en cuenta a la hora de contruir el sitio.

## Crea un repositorio en GitHub

Visita [repo.new](https://repo.new/) para crear un nuevo repositorio en GitHub. Después has `push` de tu aplicación local al repositorio que acabas de crear, para ello corre los siguientes comandos en tu terminal:

```bash
$ git remote add origin https://github.com/<your-gh-username>/<repository-name>
$ git branch -M main
$ git push -u origin main
```

## Desplegar en Cloudflare Pages

Para desplegar tu sitio en `Pages` necesitas:

1. Inglresar al [Cloudflare dashboard](https://dash.cloudflare.com/) y seleccionar tu cuenta.
2. En la página de inicio selecciona **Workers & Pages** > **Create application** > **Pages** > **Connect to Git**.
3. Después selecciona el repositorio que creaste y en la parte de **Set up builds and deployments**, proporciona la siguiente información:

Opción de configuración | Valor
---|---
Production branch | `main`
Build command | `hugo`
Build directory | `public`
Base URL configuration

Hugo te permite configurar la `baseURL` de tu aplicación. Esto te permite utilizar `absURL` para construir `canonical URLs`. Para hacer esto con `Pages`, debes pasar los parametros `-b ` o `--baseURL` con la variable de entorno `CF_PAGES_URL`.

El comando debería lucir algo así:

```bash
hugo -b $CF_PAGES_URL
```

Después de completar la configuración damos clic en el botón de **Save and Deploy**. Deberías ver a Cloudflare Pages instalando `hugo` y las dependencias de tu proyecto, y luego construyendo tu sitio, antes de desplegarlo.

Para una guía completa sobre el despliegue en Cloudflare Pages puedes dirigirte a la [Guía Inicial](https://developers.cloudflare.com/pages/get-started/).

Una vez desplegado tu sitio recibirás un subdominio único para tu proyecto, algo como `*.pages.dev`. Cada que haces un `commit` a tu repositorio, Cloudflare Pages automaticamente volverá a contruir y desplegar tu proyecto. También tienes acceso a una [vista previa](https://developers.cloudflare.com/pages/platform/preview-deployments/), para que puedas ver cómo lucen tus cambios antes de enviarlos a producción.


## Usa una versión específica de Hugo

Para usar una [versión específica de Hugo](https://github.com/gohugoio/hugo/releases), crea la variable de entorno `HUGO_VERSION` en tu **Pages project** > **Settings** > **Environment variables**. Configura el valor según tus gustos o necesidades.

Por ejemplo, `HUGO_VERSION: 0.110.0`

