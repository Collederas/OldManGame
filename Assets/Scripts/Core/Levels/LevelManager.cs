using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelMaster", menuName = "Levels/Level Master")]
public class LevelManager : ScriptableObject
{
    public List<Level> levels;
}