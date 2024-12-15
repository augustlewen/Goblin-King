using System;
using GameSystems.Crafting;
using GameSystems.GridObjects;
using UnityEngine;

namespace UI.General
{
    public class WindowManager : MonoBehaviour
    {
        public static WindowManager i;
        
        public Transform goblinPanel;
        public CraftingUI craftingUI;


        private void Awake()
        {
            i = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                goblinPanel.gameObject.SetActive(!goblinPanel.gameObject.activeSelf);
            }
        }

        public void OpenCraftingUI(CraftingStation craftingStation)
        {
            craftingUI.SetupUI(craftingStation);
            craftingUI.gameObject.SetActive(true);
        }
    }
}