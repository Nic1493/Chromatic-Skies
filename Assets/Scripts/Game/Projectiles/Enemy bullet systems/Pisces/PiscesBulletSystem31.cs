using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem31 : EnemyBulletSubsystem<EnemyBullet>
{
    const int WaveCount = 60;
    const int BulletCount = 3;
    const int BulletSpacing = 360 / BulletCount;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(3f);

        for (int i = 0; i < WaveCount; i++)
        {
            float randAngle = Random.Range(0, BulletSpacing);

            for (int j = 0; j < BulletCount; j++)
            {
                float z = randAngle + (j * BulletSpacing);
                SpawnProjectile(1, z, 0.5f * transform.up.RotateVectorBy(z + 90)).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}