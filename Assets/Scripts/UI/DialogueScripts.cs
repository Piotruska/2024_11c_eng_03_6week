using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DialogueScripts : MonoBehaviour
    {
        private static readonly List<List<string>> Scripts = new List<List<string>>();
        
        // Buttons
        private string _movementKeys = "Arrow Keys"; 

        public static List<string> GetScript(int index)
        {
            return Scripts[index];
        }

        private void Awake()
        {
            List<string> newScript = new List<string>();
            // Script 0 - Test
            newScript.Clear();
            newScript.Add("This is a Test Script");
            Scripts.Add(newScript);
            // Script 1 - Tutorial 1
            newScript.Clear();
            newScript.Add("Ahoy, Matey! Haven't seen a livin' soul on dis island since forever. Anyway, the names Johnny!");
            newScript.Add("Well since I'm here already I might as well help ye out! Ye might 'ave figured it out already, but ye can use " + _movementKeys + " fer movement.");
            newScript.Add("Try it out 'n meet me a wee further away from here. Thar be much t' learn!");
            Scripts.Add(newScript);
        }
    }
}
