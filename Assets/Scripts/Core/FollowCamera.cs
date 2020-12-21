using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [Range(0f, 1f)]
    public float smoothing;

    private GameManager _gameManager;
    private Level _currentLevel;
    private GameObject _target;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _gameManager = FindObjectOfType<GameManager>();
        _gameManager.PlayerSpawned += SetTarget;
        _currentLevel = _gameManager.GetLevelMaster().GetCurrentLevel();
    }
    
    private void SetTarget()
    {   
        _target = _gameManager.GetPlayer().gameObject;
    }

    private void LateUpdate()
    {
        if (!_target) return;
        var targetPosition = _target.transform.position;
        var newCameraPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z)
        {
            x = Mathf.Clamp(targetPosition.x, 0, _currentLevel.levelSize.x),
            y = Mathf.Clamp(targetPosition.y, 0, _currentLevel.levelSize.y)
        };
        
        transform.position = Vector3.Lerp(transform.position, newCameraPosition, smoothing);
    }
}
