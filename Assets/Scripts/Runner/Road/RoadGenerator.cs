using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Random = System.Random;

public class RoadGenerator : MonoBehaviour
{
    public List<GameObject> roadPrefabs;
    public int maxDistanceGenerated;

    private float _roadOffsetTotal;
    protected float GroupOffset = 0;
    protected readonly Queue<GameObject> Groups = new Queue<GameObject>();
    

    protected void OnEnable()
    {
        if(gameObject.transform.childCount > 0) return;

        for (int i = 0; i < maxDistanceGenerated; i++)
        {
            GameObject groupOfRoads = CreateGroup(i);
            MoveGroupFragment(groupOfRoads, GroupOffset);
            Groups.Enqueue(groupOfRoads);
        }
    }

    public GameObject RemoveFromGroup()
    {
        return Groups.Dequeue();
    }
    public void AddToGroup(GameObject group)
    {
        Groups.Enqueue(group);
    }
    private GameObject CreateGroup(int i)
    {
        GameObject groupOfRoads = new GameObject(i.ToString());
        groupOfRoads.transform.SetParent(gameObject.transform);
        
        float roadOffset = 0;

        roadPrefabs = Shuffle(roadPrefabs);
        foreach (var roadFragment in roadPrefabs)
        {
            GameObject road = Instantiate(roadFragment, new Vector3(0, 0, roadOffset), Quaternion.identity);
            roadOffset += roadFragment.transform.localScale.z;
            road.transform.SetParent(groupOfRoads.transform);
            RoadObstacleRoute(road, groupOfRoads);
        }
        GroupOffset = roadOffset;

        return groupOfRoads;
    }
    protected virtual void RoadObstacleRoute(GameObject road, GameObject group){}
    public void MoveGroupFragment(GameObject groupOfRoads, float offset)
    {
        var position = groupOfRoads.transform.position;
        position = new Vector3(gameObject.transform.position.x, position.y, _roadOffsetTotal);

        groupOfRoads.transform.localEulerAngles = Vector3.zero;
        
        _roadOffsetTotal += offset;
        groupOfRoads.transform.position = position;
    }
    private List<GameObject> Shuffle(List<GameObject> list)  
    {   
        Random rng = new Random();
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            GameObject value = list[k];  
            list[k] = list[n];  
            list[n] = value;  
        }

        return list;
    }
}
