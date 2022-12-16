using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    [Header("Dummy Stats")]
    public float speed;
    public float dummyHealth;
    public float frezzeTime;
    public float damage;
    [SerializeField] int dropsManyCoins;

    [Header("Dummy other data")]
    public Player playerController;
    public Transform playerTransform;
    SpriteRenderer spriteRenderer;
    public Color freezeColor;
    public Color normalColor;
    public GameController gameController;

    [Header("Dummy debug info")]
    [SerializeField] Transform startTransform;
    [SerializeField] bool canMove;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] RaycastHit2D hit;

    void Start()
    {
        canMove = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        startTransform = this.transform;
    }

    void Update()
    {
        int randomCoin = Random.Range(0, 10);

        if (gameController.isGameOver == false)
        {
            if (dummyHealth <= 0)
            {
                
                if (randomCoin >= 7)
                {
                    gameController.coins = +dropsManyCoins;
                }
                gameController.mobsDeafeted = gameController.mobsDeafeted + 1;
                Destroy(gameObject);
            }
            if (canMove == true && gameController.isPaused == false)
            {
                targetPosition = new Vector3(playerTransform.position.x, startTransform.position.y, 0);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
            }
        }
    }

    void FixedUpdate()
    {
        /*
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(playerTransform.position.x, playerTransform.position.y));
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), new Vector3(playerTransform.position.x, playerTransform.position.y, transform.position.z), Color.red);
        if (hit.collider.gameObject.tag == "Player")
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
        Debug.Log(hit);
        */
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            spriteRenderer.color = freezeColor;
            canMove = false;
            dummyHealth = dummyHealth - playerController.attackDamage;
            Invoke("UnFrezzeMovement", frezzeTime);
        }
    }
    //
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerController.isBlocking == false)
        {
            if (collision.gameObject.tag == "Player")
            {
                playerController.HurtPlayer(damage);

            }
        }
    }

    void UnFrezzeMovement()
    {
        spriteRenderer.color = normalColor;
        canMove = true;
    }
}