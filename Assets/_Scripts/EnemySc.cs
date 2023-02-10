using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemySc : MonoBehaviour
{
    public NavMeshAgent agent;
    public float attackForce=10f;
    public GameObject target;
    bool attacked;
    public float waitAttack;
    public Room room;   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target=other.gameObject;
            agent.SetDestination(target.transform.position);
        }
    }

    private void FixedUpdate()
    {
        if (target!=null)
        {
            agent.SetDestination(target.transform.position);
            if (Vector3.Distance(transform.position, target.transform.position) < 1.45f &&!attacked)
            {

                PlayerStats.instance.Damage(8);
                target.GetComponent<Rigidbody>().AddForce((target.transform.position-transform.position).normalized * attackForce,ForceMode.Impulse);
                attacked = true;
                StartCoroutine(WaitAttack());
            }
        }
        
    }
    IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(waitAttack);
        attacked = false;
    }


}
