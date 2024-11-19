using System;
using System.Collections.Generic;
using GameSystems.Items.SO;
using UI.GoblinPanel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameSystems.Items.UI
{
    public class ItemInHand : MonoBehaviour
    {
        private ItemSlotUI itemInHandSlot;
        private ItemData item;
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
                if (itemSlot != null && itemSlot.item != null)
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

        private void HoldItem(ItemData item, ItemSlotUI itemSlotUI)
        {
            itemInHandSlot = itemSlotUI;
            
            this.item = item;
            isHoldingItem = this.item != null;
            itemImage.gameObject.SetActive(isHoldingItem);
            
            if (isHoldingItem)
                itemImage.sprite = item.GetSprite();
        }
        
        private void ReleaseItem()
        {
            if (item.GetItemType() == ItemType.Bag)
            {
            }
            
            var hoveredItemSlot = GetHoveredItemSlot();
            if (hoveredItemSlot != null)
            {
                if (hoveredItemSlot.item != null)
                {
                    itemInHandSlot.SetItem(hoveredItemSlot.item);
                }
                else
                {
                    itemInHandSlot.SetItem(null);
                }
                
                hoveredItemSlot.SetItem(item);    
            }
            
            HoldItem(null, null);
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