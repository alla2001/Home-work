using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Skill :MonoBehaviour
{
    public Skill leftSkill;
    public Skill rightSkill;
    public Skill parent;
    public bool unlocked;
    public int costToUnlock;
    public SkillData skillData;
    public GameObject overlay;

    [SerializeField]private GameObject LeftLine;
    [SerializeField]private GameObject RightLine;
    public virtual void ApplySkill()
    {
        
    }
    public void OnHover()
    {
        overlay.SetActive(true);
    }
    public void OnOut()
    {
        overlay.SetActive(false);
    }
    public void UnlockSkill()
    {

      if(SkillsManager.instance.UnlockSkill(this))
        {
            GetComponent<Button>().enabled= false;
            GetComponent<Image>().color = Color.green;
        }
    }
    public void UnHilight()
    {
        if (LeftLine && rightSkill)
        {
            LeftLine.GetComponent<Image>().color = Color.white;
            RightLine.GetComponent<Image>().color = Color.white;

        }
 
    }
    public void HighlightLine(bool left)
    {
     
        if (left)
        {
            LeftLine.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            RightLine.GetComponent<Image>().color = Color.yellow;
        }
    }
   
}
