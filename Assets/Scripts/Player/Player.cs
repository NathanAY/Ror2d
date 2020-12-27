using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, IDamageable
{

    public static Player Instance { get; private set; }
    public MovementJoystick movementJoystick;
    public MovementJoystick shootJoystick;
    public Transform firePoint;
    public Transform aimPoint;
    public int maxHealth = 50;
    public int currentHealth = 50;
    
    public float moveSpeed = 40;
    public float rotationSpeed = 5;
    public bool firing;

    private bool canShoot = true;
    private Rigidbody2D rigidbody2D;
    private Animator legAnim;
    private float nextTimeOfFire = 0;//todo move to manager
    
    public GameObject bulletPrefab;
    public float fireRate = 1;
    public int damage = 20;
    public float range = 100;

    private void Start()
    {
        Instance = this;
        rigidbody2D = GetComponent<Rigidbody2D>();
        legAnim = transform.GetChild(3).GetComponent<Animator>();
    }

    void Update()
    {
        Rotation();
        Attack();
    }

    void FixedUpdate()
    {
        JoystickMovement();
        HandleMovement();
    }

    private void Attack()
    {
        if (firing && Input.GetMouseButton(0) && canShoot)
        {
            if (Time.time >= nextTimeOfFire)
            {
                CreateBullet(firePoint, aimPoint);
                nextTimeOfFire = Time.time + 1 / fireRate;
            }
        }
    }

    
    
    private void CreateBullet(Transform startPosition, Transform targetPosition)
    {
        GameObject bullet = Instantiate(bulletPrefab, startPosition.transform.position, Quaternion.identity);
        bullet.GetComponent<BulletMovement>().SetupDirection(startPosition.transform.position, targetPosition.transform.position, range);
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
            rigidbody2D.velocity = new Vector2(movementJoystick.vector.x * moveSpeed, movementJoystick.vector.y * moveSpeed);
            legAnim.SetBool("Moving", true);
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
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

    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            //todo is dead
        }
    }
}