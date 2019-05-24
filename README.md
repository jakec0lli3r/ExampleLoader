
# ExampleLoader - C#
Hi! ExampleLoader is a cheat loader written in C#.  The name says it all, this is just an **example**. 
Do not expect any sort of anti-leak features for injection. 
This is mostly a demonstration of the awesome stuff you can do with CefSharp. 


## Features
* Manual Map injection
* PHP Login API
* HWID System
* Product Selection (hardcoded)
* Remember me
* Clean design
* Animations
* ParticleJS background

## Information

This loader features a clean design made possible by CefSharp. CefSharp lets us use an embed Chromium Browser. Meaning we will be able to render HTML5, use javascript and most importantly run it smoothly. 

Please note this is not a practical example of a loader, because of CefSharp this loader is far from standalone, in fact all of its dependencies will add up to a total size of over 100MB. Apart from that there also isn't any injection security implemented (as far as anti-leak), this is just an simple example of how to do a loader, but a pretty advanced example of what can be done with CefSharp. For example creating custom objects, and communicating with them through JavaScript.

## Screenshots & videos

![Login](https://i.imgur.com/dcv2ipz.png)                  			               ![Selector](https://i.imgur.com/AYISx0X.png)

Short demo video: https://i.gyazo.com/2720e902a3efc680ac1b2296f0531921.mp4

## Web-end setup

1. Create a MySQL database, and user. Assign the user to the database with at least SELECT and UPDATE permissions.

 2. Create a table called `user` with the following columns: **id**, **username**, **password** and **hwid**. 
 
	Or simply execute this SQL script: 
	
	*Creating the table*.
	>  CREATE TABLE `user` (/n
	  `id` int(11) NOT NULL,
	  `username` varchar(255) NOT NULL,
	  `password` varchar(255) NOT NULL,
	  `hwid` varchar(255) NOT NULL
	) ENGINE=MyISAM DEFAULT CHARSET=latin1;

	*Adding primary key*.
	>ALTER TABLE `user`
  ADD PRIMARY KEY (`id`);

3. Download the web-files, and change the credentials in **`core/login.connect.php`** to yours.

4. Host the web-files. The url to **`handle.php`** will be your API url.
## Credits
 - [CefSharp](https://github.com/cefsharp/CefSharp) 
 - [Bleak](https://github.com/Akaion/Bleak) - A awesome injection library.
 - [JSON.Net](https://www.newtonsoft.com/json) - Used for reading & converting JSON objects.
