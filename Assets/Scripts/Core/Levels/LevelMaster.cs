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
    private static readonly int Start = Animator.StringToHash("Start");
    public event Action<Level> LevelLoaded;
    
    public Level GetCurrentLevel()
    {
        return levels[currentLevelIndex];
    }
    public void LoadLevelWithIndex(int index, Animator transition)
    {
        
        if (index <= levels.Count - 1)
        {
            currentLevelIndex = index;
            levels[index].scene.LoadSceneAsync().Completed += LevelLoadComplete;
            transition.SetTrigger(Start);
            return;
        }
        Debug.LogWarning("Attempted to load level (index: " + index + ") which doesn't exist.");
    }

    public void NextLevel(Animator transition)
    {            
        LoadLevelWithIndex(currentLevelIndex + 1, transition);
    }

    public void RestartLevel(Animator transition)
    {
        LoadLevelWithIndex(currentLevelIndex, transition);
    }

    public void NewGame(Animator transition)
    {
        LoadLevelWithIndex(0, transition);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void LevelLoadComplete(AsyncOperationHandle<SceneInstance> sceneInstance)
    {
        if (sceneInstance.Status == AsyncOperationStatus.Succeeded)
        {
            LevelLoaded?.Invoke(levels[currentLevelIndex]);
        }
    }
}