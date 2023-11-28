using UnityEngine;

public class Stone : MonoBehaviour, IObjectManager
{
    private AudioSource audioSource; 

    void Start()
    {
        // Audio source component
        audioSource = GetComponent<AudioSource>();
    }

    public float Collide()
    {
        return 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.Stop();

            // Check for audio source and play sound
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
