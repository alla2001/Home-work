using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUI : MonoBehaviour
{
    public GameObject keyUIPrefab;
    List<GameObject> temp = new List<GameObject>();
    public static InventoryUI instance;
    public GameObject parent;
    public GameObject btn;
    bool open;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    public void Trigger()
    {
        open = !open;
        parent.SetActive(open);
        btn.SetActive(open);
        if (open)
            LoadUI();
        else
            Clear();
    }
    public void LoadUI()
    {
        foreach (Key key in InventroyManager.instance.keys)
        {
            GameObject Ui =Instantiate(keyUIPrefab, parent.transform);
            Ui.GetComponentInChildren<TextMeshProUGUI>().text = key.level.ToString();
            temp.Add(Ui);
        }
    }
    public void Clear()
    {
        foreach (GameObject ui in temp)
        {
            Destroy(ui);
        }
        temp.Clear();
    }
    public void Sort()
    {
        Clear();
        InventroyManager.instance.Sort();
        LoadUI();
    }
}
