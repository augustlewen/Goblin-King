using System;
using System.Collections.Generic;
using UI.GoblinPanel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSystems.Items.UI
{
    public class ItemInHand : MonoBehaviour
    {
        private ItemSlotUI itemInHandSlot;
        private ItemSO item;
        private bool isHoldingItem;

        public Image itemImage;
        private RectTransform rectTransform;
        
        public EventSystem eventSystem;  // Link to the EventSystem in the scene
        
        
        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var itemSlot = GetHoveredItemSlot();
                if (itemSlot != null)
                    HoldItem(itemSlot.item, itemSlot);
            }
            else if (Input.GetMouseButtonUp(0) && isHoldingItem)
            {
                ReleaseItem();
            }
            
            if(!isHoldingItem)
                return;

            rectTransform.anchoredPosition = Input.mousePosition;
        }

        private void HoldItem(ItemSO itemSO, ItemSlotUI itemSlotUI)
        {
            itemInHandSlot = itemSlotUI;
            
            item = itemSO;
            isHoldingItem = item != null;
            itemImage.gameObject.SetActive(isHoldingItem);

            if (isHoldingItem)
                itemImage.sprite = itemSO.sprite;
        }
        
        private void ReleaseItem()
        {
            var itemSlot = GetHoveredItemSlot();
            if (itemSlot != null)
            {
                itemInHandSlot.SetItem(itemSlot.item);
                itemSlot.SetItem(item);    
            }
            
            HoldItem(null, null);
            GoblinPanelUI.i.UpdateUI();
        }
        
        
        
        // ReSharper disable Unity.PerformanceAnalysis
        private ItemSlotUI GetHoveredItemSlot()
        {
            // Raycast from the current mouse position
            PointerEventData pointerData = new PointerEventData(eventSystem)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            eventSystem.RaycastAll(pointerData, raycastResults);

            foreach (RaycastResult result in raycastResults)
            {
                var itemSlot = result.gameObject.GetComponent<ItemSlotUI>();
                if (itemSlot != null)
                {
                    return itemSlot;
                }
            }

            return null;
        }
        
    }
}