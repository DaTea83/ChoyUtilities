# Changelog

## [1.1.1] - 2026-04-27

Fixed
* UtilitiesMenuWindow, added lazy initialization
* TextAssetMenuWindow, added lazy initialization

Added
* Added Locker
* Added Vault
* Added Reserve

Changed
* RawSet, added Span consturctor
* RawSet, added INativeList interface
* Floater, added Span consturctor
* UtilitiesMenuWindow, changed font to Jetbrains Mono
* TextAssetMenuWindow, changed font to Jetbrains Mono



## [1.1] - 2026-04-27

Added
* Added UtilitiesMenu series, for now finished intergrate with SceneTools, rest will be added subsiquent version
* Added new resources folder
* Editor, added SortSceneObject
* Added Core/Bootloader series



## [1.0.10] - 2026-04-24

Fixed
* MotionEvaluate, fixed incorrect equation for Coswave
* MotionEvaluate, fixed incorrect equation for ElasticOut
* MotionEvaluate, SqrEaseInOut use value check instead full lerp between two methods
* MotionEvaluate, CubeEaseInOut use value check instead full lerp between two methods
* TeaMotion, Run will be cancelled if Dispose is manually called

Changed
* MotionEvaluate, using "static Unity.Mathematics.math" instead of "Unity.Mathematics"
* MotionEvaluate, added ElasticIn, BurstOut and BurstIn motion
* MotionEvaluate, SqrEaseInOut renamed to SqrSnap
* MotionEvaluate, CubeEaseInOut renamed to CubeSnap



## [1.0.9] - 2026-04-23

Changed
* GenericAudioManager, now returns tuple values for all PlayClipAtPos and PlayClip
* GenericParticleManager, now returns the said particle system for PlayEffectAtPosition
* ETransformType, added flags attribute
* TransformMotionIJob, now do flag checks instead of switch case
* SimpleRotator, now uses TransformMotionIJob
* SimpleFollower, now uses TransformMotionIJob



## [1.0.8] - 2026-04-22

Changed
* Renamed TeaTranformMotion back to TeaMotion
* TeaMotion, end transform now directly only use Floater value instead of incrementing it with start transform 
* TeaMotion, prevent class disposal when motion still in progress
* MotionEvaluation, added SineWave and CosWave motion
* MotionEvaluation, lower the intensity of elastic out



## [1.0.7] - 2026-04-21

Changed
* GenericSfxManager, renamed back to GenericAudioManager
* Updated GenericMixerManager



## [1.0.6] - 2026-04-21

Fixed
* Floater, fixed typo
* RawSet, ToString same as Floater now returns the type of the container

Changed
* TeaTransformMotion, change time constant to 0.008333f
* EnumCollection, added back extensions previously removed
* AsyncCollection, marked RotateObjectAsync as obsolete
* AsyncCollection, marked ScaleObjectAsync as obsolete
* AsyncCollection, marked MoveAsync as obsolete



## [1.0.5] - 2026-04-19

Added
* Added EditorCollection.Address
* Added SceneTemplateEditor

Changed
* Floater, added Burstdiscard to a new function where exception is thrown
* Updated TeaMotion
* Moved "RemovedMissingScriptsEditor" to Editor/MenuItem



## [1.0.4] - 2026-04-18

Changed
* Full code cleanup, with added header and renames



## [1.0.3] - 2026-04-17

Added
* Added GenericMixerManager
* Added unit testing for Floater

Changed
* AsyncCollection, optimize catch exception
* GenericSingleton, added DisallowMultipleComponent attribute, one object one singleton thanks
* AsyncCollection, moved to Motion/Legacy
* CoroutineCollection, moved to Motion/Legacy
* GenericAudioManager renamed to GenericSfxManager
* GenericSfxManager, removed AudioMixerSerialize
* GenericSingleton, moved to Core/Generic
* GenericSpawnManager, moved to Spawn/
* GenericPoolingManager, moved to Spawn/
* GenericParticleManager, moved to Spawn/
* GenericLegacyUIManager, moved to UI/Generic
* GenericOverlayUIManager, moved to UI/Generic
* GenericFSM, moved to StateMachine/

Removed
* Microphone Recorder, moved to Misc
* Microphone Attribute, moved to Misc
* Quaternion cast from Floater



## [1.0.2] - 2026-04-16

Reduce the use of reflection.

### Added
* Added EMotion
* Added MotionUtils
* Added HelperCollection.Broadcaster
* Added ActivateMultiDisplay

### Changed
* Floater, removed IEquatable as its not compatible with Burst
* Floater, added (float3, float3) and (float3, quaternion) constructors
* All extension in UnmanagedCollections are now Burst Compiled
* RawSet, removed IEquatable
* Moved EnumCollection to Enum/Utils
* AsyncCollection, all extensions now use EMotion

