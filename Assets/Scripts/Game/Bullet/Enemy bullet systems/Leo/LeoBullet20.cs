using System.Collections;
using UnityEngine;

public class LeoBullet20 : EnemyBullet
{
    LeoBulletSystem2 ownerBulletSystem;
    protected override float MaxLifetime => 3f;

    protected override void Awake()
    {
        base.Awake();
        ownerBulletSystem = FindObjectOfType<LeoBulletSystem2>();
    }

    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 0f, 1f);
    }

    public override void Destroy()
    {
        float randOffset = Random.Range(0f, 60f);

        for (int i = 0; i < 3; i++)
        {
            float z = (i * 120f) + randOffset;
            ownerBulletSystem.SpawnProjectile(1, z, transform.position, false).Fire();
        }

        base.Destroy();
    }
}