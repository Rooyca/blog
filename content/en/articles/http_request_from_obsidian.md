---
title: "HTTP request from Obsidian notes"
date: 2024-06-02
type: page
tags: 
- "Obsidian"
- "plugin"
- "productivity"
description: Make HTTP request within your Obsidina notes.
---

Are you an [Obsidian](https://obsidian.md/) user looking to elevate your note-taking experience with dynamic data integration? Look no further than APIR ([api-request](https://rooyca.github.io/obsidian-api-request/)) – an Obsidian plugin designed to streamline HTTP requests directly into your notes.


## ⚡ How to Use

With APIR, integrating HTTP requests into your notes is a breeze. Simply create a code-block within your note, specifying the language as `req`. Inside this code-block, customize parameters such as URL, method, body, headers, and more to tailor your request precisely to your needs.

### 👨🏻‍💻 Example Code-block

~~~markdown
```req
url: https://my-json-server.typicode.com/typicode/demo/comments
method: post
body: {"id":1}
headers: {"Accept": "application/json"}
format: <h1>{}</h1>
req-id: id-persona
disabled
```
~~~

This example demonstrates how to send a POST request to a server, displaying the response 'id' field within an HTML heading tag. For more info about all the `flags` you can visit [APIR docs](https://rooyca.github.io/obsidian-api-request/).

Ready to revolutionize your note-taking workflow? **Try APIR today!** 🌟

---

P.S. If you find any bug, have any problem, doubt or want to add any functionality don't hesitate to write me. (You can also leave your issue at [https://github.com/Rooyca/obsidian-api-request/issues](https://github.com/Rooyca/obsidian-api-request/issues))