using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem2 : EnemyBulletSystem
{
    IEnumerator Start()
    {
		while (true)
		{
			yield return null;
		}        
    }
}