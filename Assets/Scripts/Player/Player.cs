using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private new Rigidbody2D rigidbody2D;
    private Animator legAnim;
    private float nextTimeOfFire = 0;//todo move to manager
    
    public GameObject bulletPrefab;
    public static float baseFireRate = 2;
    public float fireRate = baseFireRate;
    public int damage = 5;
    public float range = 300;
    public float bulletSpeed = 40;

    public UI_Inventory uiInventory;
    public Inventory Inventory { get; set; }

    protected List<AttackModifier> attackModifiers = new List<AttackModifier> { new TripleShooting() };


    private void Awake()
    {
        Instance = this;
        Inventory = new Inventory();
        uiInventory.SetPlayer(this);
        uiInventory.SetInventory(Inventory);
    }

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        legAnim = transform.GetChild(3).GetComponent<Animator>();
        EventManager.OnDeath += TriggerOnEnemyDeathEvent;
        Inventory.OnItemListChanged += recalculateStats;
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
        BulletMovement bullet = Instantiate(bulletPrefab, startPosition.transform.position, Quaternion.identity).GetComponent<BulletMovement>();
        foreach (var attackModifier in attackModifiers)
        {
            attackModifier.ApplyModifier(startPosition, targetPosition, bulletPrefab, range, bulletSpeed);
        }
        bullet.SetupDirection(startPosition.transform.position, targetPosition.transform.position, range);
        bullet.Speed = bulletSpeed;
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
        // rigidbody2D.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            //todo is dead
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            Inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
    
    private void recalculateStats(object sender, EventArgs e)
    {
        foreach (Item item in Inventory.GetItemList())
        {
            if (item.itemType.Equals(Item.ItemType.SoldiersSyringe))
            {
                int amount = item.amount;
                fireRate = (float) (baseFireRate + baseFireRate * 0.15 * amount);
            }
        }
    }
    
    private void TriggerOnEnemyDeathEvent(Vector3 deathPosition)
    {
        foreach (Item item in Inventory.GetItemList())
        {
            if (item.itemType.Equals(Item.ItemType.SurpriseMF))
            {
                ItemAssets.Instance.surpriseMf.GetComponent<OnEnemyDeathTrigger>().OnEnemyDeathTrigger(gameObject, deathPosition);
            }
        }
    }
}