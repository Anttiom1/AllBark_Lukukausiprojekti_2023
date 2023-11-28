using UnityEngine;

public class Stone : MonoBehaviour, IObjectManager
{
    private AudioSource audioSource; // Tässä määritellään audioSource

    void Start()
    {
        // Tässä haetaan ja tallennetaan AudioSource-komponentti audioSource-muuttujaan
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

            // Tarkista, onko audioSource määritelty ja toista ääni
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
