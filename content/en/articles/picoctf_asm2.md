---
title: "PicoCTF Writeup – Reverse Engineering (asm2)"
date: 2022-08-31
type: page
tags: 
- "picoctf"
- "assembly"
- "writeup"
description: This is a writeup for one of the challenges on the PicoCTF website called asm2
topic: asm2,picoctf,writeup
---

# Información:

Nombre de la plataforma: [PicoCTF](https://play.picoctf.org/practice/)

Nombre del reto: [asm2](https://play.picoctf.org/practice/challenge/16?page=4)

Categoría: Ingeniería Inversa

Puntos: 250

PicoCTF 2019.

## Descripción del reto:

¿Qué retorna asm2(0xb, 0x2e)? Envía la bandera como un valor hexadecimal (comienza con '0x'). NOTA: El envío para este reto NO será en el formato normal de las banderas. [Código](https://jupiter.challenges.picoctf.org/static/717467c8c8b4332ea5873ad8fe7b2dad/test.S).

Pista: [Condiciones](https://www.tutorialspoint.com/assembly_programming/assembly_conditions.htm) en ensamblador


# Writeup (Inicio)

En este reto, tenemos el código fuente en ensamblador. El archivo se llama _test.S_ y acontinuación puedes ver el código en cuestion:

```
asm2:
	<+0>:	push   ebp
	<+1>:	mov    ebp,esp
	<+3>:	sub    esp,0x10
	<+6>:	mov    eax,DWORD PTR [ebp+0xc]
	<+9>:	mov    DWORD PTR [ebp-0x4],eax
	<+12>:	mov    eax,DWORD PTR [ebp+0x8]
	<+15>:	mov    DWORD PTR [ebp-0x8],eax
	<+18>:	jmp    0x509 <asm2+28>
	<+20>:	add    DWORD PTR [ebp-0x4],0x1
	<+24>:	sub    DWORD PTR [ebp-0x8],0xffffff80
	<+28>:	cmp    DWORD PTR [ebp-0x8],0x63f3
	<+35>:	jle    0x501 <asm2+20>
	<+37>:	mov    eax,DWORD PTR [ebp-0x4]
	<+40>:	leave  
	<+41>:	ret 
```

Como dige anteriormente, esto es código en ensamblador, ya solucioné un reto sobre ensamblador por lo que podría ser parecido, vamos a probar.

Para entender el programa decidí ir línea por línea.

```
	<+0>:	push   ebp
	<+1>:	mov    ebp,esp
```

Como ya sabemos **asm2(0xb, 0x2e)** está siendo puesto en el _stack_. Esto pasa en las primeras dos líneas. La primera hace _push_ de **asm2** hacia **ebp** y después se mueve dicho valor hacia **esp**.

Después de estas dos instrucciones, el estado del _stack_ es el siguiente:


|Direccion|Valor|Instrucción|
|---|---|---|
|  0xc | 0x2e  | ebp+0xc  |
| 0x8  | 0xb  |  ebp+0x8 |
|  0x4 |  ret |  ebp+0x4  |
|0x0|ebp||

Saber esto nos ayuda bastante. Ahora vamos a ver la siguiente instrucción:

```
<+3>:	sub    esp,0x10
```

En esta instrucción, simplemente estamos a asignando espacio para algunas variables locales, por lo que el stack quedaría así:

|Direccion|Valor|Instrucción|
|---|---|---|
|  0xc | 0x2e  | ebp+0xc  |
| 0x8  | 0xb  |  ebp+0x8 |
|  0x4 |  ret |  ebp+0x4  |
|0x0|ebp||
|-0x4|local1|ebp-0x4           
|-0x8|local2|ebp-0x8           
|-0xc|local3|ebp-0xc           
|-0x10|local4|ebp-0x10
 
Continuamos con:

```
	<+6>:	mov    eax,DWORD PTR [ebp+0xc]
	<+9>:	mov    DWORD PTR [ebp-0x4],eax
	<+12>:	mov    eax,DWORD PTR [ebp+0x8]
	<+15>:	mov    DWORD PTR [ebp-0x8],eax
	<+18>:	jmp    0x509 <asm2+28>
```

Vamos instrucción por instruccion:

1. Primero, le asignamos al registro **eax** el valor de **ebp+0xc** que sería **0x2e**, según el _stack_ actual.
2. Después, movemos el valor del registro **eax** a ebp-0x4, que en nuestro caso sería **local1**, es decír la variable **local1** tendría el valor **0x2e**.
3. Luego movemos el contenido de **epb+0x8** a **eax**, es decír, su valor sería **0xb**
4. En la linea +15 movemos el contenido del registro **eax** hacia **ebp-0x8**, es decír la variable **local2** tendría el valor **0xb**.
5. Finalmente hacemos un salto hacia **asm2+28**.

El estado actual del _stack_ es:

|Direccion|Valor|Instrucción|
|---|---|---|
|  0xc | 0x2e  | ebp+0xc  |
| 0x8  | 0xb  |  ebp+0x8 |
|  0x4 |  ret |  ebp+0x4  |
|0x0|ebp||
|-0x4| 0x2e|ebp-0x4           
|-0x8|0xb|ebp-0x8           
|-0xc|local3|ebp-0xc           
|-0x10|local4|ebp-0x10

Pasemos a la siguiente instrucción:

```
	<+28>:	cmp    DWORD PTR [ebp-0x8],0x63f3
	<+35>:	jle    0x501 <asm2+20>
```

Como sabrás, la anterior instrucción nos hizo saltar hacia **asm2+28**, la cual es una comparación seguida de un salto si el resultado es menor o igual que. En este caso, comparamos **ebp-0x8** (es decir, la variable **local2** que tiene el valor **0xb**) y **0x63f3**. Sabemos que **0xb** es menor que **0x63f3** por lo que hacemos el salto hacia **asm2+20**.
 
```
	<+20>:	add    DWORD PTR [ebp-0x4],0x1
	<+24>:	sub    DWORD PTR [ebp-0x8],0xffffff80
```

En la linea +20 tenemos una istrucción _add_, añadimos 1 a **ebp-0x4**, convirtiéndolo en **0x2f**.
En la linea +24 tenemos una instrucción _sub_, restamos **0xffffff80** a **ebp-0x8**. Esto es x86 por lo que tenemos que truncar el resultado, para ello simplemente realizamos esta operación en Python:

```python
local2 = (local2 - 0xffffff80) & 0xffffffff
```

Ahora el valor de _local2_ es: **0x8b**

Continuando con la siguiente linea nos encontramos nuevamente en la +28 y sabemos que **0x8b** es menor que **0x63f3** por lo que volvemos a dar el salto. Para facilitar el proceso creé un _script_ en Python que no es más que una "traducción" de este código en ensamblador. El _script_ en cuestión es el siguiente:

```python
'''
Stack:
[  local4 ]  <--- ebp-0x10
[  local3 ]  <--- ebp-0xc
[  local2 ]  <--- ebp-0x8
[  local1 ]  <--- ebp-0x4
[   ebp   ]
[   ret   ]  <--- ebp+0x4
[   arg1  ]  <--- ebp+0x8
[   arg2  ]  <--- ebp+0xc
'''
#We know that asm2 receives two arguments
def asm2(arg1, arg2):
#asm2:
    #<+0>:  push   ebp
    #<+1>:  mov    ebp,esp
    #<+3>:  sub    esp,0x10

    #<+6>:  mov    eax,DWORD PTR [ebp+0xc]
    eax = arg2

    #<+9>:  mov    DWORD PTR [ebp-0x4],eax
    local1 = eax

    #<+12>: mov    eax,DWORD PTR [ebp+0x8]
    eax = arg1

    #<+15>: mov    DWORD PTR [ebp-0x8],eax
    local2 = eax

    #<+18>: jmp    0x509 <asm2+28>
    #<+20>: add    DWORD PTR [ebp-0x4],0x1
    #<+24>: sub    DWORD PTR [ebp-0x8],0xffffff80
    #<+28>: cmp    DWORD PTR [ebp-0x8],0x63f3
    #<+35>: jle    0x501 <asm2+20>
    while(local2 <= 0x63f3):
        local1 = (local1 + 1) & 0xffffffff              #This truncates the result to 32 bits.
        local2 = (local2 - 0xffffff80)  & 0xffffffff    #This truncates the result to 32 bits.           
    '''
       It is necessary to truncate the restuls because in python does not have
       buffer overflow but 0x86 can have so we have to truncate it.
       '''

    #<+37>: mov    eax,DWORD PTR [ebp-0x4]
    #<+40>: leave
    #<+41>: ret
    return hex(local1)

print(asm2(0xb, 0x2e))
```
Como dige anteriormente, el _script_ es una "traducción" como hice antes pero con sintáxis de Python 3. Después de ejecutar el código obtendremos la bandera.

***

Espero les haya sido de tanta utilidad como a mí, cualquier duda no _duden_ en enviarme un mensaje.

-    Discord: rooyca#6075
-    Telegram: @seiseiseis
-	 Mastodon: @rooyca@mas.to

---

##### Post original: [https://mregraoncyber.com/picoctf-writeup-asm2/](https://mregraoncyber.com/picoctf-writeup-asm2/)