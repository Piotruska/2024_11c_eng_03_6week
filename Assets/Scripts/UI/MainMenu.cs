using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private Image _selectionPanel1;
        private Image _selectionPanel2;
        private Image _selectionPanel3;
        private Image _selectionPanel4;
        // Inputs
        private float _yAxisInput;
        private bool _yInput;
        private bool _confirmInput;

        private int _currentSelection;
        private void Awake()
        {
            _selectionPanel1 = GameObject.Find("SelectionPanel1").GetComponent<Image>();
            _selectionPanel2 = GameObject.Find("SelectionPanel2").GetComponent<Image>();
            _selectionPanel3 = GameObject.Find("SelectionPanel3").GetComponent<Image>();
            _selectionPanel4 = GameObject.Find("SelectionPanel4").GetComponent<Image>();
        
            SetVisible(_selectionPanel1, true);
            SetVisible(_selectionPanel2, false);
            SetVisible(_selectionPanel3, false);
            SetVisible(_selectionPanel4, false);

            _currentSelection = 0;
        }

        private void Update()
        {
            _yAxisInput = Input.GetAxis("Vertical");
            _yInput = Input.GetButtonDown("Vertical");
            _confirmInput = Input.GetButtonDown("Confirm");

            if (_yInput && _yAxisInput > 0)
            {
                if (_currentSelection == 0)
                {
                    _currentSelection = 3;
                }
                else
                {
                    _currentSelection--;
                }
            }
        
            if (_yInput && _yAxisInput < 0)
            {
                if (_currentSelection == 3)
                {
                    _currentSelection = 0;
                }
                else
                {
                    _currentSelection++;
                }
            }

            switch (_currentSelection)
            {
                case 0:
                    SetVisible(_selectionPanel1, true);
                    SetVisible(_selectionPanel2, false);
                    SetVisible(_selectionPanel3, false);
                    SetVisible(_selectionPanel4, false);
                    if (_confirmInput)
                    {
                        SceneManager.LoadScene("Tutorial");
                        //Debug.Log("Starting Game");
                    }
                    break;
                case 1:
                    SetVisible(_selectionPanel1, false);
                    SetVisible(_selectionPanel2, true);
                    SetVisible(_selectionPanel3, false);
                    SetVisible(_selectionPanel4, false);
                    break;
                case 2:
                    SetVisible(_selectionPanel1, false);
                    SetVisible(_selectionPanel2, false);
                    SetVisible(_selectionPanel3, true);
                    SetVisible(_selectionPanel4, false);
                    break;
                case 3:
                    SetVisible(_selectionPanel1, false);
                    SetVisible(_selectionPanel2, false);
                    SetVisible(_selectionPanel3, false);
                    SetVisible(_selectionPanel4, true);
                    break;
            } 
        }

        private void SetVisible(Image image, bool visible)
        {
            if (visible)
            {
                var tempColor = image.color;
                tempColor.a = 100;
                image.color = tempColor;
            }
            else
            {
                var tempColor = image.color;
                tempColor.a = 0;
                image.color = tempColor;
            }
        }
    }
}