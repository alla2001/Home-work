using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSc :Weapon
{
    public List<Collider> hits;
    public BoxCollider box;
    public Animator animator;
    bool attacking;
    public float waitTime=0.3f;
    bool left=false;
    public override void Attack()
    {
        if (attacking) return;
        if (left)
        {
            animator.Play("Swing left");
        }
        else
        {
            animator.Play("Swing");

        }
     
        
        left = !left;
    }
    public void StartSwing()
    {
        attacking=true;
        box.enabled = true;
        StartCoroutine(WaitBeforeSwing());
    }
    public void FinishedSwing()
    {
        //StartCoroutine(WaitBeforeSwing());
        //box.enabled=false;
        //hits.Clear();
    }
    public IEnumerator WaitBeforeSwing()
    {
        yield return new WaitForSeconds(waitTime);
        attacking = false;
        box.enabled = false;
        hits.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damagable")&& !hits.Contains(other))
        {
            other.GetComponent<EnemyHealthManager>().Damage( PlayerStats.instance.DamageOutPut() * 2);
            hits.Add(other);
        }
    }
}
