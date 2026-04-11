# ChoyUtilities

Just a person's Unity Library

## Requirements

* Ideally Unity 6 and above

## Installation

```

Open Unity > Window > Package Management > Package Manager
> Top Left Plus Icon > Install package from git URL > Copy Paste the link below

```

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
TODO
