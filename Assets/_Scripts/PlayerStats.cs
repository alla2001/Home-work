using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    public int points { get; private set; }
    
    public int hp;
    public static PlayerStats instance;
    public BuffStats buffstats;
    public TextMeshProUGUI scoreText;
    public float damageFXTime;
    public Image bar;
    int maxHP;
    private void Start()
    {
        maxHP = hp;
        Door.Used += (r) => {
            if 
            (r == RoomManager.instance.startingRoom)
            {

                hp = maxHP;
                bar.fillAmount = (float)hp / maxHP;
            }
            };

    }
    [System.Serializable]
    public class BuffStats
    {
        public int damageBuff;
        public int movementBuff;
        public int shieldBuff;
        public bool tripleArrows;
        public void ReSet()
        {
            damageBuff = 0;

            movementBuff = 0;
            shieldBuff = 0;

        }
    }

    public int DamageOutPut()
    {
      
        return buffstats.damageBuff;
    }
    public void AddPoints(int value)
    {
        points += value;
        if(points<0)points=0;
        scoreText.text = "Points :" + points.ToString();
    }
    private void Awake()
    {

        if (instance == null) instance = this;
        else Destroy(this);
        AddPoints(0);
    }
    public IEnumerator DamageEffect()
    {
        gameObject.layer = LayerMask.NameToLayer("Damage");
        yield return new WaitForSeconds(damageFXTime);
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
    public void Damage(int value)
    {
        StopAllCoroutines();
   
      
        int reduction = (value * (100 - buffstats.shieldBuff)) / 100;
        hp-=reduction;
        StartCoroutine(DamageEffect());
        bar.fillAmount = (float)hp / maxHP;
        if (hp<=0)
        {
            Dead();
        }
    }
    public void Dead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //reload Scene
    }
    public void Heal(int value)
    {

    }
}
