using System.Collections;
using UnityEngine;

public class VirgoBullet30 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(0f, Random.Range(2.5f, 3f), 1f, 2f);
    }
}