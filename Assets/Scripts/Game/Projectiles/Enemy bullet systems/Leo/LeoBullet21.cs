using System.Collections;
using UnityEngine;

public class LeoBullet21 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float randSpeed = Random.Range(2f, 3f);
        yield return this.LerpSpeed(1f, 2.5f, 1f);
    }
}