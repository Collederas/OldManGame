using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelMaster", menuName = "Levels/Level Master")]
public class LevelManager : ScriptableObject
{
    private static readonly int Start = Animator.StringToHash("Start");
    public List<Level> levels = new List<Level>();
    public int currentLevelIndex;
    public AsyncOperationHandle<SceneInstance> currentLoadedSceneHandle;
    public event Action<Level> LevelLoaded;

    public Level GetCurrentLevel()
    {
        return levels[currentLevelIndex];
    }

    public void LoadLevel(int index, Animator transition)
    {
        if (index <= levels.Count - 1)
        {
            currentLevelIndex = index;
            levels[index].scene.LoadSceneAsync(LoadSceneMode.Additive).Completed += OnLevelLoadComplete;
            return;
        }

        Debug.LogWarning("Attempted to load level (index: " + index + ") which doesn't exist.");
    }

    public void UnloadCurrentLevel()
    {
        Addressables.UnloadSceneAsync(currentLoadedSceneHandle).Completed += OnLevelUnloadComplete;
    }

    public void RestartLevel(Animator transition)
    {
        LoadLevel(currentLevelIndex, transition);
    }

    private void OnLevelLoadComplete(AsyncOperationHandle<SceneInstance> sceneInstance)
    {
        if (sceneInstance.Status == AsyncOperationStatus.Succeeded)
        {
            currentLoadedSceneHandle = sceneInstance;
            LevelLoaded?.Invoke(GetCurrentLevel());
            return;
        }

        Debug.LogError("[LevelManager] Unable to load level: " + levels[currentLevelIndex].name);
    }

    private void OnLevelUnloadComplete(AsyncOperationHandle<SceneInstance> sceneInstance)
    {
        if (sceneInstance.Status == AsyncOperationStatus.Succeeded) Addressables.Release(sceneInstance);
    }
}