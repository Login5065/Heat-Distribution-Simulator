using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeneratorMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GridGenerator grid;
    private bool clicked = false;
    private GameObject[] panels;
    public void OnPointerDown(PointerEventData eventData)
    {
        clicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        clicked = false;
        panels = GameObject.FindGameObjectsWithTag("object1");
        GameObject closest = null;
        float distance = 1000.0f;
        foreach(GameObject panel in panels)
        {
            float dist = Vector2.Distance(gameObject.transform.position, panel.transform.position);
            if (dist<distance)
            {
                distance = dist;
                closest = panel;
            }
        }
    }
    public Canvas parentCanvas;
    public void Start()
    {
        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform, Input.mousePosition,
            parentCanvas.worldCamera,
            out pos);

        grid = GameObject.Find("CanvasObjectSimulation2").GetComponent<GridGenerator>();

    }
    public void Update()
    {
        if(clicked)
        {
            Vector2 movePos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.transform as RectTransform,
                Input.mousePosition, parentCanvas.worldCamera,
                out movePos);

            transform.position = parentCanvas.transform.TransformPoint(movePos);
            
        }
        
    }
}
