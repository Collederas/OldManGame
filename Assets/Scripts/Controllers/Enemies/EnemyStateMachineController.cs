using System.Collections;
using UnityEngine;

public class EnemyStateMachineController : MovingObjectController
{
    public AudioClip fallingAudioClip;
    
    public float fallingSpeed = 2f;
    protected EnemyBaseState currentState;
    protected EnemyFallState fallState;

    public bool frozen;
    private AudioSource _audioSource;
    public Vector2 FallTargetPosition { get; set; }

    private bool _clipPlaying;
    
    protected virtual void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        fallState = new EnemyFallState(this);
        currentState.Enter();
    }
    
    protected override void Update()
    {
        currentState.Update();
        base.Update();
    }

    protected override void FixedUpdate()
    {
        currentState.FixedUpdate();
        base.FixedUpdate();
    }

    public void ChangeState(EnemyBaseState newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }

    public override void Fall(Vector2 fallTargetPosition)
    {
        ChangeState(fallState);
        FallTargetPosition = fallTargetPosition;
        if(!_clipPlaying)
            StartCoroutine(PlayClip(fallingAudioClip));
    }
    
    protected IEnumerator PlayClip(AudioClip clip)
    {
        _clipPlaying = true;
        _audioSource.clip = fallingAudioClip;
        _audioSource.Play();
        yield return new WaitForSeconds(_audioSource.clip.length);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}