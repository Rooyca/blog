---
title: "Enviar correos desde Gmail usando tu propio dominio"
date: 2022-07-09
type: page
tags: 
- "gmail"
- "cloudflare"
- "dominio"
description: ¿Cansado de pagarle a tu proveedor mes a mes para poder usar tu(s) correo(s)? Pues ya no más porque hoy aprenderemos a configurar nuestras cuentas de correo con Gmail en cuatro sencillos pasos.
topic: gmail,Cloudflare,dominio
---

# Primero - Redireccionar el trafico

Lo primero que debemos hacer es redireccionar el trafico de nuestro cuenta de correo electrónico (en este caso me@rooyca.xyz) hacia Gmail. Para ello vamos a nuestro proveedor de dominio o nuestro CDN de preferencia, en mi caso CloudFlare. Ahora nos dirigimos a la parte de *Email* > *Routes* > *Create address*.

![Create Cuenta](https://res.cloudinary.com/rooyca/image/upload/c_scale,w_972/v1657979535/Blog/Imgs/domain-gmail/13_fmzgzs.png)

Creamos nuestra dirección apuntando al Gmail con el que vamos a configurar nuestra cuenta. 

![Apuntando cuenta](https://res.cloudinary.com/rooyca/image/upload/c_scale,w_972/v1657979535/Blog/Imgs/domain-gmail/14_sfypei.png)

Listo, con esto habríamos finalizado el primer paso. Ahora mismo deberíamos estar recibiendo todos los correos enviados al correo que configuramos (en mi caso *me@rooyca.xyz*) en nuestra cuenta de Gmail. Es recomendable ensayar que todo está funcionando correctamente enviándote un correo desde otra cuenta. Si todo funciona de manera correcta continuamos. (Si presentas algún inconveniente no dudes en dejarme un comentario).

# Segundo - Creando la contraseña

A continuación necesitaremos crear las credenciales con las que ingresaremos más tarde. Para ello damos clic en nuestra imagen de perfil y después en *Gestionar tu cuenta de Google*.

![Gestionar Cuenta](https://res.cloudinary.com/rooyca/image/upload/c_scale,w_972/v1657979532/Blog/Imgs/domain-gmail/1_ebmiie.png)

Vamos a la pestaña de *Seguridad*, en el panel de la izquierda y bajamos un poco hasta encontrar la opción *Contraseñas de aplicaciones* 

![Contra](https://res.cloudinary.com/rooyca/image/upload/c_scale,w_972/v1657979531/Blog/Imgs/domain-gmail/3_kvt0qx.png)

Creamos unas nuevas credenciales. En tipo ponemos *correo* y en dispositivo ponemos *otra*.

![Credenciales](https://res.cloudinary.com/rooyca/image/upload/v1657979532/Blog/Imgs/domain-gmail/4_mlc6mf.png)

Ahora ponemos la cuenta que creamos en el paso uno.

![Credenciales 2](https://res.cloudinary.com/rooyca/image/upload/v1657979532/Blog/Imgs/domain-gmail/5_gnv9s1.png)

Listo, se generarán nuestras credenciales, guárdalas en algún lugar porque las necesitaremos más adelante.

![Credenciales final](https://res.cloudinary.com/rooyca/image/upload/v1657979532/Blog/Imgs/domain-gmail/6_avvqqr.png)


# Tercero - Importando nuestra cuenta

Ahora nos vamos a *configuración* > *ver todos los ajustes*.

![Ver ajustes](https://res.cloudinary.com/rooyca/image/upload/c_scale,w_972/v1657979534/Blog/Imgs/domain-gmail/7_v5e61b.png)

Después nos dirigimos a *Cuentas e importación* > *Añadir otra dirección de correo electrónico*

![Añadir cuenta](https://res.cloudinary.com/rooyca/image/upload/v1657979534/Blog/Imgs/domain-gmail/8_ihlv58.png)

Nos aparecerá una ventana emergente en la que debemos ingresar el nombre de nuestra cuenta (el que aparecerá cuando enviemos un correo) y nuestra dirección de correo creada en el primer paso. Es importante asegurarnos de que la opción *Tratar como alias* está deshabilitada.

![configuramos](https://res.cloudinary.com/rooyca/image/upload/c_scale,w_972/v1657979535/Blog/Imgs/domain-gmail/9_fq5rqa.png)

Aquí es IMPORTANTE ingresar:

> smtp.google.com

En la parte de *Servidor SMTP*

En *Nombre de usuario* ponemos la cuenta de gmail que estamos usando pero sin el ***@gmail.com***, en mi caso sería *rooyca*. Por último, en contraseña ingresamos la que creamos en el anterior paso. Lo demás lo dejamos tal cual está.

![Ingresar contraseña](https://res.cloudinary.com/rooyca/image/upload/v1657979534/Blog/Imgs/domain-gmail/10_iexzfy.png)

Después de dar en añadir cuenta nos aparecerá una ventana en la que nos pedirá un código de verificación. 

![verificación ](https://res.cloudinary.com/rooyca/image/upload/v1657979536/Blog/Imgs/domain-gmail/11_kg2zfo.png)

Revisamos nuestro bandeja de entrada y deberíamos tener un coreo con el código de verificación y un enlace para completar la verificación.

![Correo verificación](https://res.cloudinary.com/rooyca/image/upload/v1657984028/Blog/Imgs/domain-gmail/16_dihmfj.png)

Copiamos y pegamos el código en la ventana. Y después damos click en el enlace.

![Copiar y pegar](https://res.cloudinary.com/rooyca/image/upload/c_scale,w_972/v1657984258/Blog/Imgs/domain-gmail/17_dedyye.png)

Se nos abriría una nueva pestaña en la que confirmaremos que deseamos enviar correos con la cuenta que creamos.

![Confirmar](https://res.cloudinary.com/rooyca/image/upload/v1657979531/Blog/Imgs/domain-gmail/18_hyncia.png)

Si nos dirigimos a la parte de *Redactar* podremos observar que ya podemos enviar correos desde nuestra dirección.

![Prueba](https://res.cloudinary.com/rooyca/image/upload/v1657979533/Blog/Imgs/domain-gmail/20_hqp1wp.png)


Y eso sería todo, así de fácil es configurar una dirección de correo con nuestro dominio. Recuerda, si tienes alguna duda, queja, sugerencia o reclamo no dudes en hacérmela saber.

