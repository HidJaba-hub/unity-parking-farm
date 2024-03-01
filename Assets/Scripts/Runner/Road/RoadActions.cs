using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadActions : RoadGenerator
{
    public List<RoadGenerator> extraGenerators;
    public PathMove pathMover;
    
    private GameObject _currentTriggeredRoad;
    private GameObject _currentTriggeredRouteRoad;
    private GameObject _lastRoad;
    private readonly Dictionary<GameObject, List<RoadObstacleRoute>> _groupOfRoadsRoute = new Dictionary<GameObject, List<RoadObstacleRoute>>();
    
    private new void OnEnable()
    {
        base.OnEnable();
        SetTriggeredRoad();
    }
    public bool RoadTriggered(GameObject road)
    {
        if(_currentTriggeredRoad == null || !road.Equals(_currentTriggeredRoad)) return false;

        ReplaceFirstRoad();
        SetTriggeredRoad();
        
        foreach (var generator in extraGenerators)
        {
            ReplaceFirstRoad(generator);
        }

        return true;
    }

    /*private void SetFirstTriggeredRoad()
    {
        _lastRoad = Groups.Dequeue();
        _currentTriggeredRouteRoad = _lastRoad.transform.GetChild(0).gameObject;
    }*/
    /*public void RoadTriggered(PathMove pathMove, GameObject road)
    {
        if(_currentTriggeredRouteRoad != road) return;
        
        SetGroupRoute(_groupOfRoadsRoute[_lastRoad], pathMove);
        SetFirstTriggeredRoad();
    }*/
    private void SetTriggeredRoad()
    {
        _lastRoad = Groups.Dequeue();
        GameObject group = Groups.Peek();
        _currentTriggeredRoad = group.transform.GetChild(1).gameObject;
    }

    private void ReplaceFirstRoad()
    {
        MoveGroupFragment(_lastRoad, GroupOffset);
        Groups.Enqueue(_lastRoad);
    }
    private void ReplaceFirstRoad(RoadGenerator road)
    {
        GameObject lastRoad = road.RemoveFromGroup();
        road.MoveGroupFragment(lastRoad, GroupOffset);
        road.AddToGroup(lastRoad);
    }

    protected override void RoadObstacleRoute(GameObject road, GameObject group)
    {
        road.TryGetComponent<RoadObstacleRoute>(out RoadObstacleRoute route);
        
        route.SetPathMover(pathMover);
        /*if (!_groupOfRoadsRoute.ContainsKey(group))
        {
            _groupOfRoadsRoute.Add(group, new List<RoadObstacleRoute>());
        }
        _groupOfRoadsRoute[group].Add(route);*/
    }
    
    /*void SetGroupRoute(List<RoadObstacleRoute> roads, PathMove pathMove)
    {
        Dictionary<float, int> dictionary = new Dictionary<float, int>();
        int side = pathMove.side;
        float z = pathMove.gameObject.transform.position.z;
        foreach (var road in roads)
        {
            z = road.gameObject.transform.position.z;
            Dictionary<float, int> d = road.SetRoute(side, z);
            dictionary = dictionary.Union(d).ToDictionary(k => k.Key, v => v.Value);
            side = road.lastSide;
            
        }
        pathMove.instructions = dictionary.GetEnumerator();
    }*/
}
