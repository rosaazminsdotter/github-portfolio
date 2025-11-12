using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using System.Collections;

public class Player_Move : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;

    private float xInput;

    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float boostDuration = 15f;

    //public bool canDoubleJump;

    [Header("Collision Info")]
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask ground;
    [SerializeField] private Transform groundCheckPoint; //DENNA
    [SerializeField] private float groundCheckRadius = 0.2f; //DENNA
    private bool isGrounded;
    private bool wasGrounded; //DENNA
    private int jumpCount = 2;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip jumpSound;


    void Update()
    {
        GroundCheck();
        JumpButton();
        Movement();
    }

    void Movement()
    {
        //Input.GetAxis får ett kontinuerligt värde mellan -1 och 1 beroende på hur spelaren trycker
        //Input.GetKeyDown utför en händelse exakt en gång när en knapp trycks ned – inte varje bildruta.
        //Ett hopp ska bara ske när knappen trycks, inte så länge den är nedtryckt. Annars kan spelaren hoppa flera gånger på en gång 
        xInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    void Jumping()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private void JumpButton()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            Jumping();
            jumpCount--;
        }
    }

    void GroundCheck()
    {
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, ground); //DENNA

        if (isGrounded && !wasGrounded)
        {
            jumpCount = 2;
        }
    }

    //ritar ett sträck så man kan se vart groundCheckDistance befinner sig i relation till transform.position.y (ett sträck ritas från ena till andra)
    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
    }

    public void JumpHigher(float jumpHeight)
    {
        StartCoroutine(JumpBoostRoutine(jumpHeight));
    }

    private IEnumerator JumpBoostRoutine(float jumpHeight)
    {
        this.jumpHeight += jumpHeight;
        yield return new WaitForSeconds(boostDuration);
        this.jumpHeight-= jumpHeight;
    }




/*
public class Player_Move : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;

    [Header("Player Input")]
    public bool Right;
    public bool Left;
    public bool Jump;


    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpHeight;
    [SerializeField] float boostDuration = 15f;



    [Header("Collision Info")]
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask ground;
    [SerializeField] private Transform groundCheckPoint; 
    [SerializeField] private float groundCheckRadius = 0.2f; 
    private bool isGrounded;
    private bool wasGrounded;
    int jumpCount = 2;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip jumpSound;

    void Update()
    {
        GroundCheck();
        JumpButton();
        Movement();
    }

    void Movement()
    {
        //Input.GetAxis får ett kontinuerligt värde mellan -1 och 1 beroende på hur spelaren trycker
        //Input.GetKeyDown utför en händelse exakt en gång när en knapp trycks ned – inte varje bildruta.
        //Ett hopp ska bara ske när knappen trycks, inte så länge den är nedtryckt. Annars kan spelaren hoppa flera gånger på en gång 

        float direction = 0;
        if (Right) direction = 1;
        else if (Left) direction = -1;
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }

    void Jumping()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
       
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private void JumpButton()
    {
        if (Jump && jumpCount > 0)
        {
            Jumping();
            jumpCount--;
            Jump = false;
        }
    }

    void GroundCheck()
    {
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, ground); //DENNA

        if (isGrounded && !wasGrounded)
        {
            jumpCount = 2;
        }
    }

    public void JumpHigher(float jumpHeight)
    {
        StartCoroutine(JumpBoostRoutine(jumpHeight));
    }

    private IEnumerator JumpBoostRoutine(float jumpHeight)
    {
        this.jumpHeight += jumpHeight;
        yield return new WaitForSeconds(boostDuration);
        this.jumpHeight -= jumpHeight;
    }

    public void SetMoveLeft(bool value) { Left = value; }
    public void SetMoveRight(bool value) { Right = value; }
    public void SetJump(bool value) { Jump = value; }
}
*/
}