using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    static bool exists;
    AudioSource audioSource;
    public TransitionState state;
    [SerializeField] float transitionSpeed;
    [SerializeField] float volume;
    [SerializeField] float basicVolume;

    void Start()
    {
        if (exists)
        {
            Destroy(this.gameObject);
            return;
        }

        exists = true;
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (state == TransitionState.TRANSITION_TO_ON)
        {
            volume += transitionSpeed * Time.deltaTime;

            if (volume >= 1)
            {
                volume = 1;
                state = TransitionState.ON;
            }
        }

        else if (state == TransitionState.TRANSITION_TO_OFF)
        {
            volume -= transitionSpeed * Time.deltaTime;

            if (volume <= 0)
            {
                volume = 0;
                state = TransitionState.OFF;
            }
        }

        audioSource.volume = volume * basicVolume;
    }

    public void Off()
    {
        state = TransitionState.TRANSITION_TO_OFF;
    }

    public void On()
    {
        state = TransitionState.TRANSITION_TO_ON;
    }
}
