using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level[] levels;
    Level currentLevel;

    public Level GetCurrentLevel()
    {
        return levels[0];
    }
}
