using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;

    Vector3 mPrevPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mPrevPos = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 direction = mPrevPos - cam.ScreenToViewportPoint(Input.mousePosition);

            cam.transform.position = new Vector3();

            cam.transform.Rotate(transform.up, -direction.x * 180, Space.World);
            cam.transform.Rotate(Camera.main.transform.right, direction.y * 180, Space.World);
            cam.transform.Translate(new Vector3(0, 0, -5));

            mPrevPos = cam.ScreenToViewportPoint(Input.mousePosition);
        }
    }
}
