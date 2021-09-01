using UnityEngine;

public abstract class EnemyBulletSystem : EnemyShooter
{
    void Start()
    {
        ownerShip.LoseLifeAction += OnLoseLife;
    }

    protected virtual void SpawnBullet(int bulletIndex, float zRotation, Vector2 offset)
    {
        var newBullet = EnemyBulletPool.Instance.Get(bulletIndex);
        newBullet.transform.SetPositionAndRotation(ShipPosition + offset, Quaternion.Euler(0, 0, zRotation));
        newBullet.gameObject.SetActive(true);
        newBullet.enabled = true;
    }

    void OnLoseLife()
    {
        StopCoroutine(shootCoroutine);
        DestroyAllBullets<EnemyBullet>();
    }
}