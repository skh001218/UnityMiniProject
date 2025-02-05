using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class SellAreaMgr : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 moveDir;
    private Vector3 cameraPos;

    public Transform limitUp;
    public Transform limitDown;

    public Repository repository;

    private void Start()
    {
        repository.UpdatePlate();
    }

    private void FixedUpdate()
    {
        float temp = Mathf.Clamp(Camera.main.transform.position.y, limitDown.position.y, limitUp.position.y);
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, temp, Camera.main.transform.position.z);
    }

    private void OnMouseDown()
    {
        if (repository.gameObject.activeSelf)
            return;
        startPos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        if (repository.gameObject.activeSelf)
            return;
        moveDir =  startPos - (Vector2)Input.mousePosition;
        moveDir.Normalize();

        if ((moveDir.y > 0 && Camera.main.transform.position.y == limitUp.position.y) ||
            (moveDir.y < 0 && Camera.main.transform.position.y == limitDown.position.y))
            return;

        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x,
            Camera.main.transform.position.y + moveDir.y, -10f);

        
    }

    private void OnMouseUp()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
