using UnityEngine;

public class enemyController : MonoBehaviour
{
    public EnemyObject enemy;
    private bool jumping;
    private float speed;
    private float JumpForce;
    private Transform player;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        speed = enemy.speed;
        JumpForce = enemy.jumpForce;
        player = GameObject.FindObjectOfType<playerController>().transform;
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("jump", 0, enemy.jumpTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < player.position.x)
        {
            rb.velocity = new Vector2(speed*Time.deltaTime, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed*Time.deltaTime, rb.velocity.y);
        }
        if (transform.position.y + 1 < player.position.y)
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }
    }
    void jump()
    {
        if (jumping)
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
    }
}
