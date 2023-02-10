using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteractable : MonoBehaviour
{
    public Key.keyLevel level;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<InventroyManager>().AddKey(new Key { level = level });
            Destroy(gameObject);
        }
    }
}
