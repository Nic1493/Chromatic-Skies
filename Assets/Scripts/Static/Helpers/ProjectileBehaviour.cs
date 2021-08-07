using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public static class ProjectileBehaviour
{
    #region Movement behaviour

    /// <summary>
    /// lerps <p.MoveSpeed> from <startSpeed> to <endSpeed>, in <lerpTime> seconds.
    /// </summary>
    public static IEnumerator ChangeSpeed(this Projectile p, float startSpeed, float endSpeed, float lerpTime, float delay = 0f)
    {
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentLerpTime = 0f;

        while (currentLerpTime < lerpTime)
        {
            p.MoveSpeed = Mathf.Lerp(startSpeed, endSpeed, currentLerpTime / lerpTime);

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    /// <summary>
    /// adds <rotateAmount> degrees to <p.eulerAngles.z>, over <lerpTime> seconds.
    /// </summary>
    public static IEnumerator RotateBy(this Projectile p, float rotateAmount, float lerpTime, float delay = 0f)
    {
        if (lerpTime <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentLerpTime = 0f;
        Vector3 startDir = p.moveDirection;
        Vector3 endDir = startDir.RotateVectorBy(rotateAmount);

        while (p.moveDirection != endDir)
        {
            p.moveDirection = Vector3.Lerp(startDir, endDir, currentLerpTime / lerpTime);

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    /// <summary>
    /// translates <p> such that it looks like it is orbiting around <target.transform.position>, at <rotateSpeed> degrees per second, for <rotateDuration> seconds.
    /// </summary>
    public static IEnumerator RotateAround(this Projectile p, Actor target, float rotateDuration, float rotateSpeed, float delay = 0f)
    {
        if (target == null || rotateDuration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentTime = 0f;

        while (currentTime < rotateDuration)
        {
            Vector3 targetPos = target.transform.position;
            Vector3 distance = p.transform.position - targetPos;
            Vector3 difference = distance.RotateVectorBy(rotateSpeed * Time.deltaTime);

            p.transform.position = targetPos + difference;
            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    /// <summary>
    /// sets <p.moveDirection> to face towards <target.transform.position>
    /// i.e. sets <p.moveDirection> such that if <target.transform.position> doesn't change,
    /// <p> will eventually collide with <target>.
    /// </summary>
    public static IEnumerator LookAt(this Projectile p, Actor target, float delay = 0f)
    {
        if (target == null) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        p.moveDirection = target.transform.position - p.transform.position;
    }

    /// <summary>
    /// sets <p.moveDirection> to continuously face towards <target.transform.position>, for <homingDuration> seconds.
    /// </summary>
    public static IEnumerator HomeInOn(this Projectile p, Actor target, float homingDuration, float delay = 0f)
    {
        if (target == null || homingDuration <= 0f) yield break;
        if (delay > 0f) yield return WaitForSeconds(delay);

        float currentTime = 0f;
        Vector3 vel = p.moveDirection;

        while (currentTime < homingDuration && target != null)
        {
            Vector3 difference = target.transform.position - p.transform.position;
            p.moveDirection = Vector3.SmoothDamp(p.moveDirection, difference, ref vel, 0.5f);

            currentTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    #endregion

    #region Helpers/Extensions

    /// <summary>
    /// (unused) returns the angle (in degrees) that the line created by <pos1> and <pos2> subtends from (0, 0).
    /// </summary>
    static float GetRotationDifference(Vector2 pos1, Vector2 pos2)
    {
        Vector2 distance = pos2 - pos1;
        return Mathf.Atan2(-distance.x, distance.y) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// rotates <v> anticlockwise by <theta> degrees along the x-y plane.
    /// </summary>
    public static Vector3 RotateVectorBy(this Vector3 v, float theta)
    {
        Vector3 _v = v;
        float theta_r = theta * Mathf.Deg2Rad;

        v.x = (Mathf.Cos(theta_r) * _v.x) - (Mathf.Sin(theta_r) * _v.y);
        v.y = (Mathf.Sin(theta_r) * _v.x) + (Mathf.Cos(theta_r) * _v.y);

        return v;
    }

    /// <summary>
    /// (remove later?) Debug.Log shortcut. 
    /// </summary>
    static void print(object message) { Debug.Log(message); }

    #endregion
}