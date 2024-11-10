using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Functions
{
    public static class ObjectPooling
    {
        public static GameObject ActivateObject(HashSet<GameObject> objects, Vector2 pos)
        {
            var obj = objects.FirstOrDefault(obj => !obj.gameObject.activeSelf);

            if (obj != null)
            {
                obj.transform.position = pos;
                obj.gameObject.SetActive(true);
            }
            
            return obj;
        }
    }
}
