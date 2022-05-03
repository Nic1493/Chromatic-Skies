using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem11 : EnemyBulletSubsystem<EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int WaveCount = 8;
    const int BulletCount = 18;
    const int BulletSpacing = 360 / BulletCount;

    Stack<EnemyBullet> bullets = new Stack<EnemyBullet>(WaveCount * BulletCount);

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        float randOffset = Random.Range(0f, BulletSpacing);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int j = 0; j < BulletCount; j++)
            {
                float z = (i * WaveCount) + (j * BulletSpacing) + randOffset;
                bulletData.colour = bulletData.gradient.Evaluate((float)i / WaveCount);

                var bullet = SpawnProjectile(2, z, Vector3.zero);
                bullet.StartCoroutine(bullet.LerpSpeed(WaveCount - i, 0f, 1f));
                bullets.Push(bullet);
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        for (int i = 0; i < WaveCount; i++)
        {
            for (int j = 0; j < BulletCount; j++)
            {
                bullets.Pop().Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}