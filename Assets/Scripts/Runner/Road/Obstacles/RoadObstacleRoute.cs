using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Runner.Moving;
using TMPro;
using UnityEngine;

public class RoadObstacleRoute : MonoBehaviour
{
    public GameObject target;
    
    private PathMove _henMove;
    private float _offset = 0f;
    private readonly Dictionary<ObstacleSide, ObstacleGroup> _obstacleGroups = new Dictionary<ObstacleSide, ObstacleGroup>();

    private void Awake()
    {
        SetGroupObstacles(gameObject);
    }

    public void SetPathMover(PathMove pathMove)
    {
        _henMove = pathMove;
        target = _henMove.gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(target))
        {
            Dictionary<float, int> d = SetRoute(_henMove.side, _henMove.gameObject.transform.position.z);
            _henMove.instructions = d.GetEnumerator();
        }
    }

    private Dictionary<float, int> FillInstructions(Dictionary<float, int> instructions, ObstacleGroup obstacleGroup, float currentPos)
    {
        foreach (var obstacle in obstacleGroup.obstacles)
        {
            if (currentPos < obstacle.transform.position.z)
            {
                currentPos = obstacle.transform.position.z - _offset;
                int side = ChooseSide(obstacleGroup, currentPos);
                instructions.TryAdd(currentPos, side);
                FillInstructions(instructions, _obstacleGroups[(ObstacleSide) side], currentPos);
            }
        }
        return instructions;
    }
    private int ChooseSide(ObstacleGroup obstacleGroup, float currentPoint)
    {
        if (obstacleGroup.obstacleSide == ObstacleSide.Left)
        {
            return (int) ObstacleSide.Center;
        }
        if (obstacleGroup.obstacleSide == ObstacleSide.Right)
        {
            return (int) ObstacleSide.Center;
        }
        
        float distance = 0;
        foreach (var obstacle in _obstacleGroups[ObstacleSide.Left].obstacles)
        {
            if (currentPoint > obstacle.transform.position.z - _offset) continue;
            distance = Math.Abs(obstacle.transform.position.z - currentPoint);
        }
        if (distance == 0) return (int) ObstacleSide.Left;
        foreach (var obstacle in _obstacleGroups[ObstacleSide.Right].obstacles)
        {
            if (currentPoint > obstacle.transform.position.z - _offset) continue;
            if(distance < Math.Abs(obstacle.transform.position.z - currentPoint))
            {
                distance = Math.Abs(obstacle.transform.position.z - currentPoint);
            }
        }
        return (int) ObstacleSide.Right;
    }
    public Dictionary<float, int> SetRoute(int side, float currentPos)
    {
        Dictionary<float, int> instructions = new Dictionary<float, int>();
        ObstacleGroup currentGroup = _obstacleGroups[(ObstacleSide) side];
        return FillInstructions(instructions, currentGroup, currentPos);
    }
    private void SetGroupObstacles(GameObject road)
    {
        int count = road.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject child = road.transform.GetChild(i).gameObject;
            if (child.TryGetComponent<ObstacleGroup>(out ObstacleGroup obstacleGroup))
            {
                _obstacleGroups.Add(obstacleGroup.obstacleSide, obstacleGroup);
            }
        }
    }
}
