****************
* COLOR STUDIO *
* README FILE  *
****************


How to use this asset
---------------------
1) Inside Unity Editor, select top menu Window -> Color Studio -> Palette Manager or Pixel Painter.

2) It's recommended to drag & drop the interface windows and and dock them to other windows as shown in this video:
https://youtu.be/Qe2yHCqWhoU

3) Click on the ? tab of Color Studio window for quick instructions

4) Use the Pixel Painter window to create your pixel art

5) Add the "Recolor" script to a gameobject or sprite to change its colors at runtime


Support
-------
Any suggestion or support request should be forwarded to:

* Website-Forum: https://kronnect.com
* Email support: contact@kronnect.com
* Twitter: @KronnectGames


Version history
---------------

Version 2.8
- Added "Pick Original Color from Scene View" button in Recolor inspector. Grabs the exact original color by clicking on the object in SceneView which is more accurate than using the color picker
- Fixes and optimizations

Version 2.7
- Allows more compact layout

Version 2.6
- Added automatic scrollbars to Pixel Painter window

Version 2.5
- Color wheel tab: options for entering primary color values directly
- Redesigned Palette RGB input section using a single line and a dropdown for color encoding option

Version 2.4
- Support for linear (non SRGB) textures

Version 2.3.2
- [Fix] Fixed wrong output size when saving a texture after resizing a canvas
- [Fix] Updated links to support site

Version 2.3.1
- [Fix] Fixed build issue with addressables

Version 2.3
- Added "Save As New" separate button to clear confusion when duplicating an open palette
- Fixes

Version 2.2
- Recolor scripts now update automatically when palette changes are saved from Color Studio window
- Prevents a runtime error when some material doesn't exist or has been disposed in a renderer with multiple materials
- Ensure palette preview is refreshed in inspector
- Removes a console warning when moving the color threshold slider in the inspector of Recolor script

Version 2.1
- Improved performance of Recolor operations

Version 2.0.3
- [Fix] Fixed quad-mode mirror drawing issue

Version 2.0
- New "Pixel Painter" window

Version 1.5
- Recolor: LUT performance optimizations

Version 1.4
- Recolor: added LUT option
- Recolor: added Color Correction options (vibrance, contrast, brightness, tinting)

Version 1.3
- Recolor now supports vertex colors transformations

Version 1.2
- Added Color Threshold option
- Realtime preview in Editor
- Added command button to add main texture colors to Color Operations section

Version 1.1
- Added Color Match mode option
- Can specify custom color operations

Version 1.0
- Initial version




