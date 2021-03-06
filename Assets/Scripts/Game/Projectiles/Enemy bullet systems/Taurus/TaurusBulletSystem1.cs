using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int BulletRowCount = 5;
    const int BulletColCount = 5;
    const float BulletSpacing = 0.8f;

    protected override float ShootingCooldown => 2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(2);

        while (enabled)
        {
            SetSubsystemEnabled(1);

            yield return WaitForSeconds(2f);

            for (int i = 0; i < BulletRowCount; i++)
            {
                for (int j = 0; j < BulletColCount; j++)
                {
                    float x = (i - ((BulletRowCount - 1) * 0.5f)) * BulletSpacing;
                    float y = (j - ((BulletColCount - 1) * 0.5f)) * BulletSpacing;
                    Vector3 spawnPos = new Vector3(x, y);

                    float z = spawnPos.GetRotationDifference(Vector3.zero);

                    var bullet = SpawnProjectile(0, z, Vector3.zero);
                    bullet.MoveSpeed = spawnPos.magnitude * 2f;
                    bullet.Fire();
                }
            }

            yield return ownerShip.MoveToRandomPosition(1f, delay: ShootingCooldown);
        }
    }
}