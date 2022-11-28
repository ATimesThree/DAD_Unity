using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PdfScrollScript : MonoBehaviour
{
    //public GameObject PopupObject;
    public int numberOfPages = 1;
    public InputActionReference action_view;
    private float scrolling_Value;

    private RectTransform rectTransform;
    int maxView = 0;

    private void Awake()
    {
        action_view.action.performed += _x => scrolling_Value = _x.action.ReadValue<float>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0);
        if (numberOfPages < 1)
        {
            numberOfPages = 1;
        }
        maxView = numberOfPages * 1000;
    }

    // Update is called once per frame
    void Update()
    {
        if (scrolling_Value > 0) // forward
        {
            Debug.Log("Scrolling up");
            if (rectTransform.anchoredPosition.y < 0)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0);
            }
            else if (rectTransform.anchoredPosition.y > 0)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, (rectTransform.anchoredPosition.y - 333));
            }
        }
        else if (scrolling_Value < 0) // backwards
        {
            Debug.Log("Scrolling down");
            if (rectTransform.anchoredPosition.y > maxView)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, maxView);
            }
            else if (rectTransform.anchoredPosition.y < maxView)
            {
                // try
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, (rectTransform.anchoredPosition.y + 333));
            }
        }
    }
}
