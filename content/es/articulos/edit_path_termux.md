---
title: "Editar PATH en Termux"
date: 2024-06-01
tags: 
- "termux"
- "android"
- "rust"
- "short"
description: "Cómo editar el PATH en Termux"
image: "https://media.licdn.com/dms/image/D5612AQESFo5ZnEAV7g/article-cover_image-shrink_600_2000/0/1707063399974?e=2147483647&v=beta&t=pkJPbSjj5aIIqeo0j5LnvZ2fyEqAlHkAf_f6_ovp9Yw"
---

En `$HOME`, usando nuestro editor de confianza, editamos `.bash_profile file` (si no existe, lo creamos)

Por ejemplo, si queremos añadir [cargo](https://doc.rust-lang.org/cargo/) a nuestro PATH, nuestro archivo final debería quedar algo así:

```
PATH="$PATH:/data/data/com.termux/files/home/.cargo/bin"
```