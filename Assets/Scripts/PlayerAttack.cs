using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    CircleCollider2D attackCollider;

    void Start(){

        attackCollider = GetComponentInChildren<CircleCollider2D>();

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            attackCollider.enabled = true;
        }
    }

    void AttackDeactivactor()
    {
        attackCollider.enabled = false;
    }
}
