using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneManager : Singleton<SceneManager>
{
    public LevelManager levelManager;
    public event Action LevelLoaded;
    private bool _loaded = false;
    private AsyncOperationHandle<SceneInstance> _sceneHandle;

    protected void Start()
    {
        _loaded = false;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadLevel(int index)
    {
        if (index <= levelManager.levels.Count - 1)
        {
            if (_loaded)
                UnloadCurrentLevel();
            levelManager.levels[index].scene.LoadSceneAsync(LoadSceneMode.Additive).Completed += OnLevelLoadComplete;
            return;
        }

        Debug.LogWarning("Attempted to load level (index: " + index + ") which doesn't exist.");
    }

    private void UnloadCurrentLevel()
    {
        Addressables.UnloadSceneAsync(_sceneHandle, true).Completed += OnLevelUnloadComplete;
    }
    
    private void OnLevelLoadComplete(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status != AsyncOperationStatus.Succeeded) return;
        _loaded = true;
        _sceneHandle = handle;
        LevelLoaded?.Invoke();
    }

    private void OnLevelUnloadComplete(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status != AsyncOperationStatus.Succeeded) Debug.LogError("[LevelManager] Error loading scene " + handle.Result.Scene);
    }
}