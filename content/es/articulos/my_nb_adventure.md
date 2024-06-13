---
title: "Una aplicación para gobernarlas a todas | nb"
date: 2024-06-10
type: page
tags: 
- "cli"
- "notes"
- "nb"
- "notetaking"
description: "Maneja todas tus notas, tareas, marcadores y demás con una sola aplicación"
image: "https://raw.githubusercontent.com/xwmx/nb/master/docs/assets/images/nb.png"
---

![nb](https://raw.githubusercontent.com/xwmx/nb/master/docs/assets/images/nb.png)

{{< box info >}}
**Ultima actualización:** *Junio 12, 2024*
{{< /box >}}

Siempre he sentido la necesidad de escribir (más que nada para recordar. *Este blog es un claro ejemplo*), pero siempre estaba la pregunta "¿Dónde debería escribir esto?".

En este post quiero contar un poco mi experiencia y trayectoria con la toma de notas. Quizás esta no sea la solución final y quizás tampoco sea la solución para ti, pero es la que me ha funcionado a mi, en este momento de mi vida. Es por eso que trataré de ir actualizando este post con el tiempo, para ver cómo evoluciona.

## ¿Qué tipo de notas tomo?

Puede que hayas escuchado del método [Zettelkasten](https://en.wikipedia.org/wiki/Zettelkasten), a saber: un sistema de toma de notas que se basa en la interconexión de las mismas. Aunque no lo sigo al pie de la letra, me gusta la idea de tener notas interconectadas. Es por eso que me gusta tener mis notas en *Markdown*.

Las notas que tomo generalmente son:

> - **Notas de estudio** (aprendizaje autodidacta o con alguna institución)
> - **Notas de proyectos personales** (idea inicial, desarrollo, problemas, soluciones, etc)
> 	- En algunos casos divido los proyectos en tareas y sub-tareas (todas unidas con una misma etiqueta, o múltiples etiquetas)
> - **Notas de configuración** (de programas, sistemas, etc)
> - **Notas de libros** (resumen, ideas, citas, etc)
> - **Notas sobre álbumes** (calificación, fecha de escucha y metadata como: genero, año, etc)
> - **Tareas generales** (cosas que tengo que hacer, recordatorios, etc). Siempre intento añadir etiquetas para poder filtrarlas de > manera más sencilla.
> - **Marcadores** (de páginas web, artículos, etc)
> - **Notas generales** (ideas, pensamientos, etc)

## Aplicaciones que usé

### Obsidian

Durante un largo tiempo (y aún... en algunos casos) utilicé [Obsidian](https://obsidian.md/) para tomar notas. Lo facinante (al menos para mí) de Obsidian es la extensibilidad mediante plugins hechos por la comunidad. Literal, el límite de lo que puedes hacer en Obsidian es tu imaginación. 

Lo que me hizo buscar alternativas fue la idea de que usar Obsidian era demasiado para el tamaño de mis notas. Pero sobre todo, que los plugins son un arma de doble filo, te ofrecen muchas funcionalidades pero esas funcionalidades solo están disponibles en Obsidian. Por lo que empecé a buscar alternativas más sencillas, donde las notas estuvieran en *texto plano*.

### Neovim

En mi búsqueda de un entorno minimalista cambié mi configuración (antes usaba [LazyVim](https://lazyvim.github.io/)) por [kickstart.nvim](https://github.com/nvim-lua/kickstart.nvim). **¡Me encantó!** Utilicé esta configuración por más de medio año. 

Lo que hice fue dividir por carpetas el tipo de notas, ejemplo `100-notas`, `200-proyectos`, etc. Al final era demasiado tedioso tener que entrar a una carpeta como por ejemplo:

```
100-Estudio/100.1_Matemáticas/100.1.1_Cálculo/100.1.1.1_Derivadas/
```
para crear una nota, por lo que, una vez más, empecé a buscar alternativas.

### nb

A [nb](https://github.com/xwmx/nb) llegué por casualidad (*la única forma en que llegan las cosas buenas*). A primera vista me pareció sobresaturada y con demasiadas funcionalidades. Con el tiempo me fue gustando (sola la usaba para crear tareas).

Después me gustó aún más, por la facilidad de crear notas desde la terminal; recuerdas que debes hacer algo, abres la terminal y escribes `nb todo "Hacer algo"`. Easy!.

### Google Keeps

A veces se me ocurria una idea, o recordaba que debía que hacer algo después de haber apagado la computadora. Entonces usaba *Google Keeps* para anotar la idea o la tarea y después, cuando encendía la computadora, la pasaba a `nb`. Claramente esta no era la mejor solución. Pasé un buen tiempo pensando alternativas, quería seguir usando `nb`, pero no veía como guardar mis notas desde el celular. [Ver solución](#desde-google-keeps)

### buku

Para los marcadores usaba [buku](https://github.com/xwmx/nb), una excelente herramienta porque antes usaba los marcadores del navegador, y aunque es cierto que los marcadores del navegador son muy útiles, al exportar los marcadores los navegadores no generan un archivo de texto. Es por eso que exporté mis marcadores de *Firefox*, los importé en buku y listo.

Luego apareció el problema de la búsqueda, aunque buku guarda los marcadores en una base de datos sql es un poco raro a la hora de hacer búsquedas. A veces, los resultados no eran precisos (al menos en mi caso, quizás haya sido la forma en que hacia las búsquedas).

## Todo en una sola aplicación

Todo este conjunto de aplicaciones y servicios me llevó a la necesidad de cambio (nuevamente). Necesitaba una sola aplicación que me permitiera tomar notas, guardar marcadores, crear tareas, y que fuera fácil de usar. Por lo que decidí pasarme por completo a `nb`.

### ¿Qué es nb?

`nb` es una CLI (command line interface) para tomar notas, guardar marcadores, almacenar y crear bases de conocimientos con funcionalidades como:

{{< box info >}}
El <✅> señala las funcionalidades que buscaba
{{< /box >}}

```markdown
- almacenamiento en texto plano  ✅
- cifrado
- filtrado, pinning, etiquetado y búsqueda
- versionado y sincronización con Git
- conversión de documentos con Pandoc
- liks de estilo [[wiki]] ✅
- navegación en terminal y buscador web GUI
- tareas con subtareas ✅
- notebooks globales y locales
- organización por carpetas
- temas de colores personalizables
- extensibilidad a través de plugins ✅
- y mucho más... Todo esto en un solo script de bash.
```

{{< box tip >}}
A diferencia de **Obsidian**, aquí aunque usemos plugins las notas siguen siendo texto plano y las podremos leer con **cualquier editor de texto**
{{< /box >}}

### Instalación

La forma más sencilla es mediante `npm`, pero hay muchas otras (ver [guía de instalación](https://github.com/xwmx/nb#installation)).

```bash
npm install -g nb.sh
```

Después de finalizar la instalación, ejecuta `sudo "$(which nb)" completions install` para instalar los scripts de autocompletado de Bash y Zsh (**recomendado**).

### Configuración

Para configurar `nb` podemos user `nb settings` o exportar las variables correspondientes, por ejemplo, si queremos desactivar la sincronización con Git:

```bash
export NB_AUTO_SYNC="0"
```

### Uso

La [documentación oficial](https://xwmx.github.io/nb) es bastante extensa y detallada, pero aquí te dejo algunos de los comandos que utilizo día a día:

```bash
nb a "Título de la nota" # Crea una nueva nota
nb t a "Tarea por hacer" # Crea una nueva tarea
nb rm ID ID ID # Borra tareas con ID
nb do ID # Marca una tarea como completada
nb undo ID # Marca una tarea como no completada
nb https://www.google.com --tags search # Guarda un marcador con la etiqueta search
nb t # Muestra todas las tareas
nb t open # Muestra todas las tareas sin completar
nb t open --tags blog # Muestra todas las tareas sin completar con la etiqueta blog
nb t --tags tag1,tag2 # Muestra todas las tareas con las etiquetas tag1 y tag2
```

Entre muchos otros.

### Migración e integración

Entonces necesitamos pasar de `neovim`, `buku` y `keeps` a `nb`.

#### Desde Neovim

Esta es la más fácil de las migraciones, simplemente ejecutas:

```bash
nb import ~/PATH/TO/NOTES/*
```

O, si solo quieres importar markdown:

```bash
nb import ~/PATH/TO/NOTES/*.md
```

#### Desde buku

Aquí ya la cosa se complica un poco, no mucho. Primero exportamos los marcadores de buku, luego, con un sencillo *script*, iteramos sobre los marcadores y los importamos a `nb`. Puedes ver la guía completa y el código en mi publicación [Migrar de buku a nb con Python](from_buku_to_nb).

#### Desde Musicboard

La aplicación que uso para llevar registro de mis álbumes se llama [musicboard](https://musicboard.app/) y para importar dicha información seguí un proceso un tanto enredado que explicaré más adelante.

{{< box important >}}
Esta parte se actualizará próximamente
{{< /box >}}

#### Desde Google Keeps

Más que importar las notas desde `Keeps` lo que necesitaba era una forma de tomar dichas notas desde mi celular (o cualquier otro dispositivo) aunque la computadora estuviera apagado. Para esto utilicé [nb-mobile](https://github.com/Rooyca/nb-mobile); una sencilla aplicación web que desarrollé. Nada del otro mundo, utiliza [flask](https://github.com/pallets/flask) tanto para la api como para el frontend. 

{{< box warning >}}
Antes de usar [nb-mobile](https://github.com/Rooyca/nb-mobile) necesitas exportar la variable `IP` con tu ip local. (Ver [.env.example](https://raw.githubusercontent.com/Rooyca/nb-mobile/master/webpage/.env.example))
{{< /box >}}

Aproveché un celular antiguo que nadie estaba utilizando (ver [Instalar Termux en Android 5 o 6 | Samsung J2 Prime](run_termux_android_six) y le instalé [termux](https://termux.com/). 

En `termux` instalé `git` y `python`. Luego cloné el repositorio de `nb-mobile` y ejecuté el servidor y el frontend. 

Luego, para *sincronizar* las notas en `nb` cree un alias en mi computador que corriera [save_notes_to_nb.py](https://github.com/Rooyca/nb-mobile/blob/master/save_notes_to_nb.py) antes de ejecutar `nb`.

```bash
alias nb="python $HOME/notes/save_notes_to_nb.py; nb"
```

Así conseguí tomar notas desde cualquier dispositivo (conectado a mi red local) y guardarlas en `nb`.

## Conclusión

`nb` es una excelente herramienta que me ha permitido tener todas mis notas, tareas, marcadores y demás en un solo lugar. Aunque aún no he migrado por completo, estoy seguro de que `nb` es la solución que estaba buscando. 

{{< signature standard >}}







