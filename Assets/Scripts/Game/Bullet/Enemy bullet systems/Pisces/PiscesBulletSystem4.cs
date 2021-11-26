using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem4 : EnemyShooter<EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    protected override float ShootingCooldown => base.ShootingCooldown / 2;

    readonly int BranchCount = 6;
    readonly float BranchWidth = 0.2f;
    readonly float ScaleFactor = 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            for (int i = 3; i < 8; i++)
            {
                SpawnProjectiles(i);
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return CreateBranch(5, BranchWidth, 1.4f, 30f);
            yield return CreateBranch(9, BranchWidth, 2.2f, 30f);

            for (int i = 12; i < 16; i++)
            {
                SpawnProjectiles(i);
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return CreateBranch(5, BranchWidth, 3.0f, 30f);
            StartCoroutine(CreateBranch(5, BranchWidth * 5, 3.4f, 90f));
            yield return CreateBranch(5, BranchWidth, 3.8f, 30f);

            for (int i = 20; i < 24; i++)
            {
                SpawnProjectiles(i);
                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return CreateBranch(4, BranchWidth, 4.6f, 30f);
            yield return CreateBranch(8, BranchWidth * 4, 5.0f, 120f);

            yield return ownerShip.MoveToRandomPosition(1f, delay: 6f);
        }
    }

    void SpawnProjectiles(int loopCount)
    {
        for (int i = 0; i < BranchCount; i++)
        {
            float z = i * 60f;

            Vector3 spawnPos = new Vector3(BranchWidth, loopCount * ScaleFactor).RotateVectorBy(z);
            Color c = bulletData.gradient.Evaluate(spawnPos.magnitude / 6f);

            var bullet = SpawnProjectile(0, z, spawnPos);
            bullet.SetColour(c);
            bullet.Fire();

            spawnPos = new Vector3(-BranchWidth, loopCount * ScaleFactor).RotateVectorBy(z);

            bullet = SpawnProjectile(0, z, spawnPos);
            bullet.SetColour(c);
            bullet.Fire();
        }
    }

    IEnumerator CreateBranch(int branchLength, float xOffset, float yOffset, float branchAngle)
    {
        for (int i = 1; i < branchLength; i++)
        {
            for (int j = 0; j < BranchCount; j++)
            {
                float xPos = Mathf.Cos(branchAngle * Mathf.Deg2Rad) * i * ScaleFactor + xOffset;
                float yPos = Mathf.Sin(branchAngle * Mathf.Deg2Rad) * i * ScaleFactor + yOffset;
                
                float zRot = 90 - branchAngle;
                float theta = j * 360 / BranchCount;

                Vector3 spawnPos = new Vector3(xPos, yPos).RotateVectorBy(theta);
                Color c = bulletData.gradient.Evaluate(spawnPos.magnitude / 6f);

                var bullet = SpawnProjectile(0, theta - zRot, spawnPos);
                bullet.SetColour(c);
                bullet.Fire();

                spawnPos = new Vector3(-xPos, yPos).RotateVectorBy(theta);

                bullet = SpawnProjectile(0, theta + zRot, spawnPos);
                bullet.SetColour(c);
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}