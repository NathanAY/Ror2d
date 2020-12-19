using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{

    public float speed = 20;
    private float range;
    private Vector3 shootDir;
    private float proccedRange = 0;

    public void SetupDirection(Vector3 startingPoint, Vector3 targetPoint, float range)
    {
        this.range = range;
        transform.position = startingPoint;
        shootDir = (targetPoint - startingPoint).normalized;
        transform.eulerAngles = new Vector3(0, 0, CalculateEulerAngles(shootDir));
    }
    
    void Update()
    {
        transform.position += shootDir * speed * Time.deltaTime;
        proccedRange += speed * Time.deltaTime;
        CheckToDestroy();
    }

    private float CalculateEulerAngles(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    private void CheckToDestroy()
    {
        if (proccedRange > range)
        {
            Destroy(this.gameObject);
        }
    }
}
