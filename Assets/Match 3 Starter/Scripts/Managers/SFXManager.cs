using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public enum Clip { Select, Swap, Clear }

    public static SFXManager instance;
    public AudioSource[] sfx;

    void Start()
    {
        instance = this;
        if (sfx == null || sfx.Length == 0)
        {
            sfx = GetComponents<AudioSource>();
        }
    }

    public void PlaySFX(Clip audioClip)
    {
        sfx[(int)audioClip].Play();
    }
}