using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    public LevelMaster levelMaster;
    public Animator transitionAnimator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            levelMaster.NextLevel(transitionAnimator);
    }
}
