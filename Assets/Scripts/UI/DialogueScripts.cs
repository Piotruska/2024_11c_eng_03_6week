using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DialogueScripts : MonoBehaviour
    {
        private static readonly List<List<string>> Scripts = new List<List<string>>();

        public static List<string> GetScript(int index)
        {
            return Scripts[index];
        }

        private void Awake()
        {
            // Tutorial 0
            List<string> newScript = new List<string>();
            newScript.Add("This is tutorial 1 Line 1");
            newScript.Add("This is tutorial 1 Line 2");
            Scripts.Add(newScript);
        }
    }
}
