using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaleController : MonoBehaviour
{
    public bool maintainWidth = true;
    float defaultWidth;
    float defaultHeight;

    Vector3 cameraPos;

    // Start is called before the first frame update
    void Start()
    {
        cameraPos = Camera.main.transform.position;

        defaultHeight = Camera.main.orthographicSize;
        defaultWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if (maintainWidth == true)
        {
            Camera.main.orthographicSize = defaultWidth / Camera.main.aspect;

            //Camera.main.transform.position = new Vector3(cameraPos.x, (defaultHeight - Camera.main.orthographicSize), cameraPos.z);
        }
    }
}
