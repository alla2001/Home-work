using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
public class RoomManager : MonoBehaviour
{
	public GameObject startingRoom;
	public List<GameObject> roomPrefabs = new List<GameObject>();
    public List<Room> rooms = new List<Room> ();
    public int numberOfRooms = 3;
	public static RoomManager instance;
    public float margin=10;
    public int numberOfPasses=2;
   
    private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
		Generate();
	
		Door.Used += (room) => { HilightPath(room); };
    }
    public void Generate()
    {
        for (int i = 0; i < numberOfRooms; i++)
        {
            int r = Random.Range(0, roomPrefabs.Count);
            GameObject _room =Instantiate(roomPrefabs[r], rooms[rooms.Count - 1].transform.position + Vector3.right * margin, Quaternion.identity);
            Room newRoom = _room.GetComponent<Room>();
            rooms.Add(newRoom);
          

        }
		// the first room should only have one connection to make map more intresting
		rooms[0].connectedRooms[0] = rooms[1];
		rooms[1].connectedRooms[2] = rooms[0];
		rooms[1].index++;
		rooms[0].numberOfConnections++;
		rooms[1].numberOfConnections++;
		//number of passes to generate connections, the more passes the more connections, connections are genereted based on chance.
		for (int n = 0; n < numberOfPasses; n++)
        {
            for (int i = 1; i < rooms.Count; i++)
            {
                int chance = Random.Range(0, 3);
				//skip first room
                int targetroom = Random.Range(1, rooms.Count);
                if (targetroom == i) continue;
                if (rooms[i].numberOfConnections < 4 && chance <= 2 && rooms[targetroom].numberOfConnections < 4)
                {


                    int targetDoor = rooms[i].index + 2;
                    if (targetDoor > 3)
                    {
                        targetDoor = targetDoor-4;
                    }
                    if (rooms[targetroom].connectedRooms[targetDoor] == null && rooms[i].connectedRooms[rooms[i].index]==null)
                    {
                        rooms[i].connectedRooms[rooms[i].index] = rooms[targetroom];
                        rooms[targetroom].connectedRooms[targetDoor] = rooms[i];
                        rooms[i].index++;
                        rooms[i].numberOfConnections++;
                        rooms[targetroom].numberOfConnections++;
                    }

                }

            }
        }
       
    }
	public void HilightPath(Room room)
    {
		if (room== rooms[0]) return;
		List<Room> path = GetShortestPath(room, rooms[0]);
		
		
		int i = 0;
        foreach (Room r in room.connectedRooms)
        {
			if(r!=null&& r == path[1])
            {
				
				room.HilightDoor(i);
				return;
            }
			i++;

		}
	}
	
	public  List<Room> GetShortestPath(Room start, Room end)
	{

		// We don't accept null arguments
		if (start == null || end == null)
		{
			return null;
		}

		// The final path
		List<Room> path = new List<Room>();

		// If the start and end are same room, we can return the start node
		if (start == end)
		{
			path.Add(start);
			return path;
		}

		// The list of unvisited rooms
		List<Room> unvisited = new List<Room>();

		// Previous rooms in optimal path from source
		Dictionary<Room, Room> previous = new Dictionary<Room, Room>();

		// The calculated distances, set all to Infinity at start, except the start room
		Dictionary<Room, float> distances = new Dictionary<Room, float>();

		for (int i = 0; i < rooms.Count; i++)
		{
			Room node = rooms[i];
			unvisited.Add(node);

			// Setting the node distance to Infinity
			distances.Add(node, float.MaxValue);
		}

		// Set the starting room distance to zero
		distances[start] = 0f;
		while (unvisited.Count != 0)
		{

			// Ordering the unvisited list by distance, smallest distance at start and largest at end
			unvisited = unvisited.OrderBy(node => distances[node]).ToList();

			// Getting the room with smallest distance
			Room current = unvisited[0];

			// Remove the current room from unvisisted list
			unvisited.Remove(current);

			// When the current node is equal to the end room, then we can break and return the path
			if (current == end)
			{

				// Construct the shortest path
				while (previous.ContainsKey(current))
				{

					// Insert the node onto the final result
					path.Insert(0, current);

					// Traverse from start to end
					current = previous[current];
				}

				// Insert the source onto the final result
				path.Insert(0, current);
				break;
			}

			// Looping through the Node connections (neighbors) and where the connection (neighbor) is available at unvisited list
			for (int i = 0; i < 4; i++)
			{
				Room neighbor = current.connectedRooms[i];
				if (neighbor == null) continue;
				// Getting the distance between the current node and the connection (neighbor)
				float length = 1;

				// The distance from start node to this connection (neighbor) of current node
				float alt = distances[current] + length;

				// A shorter path to the connection (neighbor) has been found
				if (alt < distances[neighbor])
				{
					distances[neighbor] = alt;
					previous[neighbor] = current;
				}
			}
		}
		
		return Bake(path);
	}
	public  List<Room> Bake(List<Room> path)
	{
		List<Room> calculated = new List<Room>();
		float m_Length = 0f;
		for (int i = 0; i < path.Count; i++)
		{
			Room node = path[i];
			for (int j = 0; j < 4; j++)
			{
				Room connection = node.connectedRooms[j];
				if (connection == null) continue;
				// Don't calcualte calculated nodes
				if (path.Contains(connection) && !calculated.Contains(connection))
				{

					// Calculating the distance between a node and connection when they are both available in path nodes list
					m_Length +=1;
				}
			}
			calculated.Add(node);
		}
		return calculated;
	}


}
