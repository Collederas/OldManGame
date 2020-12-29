using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Range(0f, 1f)] public float smoothing;
    private Level _currentLevel;
    private bool _followPlayer;

    public GameObject Target { get; set; }

    public Vector2 CurrentLevelBoundaries { get; set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _followPlayer = true;
    }

    private void LateUpdate()
    {
        if (!Target || !_followPlayer) return;
        var targetPosition = Target.transform.position;
        var newCameraPosition = new Vector3(targetPosition.x, targetPosition.y, transform.position.z)
        {
            x = Mathf.Clamp(targetPosition.x, 0, CurrentLevelBoundaries.x),
            y = Mathf.Clamp(targetPosition.y, 0, CurrentLevelBoundaries.y)
        };

        transform.position = Vector3.Lerp(transform.position, newCameraPosition, smoothing);
    }

    public void SetFollowPlayer(bool value)
    {
        _followPlayer = value;
    }

    private void SetTarget()
    {
        Target = GameManager.Instance.GetPlayerController().gameObject;
    }
}