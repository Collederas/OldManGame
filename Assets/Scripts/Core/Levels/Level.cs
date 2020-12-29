﻿using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Level", menuName = "Levels/Level", order = 1)]
public class Level : ScriptableObject
{
    public enum LevelType
    {
        Menu,
        Gameplay
    }

    public enum LevelLoadStatus
    {
        NotLoaded,
        Loading,
        Loaded,
    }
    
    public Vector2 levelSize;
    public AssetReference scene;
    public LevelType levelType;
    public LevelLoadStatus levelLoadStatus = LevelLoadStatus.NotLoaded;
}