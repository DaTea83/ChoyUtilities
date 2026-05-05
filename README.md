# ChoyUtilities

A personal Unity Utility Library, mainly covers editor tools and extension for math, coroutines, async/await and collections.  
\
It also provides generic template for:  
* Singletons  
* Pooling  
* Spawn Managers  
* Audio managers  
* UI Managers.  

Other than that it also has a motion tween system driven using Burst Job + async/await  
\
On top of this it also provides component templates for Unity DOTS, 
ranging from:  
* Entity Spawning  
* Entity Translation  
* Entity Destruction  
* Entity Physics  
* GameObject bridging
* Simple Entity Path Movement

This is mainly used by myself for every Unity project, if you stumble across this repo and took interest in it,  
feel free to use it.   
\
I'll be continuously updating it to add more features, mainly to solve issues I met during development

## Glossary

[Requirements](#requirements)  
\
[Installation](#installation)  
\
[Features](#features)  
* [Base Package](#base-package)
	* [Editor](#editor)
 		* [Fancy Replace](#fancy-replace) 
 	* [Runtime](#runtime)
  		* [Floater](#floater) 

## Requirements

* Github desktop
	* I'm getting reports of if you don't restart newly installed Unity and Github you will get a package error
* Ideally Unity 6000.0 LTS and above
* Minimum Unity 2023.1
* URP only
* Package:

  * "com.unity.burst": "1.8.27"
  * "com.unity.collections": "2.6.5"
  * "com.unity.mathematics": "1.3.2"  
* Extra requirements for DOTS:

  * "com.unity.entities": "1.4.5"
  * "com.unity.entities.graphics": "1.4.18"
  * "com.unity.physics": "1.4.5"
  * "com.unity.render-pipelines.universal": "17.0.4"  
    
## Installation

> [!Note]
> 1. Open Unity  
> 2. Window (Top middle left of editor)  
> 3. Package Management  
> 4. Package Manager  
> 5. Top Left Plus Icon
> 6. Install package from git URL
> 7. Copy Paste the link below  

For the base package

```

https://github.com/DaTea83/ChoyUtilities.git?path=src/ChoyUtilities/Assets/ChoyUtilities/Common

```

For DOTS package (Requires the base to work)

```

https://github.com/DaTea83/ChoyUtilities.git?path=src/ChoyUtilities/Assets/ChoyUtilities/Entities

```

## Features

## Base Package
## Editor
## Utilities Menu  

// TODO

## Fancy Replace

A Simple Editor Tool which allows you to replace the original icon with custom made.

In your assets folder, right click and find the menu item "Fancy Replace", which will open this window

<img width="425" height="381" alt="image" src="https://github.com/user-attachments/assets/dde78c82-68b1-4a02-b030-deb900004be6" />

Click on the icon you wished to replace, if you want to remove theres a ` Reset Selection ` option

The ` Global Tint Color ` will apply a global tint color to your project window, and ` Reset Color ` will set it back the engine default

After you make your selection it will generate a save file at ` Asset/FancyReplace/YourProjectName_save.json `, if you want to do a complete reset just delete that json (or directory). 
Next time the engine recompiles it will just simply make a new one.  

After:

<img width="491" height="160" alt="image" src="https://github.com/user-attachments/assets/15863b90-f36a-4986-860a-da2526669e89" />

PS. In a group project settings use it with caution as it confuses any unknowing person easily

## Runtime

## Entities Package

// TODO
