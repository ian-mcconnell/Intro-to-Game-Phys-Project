using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTank;
    public float HorizontalOffset = 0f;
    public float VerticalOffset = 7f;

    private void FixedUpdate ()
    {
        transform.position = new Vector3(playerTank.position.x, playerTank.position.y, transform.position.z) + new Vector3(-HorizontalOffset, VerticalOffset, 0f);
    }
}
