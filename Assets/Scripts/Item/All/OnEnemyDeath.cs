using UnityEngine;

public interface OnEnemyDeathTrigger
{
    void OnEnemyDeathTrigger(GameObject whoKill, Vector3 whereDied);
}