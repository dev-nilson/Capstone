using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public GameObject selectedObject;
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
                Debug.Log("Mouse postion x: " + selectedObject.transform.position.x);
                Debug.Log("Mouse postion y: " + selectedObject.transform.position.y);
            }
        }
    }
}