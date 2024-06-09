---
title: "Build y publica tus paquetes en PyPI"
date: 2023-08-17
type: page
tags: 
- "pypi"
- "python"
- "build"
description: "En la siguiente guía aprenderemos, de manera sencilla, a crear y publicar paquetes en PyPI."
image: "https://datascientest.com/en/wp-content/uploads/sites/9/2024/03/pypi-datascientest-1024x512-1.png"
---

## Introducción

En PyPI los desarrolladores pueden publicar sus paquetes Python para que otros los descarguen e instalen en sus propios proyectos. Esto hace que sea mucho más fácil compartir y reutilizar código en la comunidad de desarrolladores de Python. Los paquetes pueden contener desde simples módulos con funciones y clases hasta frameworks y herramientas complejas.

## Requisitos

1. Python 3.6 o superior.
2. [Cuenta en PyPI](https://pypi.org/account/register/).
3. PIP Actualizado.

```bash
python3 -m pip install --upgrade pip
```

4. [Twine](https://pypi.org/project/twine/).

```bash
python3 -m pip install --upgrade twine
```

5. [PyPA's Build](https://pypi.org/project/build/).

```bash
python3 -m pip install --upgrade build
```

## Estrucutra de carpetas y archivos

```bash
packaging_tutorial/
├── LICENSE
├── pyproject.toml
├── README.md
├── src/
│   └── example_package_YOUR_USERNAME_HERE/
│       ├── __init__.py
│       └── example.py
└── tests/
```

### pyproject.toml

```toml
[project]
name = "example_package_YOUR_USERNAME_HERE"
version = "0.0.1"
authors = [
  { name="Example Author", email="author@example.com" },
]
description = "A small example package"
readme = "README.md"
requires-python = ">=3.7"
classifiers = [
    "Programming Language :: Python :: 3",
    "License :: OSI Approved :: MIT License",
    "Operating System :: OS Independent",
]

[project.urls]
"Homepage" = "https://github.com/pypa/sampleproject"
"Bug Tracker" = "https://github.com/pypa/sampleproject/issues"

[build-system]
requires = ["hatchling"]
build-backend = "hatchling.build"
```

### README.md

```md
# Example Package

This is a simple example package. You can use
[Github-flavored Markdown](https://guides.github.com/features/mastering-markdown/)
to write your content.
```

### LICENSE

```txt
Copyright (c) 2018 The Python Packaging Authority

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

## Building

```bash
python3 -m build
```

Este comando debería generar dos archivos en el directorio dist:

```bash
dist/
├── example_package_YOUR_USERNAME_HERE-0.0.1-py3-none-any.whl
└── example_package_YOUR_USERNAME_HERE-0.0.1.tar.gz
```

Podemos probar que el paquete funciona correctamente instalándolo en un entorno virtual:

```bash
python3 -m venv tutorial-env
source tutorial-env/bin/activate
python3 -m pip install example_package_YOUR_USERNAME_HERE-0.0.1-py3-none-any.whl
```

## Publicando en PyPI

```bash
python3 -m twine upload dist/*
```

Se te pedirá un nombre de usuario y una contraseña. Para el nombre de usuario, usa `__token__`. Para la contraseña, usa el valor del token, incluido el prefijo `pypi-`, o podemos crear un archivo `.pypirc` en nuestro directorio de inicio con el siguiente contenido:

```txt
[pypi]
username = __token__
password = pypi-XXXXXX
```
Al finalizar el comando debería darnos algo como esto:

```bash
Uploading distributions to https://test.pypi.org/legacy/
Enter your username: __token__
Uploading example_package_YOUR_USERNAME_HERE-0.0.1-py3-none-any.whl
100% ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ 8.2/8.2 kB • 00:01 • ?
Uploading example_package_YOUR_USERNAME_HERE-0.0.1.tar.gz
100% ━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━ 6.8/6.8 kB • 00:00 • ?
```

## Instalando el paquete

Si todo salió bien, podemos instalar nuestro paquete con:

```bash
python3 -m pip install example_package_YOUR_USERNAME_HERE
```

---

{{< box info >}}
[Post original, en inglés](https://packaging.python.org/en/latest/tutorials/packaging-projects/)
{{< /box >}}

---

{{< signature standard >}}