using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    [SerializeField] public float moveDistance;
    [SerializeField] public float moveSpeed;
    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        transform.position = new Vector3(startPosition.x + x, transform.position.y, transform.position.z);
    }
}
