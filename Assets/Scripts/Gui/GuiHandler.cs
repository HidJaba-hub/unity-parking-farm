using System;
using Gui.ModelViews;
using UnityEngine;

namespace Gui
{
    public class GuiHandler : MonoBehaviour
    {
        public static GuiHandler Gui;

        public DialogueBoxViewModel dialogueBox;

        private void Awake()
        {
            Gui = this;
        }
    }
}