# FrameTimesOverlay

Simple text overlay displaying CPU and GPU frame times using data from the [Frame Timing Manager](https://docs.unity3d.com/2020.3/Documentation/ScriptReference/FrameTimingManager.html).

![image](https://user-images.githubusercontent.com/71269862/226328470-93cc6807-8b61-42e6-a612-a0a02d667360.png)

# How to use:

1. Add the package to your Unity project.
2. Add the **FrameTimes** prefab from the *Prefab* folder of to the scene.
3. Link a rendering camera to the ***Render Camera*** field of the **Canvas** at the root of the prefab.
4. Make sure **Frame Timing Stats** is enabled in the **Player Settings**.

# Important notes

The CPU frame times include VSync, so it would never report any times lower than the current target framerate.

On Unity versions prior to 2022.2, [Frame Timing Manager](https://docs.unity3d.com/2020.3/Documentation/ScriptReference/FrameTimingManager.html) only reports timings on consoles and mobile devices.

# Original repo

https://github.com/danielreidler-unity/FrameTimesOverlay
