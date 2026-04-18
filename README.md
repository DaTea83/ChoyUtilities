# ChoyUtilities

A personal Unity Utility Library, mainly covers editor tools and extension for math, coroutines, async/await and collections.  
\
It also provides generic template for:  
* Singletons  
* Pooling  
* Spawn Managers  
* Audio managers  
* UI Managers.  

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

## Requirements

* Ideally Unity 6000.0 LTS and above
* Minimum Unity 2023.1
* URP only
* Package:
  * "com.unity.burst": "1.8.27"
  * "com.unity.collections": "2.6.5"
  * "com.unity.mathematics": "1.3.2"

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

## Floater

In a nutshell this is just a float array, but a very flexible one  
Its `Burst Compile` and `Serializable` supported  

It's constructor supports almost any data types commonly found in Unity, like:

```

	new Floater(float)
	new Floater(int)
	new Floater(bool)
	new Floater(string)
	new Floater(Color)
	new Floater(Transform)
	new Floater(Quaternion)
	new Floater(float2~4)
	new Floater(Vector2~4)
	new Floater(float4x4)
	new Floater(Enum)
	...etc

```
It also supports any type casting between these data types

So someting like:  

```

	string value = new Floater(Color)
	Transform value = new Floater(bool[9])
	Quaternion value = new Floater(Enum[])
	...and more

```

These types of convertion is now possible  
Why? I do because I can

## Entities Package

// TODO
