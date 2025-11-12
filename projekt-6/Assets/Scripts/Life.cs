using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] public float pulsSpeed = 2f;      
    [SerializeField] public float pulsAmount = 0.1f;   

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }
     void Update()
    {
        Pulsating();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Player_lives player = collision.gameObject.GetComponent<Player_lives>();
            player.AddLives();
        }
    }

    void Pulsating()
    { 
        float scale = 1 + Mathf.Sin(Time.time * pulsSpeed) * pulsAmount;
        transform.localScale = originalScale * scale;
    } 
}
