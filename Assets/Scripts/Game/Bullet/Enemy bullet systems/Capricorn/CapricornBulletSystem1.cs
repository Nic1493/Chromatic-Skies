using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornBulletSystem1 : EnemyShooter<EnemyBullet>
{
    protected override IEnumerator Shoot()
    {
		while (true)
		{
			yield return null;
		}        
    }
}