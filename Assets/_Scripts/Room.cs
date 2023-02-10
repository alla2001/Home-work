using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Room> connectedRooms = new List<Room>();
    public List<Door> doors = new List<Door>();
    public int index = 0;
    public int numberOfConnections;
    public List<EnemySc> enemies;
    public int numberOfEnemies;
    public GameObject enemyPrefab;
    public Transform roomCenter;
    public bool Clear;
    public GameObject key;
    private void Start()
    {

        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i] != null)
            {
                if (connectedRooms[i] == null)
                {
                    Destroy(doors[i].gameObject);
                    continue;
                }
                doors[i].index = i;
                doors[i].level = (Key.keyLevel)i;
            }

        }
        if (enemyPrefab == null) return;
        for (int i = 0; i < numberOfEnemies+Random.Range(0,5); i++)
        {
            GameObject enemy= Instantiate(enemyPrefab, transform);
            enemy.GetComponentInChildren<EnemySc>().room = this;
            enemies.Add(enemy.GetComponentInChildren<EnemySc>());
            enemy.transform.Translate(Random.Range(-15f,15f),0, Random.Range(-10f,10f));
        }
        for (int i = 0; i < doors.Count; i++)
        {
            if (doors[i] == null) continue;    
            GameObject k= Instantiate(key, transform);
            k.transform.Translate(Random.Range(-5f, 5f), 0, Random.Range(-3f, 3f));
            k.GetComponent<KeyInteractable>().level =doors[i].level;
        }
           

    }
    public void EnemyKilled(EnemySc enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count <= 0) Clear = true;
    }
    public void HilightDoor(int index)
    {
        if (index<doors.Count)
        {
            doors[index].Hilight();
        }
    }
   

}
