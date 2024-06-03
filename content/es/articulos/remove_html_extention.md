---
title: "Eliminar la extensión .html de una URL"
date: 2024-06-03
type: page
tags: 
- "HTML"
description: "Cómo eliminar la extensión .html de una URL en un sitio web estático."
topic: HTML
---

Si no sabes de lo que estoy hablando, es cuando visitas un sitio web y la URL se ve algo así https://example.com/about.html. Específicamente, la ultima parte `.html`. La mayoría de las personas les gustaría ver algo como https://example.com/about. ¿Es más bonito, no te parece?

Afortunadamente, es bastante fácil de hacer en un sitio web estático. Aquí te muestro dos formas de hacerlo.

## Método 1: Poner las páginas en carpetas

Por ejemplo, si tenemos una estructura de páginas como esta:

```markdown
|-- 📄 index.html
|-- 📄 about.html
```

Podemos mover las páginas a carpetas con el mismo nombre:

```markdown
|-- 📄 index.html
|-- 📁 about
     |-- 📄 index.html
```

Eso sería todo. Ahora, si visitas https://example.com/about, se mostrará el contenido de `about/index.html`.

Bueno, todo está bien si solo tienes un par de páginas en tu sitio web. ¿Qué pasa si tienes un blog con decenas, o incluso cientos de páginas? Puede que no sea factible reestructurar todo tu sitio para eliminar la extensión HTML de tus URLs.

## Método 2: Usar un archivo `.htaccess`


Otra alternativa es usar el archivo `.htaccess` para eliminar la extensión de la URL. Esto se hace de manera muy simple en unas pocas líneas.

En el directorio raíz `/`, creamos un archivo `.htaccess` y agregamos el siguiente código:


```markdown
RewriteEngine On
RewriteCond %{REQUEST_FILENAME} !-f
RewriteRule ^([^/]+)/$ $1.html
RewriteRule ^([^/]+)/([^/]+)/$ /$1/$2.html
RewriteCond %{REQUEST_FILENAME} !-f
RewriteCond %{REQUEST_FILENAME} !-d
RewriteCond %{REQUEST_URI} !(\[a-zA-Z0-9]{1,5}|/)$
RewriteRule (.*)$ /$1/ [R=301,L]
```
Lo que este fragmento de código hace es eliminar `.html` de la URL. También redirigirá a cualquiera que visite una URL con `.html` al final, por lo que no deberías recibir errores 404 si las personas visitan la URL completa.

## Conclusión

Eso sería todo. Con cualquiera de estas soluciones, deberías poder eliminar la extensión HTML de todas tus URL. Si tienes alguna pregunta, no dudes en dejarme un comentario.

---

**Post original (en Inglés)**: https://kevquirk.com/how-to-remove-the-html-extension-from-a-url