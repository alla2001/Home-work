using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{

    public Skill rootSkill;
    //all skills in the game
    public List<Skill> SkillList;
    //skills the player unlocked
    public List<Skill> unlockedSkills;   
    public static SkillsManager instance;
    private void Awake()
    {
        if(instance==null) instance = this;
        else Destroy(this);
    }
    int pointsneeded;
   //unlocks skill
    public bool UnlockSkill(Skill _skill)
    {
    
        if(_skill.parent==null )
        {
            if (!unlockedSkills.Contains(_skill))
            {
                PlayerStats.instance.AddPoints( -_skill.costToUnlock);
                _skill.unlocked = true;
                unlockedSkills.Add(_skill);
                ApplyBuff();
                HilightSkillPath();
                return true;
            }
            return false;
        }
        if (_skill.parent.unlocked && _skill.costToUnlock<=PlayerStats.instance.points)
        {
            PlayerStats.instance.AddPoints(-_skill.costToUnlock);
            _skill.unlocked = true;
            unlockedSkills.Add(_skill);
            ApplyBuff();
            HilightSkillPath();
            return true;

        }
        return false;
    }
    //tmp list 
    List<Skill> skills = new List<Skill>();
    //unlocks all skills needed before that selected skill and the skill it self if possible .
    public bool UnLockRecusvly(Skill _skill)
    {
        pointsneeded += _skill.costToUnlock;
        Skill tmpskill = _skill.parent;

        if (tmpskill == null || tmpskill.unlocked)
        {
            if (pointsneeded <= PlayerStats.instance.points)
            {
                PlayerStats.instance.AddPoints(-pointsneeded);
                pointsneeded = 0;
                foreach (Skill sk in skills)
                {
                    sk.unlocked = true; 
                    unlockedSkills.Add(sk);
                }
                _skill.unlocked = true;
                unlockedSkills.Add(_skill);
                ApplyBuff();
                return true;
            }
        }
        else
        if (!tmpskill.unlocked)
        {
            skills.Add(tmpskill);
            pointsneeded += _skill.costToUnlock;
            UnlockSkill(tmpskill);

        }

        return false;
    }


    public void ApplyBuff()
    {
        PlayerStats.instance.buffstats.ReSet();
        length = 0;
        maxlength = -1;
        visitedSkills.Clear();
        DFSGoFarthest(rootSkill);
        List<Skill> sks= new List<Skill>();
        GoUp(furthersSkill, sks);
        sks.Add(rootSkill);
        sks.Add(furthersSkill);
        foreach (var sk in sks)
        {
            PlayerStats.instance.buffstats.damageBuff += sk.skillData.damageBuff;
            PlayerStats.instance.buffstats.shieldBuff += sk.skillData.shieldBuff;
            PlayerStats.instance.buffstats.movementBuff += sk.skillData.movementBuff;
            if (sk.skillData.TripleArrow)
                PlayerStats.instance.buffstats.tripleArrows = true;
        }
       
        
    }
    //temp variables for finding longs branch
    int length = 0;
    int maxlength=-1;
    Skill furthersSkill;
    List<Skill> visitedSkills = new List<Skill>();
    //depth first search for finding furthers node unlocked
    void DFSGoFarthest(Skill skl)
    {

        visitedSkills.Add(skl) ;
        if (skl.leftSkill&&skl.leftSkill.unlocked)
        {
            if (!visitedSkills.Contains(skl.leftSkill))
            {
                length++;
                DFSGoFarthest(skl.leftSkill);
                length--;
            }
        }
        if (skl.rightSkill && skl.rightSkill.unlocked)
        {
            if (!visitedSkills.Contains(skl.rightSkill))
            {
                length++;
                DFSGoFarthest(skl.rightSkill);
                length--;
            }
        }
        if (length > maxlength)
        {
            maxlength = length;
            furthersSkill = skl;
        }
    }
    //hilights the edges for the longest path but going in reverse from furthers node unlocked 
    public void HilightSkillPath()
    {
        foreach (Skill skl in SkillList)
        {
            skl.UnHilight();
        }
        length = 0;
        maxlength= -1;
        visitedSkills.Clear();
        DFSGoFarthest(rootSkill);
      
        GoUp(furthersSkill,null);
    }
    public void GoUp(Skill skl,List<Skill> skills)
    {
        if (skl.parent == null) return;
        Skill tmpskill = skl.parent;
       
        if (tmpskill != null)
        {
            if (tmpskill.leftSkill == skl)
            {
                tmpskill.HighlightLine(true);
                if(skills!=null)
                skills.Add(tmpskill);
            }
            else
            {
                tmpskill.HighlightLine(false);
                if (skills != null)
                    skills.Add(tmpskill);
            }
            GoUp(tmpskill,skills);
        }
    }



}
