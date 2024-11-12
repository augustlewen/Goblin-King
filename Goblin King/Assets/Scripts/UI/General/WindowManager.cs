using System;
using UnityEngine;

namespace UI.General
{
    public class WindowManager : MonoBehaviour
    {
        public Transform goblinPanel;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                goblinPanel.gameObject.SetActive(!goblinPanel.gameObject.activeSelf);
            }
        }
    }
}