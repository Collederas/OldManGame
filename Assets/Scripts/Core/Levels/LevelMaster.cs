using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

[CreateAssetMenu(fileName = "LevelMaster", menuName = "Levels/Level Master")]
public class LevelMaster: ScriptableObject
{
    public List<Level> levels = new List<Level>();
    public int currentLevelIndex = 0;
    public event Action<Level> LevelLoaded;

    public Level GetCurrentLevel()
    {
        return levels[currentLevelIndex];
    }
    public void LoadLevelWithIndex(int index)
    {
        
        if (index <= levels.Count - 1)
        {
            currentLevelIndex = index;
            levels[index].scene.LoadSceneAsync().Completed += LevelLoadComplete;
            return;
        }
        Debug.LogWarning("Attempted to load level (index: %d) which doesn't exist.");
    }

    public void NextLevel()
    {            
        LoadLevelWithIndex(currentLevelIndex + 1);
    }

    public void RestartLevel()
    {
        LoadLevelWithIndex(currentLevelIndex);
    }

    public void NewGame()
    {
        LoadLevelWithIndex(1);
    }

    private void LevelLoadComplete(AsyncOperationHandle<SceneInstance> sceneInstance)
    {
        if (sceneInstance.Status == AsyncOperationStatus.Succeeded)
        {
            LevelLoaded?.Invoke(levels[currentLevelIndex]);
        }
    }
}