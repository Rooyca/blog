---
title: "Alternativa self-hosted a Google Analitycs | Ackee"
date: 2024-06-05
type: page
tags: 
- "hugo"
- "blog"
- "analitycs"
description: "Obten información de tus visitantes sin depender de Google Analitycs"
---

![img_head](https://images.unsplash.com/photo-1599658880436-c61792e70672?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=MnwxMTc3M3wwfDF8c2VhcmNofDF8fGdvb2dsZSUyMGFuYWx5dGljc3xlbnwwfHx8fDE2ODIzMDM0NTA&ixlib=rb-4.0.3&q=80&w=2000)
 
[**Ackee**](https://ackee.electerious.com/) es una alternativa a Google Analitycs, es de código abierto y self-hosted. Es una herramienta que te permite obtener información de tus visitantes sin depender de Google Analitycs. 

## Ventajas de Ackee

- **Privacidad**: No compartes información con terceros.
- **Open Source**: Puedes ver el código fuente y modificarlo a tu gusto.
- **Self-hosted**: Tienes el control total de tus datos.
- **Fácil de instalar**: Puedes instalarlo en tu servidor en minutos.

## Instalación de Ackee

Para instalar Ackee necesitas tener un servidor con `Node.js` y `MongoDB`. Puedes instalarlo en tu servidor o en un servicio como Heroku, Vercel o Netlify. En la documentación oficial puedes encontrar una [lista de proveedores](https://docs.ackee.electerious.com/#/docs/Get%20started) compatibles.

En este artículo vamos a usar `Netlify`. Para ello debemos seguir los siguientes pasos:

 ### 1. Desplegar Ackee en Netlify

[![Deploy](https://www.netlify.com/img/deploy/button.svg)](https://app.netlify.com/start/deploy?repository=https://github.com/electerious/Ackee)

### 2. Configurar Ackee

- Necesitas una instancia de MongoDB (ej. [MongoDB Atlas](https://www.mongodb.com/cloud/atlas))
> **MongoDB**: `mongodb+srv://<USERNAME>:<PASSWORD>@<CLUSTER>/<DATABASE>`
- Configuramos un nombre de usuario y una contraseña.
- Nos aseguramos de usar los headers CORS correctos. Ver: [`ACKEE_ALLOW_ORIGIN`](https://github.com/electerious/Ackee/blob/master/docs/CORS%20headers.md#platforms-as-a-service-configuration).
> **CORS**: `https://<YOUR-SITE>,https://<YOUR-SITE>...`

### 3. Actualizar Ackee

Netlify añade un *fork* de Ackee a tus repositorios de GitHub. Siempre puedes [hacer *pull*](https://docs.github.com/en/free-pro-team@latest/github/collaborating-with-issues-and-pull-requests/syncing-a-fork) y después *push*. Tu repositorio desplegará automáticamente la nueva versión de Ackee en Netlify.

## Comenzar a usar Ackee

Después de desplegar Ackee, inicia sesión en `https://<YOUR-SITE>.netlify.app` con el usuario y la contraseña que configuraste en el paso 2.

Añade un dominio, dando clic en `Settings` > `Domains` > `New domain`. Ingresa el nombre del dominio (es recomendable usar la url del dominio) y clic en `Add`. Copia el código de seguimiento y añádelo a tu sitio web. Si usas `Hugo` (como yo), puedes añadirlo en el archivo `layouts/partials/footer.html`. 

```html
{{- if (not .Site.Params.disableAckee ) }}
<script async 
    src="{{ .Site.Params.ackeeScriptURL }}"
    data-ackee-server="{{ .Site.Params.ackeeServerURL }}" 
    data-ackee-domain-id="{{ .Site.Params.ackeeDomainID }}"> 
</script>
{{- end }}
```
De esta manera te aseguras de que Ackee registre las visitas de todo tu sitio no solo de la página principal.

Ahora añade las variables de configuración en el archivo `config.toml`.

```yaml
params:
	ackeeScriptURL: "https://<YOUR-SITE>.netlify.app/tracker.js"
	ackeeServerURL: "https://<YOUR-SITE>.netlify.app"
	ackeeDomainID: "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"
```

> Puedes desactivarlo en cualquier momento añadiendo la variable `disableAckee` en el archivo `config.toml`.

## Conclusiones

Así de sencillo es instalar Ackee en Netlify y comenzar a obtener información de tus visitantes. Si te preocupa la privacidad de tus usuarios, esta es una excelente alternativa a Google Analitycs. 

Si tienes alguna duda o sugerencia, déjame un comentario. ¡Gracias por leer!