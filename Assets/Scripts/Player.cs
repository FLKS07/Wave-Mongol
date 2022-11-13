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
    [SerializeField] float playerHealth;
    [SerializeField] bool isAttacking; //This is to control the velocity of the rigidBody of the player
    public float attackDamage;
    public bool isDead;

    [Header("Animator")]
    [SerializeField] bool isWalking;

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
        if (isAttacking == false || isDead == false)
        {
            rb2D.velocity = new Vector2(horizontal * movementSpeed, rb2D.velocity.y); // The x axis movement
        }


        if (isGrounded == true && (isAttacking == false || isDead == false))
        {
            rb2D.AddForce(new Vector2(rb2D.velocity.x, vertical * jumpForce));
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
            playerAnimator.SetTrigger("Die");
            isDead = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isWalking == false)
        {
            Attack(dammageAttack);
        }



        playerAnimator.SetBool("Walking", isWalking);

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
        Debug.Log("The player is attacking");
    }

    public void FinishAttack()
    {
        attackCollider.SetActive(false);
        isAttacking = false;
    }

    public void HurtPlayer(float damage)
    {
        playerHealth = playerHealth - damage;
    }
}
