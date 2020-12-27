using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAi : MonoBehaviour
{

    private enum State {
        Roaming,
        ChaseTarget,
        ShootingTarget,
        GoingBackToStart,
    }


    // private EnemyPathfindingMovement pathfindingMovement;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private float nextShootTime;
    private State state;
    
    public GameObject target;
    private Rigidbody2D rb;
    private NavMeshAgent agent;
    private bool stop;
    private float viewRange = 200f;
    private float attackRange = 100f;
    private Enemy enemy;
    
    
    private void Awake()
    {
        // pathfindingMovement = GetComponent<EnemyPathfindingMovement>();
        state = State.Roaming;
        enemy = GetComponent<Enemy>();
    }

    void Start()
    {
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
        
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = enemy.moveSpeed;
        agent.angularSpeed = 1200;
        agent.acceleration = 100;
    }


    void Update()
    {
        switch (state) {
            default:
            case State.Roaming:
                MoveToPosition(roamPosition);
                float reachedPositionDistance = 5f;
                if (Vector3.Distance(transform.position, roamPosition) < reachedPositionDistance)
                {
                    roamPosition = GetRoamingPosition();
                }
                FindTarget();
                break;
            case State.ChaseTarget:
                MoveToPosition(Player.Instance.transform.position);
                if (Vector3.Distance(transform.position, Player.Instance.transform.position) < viewRange)
                {
                    if (Time.time > nextShootTime)
                    {
                        // pathfindingMovement.StopMoving();
                        // state = State.ShootingTarget;
                        // aimShootAnims.ShootTarget(Player.Instance.transform.position, () => {
                        // state = State.ChaseTarget;
                        // });
                        if (Vector3.Distance(transform.position, Player.Instance.transform.position) < attackRange)
                        {
                            CreateBullet(enemy.firePoint, Player.Instance.transform);
                        }
                        float fireRate = .15f;
                        nextShootTime = Time.time + fireRate;
                    }
                }
                if (Vector3.Distance(transform.position, Player.Instance.transform.position) > viewRange)
                {
                    state = State.GoingBackToStart;
                }
                break;
            case State.GoingBackToStart:
                MoveToPosition(startingPosition);
                FindTarget();
                reachedPositionDistance = 10f;
                if (Vector3.Distance(transform.position, startingPosition) < reachedPositionDistance)
                {
                    state = State.Roaming;
                }
                break;
        }
    }

    private void CreateBullet(Transform startPosition, Transform targetPosition)
    {
        GameObject bullet = Instantiate(enemy.bulletPrefab, startPosition.transform.position, Quaternion.identity);
        bullet.GetComponent<BulletMovement>().SetupDirection(startPosition.transform.position, targetPosition.transform.position, enemy.range);
    }
    
    private void MoveToPosition(Vector3 position)
    {
        agent.SetDestination(position);
        Vector3 direction = position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle + 90;
        Debug.DrawLine(transform.position, position);
    }
    
    private Vector3 GetRoamingPosition()
    {
        Vector3 randomDir = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        return startingPosition + randomDir * Random.Range(10f, 70f);
    }
    
    private void FindTarget() {
        if (Vector3.Distance(transform.position, Player.Instance.transform.position) < viewRange) {
            // Player within target range
            state = State.ChaseTarget;
        }
    }
}
