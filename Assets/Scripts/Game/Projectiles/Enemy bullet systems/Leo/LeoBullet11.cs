using System.Collections;

public class LeoBullet11 : EnemyBullet
{
    protected override float MaxLifetime => 15f;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.RotateBy(60f, 3f, delay: 0.5f));
        StartCoroutine(this.LerpSpeed(1f, 4f, 1f));
        yield return this.LerpSpeed(4f, 1f, 2f);
    }
}