using UnityEngine;

namespace ObjectSelection
{
    public class SelectMode : MonoBehaviour
    {
        public static SelectMode i;
        public SelectModeType currentSelectMode = SelectModeType.GoblinInteract;

        private void Awake()
        {
            i = this;
        }

        public enum SelectModeType
        {
            GoblinInteract,
            Select
        }


        public void UpdateSelectMode(SelectModeType mode)
        {
            currentSelectMode = mode;
        }
        
    }
}
