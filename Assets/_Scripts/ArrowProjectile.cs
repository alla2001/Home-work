using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    Vector3 lastPos;
    public int damage;
    public float speed;
    public LayerMask ignoreLayer;
    private void Awake()
    {
        StartCoroutine(WaitDestroy());
        lastPos = transform.position;
    }
    private void FixedUpdate()
    {
        transform.position+=transform.right * speed * Time.deltaTime;
        RaycastHit hit;
        if (Physics.SphereCast(lastPos,0.2f, lastPos - transform.position, out hit, Vector3.Distance(lastPos,transform.position), ~ignoreLayer))
        {
            if (hit.collider.CompareTag("Damagable"))
            {
                hit.collider.GetComponent<EnemyHealthManager>().Damage(PlayerStats.instance.DamageOutPut() );
            }
            StopAllCoroutines();
            Destroy(gameObject);
        }
       
        lastPos=transform.position;
    }
    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
