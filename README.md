# SFG: High-density Mobile Cloud Gaming on Edge SoC Farms

This repo contains Unity packages for 

1. partitioning a game session given a rectangle area on the main camera; 

2. streaming the rendering results from one of the partitioned game session to another side through WebRTC, and combining to a whole scene;

3. assessing game performance (e.g., FPS)

There is also a separate WebRTC signaling server implementation.

## Project structure

- FPSUtils: Unity prefab for showing FPS on the main game screen (derived from the Viking Village project).

- FrameTimesOverlay: Unity prefab for showing game rendering time on the game screen.

- GameSplit: The core game partitioning logic implementation

    - DistanceSplit: Code for implementing distance-based game partitioning;
    it allows rendering near part and the remote part.

    - SFG: Vertical game partitioning or rectangle-based game partitioning.

    - Prefabs: A list of Unity Prefabs that aggregating all above game partitioning features and metrics.

    - Scripts: Common implementation for FPS counting, frame quality controlling, etc.

- UnityWebRTCSignalingServer: The WebRTC signaling server implementation.