using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 200;
    float WaveSpacing = 0f;
    const int BranchCount = 5;
    const float BranchSpacing = 360f / BranchCount;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            yield return WaitForSeconds(1f);

            WaveSpacing = 0f;
            float r = Random.Range(0f, BranchSpacing);

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = ii * BranchSpacing + WaveSpacing + r;
                    SpawnProjectile(0, z, Vector2.zero).Fire();
                }

                WaveSpacing += i;
                yield return WaitForSeconds(ShootingCooldown);
            }
            yield return ownerShip.MoveToRandomPosition(1f, minSqrMagDelta: 1f, maxSqrMagDelta: 3f, delay: 1f);
        }
    }
}