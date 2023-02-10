using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillShop : MonoBehaviour
{
    bool canBuy;
    public GameObject SkillsUI;

    public static SkillShop instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canBuy = true;
            SkillsUI.SetActive(true);
            SkillsManager.instance.HilightSkillPath();

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canBuy = false;
            SkillsUI.SetActive(false);
         
        }
    }



}
