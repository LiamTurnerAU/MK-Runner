﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Controls the player
    public float playerSpeed, playerJump;
    public LayerMask groundLayer;

    public bool canJump = true;
    private Rigidbody2D playerRB;
    private Collider2D playerCollider;

    

    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Touch Controls
         
        /* if (Input.touchCount > 0)
         {
             Jump();


         }*/

        //Can jump if touching a ground object
        canJump = Physics2D.IsTouchingLayers(playerCollider, groundLayer);
        

        if (Input.GetMouseButtonDown(0) && canJump )
        {
            Jump();

        }
    }

    private void Die()
    {
        GameEvents.InvokePlayerDeath();
    }

    private void Jump()
    {
        playerRB.velocity += new Vector2(0, playerJump);
    }
}