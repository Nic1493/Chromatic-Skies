using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LibraBulletSystem21 : EnemyBulletSubsystem<Laser>
{
    const float MinLaserSpacing = 1f;
    const float MaxLaserSpacing = 3f;
    const float MinLaserRotation = 5f;
    const float MaxLaserRotation = 30f;

    List<Vector2> spawnPositions = new List<Vector2>();

    protected override float ShootingCooldown => 0.04f;

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            yield return WaitForSeconds(2f);

            spawnPositions = GetRandomPointsAlongBounds(new Vector2(-camHalfWidth, camHalfHeight), new Vector2(camHalfWidth, camHalfHeight));

            for (int i = 0; i < spawnPositions.Count; i++)
            {
                float z = Random.Range(MinLaserRotation, MaxLaserRotation) * PositiveOrNegativeOne + 180f;
                SpawnProjectile(0, z, spawnPositions[i], false).Fire(1f);

                yield return WaitForSeconds(ShootingCooldown);
            }

            spawnPositions.Clear();

            yield return WaitForSeconds(2f);
        }
    }
}