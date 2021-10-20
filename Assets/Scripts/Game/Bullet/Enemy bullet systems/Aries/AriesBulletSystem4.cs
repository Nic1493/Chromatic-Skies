using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBulletSystem4 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            yield return WaitForSeconds(2f);

            float n = 0f;
            float r = Random.value - 0.5f;
            float t = Random.Range(0f, 90f);

            for (int i = 0; i < 360; i += 4)
            {
                for (int j = 0; j < 5; j++)
                {
                    float z = j * 72f + n + t;
                    SpawnProjectile(0, z, Vector2.zero).Fire();
                }

                n += i * Mathf.Sign(r);
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return ownerShip.MoveToRandomPosition(1f, 1f);
        }
    }
}