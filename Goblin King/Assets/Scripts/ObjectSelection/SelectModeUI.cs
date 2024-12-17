using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Button = UnityEngine.UI.Button;

namespace ObjectSelection
{
    public class SelectModeUI : MonoBehaviour
    {
        private Dictionary<SelectMode.SelectModeType, Button> selectionButtons = new();

        public SelectButtonUI[] buttons;
        
        [Serializable]
        public class SelectButtonUI
        {
            public Button button;
            public SelectMode.SelectModeType modeType;
        }
        private void Start()
        {
            foreach (var button in buttons)
            {
                selectionButtons.Add(button.modeType, button.button);

                if (SelectMode.i.currentSelectMode == button.modeType)
                    button.button.interactable = false;
            }
            
            foreach (var selectButtonKey in selectionButtons)
                selectButtonKey.Value.onClick.AddListener(() => OnClick(selectButtonKey.Key, selectButtonKey.Value));
        }

        private void OnClick(SelectMode.SelectModeType modeType, Button button)
        {
            SelectMode.i.UpdateSelectMode(modeType);

            foreach (var selectButtonKey in selectionButtons)
                selectButtonKey.Value.interactable = button != selectButtonKey.Value;
        }
    }
}