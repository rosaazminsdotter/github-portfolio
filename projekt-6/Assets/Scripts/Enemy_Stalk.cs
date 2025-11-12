using UnityEngine;
using System.Collections;

public class Enemy_Stalk : MonoBehaviour
{
    public Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] float delay = 1f;
    private float targetX;

    void Start()
    {
        StartCoroutine(UpdateTargetXWithDelay());
    }

    void Update()
    {
        Vector3 newPos = transform.position;
        newPos.x = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
        transform.position = newPos;
    }

    IEnumerator UpdateTargetXWithDelay()
    {
        while (true)
        {
            targetX = player.position.x;
            yield return new WaitForSeconds(delay);
        }
    }
}
