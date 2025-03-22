---
title: "Editar PATH en Termux"
date: 2024-06-01
tags: 
- "termux"
- "android"
- "rust"
- "short"
description: "Cómo editar el PATH en Termux"
image: "https://miro.medium.com/v2/resize:fit:1400/1*5R38Aa6bT9pLLYwL3fwDBA.png"
---

En `$HOME`, usando nuestro editor de confianza, editamos `.bash_profile file` (si no existe, lo creamos)

Por ejemplo, si queremos añadir [cargo](https://doc.rust-lang.org/cargo/) a nuestro PATH, nuestro archivo final debería quedar algo así:

```
PATH="$PATH:/data/data/com.termux/files/home/.cargo/bin"
```