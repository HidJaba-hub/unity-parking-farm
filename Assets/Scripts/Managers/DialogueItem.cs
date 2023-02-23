using System;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public struct DialogueItem
    {
        [TextArea]
        public string Message;
    }
}