### Removed
* Removed CallGenericInstanceMethod



## [1.0.1] - 2026-04-12

From this point on changelog for Common and Entities will be seperated.

### Added
* Added RawSet

### Changed
* Floater is now partial and seperated to three files
* Floater removed consturctors and implicit operator for Enum, changed to use extension instead



## [1.0.0] - 2026-04-10/11

After a while I finally uploaded this to Github, which from this point onwards version increment will not as frequent as before.
But update in changelog will stays the same. Given will be download to plenty of projects, there are some issue needs to solve.
As much I like it, but most team projects I'm in don't use DOTS, for that section needs to be seperated. This includes other Unity packages like: Localization and Spline, those need to separated into their own directory.

### Added,
* Added FancyReplace
* Added Floater

### Changed
* Directory split into 3, Default, Entities and Misc
* Each new directory will come in its own asmdef, where Entities and Misc need default package to work
* All EugeneC namespace replaced with ChoyUtilities
* FPSCounter reverted back to use normal string

### Removed
* Removed Obsolete folder from the package, moved to Assets/Sandbox
* Removed Entities/UI from the package, moved to Assets/Sandbox
* Removed MultiInput from the package, moved to Assets/Sandbox
* Deleted FolderColor, replace with FancyReplace
* Deleted BasicAttributes
* Deleted ParticleAttributes
* Deleted DialogueAttributes

## [0.1.16] - 2026-04-06

### Changed
* GenericPoolingManager, now use scriptable object to store pool data
* GenericLegacyUIManager, CloseAll() added parameter to immediately close UI
* GenericAudioManager, now no longer inherit from GenericPoolingManager
* GenericAudioManager, now use scriptable object to store pool data

## [0.1.15] - 2026-04-04

### Added
* Added EuCEditorSystemGroup
* HelperCollection, added GetLocaleCode()
* GenericSingleton, added toggle for getting ECS World

### Changed
* MonoFpsCounnter, now requires LocalizedString

## [0.1.14] - 2026-04-03

### Added
* Added EuCCleanupSystemGroup, changed all clean up system to use this system group
* Added EuCSpawnSystemGroup, changed all spawn system to use this system group
* Added EuCManagedComponentSystemGroup, changed all component initialization system to use this system group

### Fixed
* GenericPoolingManager, GenericAudioManager, GenericLegacyUIManager, now manually sets the siblings index of each spawned object

### Changed
* Replace all Eu_ in systemgroup with EuC

## [0.1.13] - 2026-04-01

### Fixed
* GenericSpawnManager doesn't do DespawnAll() properly

### Changed
* GenericPoolingManager, InitPool(createFunc) is now public
* UiHelper, reverted back to pre 0.1.6 version
* GenericWorldUIManager, renamed to GenericLegacyUIManager and funtionality reverted back to pre 0.1.6 version
* HelperCollection renamed to MathCollection
* ExtensionStuff to TransformCollection
* ExtensionAsync to AsyncCollection
* ExtensionBlobArray to BlobCollection
* ExtensionCoroutine to CoroutineCollection
* ExtensionEntities to EntityCollection
* ExtensionSplinePoints to SplineCollection
* StaticStuff to HelperCollection

### Deleted
* Replaced all UtilityCollection with HelperCollection

## [0.1.12] - 2026-03-30

### Added
* Added AgentMoveICleanupTag
* Added CleanupAgentMoveISystem
* Added GroupTagAuthoring,
* GenericSingleton, added GetSingletonBuffer

### Changed
* Renamed Entities/Animations to Entities/GameObjects
* Renamed AgentMoveNodeIBuffer to ConnectedNodeIBuffer
* Renamed AgentSpawnNodeAuthoring to SpawnNodeAuthroing
* Renamed AgentMoveNodeAuthoring to MoveNodeAuthoring
* Renamed AgentSpawnIBuffer to SpawnNodeIBuffer
* Renamed AgentSpawnNodeIData to SpawnNodeIEnableable
* Renamed AgentSpawnISingleton to AgentISingleton
* Renamed InitializeAnimatorISystem to InitialzeObjectAnimatorISystem

## [0.1.11] - 2026-03-28/29

### Added
* Added AgentSpawnISingleton
* HelperCollection, added GetDistanceAndDot() extensions
* Added GameObjectAuthoring
* Added InitializeAnimatorAuthoring
* Added AnimatorTransformICleanup
* Added AgentStatsAuthoring

### Changed
* AgentSpawnISystem, now tracks how many agents are spawned with AgentSpawnISingleton
* AgentMoveISystem, now before moving towards the target agent it first rotate towards the target

### Removed
* Removed the IDispoable from ObjTransformIData, destroying gameobject will be handled by ICleanupComponent
* Removed AnimationController
* Removed AgentMoveISingleton

## [0.1.10] - 2026-03-27

