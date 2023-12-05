using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] float damage;
    public float rate;
    [SerializeField] BoxCollider attackArea;
    [SerializeField] TrailRenderer trailEffect;

    public void Use()
    {
        StopCoroutine("Attack");
        StartCoroutine("Attack");
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.1f);
        attackArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.1f);
        trailEffect.enabled = false;

        yield return new WaitForSeconds(0.3f);
        attackArea.enabled = false;

    }


}
