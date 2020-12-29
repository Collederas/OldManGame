using UnityEngine;

public class MenuClickEvents : MonoBehaviour
{
    public void StartGame(int levelIndex)
    {
        GameManager.Instance.LoadLevel(levelIndex);
    }
    
    public void QuitApplication()
    {
        Application.Quit();
    }
}
