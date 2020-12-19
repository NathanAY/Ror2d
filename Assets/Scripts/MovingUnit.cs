using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUnit : MonoBehaviour
{
    public float moveSpeed = 40;
    public float rotationSpeed = 5;
    public Vector3 moveDestination;

    protected Rigidbody2D rigidbody2D;
    protected Animator legAnim;

    protected bool needToMove => transform.position != moveDestination;

    protected void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        legAnim = transform.GetChild(3).GetComponent<Animator>();
    }

    protected void Update()
    {
        Rotation();
    }

    protected void FixedUpdate()
    {
        Movement();
        HandleMovement();
    }

    protected void Rotation()
    {
        Vector2 dir = moveDestination - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    protected void Movement()
    {
        if (needToMove)
        {
            rigidbody2D.velocity = new Vector2(moveDestination.x * moveSpeed, moveDestination.y * moveSpeed);
            legAnim.SetBool("Moving", true);
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
            legAnim.SetBool("Moving", false);
        }
    }

    protected void HandleMovement()
    {
        transform.position += moveDestination * moveSpeed * Time.deltaTime;
    }
}
