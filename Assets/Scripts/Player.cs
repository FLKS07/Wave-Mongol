using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Speeds")]
    public float movementSpeed;
    public float jumpForce;

    private float horizontal;
    private float vertical;

    [Header("Player Status")]
    public float playerHealth;
    [SerializeField] bool isAttacking; //This is to control the velocity of the rigidBody of the player
    public float attackDamage;
    public bool isDead;
    public float knobackX;
    public float knobackY;
    [SerializeField] bool godMode;
    public bool isBlocking;

    [Header("Blocking")]
    public GameObject blockingObject;

    [Header("Max boundries")]
    public Transform BoundryA; // x -
    public Transform BoundryB; // x +

    [Header("Animator")]
    [SerializeField] bool isWalking;

    [Header("GameController")]
    public GameController gameController;

    [Header("Groundcheck")]
    public Transform A;
    public Transform B;
    [SerializeField] bool isGrounded;

    [Header("Flip")]
    [SerializeField] bool isLookingLeft;

    [Header("AttackCollider")]
    public GameObject attackCollider;
    public bool startActive;
    public float dammageAttack;


    private bool isPaused;
    private Rigidbody2D rb2D;
    Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        attackCollider.SetActive(startActive);

    }

    // Update is called once per frame 
    void Update()
    {
        // The player movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Jump");
        if (isAttacking == true && isBlocking == true)
        {
            horizontal = 0;
            vertical = 0;
        }


        if (isAttacking == false && isDead == false && isBlocking == false)
        {
            rb2D.velocity = new Vector2(horizontal * movementSpeed, rb2D.velocity.y); // The x axis movement
        }

        if (isGrounded == true && (isAttacking == false && isDead == false && isBlocking == false))
        {
            rb2D.AddForce(new Vector2(rb2D.velocity.x, vertical * jumpForce));
        }


        // The player is blocking
        if (isBlocking == false && (Input.GetKey(KeyCode.Mouse1)) && isAttacking == false)
        {
            isBlocking = true;
            playerAnimator.SetBool("isBlocking", true);
            blockingObject.SetActive(true);

        }
        if (isBlocking == true && (Input.GetKey(KeyCode.Mouse1) == false))
        {
            isBlocking = false;
            playerAnimator.SetBool("isBlocking", false);
            blockingObject.SetActive(false);
        }


        // The player movement code;

        // This is to flip the player object
        if (isDead == false)
        {
            if (horizontal > 0 && isLookingLeft == true && isAttacking == false)
            {
                Flip();
            }
            if (horizontal < 0 && isLookingLeft == false && isAttacking == false)
            {
                Flip();
            }
        }
        // This is to flip the player object;

        if (horizontal != 0)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        //

        if (playerHealth <= 0)
        {
            playerAnimator.SetBool("isDead", true);
            isDead = true;
            gameController.isGameOver = true;
            playerHealth = 0;
        }
        if (playerHealth >= 10)
        {
            playerHealth = 10;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isWalking == false)
        {
            Attack(dammageAttack);
        }

        playerAnimator.SetBool("Walking", isWalking);


        CheckBoundries();

    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(A.position, B.position);
    }


    void Flip()
    {
        isLookingLeft = !isLookingLeft;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    void Attack(float damage)
    {
        isAttacking = true;
        attackCollider.SetActive(true);
        playerAnimator.SetTrigger("Attack");
    }

    public void FinishAttack()
    {
        attackCollider.SetActive(false);
        isAttacking = false;
    }

    public void HurtPlayer(float damage)
    {
        if (godMode == false) // If the god mod is not active, is when the player takes damage;
        {
            playerHealth = playerHealth - damage;
        }


        /*
        if (isLookingLeft == true)
        {
            rb2D.velocity = new Vector2(knobackX, transform.position.y) * -1;
            rb2D.AddForce(new Vector2(transform.position.x, knobackY) * -1);
        }
        else if (isLookingLeft == false)
        {
            rb2D.velocity = new Vector2(knobackX, transform.position.y) * 1;
            rb2D.AddForce(new Vector2(transform.position.x, knobackY) * 1);
        }
        */
    }

    void CheckBoundries()
    {
        if (this.transform.position.x < BoundryA.transform.position.x)
        {
            Debug.Log("A");
            this.transform.position = BoundryA.transform.position;
        }
        else if (this.transform.position.x > BoundryB.transform.position.x)
        {
            Debug.Log("B");
            this.transform.position = BoundryB.transform.position;
        }
    }

    public void GameOver()
    {
        gameController.gameOver();
    }

}
