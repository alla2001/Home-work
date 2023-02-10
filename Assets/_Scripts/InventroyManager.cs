using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class InventroyManager : MonoBehaviour
{
    public List<Key> keys = new List<Key>();
    public static InventroyManager instance;
  
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    public void AddKey(Key newKey)
    {
        keys.Add(newKey);
       
    }
    public bool UseKey(Key.keyLevel lvl)
    {
        Key key = keys.FirstOrDefault((x) => x.level == lvl) ;
        if (key!=null)
        {
            keys.Remove(key);
            return true;
        }
        return false;
    }

    public void Sort()
    {
         List<Key> tmpList = new List<Key>(keys);
        Key tmp = tmpList[0];
        for (int write = 0; write < tmpList.Count; write++)
        {
            for (int sort = 0; sort < tmpList.Count - 1; sort++)
            {
                if (tmpList[sort].level > tmpList[sort + 1].level)
                {
                    tmp = tmpList[sort + 1];
                    tmpList[sort + 1] = tmpList[sort];
                    tmpList[sort] = tmp;
                }
            }
        }

        keys = tmpList;

    }

}
