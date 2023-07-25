---
title: "Creando un acortador de enlaces en 12 lineas de código usando Cloudflare Workers + Bonus"
date: 2023-03-19
type: page
tags: 
- "Cloudflare"
- "URL Shortener"
description: Crear un acortador de enlaces con esta herramienta es muy fácil, aquí un pequeño tutorial de cómo hacerlo.
topic: Cloudflare
---

*Post original [aquí](https://thomaslevesque.com/2022/11/01/building-a-url-shortener-in-12-lines-of-code-using-cloudflare-workers/)*

## Cloudflare Workers

[Cloudflare Workers](https://developers.cloudflare.com/workers/), de forma muy resumida, es un entorno serverless que corre Javascript. Básicamente, escribes código que se ocupa de peticiones HTTP, similar a Node.js. El código es desplegado globalmente en toda la red de Cloudflare, lo que significa que siempre está corriendo lo más cerca posible del usuario final.

Para este proyecto, al ser algo pequeño, con el plan gratis será más que suficiente. Si te interesa hacer algo más grande te vendría bien revisar los planes de pago.

## KV Storage

Primero, necesitaremos un lugar para almacenar nuestra información. Usaremos [Workers KV](https://developers.cloudflare.com/workers/runtime-apis/kv), el cual no es más que un almacenamiento de llave - valor (key-value, en inglés) proporcionado por Cloudflare Workers. Nos dirigimos a **Workers > KV** damos click en **Create namespace**. Seleccionamos un nombre y damos click en **Add**:


![Screenshot of the &lsquo;Create namespace&rsquo; form in Cloudflare dashboard](https://thomaslevesque.com/2022/11/01/building-a-url-shortener-in-12-lines-of-code-using-cloudflare-workers/create-kv-namespace.png "Create namespace")

Damos click en **View** en la lista de *namespace* para acceder a nuestro nuevo *namespace*,  y añadimos algunas entradas. La llave será el "*token*" para nuestro enlace corto, y el valor será la URL a la que se redirecionará.

![Screenshot of the KV key-value editor UI](https://thomaslevesque.com/2022/11/01/building-a-url-shortener-in-12-lines-of-code-using-cloudflare-workers/kv-entries.png "KV entries")

## Creando el servicio *worker*

Ahora, nos dirigimos a la parte de **Workers > Overview**, y damos click en **Create a service**. Escogemos un nombre, seleccionamos la platilla de **HTTP handler** con el modulo sintaxis, y finalmente damos click en **Create service**:

![Screenshot of the &lsquo;Create a service&rsquo; form in Cloudflare dashboard](https://thomaslevesque.com/2022/11/01/building-a-url-shortener-in-12-lines-of-code-using-cloudflare-workers/create-service.png "Create a service")

Debería redireccionarnos a nuestro nuevo servicio:

![Screenshot of the worker service page](https://thomaslevesque.com/2022/11/01/building-a-url-shortener-in-12-lines-of-code-using-cloudflare-workers/worker-service-page.png "Service page")

## Asignando el KV namespace a una variable

Antes de comenzar a programar, necesitamos asignar nuestro KV namespace a una variable, de tal manera que podamos accederlo desde nuestro código. Vamos a la pestaña de **Settings**, luego a la sección de **Variables**:

![Screenshot of the Variables page of the service](https://thomaslevesque.com/2022/11/01/building-a-url-shortener-in-12-lines-of-code-using-cloudflare-workers/settings-variables.png "Settings > Variables")

En **KV Namespace Bindings** damos click en **Add binding**, ingresamos `kv` como nombre de  variable  (tú puedes seleccionar otro nombre, si gustas), seleccionamos el KV namespace que creamos antes, y damos **Save**.

![Screenshot of the &ldquo;Add KV namespace binding&rdquo; form](https://thomaslevesque.com/2022/11/01/building-a-url-shortener-in-12-lines-of-code-using-cloudflare-workers/kv-namespace-binding.png "KV namespace binding")

## El código

Estamos listo para comenzar a programar! Regresamos a la pestaña de **Resources**, y damos click en el botón de **Quick edit**:

![Screenshot of the code editor for the service](https://thomaslevesque.com/2022/11/01/building-a-url-shortener-in-12-lines-of-code-using-cloudflare-workers/code-editor.png "Code editor")

En la izquierda encontramos un simple editor de Javascript, y en la derecha una UI para testear nuestro worker. Puedes enviar peticiones al worker y verificar cómo responde. Esto hará que testear nuestro código sea *muy* sencillo!

Remplazamos el código con el siguiente:

```javascript
export default {
  async fetch(request, env) {

    if (request.method != "GET") {
      return new Response("Forbidden", { status: 403 });
    }

    const { pathname } = new URL(request.url);
    const key = pathname.split('/')[1];
    if (!key) {
      return new Response("Not found", { status: 404 });
    }

    const dest = await env.kv.get(key);
    if (dest) {
      return new Response("Redirecting...", { status: 302, headers: { "Location": dest }});
    }

    return new Response("Not found", { status: 404 });
  }
}
```

Rápida explicación del código:

-   Tenemos dos parámetros:
    -   `request`, el cual representa la petición HTTP que acabamos de recibir
    -   `env`, el cual nos da acceso a las variables de entorno y al *KV namespace bindings*
-   Solo queremos procesar peticiones del tipo `GET`, por lo que respondemos `403 Forbidden` si la petición no es tipo `GET`.
-   Extraemos la dirección de la URL, y luego tomamos la primera parte de la dirección; este es el token (o llave) para el enlace corto.
-   Accedemos al KV namespace para encontrar nuestra URL de destino, la cual está asociada con nuestro token.
-   Si lo encontramos, respondemos con `302 Found` para redireccionar a nuestra URL (esto lo especificamos en el header `Location`)
-   De lo contrario, respondemos con un `404 Not Found`

## Testeando y desplegando

Puedes hacer un test usando el panel **HTTP**:

![Screenshot of a request for a non-existing URL token, returning a 404 response](https://thomaslevesque.com/2022/11/01/building-a-url-shortener-in-12-lines-of-code-using-cloudflare-workers/test-notfound.png "Not found") ![Screenshot of a request for an existing URL token, returning a 302 response](https://thomaslevesque.com/2022/11/01/building-a-url-shortener-in-12-lines-of-code-using-cloudflare-workers/test-found.png "Found")

Una vez que estamos satisfechos con los resultados y hemos comprobado que todo funciona correctamente podemos desplegar la instancia dando click en el botón de **Save and deploy**.

Cuando lo hemos desplegado, podemos ingresar la dirección del panel de test directamente en nuestro navegador y nos redireccionará a nuestra URL de destino.

## Bonus - Añadiendo un subdominio para nuestro worker

Nos dirigimos a la pestaña de **Triggers**, damos click en el botón **Add Custom Domain**:

![Custom domains](https://res.cloudinary.com/rooyca/image/upload/v1679243345/Blog/Imgs/Cloudflare%20Worker/Pasted_image_20230319111559_i9ceaq.png)

Luego ingresamos nuestro subdominio, del tipo **radio.example.com**:

![add subdomain](https://res.cloudinary.com/rooyca/image/upload/v1679243345/Blog/Imgs/Cloudflare%20Worker/Pasted_image_20230319111912_lmoppf.png)

Y listo, al ingresar a nuestro subdominio con un token valido, nos redireccionará a nuestra URL.
## Conclusión

Acabamos de crear un acortador de enlaces en 12 lineas de código (claro, sin contar los espacios en blanco ni los corchetes) Nada mal. Es bastante minimalista, pero es un buen comienzo. Recordemos que siempre podemos añadir más URL desde el *KV editor*.