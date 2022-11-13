using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    public float speed;
    public float dummyHealth;
    public float frezzeTime;
    public Player playerController;
    public Transform playerTransform;
    SpriteRenderer spriteRenderer;
    public Color freezeColor;
    public Color normalColor;
    public float damage;

    bool canMove;

    void Start()
    {
        canMove = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (dummyHealth <= 0)
        {
            Destroy(gameObject);
        }
        if (canMove == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed);
        }
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

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
        if (collision.gameObject.tag == "Player")
        {
            playerController.HurtPlayer(damage);
        }

    }

    void UnFrezzeMovement()
    {
        spriteRenderer.color = normalColor;
        canMove = true;
    }


}