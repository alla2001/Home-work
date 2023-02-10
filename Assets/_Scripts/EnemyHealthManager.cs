using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthManager : MonoBehaviour
{
    public int hp;
    public EnemySc enemy;

    public float damageFXTime;
    public Image bar;
    public GameObject HealthBar;
    int maxHP;
    private void Start()
    {
        maxHP = hp;
    }
    private void Update()
    {
        HealthBar.transform.forward= CameraSingelton.instance.transform.position- HealthBar.transform.position;
    }
    public void Damage(int value)
    {
        StopAllCoroutines();
        hp -= value;
        enemy.target = PlayerStats.instance.gameObject;
        StartCoroutine(DamageEffect());
        bar.fillAmount=(float) hp/maxHP;
        if (hp <= 0)
        {
            Dead();
        }
            
    }
    public void Dead()
    {
        enemy.room.EnemyKilled(enemy);
        PlayerStats.instance.AddPoints(10);
        Destroy(gameObject);
    }
    public IEnumerator DamageEffect()
    {
        gameObject.layer = LayerMask.NameToLayer("Damage");
        yield return new WaitForSeconds(damageFXTime);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
}
