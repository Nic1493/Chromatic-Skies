using System.Collections;
using UnityEngine;

public class AriesBullet2 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        //yield return StartCoroutine(this.LerpSpeed(3f, 0f, 0.5f));
        yield return StartCoroutine(this.LerpSpeed(0f, 2f, 2f));
    }
}