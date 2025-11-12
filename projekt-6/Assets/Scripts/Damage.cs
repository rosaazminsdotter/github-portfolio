using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class Damage : MonoBehaviour
{
    [SerializeField] public int damage;
    bool hasDamagedPlayer = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && !hasDamagedPlayer)
        {
            Player_lives player = collision.gameObject.GetComponent<Player_lives>();
            if (player.Lives > damage)
            {
                player.LoseLives(damage);
                player.Respawn();
                hasDamagedPlayer = true;
            }
            else
            {
                RestartGame();
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            hasDamagedPlayer = false;
        }
    }
    
     void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
