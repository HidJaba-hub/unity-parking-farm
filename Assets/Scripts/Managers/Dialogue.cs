using System;
using System.Collections;
using System.Collections.Generic;
using Gui;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Managers
{
    [Serializable]
    public class Dialogue
    {
        private bool _isSkipped;
        private bool _isDisplayed;

        [SerializeField] private UnityEvent _completeAction;
        [SerializeField] private List<DialogueItem> _dialogueItems;

        public void InvokeDialogue(Vector3 worldObjectPosition)
        {
            var boxPosition = Camera.main.WorldToScreenPoint(worldObjectPosition);
            GuiHandler.Gui.StartCoroutine(StartDialogue(boxPosition));
        }

        private IEnumerator StartDialogue(Vector3 boxPosition)
        {
            foreach (var item in _dialogueItems)
            {
                _isSkipped = false;
                
                GuiHandler.Gui.dialogueBox.ShowText(boxPosition, item.Message);

                yield return new WaitWhile(() =>
                {
                    if (_isSkipped) return false;
                    
                    _isSkipped = Input.GetKeyDown(KeyCode.Space);
                    return true;
                });
                
                GuiHandler.Gui.dialogueBox.Hide();
            }
            
            _completeAction?.Invoke();
        }
    }
}