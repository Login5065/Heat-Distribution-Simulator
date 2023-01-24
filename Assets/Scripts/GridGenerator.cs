using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    //GameObject objectCanvas;
    public GameObject panel;

    public float width, heigth;
    public float DeltaTime = 1;
    public double Temp = 40;
    public float GridDensity;
    public int GridX, GridY;
    public int k = 401; /// <summary>
                        //
                        // to wlasnie ta zmienna to to q ze wzorów
    
    public float t= 0.5f;
    public float d;
    public float cp;

    /// <summary>
    /// tutaj odstep czasowy pomiedzy tyknieciami
    /// </summary>
    ///
    public float dist = 0.001f;

    public float alpha = 1;
    
    public Vector2 GridStartPoint;
    public List<GridPanel> Panel;

    // Start is called before the first frame update
    void Start()
    {
        //objectCanvas = GameObject.Find("CanvasObjectSimulation2");
        Generate(10, 30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// generuje siatke na podstawie ilosci punktow w szerokosci i wysokosci
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Generate(int x, int y)
    {
        int pivot;
        float density = 0.0f;
        Vector2 startPoint;
        float W = this.GetComponent<RectTransform>().rect.width;
        float H = this.GetComponent<RectTransform>().rect.height;
        if (x == y)
        {
            startPoint = Vector2.zero;
            density = W / x;
            DrawGrid(x, y, density, startPoint);
        }
        else if (x > y)
        {
            pivot = x - y;
            density = W / x;
            startPoint = new Vector2(0, -pivot / 2 * density);
            DrawGrid(x, y, density, startPoint);
        }
        else
        {
            pivot = y - x;
            density = H / y;
            startPoint = new Vector2(pivot / 2 * density, 0);
            DrawGrid(x, y, density, startPoint);
        }
    }

    void DrawGrid(int x, int y, float density, Vector2 startPoint)
    {
       
    Panel = new List<GridPanel>();
    
    for(int i = 0; i < x; i++)
    {
        for(int j = 0; j < y; j++)
        
        {
            var myNewGridPoint = Instantiate(panel, this.transform, false);
            myNewGridPoint.transform.localPosition += new Vector3(startPoint.x + i * density, startPoint.y - j * density, 0);
            myNewGridPoint.GetComponent<RectTransform>().sizeDelta = new Vector2(density, density);
            myNewGridPoint.name = ("PanelGen " + j +" "+ i);
                //myNewGridPoint.GetComponent<GridPanel>().x = j;
                //myNewGridPoint.GetComponent<GridPanel>().y = i;
            Panel.Add(myNewGridPoint.GetComponent<GridPanel>());
           


        }
    
    }
            
        

       
        GridX = x;
        GridY = y;
        GridDensity = density;
        GridStartPoint = startPoint;
        
    }
    public void DESTROY_THE_CHILD()
    {
        foreach(Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
