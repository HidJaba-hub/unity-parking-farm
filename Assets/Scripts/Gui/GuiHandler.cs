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
            Screen.SetResolution(1920, 1080, true);
            Gui = this;
        }
    }
}