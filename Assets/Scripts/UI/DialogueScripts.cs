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
            List<string> newScript;
            // Script 0 - Test
            newScript = new List<string>();
            newScript.Add("This is a Test Script");
            Scripts.Add(newScript);
            // Script 1 - Tutorial 1
            newScript = new List<string>();
            newScript.Add("Ahoy, matey! Haven't seen a livin' soul on dis island since forever.");
            newScript.Add("Anyway, the names Johnny! Pleasure t' be doin' business wit' ye!");
            newScript.Add("Well since I'm here already I might as well help ye out! Ye might 'ave figured it out already, but ye can use " + _movementKeys + " fer movement.");
            newScript.Add("Try it out 'n meet me a wee further away from here. Thar be much t' learn!");
            Scripts.Add(newScript);
            // Script 2 - Level End - Enough Diamonds
            newScript = new List<string>();
            newScript.Add("Nice job matey! I'll be takin' those now, but a deal be a deal, I'll take ye t' yer next destination when ye be ready.");
            Scripts.Add(newScript);
            // Script 3 - Level End - Not enough Diamonds
            newScript = new List<string>();
            newScript.Add("Ahoy matey. Aren't ye missin' somethin'? The diamonds mate, ye know, the shiny things. Come back when ye 'ave all three 'n then we'll talk.");
            Scripts.Add(newScript);
            // Script 4 - Level End - Level already cleared
            newScript = new List<string>();
            newScript.Add("Go on then, mate. The next adventure awaits!");
            Scripts.Add(newScript);
        }
    }
}
