using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("moving")]
    public bool canKill;
    public float walkSpeed;
    public Transform BluePortal;
    public Transform OrangePortal;
    public float dashSpeed;
    public float smoothTime;
    public float stopspeed;
    [Header("jumping")]
    public float JumpForce;
    public AudioSource jumpSfx;
    public AudioSource OrangePortalFire;
    public AudioSource BluePortalFire;
    public float dampForce;
    private Rigidbody2D rb;
    private Vector2 bVel = Vector2.zero;
    public float charHeight;
    float jumpTime;
    bool wallJump = false;
    bool WasClose = false;
    [Header("die")]
    bool grounded = false;
    Vector3 lastgroundpos;
    public int lives;
    public float deathHeight;
    public float cTime;
    float cTimer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Die()
    {
        transform.position = lastgroundpos;
        lives -= 1;
    }
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.right);
            if (hit.collider != null && hit.collider.gameObject.tag != "Portal")
            {
                BluePortal.position = hit.point;
                BluePortalFire.Play();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);
            if (hit.collider != null && hit.collider.gameObject.tag != "Portal")
            {
                OrangePortal.position = hit.point;
                OrangePortalFire.Play();
            }
        }
    }
    void FixedUpdate()
    {
        Vector2 WallForce = Vector2.zero;
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        RaycastHit2D groundCheckHit = Physics2D.CapsuleCast(transform.position, new Vector2(.4f,.4f),CapsuleDirection2D.Vertical,90,-Vector2.up);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -Vector2.right);
        Debug.DrawRay(transform.position, -Vector2.up, Color.red);
        if (groundCheckHit.collider != null)
        {
            float distance = Mathf.Abs(groundCheckHit.point.y - transform.position.y);
            if (distance < charHeight)
            {
                lastgroundpos = transform.position + new Vector3(0, 0.1f, 0);
                grounded = true;
                if (groundCheckHit.collider.gameObject.tag == "Enemy" && canKill)
                {
                    Destroy(groundCheckHit.collider.gameObject);
                }
                cTimer = 0;
            }
            else
            {
                grounded = false;
            }
        }
        else
        {
            cTimer += Time.deltaTime;
            if (cTimer > cTime)
            {
                grounded = false;
            }
        }
        if (hitLeft.collider != null && hitRight.collider != null)
        {
            bool lastWasClose = WasClose;
            float distanceLeft = Mathf.Abs(hitLeft.point.x - transform.position.x);
            float distanceRight = Mathf.Abs(hitRight.point.x - transform.position.x);
            wallJump = false;
            if (distanceLeft < charHeight)
            {
                wallJump = true;
                WasClose = true;
            }
            if (distanceRight < charHeight)
            {
                wallJump = true;
                WasClose = true;
            }
            if (lastWasClose == false && WasClose == true)
            {
                rb.AddForce(-stopspeed * rb.velocity);
            }
            if (distanceLeft > distanceRight)
            {
                WallForce = Vector2.up - Vector2.right;
            }
            else
            {
                WallForce = Vector2.up + Vector2.right;
            }
        }
        else
        {
            wallJump = false;
        }
        if (transform.position.y < deathHeight)
        {
            Die();
        }
        jumpTime += Time.deltaTime;
        float xinput = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
        {
//            Debug.Log("spacebar hit");
            if (jumpTime > .2f)
            {
                jumpSfx.pitch = Random.Range(0.7f, 1.0f);
                if (grounded)
                {
                    jumpSfx.Play();
                    jumpTime = 0;
                    rb.AddForce(Vector2.up * JumpForce);
                    grounded = false;
                }
                else if (wallJump)
                {
                    jumpSfx.Play();
                    jumpTime = 0;
                    rb.AddForce(WallForce * JumpForce);
                    wallJump = false;
                }
            }
        }
        if (jumpTime > 0.2f)
        {
            Vector2 targetVelocity = new Vector2(walkSpeed * Time.deltaTime * 1000 * xinput, rb.velocity.y);
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref bVel, smoothTime);
            if (xinput == 0)
            {
                rb.AddForce(-stopspeed * new Vector2(rb.velocity.x, 0));
            }
        }
        else
        {
            rb.velocity = rb.velocity;
//            Debug.Log("work" + jumpTime);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.AddForce(new Vector2(dashSpeed * xinput, 0));
        }
    }
}