using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public Animator animator;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    public bool jump = false;
    public bool CanPlayerDash = false;
    public float DashDistance = 15f;
    public bool isDashing;
    float doubleTaptime;
    KeyCode lastKeycode; 
    public float input = 0f;

    [SerializeField] Rigidbody2D rb;
    public bool canWallJump;
    public LayerMask groundLayer;
    public Transform wallGrabPoint;
    private bool canGrab, isGrabbing;
    public float wallJumpTime = .2f;
    private float wallJumpCounter;
    public float gravityStore;
    public bool canTeleport = false;
    public Text timerText;
    public GameObject timerTextObject;
    public GameObject timerImage;
    public int timer = 60;
    public bool canTeleportAgain = true;


    void Start() 
    {
        gravityStore = rb.gravityScale;
    }

    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        horizontalMove = input * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        CharacterController2D instance = GameObject.Find("Player").GetComponent<CharacterController2D>();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        if (CanPlayerDash)
        {
            // Dashing left
            if(Input.GetKeyDown(KeyCode.A))
            {
                if(doubleTaptime > Time.time && lastKeycode == KeyCode.A) {
                    animator.SetBool("IsDashing", true);
                    StartCoroutine(Dash(-1f));
                } else {
                    doubleTaptime = Time.time + 0.5f;
                }
                lastKeycode = KeyCode.A;
            }

            // Dashing right
            if(Input.GetKeyDown(KeyCode.D))
            {
                if(doubleTaptime > Time.time && lastKeycode == KeyCode.D) {
                    animator.SetBool("IsDashing", true);
                    StartCoroutine(Dash(1f));
                } else {
                    doubleTaptime = Time.time + 0.5f;
                }
                lastKeycode = KeyCode.D;
            }
        }

        if (canTeleport && Input.GetKeyDown(KeyCode.T) && canTeleportAgain)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mousePos.x, mousePos.y);
            canTeleportAgain = false;
            Chaostimer();
        }

        if (canWallJump)
        {
            if (wallJumpCounter <= 0)
            {
                canGrab = Physics2D.OverlapCircle(wallGrabPoint.position, .2f, groundLayer);

                isGrabbing = false;
                if (canGrab && !instance.m_Grounded && input != 0)
                {
                    if((transform.localScale.x == 1.6f && Input.GetAxisRaw("Horizontal") > 0) || (transform.localScale.x == -1.6f && Input.GetAxisRaw("Horizontal") < 0))
                    {
                        isGrabbing = true;
                    }
                }

                if (isGrabbing)
                {
                    rb.gravityScale = 0f;
                    rb.velocity = Vector2.zero;

                    if(Input.GetButtonDown("Jump"))
                    {
                        wallJumpCounter = wallJumpTime;

                        rb.velocity = new Vector2(-Input.GetAxisRaw("Horizontal") * 25, 10);
                        rb.gravityScale = gravityStore;
                        isGrabbing = false;
                    }
                } else {
                    rb.gravityScale = gravityStore;
                }
            } else {
                wallJumpCounter -= Time.deltaTime;
            }

        }
    }

    public void OnLanding ()
    {
        animator.SetBool("IsJumping", false);
    }

    void FixedUpdate ()
    {
        if(!isDashing)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;
        }
    }

    public void Chaostimer()
    {
        timerTextObject.SetActive(true);
        timerImage.SetActive(true);
        timer -= 1;
        timerText.text = timer.ToString();

        if(timer == 0)
        {
            timerTextObject.SetActive(false);
            timerImage.SetActive(false);
            canTeleportAgain = true;
            timer = 60;  
        } else {
            Invoke("Chaostimer",  1);
        }
    }
 
    IEnumerator Dash(float direction)
    {
        isDashing = true;
        rb.velocity = new Vector2 (rb.velocity.x, 0f);
        rb.AddForce(new Vector2(DashDistance * direction, 0f), ForceMode2D.Impulse);
        rb.gravityScale = 0f;
        yield return new WaitForSeconds(0.4f);  
        isDashing = false;
        animator.SetBool("IsDashing", false);
        rb.gravityScale = 1;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y), new Vector2(0.5f, 0.9f));
        Gizmos.DrawCube(new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y), new Vector2(0.5f, 0.9f));
    }
}
