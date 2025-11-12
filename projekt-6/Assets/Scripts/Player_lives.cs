using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Player_lives : MonoBehaviour
{
    [Header("Death and Respawning")]
    public float respawnWaitTime = 2f;
    private Vector3 spawnPoint;
    public int Lives { get; private set; } = 5;

    
    [Header("Hearts Display")]
    public Image[] hearts = new Image[5];
    public Sprite emptyHeart;
    public Sprite fullHeart;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip damageSound;


    private void Start()
    {
        spawnPoint = transform.position;
    }
    
    void Update()
    {
        UpdateLivesDisplay();
    }

    void UpdateLivesDisplay()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < Lives; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }

    public void LoseLives(int damage)
    {
        Lives -= damage;
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }

    public void AddLives()
    {
        if (Lives < 5)
        {
            Lives++;
        }
    }
    public void Respawn()
    { 
        transform.position = spawnPoint;
    }
}
