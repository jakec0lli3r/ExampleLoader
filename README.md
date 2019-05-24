
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
	>  CREATE TABLE `user` (
	  `id` int(11) NOT NULL,
	  `username` varchar(255) NOT NULL,
	  `password` varchar(255) NOT NULL,
	  `hwid` varchar(255) NOT NULL
	) ENGINE=MyISAM DEFAULT CHARSET=latin1;

	*Adding primary key*.
	>ALTER TABLE `user`
  ADD PRIMARY KEY (`id`);

2. Download the web-files, and change the credentials in **`core/login.connect.php`** to yours.
3. Host the web-files. The url to **`handle.php`** will be your API url.

## Loader setup

 1. Download the loader source-code, and extract it.

 3. Open the **`Loader.sln`** file, to open the project in Visual Studio.
 4. Right click the project in the **Solution Explorer**, then click on *"Manage NuGet Packages"*.
 5. Search for, and install the following packages:
	 	 - **`CefSharp.WinForms`**
		 - **`Newtonsoft.Json`**
		 
5. Now go to `Core/Constants.cs` and change the **apiUrl** & **dllUrl** strings to yours.
6. Make sure all files in the **Content** folder are set to *"Copy always"*.
7. Also make sure you are building in x64 or x86. Im fairly sure CefSharp does support AnyCPU, but it will require some additional configuration.
	 


## Credits
 - [CefSharp](https://github.com/cefsharp/CefSharp) 
 - [Bleak](https://github.com/Akaion/Bleak) - A awesome injection library.
 - [JSON.Net](https://www.newtonsoft.com/json) - Used for reading & converting JSON objects.
