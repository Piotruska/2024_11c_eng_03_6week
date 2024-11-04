using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class DialogueScripts : MonoBehaviour
    {
        private static readonly List<List<string>> Scripts = new List<List<string>>();
        
        // Buttons
        private string _movementKeys = "Arrow Keys";
        private string _jumpKey = "Z";
        private string _attackKey = "X";
        private string _dashKey = "C";
        private string _item1Key = "A";
        private string _item2Key = "S";

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
            // Script 5 - Tutorial 2
            newScript = new List<string>();
            newScript.Add("Time fer yer next lesson!");
            newScript.Add("See dis flag here? Dat's a checkpoint. Even if some evil fate befalls ye, ye'll always be able t' come back!");
            newScript.Add("Now then, t' get over dis obstacle ye'll needs t' jump like a rabbit! Press the " + _jumpKey + " key 'n get to it. Ye can even jump twice, so use dis t' yer advantage!");
            Scripts.Add(newScript);
            // Script 6 - Tutorial 3
            newScript = new List<string>();
            newScript.Add("See dis bottle? Dat's a health potion. Ye can press " + _item1Key + " to use it 'n trust me ye'll needs it soon!");
            newScript.Add("If ye continue fore ye'll encounter a quite devious creature, but do nah be afeared! Use everythin' ye know and ye should be able t' escape safely!");
            newScript.Add("Oh, nigh-on forgot! Ye can dash by pressin' C, I be sure dat shall come handy in the comin' encounter.");
            Scripts.Add(newScript);
        }
    }
}
