using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    public AudioClip mainMenuClip;
    public AudioClip gameClip;
    public InputAction mute;

    private AudioSource _audioSource;
    private bool _isAudioSourcePlaying;
    private bool _UICoroutineInProgress;
    private TMP_Text _audioText;
    
    protected override void Awake()
    {
        base.Awake();
        mute.Enable();
    }

    void Start()
    {
        _audioText = GetComponentInChildren<TMP_Text>();
        _audioText.gameObject.SetActive(false);
        DontDestroyOnLoad(this);
        mute.performed += OnMutePerformed;
        _audioSource = GetComponent<AudioSource>();

        GameManager.Instance.GameStateChanged += OnGameStateChanged;
    }

    private void OnMutePerformed(InputAction.CallbackContext obj)
    {
        if (_audioText.gameObject.activeSelf == false)
            _audioText.gameObject.SetActive(true);
        
        if (_isAudioSourcePlaying)
        {
            _audioSource.Pause();
            _isAudioSourcePlaying = false;
            if (!_UICoroutineInProgress)
               StartCoroutine(DisplayAudioStatus());
        }
        else
        {
            _audioSource.UnPause();
            _isAudioSourcePlaying = true;
            if (!_UICoroutineInProgress)
                StartCoroutine(DisplayAudioStatus());
        }
    }
    
    private IEnumerator DisplayAudioStatus()
    {
        _UICoroutineInProgress = true;
        {
            float t = 0;
            Color startColor = new Color32(255,255,255,0);
            Color endColor = new Color32(255,255,255,255);
 
            _audioText.color = startColor;
            
            if (_isAudioSourcePlaying)
                _audioText.text = "Audio: ON";
            else
            {
                _audioText.text = "Audio: OFF";
            }
     
            while (t < 1)
            {
                _audioText.color = Color.Lerp(startColor, endColor, t);
                t += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(1);

            t = 0;
            
            while (t < 1)
            {
                _audioText.color = Color.Lerp(endColor, startColor, t);
                t += Time.deltaTime;
                yield return null;
            }

            _UICoroutineInProgress = false;
        }
    }

    private void OnGameStateChanged(GameManager.GameState previousState, GameManager.GameState currentState)
    {

        if (currentState == GameManager.GameState.Running)
        {
            _audioSource.clip = gameClip;
        }

        if (currentState == GameManager.GameState.Pregame)
        {
            _audioSource.clip = mainMenuClip;
        }

        if (_audioSource.clip)
        {
            _audioSource.Play();
            _isAudioSourcePlaying = true;
        }
        else
        {
            Debug.LogWarning("No clip to play :/");
        }
    }
}
