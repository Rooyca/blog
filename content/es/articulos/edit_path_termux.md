---
title: "Editar PATH en Termux"
date: 2024-06-01
tags: 
- "termux"
- "android"
- "rust"
- "short"
description: Cómo editar el PATH en Termux
topic: termux,rust,android,short
---

En `$HOME`, usando nuestro editor de confianza, editamos `.bash_profile file` (si no existe, lo creamos)

Por ejemplo, si queremos añadir [cargo](https://doc.rust-lang.org/cargo/) a nuestro PATH, nuestro archivo final debería quedar algo así:

```
PATH="$PATH:/data/data/com.termux/files/home/.cargo/bin"
```