using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{
    // To hold the ScrollPanel

    public RectTransform panel;

    // Array of buttons

    public Button[] bttn;

    // Centre to compare the distance for each button

    public RectTransform centre;

    // All buttons distance to centre

    private float[] distance;

    // Will be true, when we drag panel

    private bool dragging = false;

    // Will hold the distance bewteen the buttons

    private int bttnDistance;

    // To hold the number of the button, with smallest distance to centre

    private int minButtonNum;

    private void Start()
    {
        int bttnLength = bttn.Length;
        distance = new float[bttnLength];

        // Get distance between buttons

        bttnDistance = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    private void Update()
    {
        for (int i = 0; i < bttn.Length; i++)
        {
            distance[i] = Mathf.Abs(centre.transform.position.x - bttn[i].transform.position.x);
        }

        // Get the minimum distance

        float minDistance = Mathf.Min(distance);

        for (int a = 0; a < bttn.Length; a++)
        {
            if (minDistance == distance[a])
            {
                minButtonNum = a;
            }
        }

        // If we are not dragging

        if (!dragging)
        {
            LerpToBttn(minButtonNum * -bttnDistance);
        }
    }

    void LerpToBttn(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);

        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }
}
