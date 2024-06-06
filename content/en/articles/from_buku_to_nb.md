---
title: "Migrating from buku to nb with Python"
date: 2024-06-06
type: page
tags: 
- "buku"
- "nb"
- "python"
description: "Transfer all your bookmarks from buku to nb using Python."
---

![img_migration](https://www.datanami.com/wp-content/uploads/2020/11/database_migration_shutterstock_hanss.jpg)

## What is buku

> ![Stars](https://img.shields.io/github/stars/jarun/buku)
> [![Latest release](https://img.shields.io/github/release/jarun/buku.svg?maxAge=600)](https://github.com/jarun/buku/releases/latest)


[buku](https://github.com/jarun/buku) is, according to its developers, "a powerful bookmark manager and a personal textual mini-web". One of its main strengths is that its records are stored in an SQL database, making searches extremely fast.

## What is nb

> ![Stars](https://img.shields.io/github/stars/xwmx/nb)
> [![Latest release](https://img.shields.io/github/v/tag/xwmx/nb)](https://github.com/xwmx/nb/tags)

[nb](https://github.com/xwmx/nb) is, according to its GitHub description, a "CLI and local web plain text note‑taking, bookmarking, and archiving with linking, tagging, filtering, search, Git versioning & syncing, Pandoc conversion, + more, in a single portable script".

For me, a clear advantage of *nb* over *buku* is its multifunctionality; I can literally use a single tool to manage all my notes (from *all* to *bookmarks*). That's why I decided to switch completely to *nb*. And one of the first challenges I encountered was how to transfer the *bookmarks* to MD files.

## Using Python

The solution I found was to use a Python script that would add one by one the `nb URL --tags TAG, TAG` files, for which I first exported my bookmarks from buku.

```
buku --export bookmarks.md
```
Then I simply ran the script.

```python
import re
import subprocess

def run_command(url, tags):
    command = f"nb {url} --tags {','.join(tags)}"
    subprocess.run(command, shell=True)

def main(filename):
    with open(filename, "r") as file:
        lines = file.readlines()
        for line in lines:
            match = re.search(r"\[.*\]\((.*)\).*TAGS: (.*)", line)
            if match:
                url = match.group(1)
                tags = match.group(2).replace(" -->","").split(",")
                run_command(url, tags)

if __name__ == "__main__":
    main("bookmarks.md")
```

And there you go, I'm one step closer to having all my files in one place.

That's all for today. If you found it useful, or if you have any questions or problems, feel free to leave me a comment. Until next time, Thanks for reading!