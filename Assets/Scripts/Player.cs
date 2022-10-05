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

    [Header("Groundcheck")]
    public Transform A;
    public Transform B;
    [SerializeField] bool isGrounded;

    [Header("Flip")]
    [SerializeField] bool isLookingLeft;

    private Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         horizontal = Input.GetAxisRaw("Horizontal");
         vertical = Input.GetAxisRaw("Jump");

        
        rb2D.velocity = new Vector2(horizontal*movementSpeed, rb2D.velocity.y);
        if(isGrounded == true)
        {
            rb2D.AddForce(new Vector2(0, vertical * jumpForce));
        }

        if(horizontal > 0 && isLookingLeft == true)
        {
            Flip();
        }
        if (horizontal < 0 && isLookingLeft == false)
        {
            Flip();
        }


    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapArea(A.position, B.position);
    }

    void Flip()
    {
        isLookingLeft = !isLookingLeft;
        transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
    }
}
