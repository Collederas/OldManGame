using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    GameObject target;

    [Range(0f, 1f)]
    public float smoothing;
    GameManager gameManager;
    Level currentLevel;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.PlayerSpawned += SetTarget;

        currentLevel = gameManager.GetCurrentLevel();
    }

    void SetTarget()
    {
        target = gameManager.GetPlayer().gameObject;   
    }

    void LateUpdate()
    {
        if(target)
        {
            var targetPos = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);

            targetPos.x = Mathf.Clamp(target.transform.position.x, 0, currentLevel.levelSize.x);
            targetPos.y = Mathf.Clamp(target.transform.position.y, 0, currentLevel.levelSize.y);

            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }
    }
}
