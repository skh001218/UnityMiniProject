using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Guest : MonoBehaviour
{
    public GameManager gm;
    public bool isSelect = false;
    public bool isRight;
    public float speed = 25f;


    public GameObject wayPoint;
    private List<Transform> wayPoints = new List<Transform>();
    private int posNum = 0;
    public GameObject rightPos;
    public GameObject leftPos;
    private float endPos;

    private float prePosX;


    private void Start()
    {
        for (int i = 0; i < wayPoint.transform.childCount; i++)
        {
            wayPoints.Add(wayPoint.transform.GetChild(i));
        }


        int temp = Random.Range(0, 2);
        if (temp == 0)
        {
            transform.position = new Vector2(rightPos.transform.position.x, rightPos.transform.position.y + 10f); 
            isRight = false;
            endPos = leftPos.transform.position.x;
        }
        else
        {
            transform.position = new Vector2(leftPos.transform.position.x, leftPos.transform.position.y - 10f);
            isRight = true;
            endPos = rightPos.transform.position.x;
        }

        prePosX = transform.position.x;
    }

    private void Update()
    {
        if(!isSelect)
        {
            Move();
        }
        else
        {

            if(transform.position.x != wayPoints[0].position.x && posNum == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(wayPoints[posNum].position.x, transform.position.y), speed * Time.deltaTime);
                return;
            }
                
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[posNum].position, speed * Time.deltaTime);
            if (transform.position == wayPoints[posNum].position )
                posNum++;
            if(posNum == wayPoints.Count)
            {
                gm.RemoveGuest(this);
                Destroy(gameObject);
            }
        }

        if( (isRight && transform.position.x >= endPos) || (!isRight && transform.position.x <= endPos) )
        {
            gm.RemoveGuest(this);
            Destroy(gameObject);
        }
           

    }

    private void Move()
    {
        Vector2 pos = transform.position;
        if (isRight)
            pos.x += speed * Time.deltaTime;
        else
            pos.x -= speed * Time.deltaTime;

        transform.position = pos;
    }

    private void OnMouseDown()
    {
        isSelect = true;
    }
}
