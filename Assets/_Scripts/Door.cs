using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Door : MonoBehaviour
{
    public Room room;
    public int index;
    public Transform spawPoint;
    bool unlocked;
    public Key.keyLevel level;
    public static UnityAction<Room> Used;
    public static UnityAction unhilight;
    private void OnTriggerEnter(Collider other)
    {
        if (!unlocked &&other.CompareTag("Player"))
        {
           if(InventroyManager.instance.UseKey(level))
            {
                unlocked = true;
               
            }
        }
        if (other.CompareTag("Player") &&room.Clear && unlocked)
        {
           
            int targetDoor = index + 2;
            if (targetDoor > 3)
            {
                targetDoor = targetDoor - 4;
            }
           
            room.connectedRooms[index].doors[targetDoor].SpawnAt();
            room.connectedRooms[index].doors[targetDoor].unlocked = true;
           
            unhilight?.Invoke();
            Used?.Invoke(room.connectedRooms[index]);
           
        }
    }
    private void Awake()
    {
        unhilight +=  UnHilight;
    }
    private void OnDestroy()
    {
        unhilight -= UnHilight;
    }
    public void SpawnAt()
    {
        PlayerStats.instance.transform.position = spawPoint.transform.position; 
    }
    public void Hilight()
    {
     
        gameObject.layer = LayerMask.NameToLayer("Hilight");
    }
    public void UnHilight()
    {
        
        gameObject.layer = LayerMask.NameToLayer("Trigger");
    }
}
