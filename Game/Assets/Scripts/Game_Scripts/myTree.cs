using UnityEngine;

public class myTree : MonoBehaviour, IObjectManager
{
    public GameObject Stump;
    private AudioSource audioSource;
    private bool isFalling = false;
    // Tree falling speed
    private float fallingSpeed = 65f; 
    // Angle where tree -> stump
    private float maxFallingAngle = 45f; 

    void Start()
    {
        // Get audio source
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isFalling)
        {
            // Turn tree on direction
            transform.Rotate(Vector3.forward, fallingSpeed * Time.deltaTime);

            // Check if tree has fallen enough degrees to create stump
            if (transform.eulerAngles.z >= maxFallingAngle && transform.eulerAngles.z < 180)
            {
                CreateStump();
                isFalling = false;
            }
        }
    }

    public virtual float Collide()
    {
        isFalling = true;
        PlaySound(); // Play sound effect
        return -10;
    }
        private void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void CreateStump()
    {
        Instantiate(Stump, transform.position, Quaternion.identity);
        Destroy(gameObject); // destroys old tree object
    }
}
