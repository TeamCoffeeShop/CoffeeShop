using System.Collections.Generic;
using System;
using System.Reflection;


    public class DialogueNode
    {
        public int NodeID = -1; // Node order
        public string tempText; // Node txt
        public List<DialogueOption> options = new List<DialogueOption>(); //  Node option

    public MethodInfo mi = null;
    }
