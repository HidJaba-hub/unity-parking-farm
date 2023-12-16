using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Entities
{
    public class Farmer : MonoBehaviour
    {
        public float speed;
        public Animator Animator;
        
        public GameObject entryPath;
        public GameObject guidePath;
        public GameObject exitPath;

        [FormerlySerializedAs("dialogue")] public Dialogue entranceDialogue;
        public Dialogue exitDialogue;
        public Dialogue guideDialogue;
        public Dialogue completeDialogue;

        private void Start()
        {
            EnterScene();
        }

        public void EnterScene()
        {
            Animator.SetBool("Walking", true);
            
            transform.Follow(entryPath, speed,
                () =>
                {
                    Animator.SetBool("Walking", false);
                    entranceDialogue.InvokeDialogue(transform.position);
                });
        }

        public void ProvideGuide()
        {
            Animator.SetBool("Walking", true);
            
            transform.Follow(guidePath, speed,
                () =>
                {
                    Animator.SetBool("Walking", false);
                    guideDialogue.InvokeDialogue(transform.position);
                });
        }

        public void ExitScene()
        {
            Animator.SetBool("Walking", true);
            
            transform.Follow(exitPath, speed,
                () =>
                {
                    Animator.SetBool("Walking", false);
                    exitDialogue.InvokeDialogue(transform.position);
                });
        }

        public void CompleteScene()
        {
            completeDialogue.InvokeDialogue(transform.position);
        }
    }
}