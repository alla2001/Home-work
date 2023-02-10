using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSc : Weapon
{


    public Animator animator;
    bool attacking;
    float waitTime = 0.3f;
    public GameObject arrow;

    
    public override void Attack()
    {
        if (attacking) return;
        StartShoot();
        //animator.SetTrigger("Attack");
    }
    public void StartShoot()
    {
        GameObject arw = Instantiate(arrow, transform.position, transform.rotation);
        arw.transform.right = transform.forward;
        if (PlayerStats.instance.buffstats.tripleArrows)
        {
            arw = Instantiate(arrow, transform.position, transform.rotation);
      
            arw.transform.right = transform.forward+transform.right*0.3f;

            arw = Instantiate(arrow, transform.position, transform.rotation);
        
            arw.transform.right = transform.forward - transform.right * 0.3f;
        }
       
        attacking = true;
        FinishedShot();
    }
    public void FinishedShot()
    {
        StartCoroutine(WaitBeforeShoot());
      
    }
    public IEnumerator WaitBeforeShoot()
    {
        yield return new WaitForSeconds(waitTime);
        attacking = false;
    }
  
}
