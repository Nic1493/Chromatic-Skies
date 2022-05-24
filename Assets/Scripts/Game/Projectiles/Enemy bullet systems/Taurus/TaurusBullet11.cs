using System;
using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet11 : ScriptableEnemyBullet<TaurusBulletSystem12, Laser>
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(0.5f);
        SpawnLasers();
        yield return WaitForSeconds(5f);
        SpawnLasers();
    }

    void SpawnLasers()
    {
        Vector3 rayOrigin = transform.position;
        float rayDistance = TaurusBulletSystem11.BulletSpacing;
        int layerMask = 1 << LayerMask.NameToLayer("Enemy bullet");

        var hits = Array.FindAll(Physics2D.OverlapCircleAll(rayOrigin, rayDistance, layerMask), i =>
        (i.transform.position.x == transform.position.x ||
        i.transform.position.y == transform.position.y) &&
        i.transform.position != transform.position);

        for (int i = 0; i < hits.Length; i++)
        {
            float z = transform.position.GetRotationDifference(hits[i].transform.position);
            SpawnBullet(0, z, transform.position, false).Fire();
        }
    }
}