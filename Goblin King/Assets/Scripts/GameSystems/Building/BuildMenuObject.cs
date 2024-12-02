using GameSystems.World.Grid;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystems.Building
{
    public class BuildMenuObject : MonoBehaviour
    {
        private GridObjectSO gridObjectSO;
        public Image iconImage;

        public void Setup(GridObjectSO goso)
        {
            gridObjectSO = goso;
            iconImage.sprite = goso.sprite;
            GetComponent<Button>().onClick.AddListener(OnClickButton);
        }

        private void OnClickButton()
        {
            BuildMode.i.BeginBuildMode(gridObjectSO);
        }
    }
}