using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems.Items.UI
{
    public class ItemInHand : MonoBehaviour
    {
        private static ItemInHand i;
        private ItemSO item;
        private bool isHoldingItem;

        private SpriteRenderer spriteRenderer;
        private RectTransform rectTransform;
        
        private void Awake()
        {
            i = this;
            spriteRenderer = GetComponent<SpriteRenderer>();
            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
            }
            
            if(!isHoldingItem)
                return;

            rectTransform.anchoredPosition = Input.mousePosition;
        }

        public static void HoldItem(ItemSO itemSO)
        {
            i.item = itemSO;
            i.isHoldingItem = i.item != null;
            i.spriteRenderer.gameObject.SetActive(i.isHoldingItem);

            if (i.isHoldingItem)
            {
                i.spriteRenderer.sprite = itemSO.sprite;
            }
        }
        
        public static void ReleaseItem()
        {
            HoldItem(null);
        }
        
    }
}