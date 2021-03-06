using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem32 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 24;
    const float BulletSpacing = 360f / BulletCount;
    protected override float ShootingCooldown => 1.5f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = i * BulletSpacing;
                SpawnProjectile(1, z, Vector3.zero).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}