using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    public float speed = 20;
    private Vector3 shootDir;

    public void SetupDirection(Vector3 startingPoint, Vector3 targetPoint)
    {
        transform.position = startingPoint;
        shootDir = (targetPoint - startingPoint).normalized;
        transform.eulerAngles = new Vector3(0, 0, CalculateEulerAngles(shootDir));
    }
    
    void Update()
    {
        transform.position += shootDir * speed * Time.deltaTime;
    }

    private float CalculateEulerAngles(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
}
