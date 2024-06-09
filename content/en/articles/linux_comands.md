---
title: "Creating our own commands in Linux | Alias"
date: 2021-09-20
type: page
tags: 
- "linux"
description: Tired of having to type twenty variables to execute a simple command? Today, we will learn to map our commands using Alias.
---

# Creating our Aliases

Alias is a simple way to map, bin, join(?) commands. This is the best way to save time when typing long strings of commands. If we type "aliases" in our terminal we should get the default aliases of our system, something like this:
![ALIASES-DEFAULT](https://res.cloudinary.com/rooyca/image/upload/v1632189896/Blog/Imgs/Commands-Linux/alias-predeterminados_lhjdfr.png)

As you may notice, in my case I have two custom aliases: **price** and **subl**; the first one is a python script I use to see the crypto market behavior and the second one is simply to open SublimeText. 

### Modifying our .bashrc file

In order to make our aliases permanent and not only during a section of the terminal we must modify a file called .bashrc, which is usually located in */home/user/* for this we must open it with the text editor of our preference, in my case I will use vim:

    vim ~/.bashrc

![VIM-ALIASES](https://res.cloudinary.com/rooyca/image/upload/v1632190837/Blog/Imgs/Commands-Linux/vim-aliases_wgtx0d.png)

We go down until we find the Aliases section and add our command as follows:

    alias our-command='command-to-bind'

A clearer example would be the following:
    
    alias price='python3 ~/Desktop/get_price.py'

Where "price" is the command I want to assign to the string: "python3 ~/Desktop/get_price.py". Now the last thing we have to do is to save the changes and that's it, that's it, we have our custom command. 

![GET-PRICE](https://res.cloudinary.com/rooyca/image/upload/v1632191334/Blog/Imgs/Commands-Linux/get-price_mntpwd.png)

That would be all, if you have any doubt don't hesitate to let me know. 

