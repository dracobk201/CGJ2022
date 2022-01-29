using UnityEngine;

public class Dragger : MonoBehaviour
{
    private float dist;
    private bool dragging = false;
    private Vector3 offset;
    private Transform toDrag;
    private Camera mainCamera;
    private Touch currentTouch;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 v3;

        if (Input.touchCount != 1)
        {
            dragging = false;
            return;
        }

        currentTouch = Input.touches[0];
        Vector3 pos = currentTouch.position;

        if (currentTouch.phase == TouchPhase.Began)
        {
            Ray ray = mainCamera.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Player")
                {
                    toDrag = hit.transform;
                    dist = hit.transform.position.z - mainCamera.transform.position.z;
                    v3 = new Vector3(pos.x, 0, dist);
                    v3 = mainCamera.ScreenToWorldPoint(v3);
                    offset = toDrag.position - v3;
                    dragging = true;
                }
            }
        }

        if (dragging && currentTouch.phase == TouchPhase.Moved)
        {
            v3 = new Vector3(Input.mousePosition.x, 0, dist);
            v3 = mainCamera.ScreenToWorldPoint(v3);
            toDrag.position = v3 + offset;
        }

        if (dragging && (currentTouch.phase == TouchPhase.Ended || currentTouch.phase == TouchPhase.Canceled))
            dragging = false;
    }
}
