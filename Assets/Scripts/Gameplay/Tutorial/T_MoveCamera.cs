using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialCameraMovement", menuName = "Tutorial Actions/Camera Movement")]
public class T_MoveCamera : TutorialAction
{
    public Vector2 moveDirection;
    public float moveAmount;

    [Range(0.1f, 20f)] public float duration;

    /* If the camera should return to focus on the player after 
       moving to destination */
    public bool returnToPlayer;

    private Camera _camera;
    private FollowCamera _cameraScript;
    private Vector3 _destination;

    public override void Init()
    {
        // ReSharper disable once Unity.PerformanceCriticalCodeCameraMain
        _camera = Camera.main;
        if (!_camera)
        {
            Debug.LogError("[T_MoveCamera] Unable to locate main camera. Aborting action.");
            return;
        }

        _cameraScript = _camera.GetComponent<FollowCamera>();
        _destination = CalculateDestination();

        _cameraScript.SetFollowPlayer(false);
    }

    public override IEnumerator Execute()
    {
        if (!_camera) yield return null;
        var elapsedTime = 0f;
        var increment = Time.fixedDeltaTime;

        while ((int)_camera.transform.position.magnitude != (int)_destination.magnitude)
        {
            var currentCameraPos = _camera.transform.position;
            _camera.transform.position += (_destination - currentCameraPos) / duration * increment;
            elapsedTime += increment;
            yield return new WaitForSeconds(increment);
        }

        if (returnToPlayer)
            _cameraScript.SetFollowPlayer(true);
    }

    private Vector3 CalculateDestination()
    {
        var moveDelta = moveDirection.normalized * moveAmount;
        var initialCameraPos = _camera.transform.position;
        return new Vector3(initialCameraPos.x + moveDelta.x, initialCameraPos.y + moveDelta.y, initialCameraPos.z);
    }
}