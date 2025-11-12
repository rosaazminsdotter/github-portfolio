using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class Jump_Effect : MonoBehaviour
{
    [SerializeField] float jumpHeight = 2;
    [SerializeField] float respawnTime = 16f;
    private Collider2D col;
    private SpriteRenderer sr;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sound;

    void Awake()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Rotate(0f, 1f, 0f * Time.deltaTime); 
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Player")
        {
            Player_Move player = collider.gameObject.GetComponent<Player_Move>();
            player.JumpHigher(jumpHeight);
             if (audioSource != null && sound != null)
            {
                audioSource.PlayOneShot(sound);
            }

            StartCoroutine(DisableTemporarily());
        }
    }
    private IEnumerator DisableTemporarily()
    {
        col.enabled = false;
        sr.enabled = false;
        yield return new WaitForSeconds(respawnTime);
        col.enabled = true;
        sr.enabled = true;
    }   
}
