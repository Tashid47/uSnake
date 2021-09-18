using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static AudioClip eatSound, deathSound;
    static AudioSource audioSrc;

    void Start()
    {
        eatSound = Resources.Load<AudioClip>("Eat");
        deathSound = Resources.Load<AudioClip>("Death");
        audioSrc = GetComponent<AudioSource>();
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "eat":
            audioSrc.PlayOneShot(eatSound);
            break;
            case "death":
            audioSrc.PlayOneShot(deathSound);
            break;
        }
    }
}
