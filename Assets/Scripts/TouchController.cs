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

        [SerializeField]
        Image draggedItemPreviewImage;

        Vector2 mouseDownPosition;
        Vector2 mousePosition;

        bool dragged = false;
        bool isActive = false;
        GameObject selectedItem;

        IDraggable draggableObject;
        IRecievable recievableObject;
        IRecievable objectDraggedFrom;

        [SerializeField]
        GraphicRaycaster m_Raycaster;
        [SerializeField]
        EventSystem m_EventSystem;
        PointerEventData m_PointerEventData;

        void Start()
        {
            if (m_Raycaster == null)
                m_Raycaster = GetComponentInParent<GraphicRaycaster>();
            if (m_EventSystem == null)
                m_EventSystem = FindObjectOfType<EventSystem>();
            if (cam == null)
                cam = Camera.main;

            draggedItemPreviewImage.gameObject.SetActive(false);
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
                draggableObject= DetectDraggables();
                objectDraggedFrom = DetectRecievables();
                if (draggableObject!= null)
                {
                    draggableObject.Select();
                }
                    dragged = false;
            }
            else if (Input.GetMouseButton(0))
            {
                if (draggableObject != null)
                {
                    mousePosition = Input.mousePosition;
                    if (dragged)
                    {
                        draggedItemPreviewImage.transform.position = mousePosition;
                        //recievableObject = DetectRecievables();
                        //if (recievableObject != null)
                        //{
                        //    recievableObject.OnObjectHoveringOver(selectedItem);
                        //}
                    }
                    else
                    if (Vector2.Distance(mouseDownPosition, mousePosition) > dragThreshold)  // TODO: Optimize.
                    {
                        if (draggableMask == (draggableMask | (1 << selectedItem.gameObject.layer)))
                        {
                            dragged = true;
                            draggedItemPreviewImage.sprite = draggableObject.OnStartDrag();
                            draggedItemPreviewImage.transform.position = mousePosition;
                            draggedItemPreviewImage.gameObject.SetActive(true); 
                        }
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (draggableObject != null)         //something is selected.
                {
                    draggedItemPreviewImage.gameObject.SetActive(false);
                    recievableObject = DetectRecievables();
                    if (!dragged)
                        return;
                    if (recievableObject != null && recievableObject!=objectDraggedFrom)         //object is over something other than where it started from.
                    {
                        recievableObject.OnObjectAdded(selectedItem);
                        draggableObject.OnDragDone();
                    }
                    else
                    draggableObject.OnCancelDrag(); 
                    
                }
            }

        }

        IDraggable DetectDraggables()
        {

            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);

            foreach (RaycastResult result in results)
            {
                
                draggableObject = result.gameObject.GetComponent<IDraggable>();
                if (draggableObject != null)
                {
                    mouseDownPosition = Input.mousePosition;
                    selectedItem = result.gameObject;
                    return draggableObject;
                }
                break;
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
                if (recievableMask == (recievableMask | (1 << result.gameObject.layer)))
                {
                    recievableObject = result.gameObject.GetComponent<IRecievable>();
                    if (recievableObject != null)
                    {
                        return recievableObject;
                    }
                }
            }
            return null;
        }

       
    }


    public interface IDraggable
    {
        void Select();
        void Deselect();
        void OnDragDone();
        void OnCancelDrag();
        Sprite OnStartDrag();
    }
    public interface IRecievable
    {
        void OnObjectAdded(GameObject selectedObject);
        void OnObjectHoveringOver(GameObject selectedObject);
    }
}