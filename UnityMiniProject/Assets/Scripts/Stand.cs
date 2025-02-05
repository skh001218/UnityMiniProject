using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public Repository repository;

    private void Start()
    {
        repository = FindObjectOfType<Repository>(includeInactive: true);
    }
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            var wPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(wPos, Vector2.zero, 1, LayerMask.GetMask("Stand"));
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log(transform.GetSiblingIndex());
                repository.OpenRepository(transform.GetSiblingIndex());
            }
        }

#elif UNITY_ANDROID
        if (Input.touchCount == 1)
        {

            var wPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(wPos, Vector2.zero, 1, LayerMask.GetMask("Stand"));
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (hit.collider.gameObject.tag == tag)
                {
                    repository.OpenRepository(transform.GetSiblingIndex());
                }
            }
        }
#endif
    }
}
