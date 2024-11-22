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
                HoldItem(null, null);
            }
            
            if(!isHoldingItem)
                return;

            rectTransform.anchoredPosition = Input.mousePosition;
        }

        private void HoldItem(ItemData newHoldItem, ItemSlotUI itemSlotUI)
        {
            itemInHandSlot = itemSlotUI;
            
            item = newHoldItem;
            isHoldingItem = item != null;
            itemImage.gameObject.SetActive(isHoldingItem);
            
            if (isHoldingItem)
                itemImage.sprite = newHoldItem.GetSprite();
        }
        
        private void ReleaseItem()
        {
            if(IsItemNonEmptyBag(item))
                return;
            
            var hoveredItemSlot = GetHoveredItemSlot();
            
            if(hoveredItemSlot == itemInHandSlot)
                return;
            
            if (hoveredItemSlot != null)
            {
                var hoveredItem = hoveredItemSlot.item;
                if(IsItemNonEmptyBag(hoveredItem))
                    return;
                    
                //Set the hovered slots item, and return remaining resource count if there are any
                int remaining = hoveredItemSlot.SetItem(item);

                //Set Old ItemSlot To Empty
                if (hoveredItem == null || (hoveredItem.GetItemType() == ItemType.Resource && remaining > 0))
                {
                    itemInHandSlot.SetItem(null);
                }
                else  
                {
                    if (hoveredItem.itemSO == item.itemSO && hoveredItem.GetItemType() == ItemType.Resource)
                        hoveredItem.GetSpecificData<ItemResourceData>().count = remaining;
                    
                    itemInHandSlot.SetItem(hoveredItem);
                }
            }
        }
        
        
        private bool IsItemNonEmptyBag(ItemData itemData)
        {
            if (itemData == null)
                return false;
            
            if (itemData.GetItemType() == ItemType.Bag)
                if(itemData.GetSpecificData<ItemBagData>().bagInventory.items.Count != 0)
                    return true;

            return false;
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