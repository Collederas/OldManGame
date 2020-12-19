using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Level", menuName = "Levels/Level", order = 1)]
public class Level : ScriptableObject
{
    public Vector2 levelSize;
    public AssetReference scene;
}
