using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float turnSpeed = 4.0f;
    private float currentX;
    private float currentY;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentX = transform.eulerAngles.y;
        currentY = transform.eulerAngles.x;
        currentX += Input.GetAxisRaw("Mouse X");
        currentY -= Input.GetAxisRaw("Mouse Y");
        currentY = Mathf.Clamp(currentY, 10, 80);
        Vector3 dir = new Vector3(0, 0, -offset.magnitude);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        Vector3 wantedPosition = player.transform.position + rotation * dir;
        transform.position = wantedPosition;

        transform.LookAt(player.transform.position);
    }
}
