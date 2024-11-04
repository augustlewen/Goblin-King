using UnityEngine;

namespace GameSystems.Units.King
{
    public class KingMovement : MonoBehaviour
    {
        public float speed;
        private void Update()
        {
            var xMove = Input.GetAxisRaw("Horizontal");
            var yMove = Input.GetAxisRaw("Vertical");

            transform.position += new Vector3(xMove, yMove) * (speed * Time.deltaTime);

            if (xMove != 0)
                transform.localScale = new Vector3(xMove, 1, 1);
        }
    }
}
