﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_AI : MonoBehaviour {

    public float enemySpeed = 1.7f;
    public int xMoveDirection;
    public int xMove;
    public float thingy = .106f;
    public Player_Health _player_health;

	// Update is called once per frame
	void Update () {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMoveDirection, 0f));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * enemySpeed;
        if(hit != null && hit.collider != null && hit.distance < thingy)
        {

            if(hit.collider.tag == "Player")
            {
                Destroy(hit.collider.gameObject);
                _player_health.Die();
            }
        }

        RaycastHit2D anotherHit = Physics2D.Raycast(transform.position, new Vector2(xMove, .9f));
        if (anotherHit != null && anotherHit.collider != null && anotherHit.distance < thingy)
        {
            if (anotherHit != null && anotherHit.collider != null && anotherHit.collider.tag == "Player")
            {
                Destroy(anotherHit.collider.gameObject);
                _player_health.Die();
            }
        }
    }

    void FlipEnemy()
    {
        if(xMoveDirection > 0)
        {
            xMoveDirection = -1;
        }

        else
        {
            xMoveDirection = 1;
        }
    }
}