using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ObstacleGroup : MonoBehaviour
{
    public ObstacleSide obstacleSide;
    public List<GameObject> obstacles = new List<GameObject>();

    private float _offset;
    private void Awake()
    {
        obstacles.Clear();
        int count = gameObject.transform.childCount;
        if(count == 0) return;
        for (int i = 0; i < count; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.activeSelf)
            {
                obstacles.Add(child);
            }
        }
        obstacles.Sort(new ObstacleGroupComparer());
    }
    public class ObstacleGroupComparer : IComparer<GameObject>
    {
        public int Compare(GameObject x, GameObject y)
        {
            if (x == null || y == null) return 0;
            float xz = x.transform.position.z;
            float xy = y.transform.position.z;
            return xz.CompareTo(xy);
        }
    }
}
