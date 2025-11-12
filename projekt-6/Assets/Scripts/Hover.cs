using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField] float hoverSpeed;
    [SerializeField] float hoverDistance;
    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        Hovering();
    }

    void Hovering()
    {
        float y = Mathf.PingPong(Time.time * hoverSpeed, hoverDistance);
        transform.position = new Vector3(startPosition.x, startPosition.y + y, startPosition.z);
    }
}
