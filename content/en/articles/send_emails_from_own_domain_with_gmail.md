---
title: "Send emails from Gmail using your own domain"
date: 2022-07-09
type: page
tags: 
- "gmail"
- "cloudflare"
- "domain"
description: Tired of paying your provider every month just to use your email(s)? Well, no more, because today we will learn how to configure our email accounts with Gmail in four simple steps
topic: gmail,Cloudflare,domain
---

# 1 - Redirecting Traffic

The first thing to do is to redirect the traffic from our email account (in this case me@rooyca.xyz) to Gmail. To do this we go to our domain provider or our CDN of choice, in my case CloudFlare. Now we go to the *Email* > *Routes* > *Create address*.

![Create account](https://res.cloudinary.com/rooyca/image/upload/c_scale,w_972/v1657979535/Blog/Imgs/domain-gmail/13_fmzgzs.png)

We create our address pointing to the Gmail with which we are going to configure our account. 

![noted account](https://res.cloudinary.com/rooyca/image/upload/c_scale,w_972/v1657979535/Blog/Imgs/domain-gmail/14_sfypei.png)

Ok, with this we would have finished the first step. Right now we should be receiving all the emails sent to the email we configured (in my case *me@rooyca.xyz*) in our Gmail account. It is advisable to test that everything is working correctly by sending an email from another account. If everything works correctly we continue (if you have any problem do not hesitate to leave me a comment).

# 2 - Creating the password

Next we will need to create the credentials with which we will log in later. To do this we click on our profile image and then on *Manage your Google Account*.

![manage account](https://res.cloudinary.com/rooyca/image/upload/c_scale,w_972/v1657979532/Blog/Imgs/domain-gmail/1_ebmiie.png)

Go to the *Security* tab on the left panel and scroll down a little until you find the *Application Passwords* option.

![Contra](https://res.cloudinary.com/rooyca/image/upload/c_scale,w_972/v1657979531/Blog/Imgs/domain-gmail/3_kvt0qx.png)

We create new credentials. In type we put *email* and in device we put *other*.

![Credenciales](https://res.cloudinary.com/rooyca/image/upload/v1657979532/Blog/Imgs/domain-gmail/4_mlc6mf.png)

Now we set up the account we created in step one.

![Credenciales 2](https://res.cloudinary.com/rooyca/image/upload/v1657979532/Blog/Imgs/domain-gmail/5_gnv9s1.png)

Done, our credentials will be generated, save them somewhere because we will need them later.

![Credenciales final](https://res.cloudinary.com/rooyca/image/upload/v1657979532/Blog/Imgs/domain-gmail/6_avvqqr.png)


# 3 - Importing our account

Now we go to *Settings* > *View all settings*.

![Ver ajustes](https://res.cloudinary.com/rooyca/image/upload/c_scale,w_972/v1657979534/Blog/Imgs/domain-gmail/7_v5e61b.png)

Then go to *Accounts and Import* > *Add another email address*.

![Añadir cuenta](https://res.cloudinary.com/rooyca/image/upload/v1657979534/Blog/Imgs/domain-gmail/8_ihlv58.png)

A pop-up window will appear in which we must enter our account name (the one that will appear when we send an email) and our email address created in the first step. It is important to make sure that the *Treat as alias* option is disabled.

![configuramos](https://res.cloudinary.com/rooyca/image/upload/c_scale,w_972/v1657979535/Blog/Imgs/domain-gmail/9_fq5rqa.png)

Here it's IMPORTANT to enter:

> smtp.google.com

In the part of *SMTP Server*.

In *Username* we put the gmail account we are using but without the ***@gmail.com***, in my case it would be *rooyca*. Finally, in password we enter the one we created in the previous step. The rest we leave it as it is.

![Ingresar contraseña](https://res.cloudinary.com/rooyca/image/upload/v1657979534/Blog/Imgs/domain-gmail/10_iexzfy.png)

After clicking on add account a window will appear asking for a verification code.

![verificación ](https://res.cloudinary.com/rooyca/image/upload/v1657979536/Blog/Imgs/domain-gmail/11_kg2zfo.png)

We check our inbox and we should have a email with the verification code and a link to complete the verification.

![Correo verificación](https://res.cloudinary.com/rooyca/image/upload/v1657984028/Blog/Imgs/domain-gmail/16_dihmfj.png)

Copy and paste the code into the window. And then click on the link.

![Copiar y pegar](https://res.cloudinary.com/rooyca/image/upload/c_scale,w_972/v1657984258/Blog/Imgs/domain-gmail/17_dedyye.png)

A new tab will open in which we will confirm that we want to send e-mails with the account we created.

![Confirmar](https://res.cloudinary.com/rooyca/image/upload/v1657979531/Blog/Imgs/domain-gmail/18_hyncia.png)

If we go to the *Redact* section we can see that we can now send e-mails from our address.

![Prueba](https://res.cloudinary.com/rooyca/image/upload/v1657979533/Blog/Imgs/domain-gmail/20_hqp1wp.png)


And that would be all, it is that easy to set up an email address with our domain. Remember, if you have any doubt, complaint, suggestion or claim do not hesitate to let me know.

