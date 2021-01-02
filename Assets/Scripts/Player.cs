using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : FiringUnit
{
    public MovementJoystick movementJoystick;
    public MovementJoystick shootJoystick;

    void Update()
    {
        Rotation();
        Attack();
        HandleMovement();
        JoystickMovement();
        
    }

    void FixedUpdate()
    {
        // JoystickMovement();
    }


    private void Attack()
    {
        if (firing && Input.GetMouseButton(0) && canShoot)
        {
            if (Time.time >= nextTimeOfFire)
            {
                currentWeapon.Shoot();
                nextTimeOfFire = Time.time + 1 / currentWeapon.fireRate;
            }
        }
        
    }
    
    private void Rotation()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        if (shootJoystick.vector != Vector2.zero)
        {
            angle = Mathf.Atan2(shootJoystick.vector.y, shootJoystick.vector.x) * Mathf.Rad2Deg + 90;
        }

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void JoystickMovement()
    {
        if (movementJoystick.vector.y != 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(movementJoystick.vector.x * moveSpeed, movementJoystick.vector.y * moveSpeed);
            legAnim.SetBool("Moving", true);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            legAnim.SetBool("Moving", false);
        }
    }

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        Vector3 moveDir = new Vector3(moveX, moveY);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}