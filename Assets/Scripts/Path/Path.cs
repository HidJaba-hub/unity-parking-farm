using UnityEngine;

namespace Entities
{
    public class Path : MonoBehaviour
    {
        
        public Color color = Color.green;
        public float radius = 0.5f;
        
        private void OnDrawGizmos()
        {
            var isFirst = true;
            Transform prevNode = null; 
            
            foreach (var node in gameObject.GetComponentsInChildren<Transform>())
            {
                if (isFirst)
                {
                    isFirst = false;
                    continue;
                }
                
                Gizmos.color = color;
                
                Gizmos.DrawSphere(node.position, radius);

                if (prevNode == null)
                {
                    prevNode = node;
                    continue;
                }
                Gizmos.DrawLine(prevNode.position, node.position);
                
                prevNode = node;
            }
        }
    }
}