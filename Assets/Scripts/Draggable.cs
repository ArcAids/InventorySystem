using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InventorySystem
{
    public class Draggable : MonoBehaviour, IDraggable
    {
        public void OnCancelDrag()
        {
            throw new System.NotImplementedException();
        }

        public void OnDeselected()
        {
            throw new System.NotImplementedException();
        }

        public void OnDragged(Vector2 screenPosition)
        {
            gameObject.transform.position = screenPosition;
        }

        public void OnLetGo(Vector2 screenPosition)
        {
            throw new System.NotImplementedException();
        }

        public void OnSelected()
        {
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}