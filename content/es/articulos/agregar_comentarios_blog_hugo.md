---
title: "Agregar comentarios a tu blog usando Github Issues | Utteranc.es"
date: 2024-03-10
type: page
tags: 
- "hugo"
- "blog"
- "github"
description: "Agrega comentarios a tu Hugo blog usando Github Issues"
---

> [Post original](https://mscipio.github.io/post/utterances-comment-engine/)

[Utteranc.es](https://utteranc.es/) es un widget liviano el cual nos permite usar los `Github Issues` como los comentarios de nuestro blog. Es de código abierto, se ve estético, los comentarios se guardan en Github y lo mejor, viene con temas oscuros. SÍ, tienes que iniciar sección con Github, pero está bien porque la gran mayoría tiene una cuenta.

## Pasos para instalar

1. Para que Utterances funcione de forma correcta es necesario tener un sitio en un repositorio público en Github.
2. Instalar la app de Utterances en el repo.

![install](https://mscipio.github.io/img/posts/utterances/app-setup.png)

3. Ingresa a la [página oficial de Utterances](https://utteranc.es/) y llena el formulario. Se generará un HTML que deberás pegar en tu sitio web. La información que te pedirá el formulario es:

> - **nombre del repositorio**: generalmente es algo como <usuario>/<repo>
> - **issue-term**: el término que se usará para identificar los comentarios. (ejemplo: `title`)
> - **etiqueta**: como los comentarios se manejarán vía GitHub Issues, necesitas una etiqueta para diferenciar cuales son comentarios y cuales no. (ejemplo: `Comentario`)

4. Copia el HTML.
5. Ve a la carpeta donde tienes tu blog y dirígete hasta `themes/TU-TEMA/layouts/partials` 
6. Pega lo que copiaste en el paso 4 en archivo `comments.html` (si no existe, créalo)

```html
<script src="https://utteranc.es/client.js"
  repo="mscipio/mscipio.github.io"
  issue-term="title"
  label="Comment"
  theme="github-dark"
  crossorigin="anonymous"
  async>
</script>
```

7. Por último, dirígete hasta `themes/academic/layouts/_defaults/single.html` y pega el siguiente código en la sección donde quieres que aparezcan los comentarios (generalmente al final).


```html
<div class="article-container">
	{{ partial "comments.html" . }}
</div>
```

8. Guarda los cambios y listo. Ahora deberías tener comentarios en tu blog.
9. Puedes ver un ejemplo aquí! 👇 

