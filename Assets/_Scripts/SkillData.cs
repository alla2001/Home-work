using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Skilldata",menuName ="Skills",order =0)]
public class SkillData : ScriptableObject
{
    public int damageBuff;
    public int movementBuff;
    public int shieldBuff;
    public bool TripleArrow;

}
