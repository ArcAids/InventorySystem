using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem {

    public class TouchController : MonoBehaviour
    {
        [SerializeField]
        Camera cam;
        [SerializeField]
        float dragThreshold = 5f;
        [SerializeField]
        LayerMask recievableMask;
        [SerializeField]
        LayerMask draggableMask;

        Vector2 mouseDownPosition;
        Vector2 mousePosition;

        bool dragged = false;
        bool isActive = false;
        public static GameObject selectedItem;

        IDraggable draggableObject;
        IRecievable recievableObject;
        IDraggable lastSelectedObject;

        [SerializeField]
        GraphicRaycaster m_Raycaster;
        [SerializeField]
        EventSystem m_EventSystem;
        PointerEventData m_PointerEventData;

        void Start()
        {
            //Fetch the Raycaster from the GameObject (the Canvas)
            if (m_Raycaster == null)
                m_Raycaster = GetComponentInParent<GraphicRaycaster>();
            //Fetch the Event System from the Scene
            if (m_EventSystem == null)
                m_EventSystem = FindObjectOfType<EventSystem>();
        }

        private void OnEnable()
        {
            isActive = true;
        }

        private void OnDisable()
        {
            isActive = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isActive)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                lastSelectedObject = draggableObject;
                draggableObject = DetectDraggables();
                if (draggableObject != null)
                {
                    if (lastSelectedObject != null)
                        lastSelectedObject.OnDeselected();
                    draggableObject.OnSelected();
                    dragged = false;
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (draggableObject != null)
                {
                    mousePosition = Input.mousePosition;
                    if (dragged)
                    {
                        draggableObject.OnDragged(mousePosition);
                        recievableObject = DetectRecievables();
                        if (recievableObject != null)
                        {
                            recievableObject.OnObjectHoveringOver(selectedItem);
                        }
                    }
                    else
                    if (Vector2.Distance(mouseDownPosition, mousePosition) > dragThreshold)  // TODO: Optimize.
                    {
                        dragged = true;
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (draggableObject != null)         //something is selected.
                {
                    recievableObject = DetectRecievables();
                    if (recievableObject != null)         //object is over something.
                    {
                        draggableObject.OnLetGo(mousePosition);
                        recievableObject.OnObjectAdded(selectedItem);
                    }
                    else
                    {
                        if (dragged)
                        {
                            draggableObject.OnCancelDrag();  //Deck/Card is Moved
                        }
                    }
                }
            }

        }

        IDraggable DetectDraggables()
        {

            //Set up the new Pointer Event
            m_PointerEventData = new PointerEventData(m_EventSystem);
            //Set the Pointer Event Position to that of the mouse position
            m_PointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            m_Raycaster.Raycast(m_PointerEventData, results);

            //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
            foreach (RaycastResult result in results)
            {
                draggableObject= result.gameObject.GetComponent<IDraggable>();
                if (draggableObject != null)
                {
                    mouseDownPosition = Input.mousePosition;
                    selectedItem = result.gameObject;
                    return draggableObject;
                }
                Debug.Log("Hit " + result.gameObject.name);
            }
            return null;

        }
    IRecievable DetectRecievables()
    {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);

            foreach (RaycastResult result in results)
            {
                recievableObject = result.gameObject.GetComponent<IRecievable>();
                if (recievableObject != null)
                {
                    return recievableObject;
                }
            }
            return null;
        }

       
    }


    public interface IDraggable
    {
        void OnSelected();
        void OnDeselected();
        void OnLetGo(Vector2 screenPosition);
        void OnCancelDrag();
        void OnDragged(Vector2 screenPosition);
    }
    public interface IRecievable
    {
        bool OnObjectAdded(GameObject selectedObject);
        void OnObjectHoveringOver(GameObject selectedObject);
    }
}