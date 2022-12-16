using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackKnighController : MonoBehaviour
{
    [Header("Status")]
    public float currentHealth;
    public float movementSpeed;
    public float attackDamage;
    [SerializeField] float freezeTime;
    public int dropQuantatyCoins;
    [SerializeField] bool canMove;
    [SerializeField] bool isWalking;
    [SerializeField] bool isDead;
    public bool isLookingLeft;
    [SerializeField] bool playerIsLeft;


    [Header("Controllers")]
    public Player playerController;
    public GameController gameController;

    [Header("Data to make work")]
    public Transform playerTransform;
    public Color normalColor;
    public Color frezzeColor;
    public GameObject playerObject;
    Vector3 targetPosition;
    Transform startPosition;
    Animator animator;
    SpriteRenderer spriteRenderer;

    [Header("Debug")]
    [SerializeField] bool alreadyTurnedLeft;
    [SerializeField] bool alreadyTurnedRigh;
    [SerializeField] bool isAttacking;
    [SerializeField] bool rangeOfPlayer;


    void Start()
    {
        canMove = true;
        startPosition = this.transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        CheckRotation();
    }

    void Update()
    {
        int randomCoin = Random.Range(0, 10);


        CheckRotation();

        objectIsLeft(playerObject);

        if (gameController.isGameOver == false)
        {
            if (currentHealth <= 0 && isDead == false)
            {
                Debug.Log(currentHealth);
                if (randomCoin >= 7)
                {
                    gameController.coins = +dropQuantatyCoins;
                }
                gameController.mobsDeafeted = gameController.mobsDeafeted + 1;
                isDead = true; animator.SetTrigger("Die");
            }
            if (canMove == true && gameController.isPaused == false && isAttacking == false && isDead == false)
            {
                isWalking = true; animator.SetBool("isWalking", isWalking);
                targetPosition = new Vector3(playerTransform.position.x, startPosition.position.y, 0);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed);
            }
            else if (isDead == false)
            {
                isWalking = false; animator.SetBool("isWalking", isWalking);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            Debug.Log(playerController.attackDamage);
            currentHealth = currentHealth - playerController.attackDamage;
            canMove = false;
            spriteRenderer.color = frezzeColor;
            Invoke("UnFrezzeMovement", freezeTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isAttacking == false)
            {
                Attack(attackDamage);
                rangeOfPlayer = true;

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rangeOfPlayer = false;
        }
    }

    void Attack(float damage)
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
    }

    void HurtPlayer()
    {
        if (playerIsLeft =! playerController.isLookingLeft)
        {
            Debug.Log("True");
        }
        if (rangeOfPlayer && playerController.isBlocking == false /*(playerController.isLookingLeft == playerIsLeft && playerController.isLookingLeft) == false*/)
        {
            if (playerController.isBlocking && (playerIsLeft = !playerController.isLookingLeft))
            {
                playerController.HurtPlayer(attackDamage);
            }
            else
            {
                playerController.HurtPlayer(attackDamage);
            }
        }
    }


    void UnfreezeAtack()
    {
        isAttacking = false;
    }


    void UnFrezzeMovement()
    {
        canMove = true;
        spriteRenderer.color = normalColor;
    }


    bool objectIsLeft(GameObject gameObject)
    {
        Transform objectTransform = gameObject.GetComponent<Transform>();
        if (transform.position.x > gameObject.transform.position.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CheckRotation()
    {
        if (objectIsLeft(playerObject) == true)
        {
            playerIsLeft = true;
        }
        else
        {
            playerIsLeft = false;
        }

        if (playerIsLeft)
        {
            if (alreadyTurnedLeft == false)
            {
                alreadyTurnedRigh = false;
                alreadyTurnedLeft = true;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }

        }
        else if (!playerIsLeft)
        {
            if (alreadyTurnedRigh == false)
            {
                alreadyTurnedRigh = true;
                alreadyTurnedLeft = false;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }

        }
    }

    void DestroyBlackKnight()
    {
        Debug.Log("Destryed a black knight");
        Destroy(gameObject);
    }
}