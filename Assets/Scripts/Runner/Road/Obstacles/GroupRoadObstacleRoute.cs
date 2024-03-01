using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroupRoadObstacleRoute
{
    void SetGroupRoute(List<RoadObstacleRoute> roads, PathMove pathMove)
    {
        Dictionary<float, int> dictionary = new Dictionary<float, int>();
        int side = pathMove.side;
        float z = pathMove.move.z;
        foreach (var road in roads)
        {
            Dictionary<float, int> d = road.SetRoute(side, z);
            side = d.GetEnumerator().Current.Value;
            z = d.GetEnumerator().Current.Key;
            Debug.Log(d.Keys);
            Debug.Log(z);
            dictionary = dictionary.Union(d).ToDictionary(k => k.Key, v => v.Value);
        }

        pathMove.instructions = dictionary.GetEnumerator();
    }
}
