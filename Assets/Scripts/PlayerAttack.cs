using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public CircleCollider2D attackCollider;





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
