using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawPanelInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PlayerController Player;
    [SerializeField] private GameObject Image;

    private float PanelWidth;
    private float PanelHeight;

    private List<float> DrawXPath = new List<float>();
    private List<float> DrawYPath = new List<float>();
    private List<Vector3> DrawingPath = new List<Vector3>();

    private List<GameObject> Images = new List<GameObject>();

    private bool bIsDrawing = false;

    // Start is called before the first frame update
    void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        PanelWidth = rt.rect.width;
        PanelHeight = rt.rect.height;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bIsDrawing)
        {
            GameObject CreatedImage = Instantiate(Image, new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0), Quaternion.identity);
            CreatedImage.transform.SetParent(transform);
            Images.Add(CreatedImage);


            DrawXPath.Add(Input.mousePosition.x / PanelWidth);
            DrawYPath.Add(Input.mousePosition.y / PanelHeight);

            DrawingPath.Add(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        bIsDrawing = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        bIsDrawing = false;

        Player.OrderUnits(DrawXPath, DrawYPath);

        DrawXPath.Clear();
        DrawYPath.Clear();
        DrawingPath.Clear();

        foreach (GameObject Image in Images)
        {
            Destroy(Image);
        }

        Images.Clear();
    }
}