### Added
* Added AnimationController
* Added FlatPlaneAuthoring.prefab

### Changed
* ObjTransformIData, now implements IDisposable, the gameobject will be destroyed when the entity is destroyed
* (Whether EntityTransformIData should also implement IDispoable is on the radar)
* Renamed AgentMoveIData to AgentMoveIEnableable and implemented IEnableableComponent

## [0.1.9] - 2026-03-25/26

### Added
* Added Runtime/Obsolete
* Added AgentMoveIData
* Added AgentMoveISystem
* Added AgentMoveNodeAuthoring
* Added AgentSpawnNodeAuthoring
* Added AgentMoveSystemAuthoring
* Added InitializeAgentMoveISystem
* Added UrpRandomColorAuthoring
* Two new overload for RandomValue()

### Changed
* Current Obsolete and ObsoleteV2 folder are now moved into Runtime/Obsolete
* Old Obsolete folder now renamed to ObsoleteV1 
* All scripts in Entities/Agents moved to ObsoleteV2 and marked obsolete
* BasicProperties renamed to BasicAttributes
* AgentScriptable renamed to AgentAttributes

### Deleted
* HelperCollection, deleted PositionRotationBlob and EntityBlob
* Deleted DemoAgent prefab

## [0.1.8] - 2026-03-24

### Changed
* Done a coding style cleanup
* GenericOverlayUIManager, TObj field moved from serializable to script itself 

## [0.1.7] - 2026-03-20

After testing I noticed that the GenericUIManager goes against on how previously UI was setup,

and the discovery of UI toolkit changes how overlay UI are setuped.

Any overlay UI from now on will gonna use the new UI Toolkit and world space canvas will be handled by GenericUIManager.

### Added
* Added GenericOverlayUIManager
* Added InitialDestroyFollowerISystem, this adds DestroyIEnableableTag to all EntityTransformIData
* EnumCollection, added EVisualElements
* UiCollection, added GetVisualElement()

### Changed
* GenericUIManager changed name to GenericWorldUIManager
* EntityFollowerISystem, Entities with EntityTransformIData will now be destroyed when the game object is null

### Removed
* Removed GenericEventManager
* CameraController, removed IsCameraReady and RunFadeScreen()
* CameraController, removed blackScreenImg and initialFadeOutTime

## [0.1.6] - 2026-03-18

### Added
* Added back the test folder
* GenericSingleton, now comes with its own get ECS.World (GetWorld())
* GenericSingleton, added GetSingletonEntity<TComponent>()
* GenericSingleton, added regions for ECS related and Async related
* GenericUIManager, poolCount is now ignored, spawn[] size is forced to 1, extra loop is removed
* GenericUIManager, now add a toggle that spawn a PhysicsCollider on the UI element
* GenericUIManager, added ECS region
* UiHelper, added TransformRect validation
* UiHelper, added id for tag
* Added Entities/UI folder
* Added UIData
* Added UiHandleSystemBase

### Removed
* GenericSingleton, removed KeepSingleton() and all child classes that using the said method

## [0.1.5] - 2026-03-16

### Fixed
* Fixed GenericAudioManager always return -1 when using GetPoolIndex()(Added an override);

### Added
* Added SpawnDelayEntityAuthoring

## Removed
* Removed old folders saves in FolderModificationData.json

### Changed
* GenericPoolingManager, field "initializeOnStart" and "collectionCheck" changed to abstract properties

## [0.1.4] - 2026-03-15

### Added

* Added GenericPoolingManager
* Added new GenericAudioManager, GenericParticleManager and GenericUIManager that inherit from GenericPoolingManager
* Added GenericSpawnManager
* HelperCollection, added "RandomValue2" and "RandomValue3"

### Changed
* Moved legacy GenericAudioManager, GenericParticleManager and GenericUIManager to ObsoleteV2 folder and marked [Obsolete]
* HelperCollection, all "RandomValue" with GameObject parameter changed to Component instead

## [0.1.3] - 2026-03-14

### Changed
* Reformatted all coding styles (no changes in functionality)

## [0.1.2] - 2026-03-13

### Added
* Added RemoveMissingScriptsEditor
* Added EditorUtils

### Changed
* Moved EditorBackgroundColor from LoadIconDisplayEditor to EditorUtils

## [0.1.1] - 2026-03-13

### Fixed
* LoadIconDisplayEditor now do extra checks when the gameobject has missing scripts

### Changed
* Renamed LoadIconDisplay to LoadIconDisplayEditor
* Renamed AnimationRecorder to AnimationRecorderEditor
* Moved CameraControllerEditor, CameraTagFollowerEditor, DestroyEntityEditor and FlatPlaneEditor to new folder called Component Editor
* Static Stuff, changed all "!=" to "is not" in CallStaticMethod, CallGenericInstanceMethod and CallInstanceMethod
