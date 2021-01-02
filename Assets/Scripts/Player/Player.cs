using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, IDamageable
{

    public static Player Instance { get; private set; }

    public VariableJoystick moveJoystick;
    public VariableJoystick shootJoystick;
    
    private Vector2 moveVelocity;
    
    private new Rigidbody2D rigidbody2D;
    private Animator legAnim;
    public Transform firePoint;
    public Transform aimPoint;

    public int maxHealth = 50;
    public int currentHealth = 50;
    
    public float moveSpeed = 40;
    public float rotationSpeed = 5;

    private bool canShoot = true;
    private float nextTimeOfFire;
    
    public GameObject bulletPrefab;
    public static float baseFireRate = 2;
    public float fireRate = baseFireRate;
    public int damage = 5;
    public float range = 300;
    public float bullerSpeed = 40;

    public UI_Inventory uiInventory;
    public Inventory Inventory { get; set; }


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
        UpdateControl();
    }

    void FixedUpdate()
    {
        JoystickMovement();
        HandleMovement();
    }

    private void UpdateControl()
    {
        if (moveJoystick.Direction != Vector2.zero)
        {
            legAnim.SetBool("Moving", true);
        }
        else
        {
            legAnim.SetBool("Moving", false);
        }
        moveVelocity = moveJoystick.Direction * moveSpeed;
    }

    private void Attack()
    {
        if (canShoot && shootJoystick.Direction != Vector2.zero)
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
        bullet.SetupDirection(startPosition.transform.position, targetPosition.transform.position, range);
        bullet.Speed = bullerSpeed;
        SoundManager.instance.Play("shoot");
    }
    
    private void Rotation()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        if (shootJoystick.Direction != Vector2.zero)
        {
            angle = Mathf.Atan2(shootJoystick.Vertical, shootJoystick.Horizontal) * Mathf.Rad2Deg + 90;
        }
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void JoystickMovement()
    {
        if (moveVelocity != Vector2.zero)
        {
            rigidbody2D.MovePosition(rigidbody2D.position + moveVelocity * Time.fixedDeltaTime);    
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
        rigidbody2D.velocity = new Vector2(moveX, moveY) * moveSpeed;
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