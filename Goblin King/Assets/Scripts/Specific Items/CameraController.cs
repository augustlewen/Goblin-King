using GameSystems.Units.King;
using UnityEngine;

namespace Specific_Items
{
    public class CameraController : MonoBehaviour
    {
        private Transform target;
        public float smoothness;
        private void Start()
        {
            target = KingMovement.i.transform;
        }

        private void LateUpdate()
        {
            transform.position = Vector2.Lerp(transform.position, target.position, Time.deltaTime * smoothness);
            transform.position += new Vector3(0, 0, -10);
        }
    }
}
