using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public class PathFollower
    {
        private readonly GameObject _follower;
        private float _followSpeed;
        
        private Queue<Transform> _pathNodes;
        private const string PathNodeTag = "PathNode";
        private event Action CallbackAction; 

        public PathFollower(GameObject follower, GameObject path)
        {
            this._follower = follower;
            _followSpeed = 1.0f;
            CachePath(path);
        }
        
        public PathFollower(GameObject follower, GameObject path, float speed)
        {
            this._follower = follower;
            _followSpeed = speed;
            CachePath(path);
        }

        private void CachePath(GameObject path)
        {
            _pathNodes = new Queue<Transform>();
            int childCount = path.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Transform child = path.transform.GetChild(i);
                
                if(child.CompareTag(PathNodeTag))
                    _pathNodes.Enqueue(child);
            }
        }

        public void SetCallback(Action callback)
        {
            CallbackAction += () => callback?.Invoke();
        }

        public void Follow()
        {
            if (_pathNodes.Count == 0)
            {
                CallbackAction?.Invoke();
                return;
            }
            
            var node = _pathNodes.Dequeue();
            _follower.transform.DOMove(node.position, ComputeFollowTime(node.position))
                .OnComplete(Follow)
                .SetEase(Ease.Linear);
            _follower.transform.DOLookAt(node.position, 0.15f);
        }

        private float ComputeFollowTime(Vector3 dest)
        {
            var baseSpeed = _followSpeed == 0 ? 1.0f : _followSpeed;
            var dist = Vector3.Distance(_follower.transform.position, dest);

            return dist / baseSpeed;
        }
    }

    public static class FollowerExtension
    {
        public static void Follow(this Transform follower, GameObject path)
        {
            PathFollower pathFollower = new PathFollower(follower.gameObject, path);
            pathFollower.Follow();
        }
        
        public static void Follow(this Transform follower, GameObject path, Action callback)
        {
            PathFollower pathFollower = new PathFollower(follower.gameObject, path);

            pathFollower.SetCallback(callback);
            pathFollower.Follow();
        }
        
        public static void Follow(this Transform follower, GameObject path, float speed, Action callback)
        {
            PathFollower pathFollower = new PathFollower(follower.gameObject, path, speed);

            pathFollower.SetCallback(callback);
            pathFollower.Follow();
        }
    }
}