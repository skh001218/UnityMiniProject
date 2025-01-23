using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BakeCount : MonoBehaviour
{
    private Vector2 pos;
    private float arriveY;
    private float alphaColor;
    private float lerfSpeed = 3f;

    public TextMeshProUGUI itemCount;
    public Image itemImage;

    public BakeCount(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }

    private void Start()
    {
        pos = transform.position;
        arriveY = pos.y + 10f;
    }
    private void FixedUpdate()
    {
        Debug.Log(arriveY);
        pos.y = Mathf.Lerp(pos.y, arriveY, lerfSpeed * Time.deltaTime);
        transform.position = pos;
        if (transform.position.y >= arriveY - 1f)
        {
            itemImage.color = Color.Lerp(itemImage.color,
                new Color(itemImage.color.r, itemImage.color.g, itemImage.color.b, 0), lerfSpeed * Time.deltaTime);

            itemCount.color = Color.Lerp(itemCount.color,
                new Color(itemCount.color.r, itemCount.color.g, itemCount.color.b, 0), lerfSpeed * Time.deltaTime);

        }

        if(itemImage.color.a == 0)
            Destroy(gameObject);

    }
}
