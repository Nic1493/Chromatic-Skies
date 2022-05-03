using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 2;
    const int BranchSpacing = 360 / BranchCount;
    const int BulletCount = 8;
    const int SpawnBranchCount = 3;
    const int SpawnBranchSpacing = 360 / SpawnBranchCount;
    const float halfPI = 0.5f * Mathf.PI;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        SetSubsystemEnabled(1);

        //lemniscate (infinity curve) (x^2 + y^2)^2 - rx^2 + ry^2 = 0, modified and broken into 3 parts:
        //positive sine wave:  f(x) = sin(x)      {1 < x < t}
        //semicircle:          1 = (x-t)^2 + y^2  {x > t}
        //negative sine wave:  f(x) = -sin(x)     {-t < x < -1}
        //where t = pi / 2
        while (enabled)
        {
            yield return WaitForSeconds(2.5f);

            float step = halfPI / BulletCount;
            float x, y;

            for (int i = 1; i < BulletCount; i++)
            {
                yield return WaitForSeconds(ShootingCooldown);

                x = i * step;
                y = Mathf.Sin(x);
                Vector3 pos = new(x, y);

                SpawnBullets(pos);
            }

            for (int i = 0; i < BulletCount * 2; i++)
            {
                yield return WaitForSeconds(ShootingCooldown);

                Vector3 offset = halfPI * Vector3.right;
                float theta = i * -90f / BulletCount;
                Vector3 pos = -transform.up.RotateVectorBy(theta) + offset;

                SpawnBullets(pos);
            }

            for (int i = BulletCount; i > 0; i--)
            {
                yield return WaitForSeconds(ShootingCooldown);

                x = i * step;
                y = -Mathf.Sin(x);
                Vector3 pos = new(x, y);

                SpawnBullets(pos);
            }

            yield return WaitForSeconds(2.5f);
        }
    }

    void SpawnBullets(Vector3 pos)
    {
        float z = pos.GetRotationDifference(Vector3.zero);

        for (int i = 0; i < BranchCount; i++)
        {
            var bullet = SpawnProjectile(0, z, Vector3.zero);
            bullet.MoveSpeed = pos.sqrMagnitude;
            bullet.Fire();

            z += BranchSpacing;
        }
    }
}