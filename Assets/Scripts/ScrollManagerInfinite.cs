using UnityEngine;
using UnityEngine.UI;

public class ScrollManagerInfinite : MonoBehaviour
{
    // To hold the ScrollPanel

    public RectTransform panel;

    // Array of buttons

    public Button[] bttn;

    // Centre to compare the distance for each button

    public RectTransform centre;

    // Button to start with (access this, if saved with PlayerPrefs, to go to selected object

    public int startButton = 1;

    // All buttons distance to centre, and distance to reposition

    private float[] distance;
    private float[] distanceReposition;

    // Will be true, when we drag panel

    private bool dragging;

    // Will hold the distance bewteen the buttons

    private int bttnDistance;

    // To hold the number of the button, with smallest distance to centre

    private int minButtonNum;

    // Private variable to hold Screen Width

    private int screenWidth;

    private float bttnStartPlacementX;

    private void Awake()
    {
        // Get Screen width

        screenWidth = Screen.width * 2;

        // Calculation for button placement based on current screen width, to make it dynamic

        float tempWidthValue = Screen.width / 2;
        bttnStartPlacementX = screenWidth / 2;
        bttnStartPlacementX += tempWidthValue;
        Mathf.Abs(bttnStartPlacementX);
    }

    private void Start()
    {
        int bttnLength = bttn.Length;
        distance = new float[bttnLength];

        distanceReposition = new float[bttnLength];

        // Get distance between buttons

        bttnDistance = (int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);

        // Starting button ( if using PlayerPrefs, read saved data for StartButton ( Selected Object ))

        panel.anchoredPosition = new Vector2((startButton - 1) * -bttnStartPlacementX / 2, 0f);
    }

    private void Update()
    {
        for (int i = 0; i < bttn.Length; i++)
        {
            distanceReposition[i] = centre.GetComponent<RectTransform>().position.x - bttn[i].GetComponent<RectTransform>().position.x;
            distance[i] = Mathf.Abs(distanceReposition[i]);

            if (distanceReposition[i] > screenWidth)
            {
                float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPosition = new Vector2(curX + (bttn.Length * bttnDistance), curY);

                bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
            }

            if (distanceReposition[i] < -screenWidth)
            {
                float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPosition = new Vector2(curX - (bttn.Length * bttnDistance), curY);

                bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPosition;
            }
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
            LerpToBttn(-bttn[minButtonNum].GetComponent<RectTransform>().anchoredPosition.x);
        }
    }

    void LerpToBttn(float position)
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
