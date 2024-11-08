using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float rotateSpeed = 8f;
    private Rigidbody rb;

    [SerializeField] GunController gunController;
    private bool shootReady = false;
    private bool shotFired = false;
    private bool targetHit = false;
    private int timeUntilNextShot = 0;
    private int minTimeUntilNextShot = 30;

    public float totalRewardPlayer = 0f;
    [SerializeField] PlayerEnvBehavior envBehavior;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Initial spawn
        envBehavior.spawnAgent();
        envBehavior.spawnTarget();
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
        Debug.Log("Total Reward for Player: " + totalRewardPlayer);
    }

    private void HandleMovement()
    {
        // Movement input for horizontal and forward motion
        float moveRotate = Input.GetAxis("Horizontal");
        float moveForward = Input.GetAxis("Vertical");

        rb.MovePosition(transform.position + transform.forward * moveForward * moveSpeed * Time.deltaTime);
        transform.Rotate(0f, moveRotate * rotateSpeed, 0f, Space.Self);
    }

    private void HandleShooting()
    {
        // Check if the Space key is pressed and shot cooldown has elapsed
        if (Input.GetKey(KeyCode.Space) && !shotFired)
        {
            shootReady = true;
        }

        if (shootReady)
        {
            targetHit = gunController.shootGun();
            timeUntilNextShot = minTimeUntilNextShot;
            shotFired = true;

            if (targetHit)
            {
                totalRewardPlayer += 1f;
                envBehavior.spawnTarget();
            }
            else
            {
                totalRewardPlayer -= 0.2f;
            }

            shootReady = false; // Reset shoot readiness after shooting
        }

        // Reduce the cooldown timer after each frame
        if (shotFired)
        {
            timeUntilNextShot--;
            if (timeUntilNextShot <= 0)
            {
                shotFired = false; // Allow shooting again after cooldown
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            totalRewardPlayer -= 0.1f;
            ResetPosition();
            envBehavior.spawnTarget();
        }
        else if (collision.gameObject.CompareTag("Target"))
        {
            totalRewardPlayer -= 0.05f;
            envBehavior.spawnTarget();
        }
    }

    private void ResetPosition()
    {
        transform.localPosition = new Vector3(0f, 10f, 0f);
        // rb.velocity = Vector3.zero;
        // rb.angularVelocity = Vector3.zero;
    }
}
