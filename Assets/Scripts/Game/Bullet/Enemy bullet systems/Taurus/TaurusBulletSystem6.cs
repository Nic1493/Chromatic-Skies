using System.Collections;

public class TaurusBulletSystem6 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
		while (enabled)
		{
			yield return null;
		}        
    }
}