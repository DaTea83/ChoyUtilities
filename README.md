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
