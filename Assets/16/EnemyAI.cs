using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EnemyAI : MonoBehaviour
    {
        public float moveSpeed = 1f;
        public float detectionRange = 5f; // The range at which the enemy detects the player.
        public float followDuration = 3f; // The duration the enemy follows the player.
        public LayerMask ground;
        public LayerMask wall;

        private new Rigidbody2D rigidbody;
        private Collider2D triggerCollider;
        private Transform player;
        private bool isFollowingPlayer = false;
        private float followTimer = 0f;

        void Start()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            triggerCollider = GetComponent<Collider2D>();
            player = GameObject.FindGameObjectWithTag("Player")?.transform; // Safely get the player transform
        }
        void Update()
        {
            if (player != null)
            {
                // Check if the player is in range
                if (IsPlayerInRange() && !isFollowingPlayer)
                {
                    isFollowingPlayer = true;
                    followTimer = followDuration;

                  //  Debug.LogWarning("Player in range");

                }

                // Follow the player if following
                if (isFollowingPlayer)
                {
                    FollowPlayer();
                }
                else
                {
                    // Move normally if not following the player
                    Move();
                }
            }
            else
            {
                Debug.LogWarning("Player not found! Ensure there is a GameObject with the 'Player' tag.");
            }
            if (player != null)
            {
                // Check if the player is in range
                if (IsPlayerInRange() && !isFollowingPlayer)
                {
                    isFollowingPlayer = true;
                    followTimer = followDuration;

                    Debug.LogWarning("Player in range");

                }

                // Follow the player if following
                if (isFollowingPlayer)
                {
                    FollowPlayer();
                }
                else
                {
                    // Move normally if not following the player
                    Move();
                }
            }
            else
            {
                Debug.LogWarning("Player not found! Ensure there is a GameObject with the 'Player' tag.");
            }
        }

     /*   void Update()
        {
            if (player != null)
            {
                // Check if the player is in range
                if (IsPlayerInRange() && !isFollowingPlayer)
                {
                    isFollowingPlayer = true;
                    followTimer = followDuration;

                    Debug.LogWarning("Player in range");

                }

                // Follow the player if following
                if (isFollowingPlayer)
                {
                    FollowPlayer();
                }
                else
                {
                    // Move normally if not following the player
                    Move();
                }
            }
            else
            {
                Debug.LogWarning("Player not found! Ensure there is a GameObject with the 'Player' tag.");
            }
        }*/
        void FixedUpdate()
        {
            // Handle wall detection and flipping
            if (!triggerCollider.IsTouchingLayers(ground) || triggerCollider.IsTouchingLayers(wall))
            {
                Flip();
            }
        }

        private void FollowPlayer()
        {
           // Debug.LogWarning("start Follow");

            if (followTimer > 0)
            {
                // Calculate direction to the player and move towards them
                float direction = (player.position.x - transform.position.x > 0) ? 1 : -1;
                rigidbody.velocity = new Vector2(moveSpeed * direction, rigidbody.velocity.y);
                followTimer -= Time.fixedDeltaTime; // Use Time.fixedDeltaTime for physics updates
            }
            else
            {
                isFollowingPlayer = false;
               // Debug.LogWarning("stop Follow");
            }
        }

        private bool IsPlayerInRange()
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            return distanceToPlayer <= detectionRange;
        }

        private void Move()
        {
            rigidbody.velocity = new Vector2(moveSpeed, rigidbody.velocity.y);
        }

        private void Flip()
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y); // Flip the enemy
            moveSpeed *= -1; // Change direction
        }
    }
}



    
