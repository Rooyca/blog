---
title: "How to extract the complete works of Shakespeare from an image"
date: 2021-09-23
type: page
tags: 
- "steganography"
- "linux"
description: Would you like to read the complete work of one of the greats of English literature but think it's too long to download? Well, today I have the solution for you. We're going to extract it from a small image (2 MB).
topic: steganography
---

# Steganography
## What is it? 

Unlike cryptography, where it is obvious that something is being hidden, steganography hides information in such a way that no suspicions are raised that something is hidden. This is achieved by means of different techniques that allow us to hide files, images, texts or even videos inside other files.

## How does it work?  

There are different techniques to hide information inside files. One of the most commonly used and perhaps the easiest to understand is commonly known as the "Least Significant Bit Technique" or LSB.

What this technique does is to change some of the last bits in a byte to encode the message. This is useful in images, where the value of red, green and blue are represented by bits (one byte) in a range from 0 to 255 (see [RGB](https://en.wikipedia.org/wiki/RGB)) in decimal or from 00000000 to 11111111 in binary.

Changing the value of the last two bits in an all-red pixel from 11111111 to 11111101 only changes the value of red from 255 to 253, which at first glance creates an almost imperceptible change, but still allows us to hide information within the image. 

![LSB](https://res.cloudinary.com/rooyca/image/upload/v1632442360/Blog/Imgs/steganography/lsb_drvccm.jpg)
The above diagram shows two images of 4 pixels each, together with their respective binary value ([Source](https://null-byte.wonderhowto.com/how-to/steganography-hide-secret-data-inside-image-audio-file-seconds-0180936/))

As we said before, this technique works very well with multimedia files, but not so well with ASCII text files where a single bit can change the characters completely. Not to mention that LSB is the easiest technique to detect.

## How to use it?

Remember that there are many steganography techniques, some better than others. It is usually best to avoid LSB and go for something a bit more sophisticated. Even, why not, design your own steganography algorithm.

It is also important to keep in mind the concepts of encrypting and compressing. Encrypting the information before hiding it adds another layer of security, while compressing the information will allow us to add more information to our file.  

For the development of this tutorial, we are going to use Steghide, a simple to use but no less effective tool. 

### 1. Hiding information

First we are going to install Steghide, using `apt`.

```bash
sudo apt-get install steghide
``` 

Once installed we proceed to hide our information, with the following command:

```bash
steghide embed -ef archivoSecreto -cf archivoDePortada -sf archivoFinal -z nivelDeCompresion -e esquema
```

Now let's explain the arguments we just used:

- **-ef** specifies the path to the file we want to hide. We can hide any kind of files inside our file, including Python scripts.
- **-cf** this is the file in which we will hide our information. There are restrictions here, only BMP, JPEG, WAV and AU files are allowed.
- **-sf** is an optional argument that specifies the name of the file resulting from this "join". If not specified the original file will be overwritten. 
- **-z** specifies the compression level, ranging from 1 to 9. If you prefer not to compress your file use the **-Z** argument.
- **-e** specifies the type of encryption. Steghide supports a large number of encryption schemes and if this argument is omitted Steghide will use the default option which is 128-bit AES. If you prefer not to use encryption simply type **-e none**. 

In the following example, I will hide a text file inside a picture of a cute puppy (to raise less suspicion :wink:). The final file will be called "puppy-secret.jpeg", I won't use the compressor or the encryptor, and the command in the terminal will look something like this:

`steghide embed -ef secret.txt -cf perrito.jpeg -sf perrito-secreto.jpeg -e none -Z` 

After entering our command Steghide will ask us to assign a passphrase to our file. Once we enter the passphrase we will have our file with confidential information inside it :smile:.

![PERRITO-SEC](https://res.cloudinary.com/rooyca/image/upload/v1632446520/Blog/Imgs/steganography/info-ocul_d2mpa7.png)

The result would be an ordinary image... If you want to know what is inside the text file, here is the image (password: topsecret):

![PERRITO](https://res.cloudinary.com/rooyca/image/upload/v1632446478/Blog/Imgs/steganography/perrito-secreto_xgzjlc.jpg)

### 2. Extracting information

The process of extracting information from our files is even simpler. The command to be executed is as follows:

`steghide extract -sf archivoSecreto -xf archivoFinal`

When we execute this command it will ask us for the password, we enter it and we should have all the files that were inside the `SecretFile`.

Now, you may be wondering, how do I extract information from a file if I don't have the password? Well, nowadays there are a lot of tools that can help us with that. But today we are going to use [**binwalk**](https://github.com/ReFirmLabs/binwalk/tree/master).

As I promised at the beginning, we are going to extract the complete work of Shakespeare from the following image:

![SHAKESPEARE](https://res.cloudinary.com/rooyca/image/upload/v1632447298/Blog/Imgs/steganography/shakespeare_hempxe.jpg)
> Or if you prefer you can visit the original post at [Twitter](https://twitter.com/David3141593/status/1058124224798380032).

Running our binwalk (`binwalk shakespeare.jpg`) tells us that the image is composed of 31 RAR files. 

![DATA-SP](https://res.cloudinary.com/rooyca/image/upload/v1632447713/Blog/Imgs/steganography/data-shake_ta3h6y.png)

We proceed to extract the files as follows 

`binwalk  -e -C ./shake shakespeare.jpeg`

![FILEs](https://res.cloudinary.com/rooyca/image/upload/v1632448228/Blog/Imgs/steganography/final-html_ios0md.png)

This will create a "shake" folder in our current directory. When executing the command we will extract the 31 RAR files, and when extracting the first of them (`shakespeare.-part001.rar`) we will have a last file called `shakespeare.html` that when opening it, we will get something like this:

![FILES-FINAL](https://res.cloudinary.com/rooyca/image/upload/v1632448228/Blog/Imgs/steganography/html1_az1vsv.png)

Unbelievable, isn't it?

Well, that's all for today. If you want to expand your knowledge on this topic I recommend reading [this](https://linuxhint.com/hide_files_inside_images_linux/) and [this](https://ostechnix.com/hide-files-inside-images-linux/) article. That's not enough? I recommend [this](https://academy.hoppersroppers.org/course/view.php?id=7#section-4) CTF course, which covers these topics and many more. And remember, if you have any doubt, complaint, suggestion or claim do not hesitate to let me know.


