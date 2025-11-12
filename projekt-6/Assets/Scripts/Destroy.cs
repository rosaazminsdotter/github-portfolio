using UnityEngine;

public class Destroy : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sound;
     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        { 
             if (sound != null)
            {
                AudioSource.PlayClipAtPoint(sound, transform.position);
            }
            gameObject.SetActive(false);
        }
    }
}
