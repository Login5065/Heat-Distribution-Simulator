using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    private bool startAnimation = false;
    private bool apply2_1 = false, apply2_2 = false, apply3_1 = false, apply3_2 = false;

    private float object1moveX, object1moveY, object2moveX, object2moveY;
    private GameObject tempObject1, tempObject2;
    private Vector3 position1, position2;
    private float tempF;
    private double maxTemp, minTemp, tempDiff;

    #region temperatury obiektow i timecounter
    private double temperature1, temperature2;
    public GameObject temperatureCounter1, temperatureCounter2;
    public GameObject timerCounter;
    #endregion


    #region JacekCzas

    public float time;
    public float timeIntervals = 1;
    public bool math;

    #endregion
    
    #region zmienne dla gridGenerator
    //wartosc K
    private float k1, k2;
    //density
    private float d1, d2;
    //cieplo wlasciwe
    private float cp1, cp2;
    #endregion

    public GameObject canvasOption1;
    public GameObject canvasSimulation1;
    public GameObject canvas;
    //public GameObject[] parameter1 = new GameObject[2];
    public GameObject sliderTemp1, sliderTemp2;    
    public GameObject sizeX1, sizeX2, sizeY1, sizeY2;
    public GameObject metal1, metal2;
    public GameObject buttonStart1;


    public Simulation simulation = new Simulation();

    void Start()
    {
        //active = canvas.gameObject.activeSelf;
        //metal.GetComponent<Dropdown>().options.Clear();
        //metal.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = "żelazo" });
        //metal.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = "platyna" });
        tempObject1 = GameObject.Find("CanvasObject1");
        tempObject2 = GameObject.Find("CanvasObject2");
    }


    private IEnumerator Math()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        GridGenerator grid1 = tempObject1.GetComponent<GridGenerator>();
        GridGenerator grid2 = tempObject2.GetComponent<GridGenerator>();
        int count = 0;
        while (true)
        {
            yield return wait;
            if (time <= 0)
            {
                Debug.Log("DONE");
                Debug.Log(count);
                for (int i = 0; i < grid1.Panel.Count; i++)
                {
                    Debug.Log(grid1.Panel[i].punkt.SavedValue);
                }

                for (int i = 0; i < grid2.Panel.Count; i++)
                {
                    Debug.Log(grid2.Panel[i].punkt.SavedValue);
                }
                StopAllCoroutines();
                
            }
        
            
            simulation.Math();
            count++;
            time -= timeIntervals;
            temperature1 = 0;
            temperature2 = 0;
            for (int i = 0; i < grid1.Panel.Count; i++)
            {
                double temp = grid1.Panel[i].punkt.SavedValue;
                temp = (maxTemp - temp) / tempDiff;
                float temp1 = (float)temp;
                grid1.Panel[i].gameObject.GetComponent<Image>().color = new Color(1 - temp1, 0.0f, temp1); 
                grid1.Panel[i].gameObject.GetComponent<GridPanel>().temp =  grid1.Panel[i].punkt.SavedValue;
                temperature1 += grid1.Panel[i].punkt.SavedValue;
   
            }
            temperature1 /= (grid1.Panel.Count);

            for (int i = 0; i < grid2.Panel.Count; i++)
            {
                double temp = grid2.Panel[i].punkt.SavedValue;
                temp = (maxTemp - temp) / tempDiff;
                float temp1 = (float)temp;
                grid2.Panel[i].gameObject.GetComponent<Image>().color = new Color(1 - temp1, 0.0f, temp1);
                grid2.Panel[i].gameObject.GetComponent<GridPanel>().temp =  grid2.Panel[i].punkt.SavedValue;
                temperature2 += grid2.Panel[i].punkt.SavedValue;
            }
            temperature2 /= (grid2.Panel.Count);
            temperatureCounter1.GetComponent<Text>().text = temperature1.ToString("#.00");
            temperatureCounter2.GetComponent<Text>().text = temperature2.ToString("#.00");
            float someTemp;
            if (float.TryParse(timerCounter.GetComponent<Text>().text, out someTemp))
            {
                someTemp += timeIntervals;
                timerCounter.GetComponent<Text>().text = someTemp.ToString("#.00");
            }
            //tempObject1.GetComponent<GridGenerator>().Panel[1].gameObject.GetComponent<Image>().color = new Color(0.5f,0.5f,1f );




        }
    }

    public void MathStart()
    {
        if(tempObject1.GetComponent<GridGenerator>().Temp > tempObject2.GetComponent<GridGenerator>().Temp)
        {
            maxTemp = tempObject1.GetComponent<GridGenerator>().Temp;
            minTemp = tempObject2.GetComponent<GridGenerator>().Temp;
        }
        else
        {
            maxTemp = tempObject2.GetComponent<GridGenerator>().Temp;
            minTemp = tempObject1.GetComponent<GridGenerator>().Temp;
        }
        tempDiff = maxTemp - minTemp;


        StartCoroutine(Math());




    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(startAnimation)
        {
            tempObject1.GetComponent<RectTransform>().localPosition = Vector3.Lerp(tempObject1.GetComponent<RectTransform>().localPosition, position1 + new Vector3(object1moveX, 0, 0), tempF);
            tempObject2.GetComponent<RectTransform>().localPosition = Vector3.Lerp(tempObject2.GetComponent<RectTransform>().localPosition, position2 - new Vector3(object2moveX, 0, 0), tempF);
            tempF += 0.002f;
            if(tempF > 1)
            {
                startAnimation = false;
                tempF = 0.0f;
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            //przyblizenie
            canvasSimulation1.GetComponent<RectTransform>().localScale += new Vector3(0.1f, 0.1f, 0);
            //canvasSimulation2.GetComponent<RectTransform>().localScale += new Vector3(0.1f, 0.1f, 0);
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            //oddalenie
            canvasSimulation1.GetComponent<RectTransform>().localScale -= new Vector3(0.1f, 0.1f, 0);
            //canvasSimulation2.GetComponent<RectTransform>().localScale -= new Vector3(0.1f, 0.1f, 0);
        }
    }

    /*
    public void LoadParameter1()
    {
        if (parameter1[0].gameObject.activeSelf)
        {
            parameter1[0].SetActive(false);
            parameter1[1].SetActive(false);
        }
        else
        {
            parameter1[0].SetActive(true);
            parameter1[1].SetActive(true);
        }
        
    }*/

    public void PopUpMenu()
    {
        canvas.SetActive(!canvas.activeSelf);
    }

    public void sizeCheckX1()
    {
        float divide;
        GameObject slider = GameObject.Find("SliderDensity1");
        divide = Divide(slider);

        float x = 0.0f;
        if (float.TryParse(sizeX1.GetComponent<Text>().text, out x))
        {
            if (!f_modulo(x, divide))
            {
                sizeX1.GetComponent<Text>().color = Color.red;
                Debug.Log("nieparzysta liczba WIDTH");
                apply2_1 = false;
            }
            else
            {
                sizeX1.GetComponent<Text>().color = Color.black;
                apply2_1 = true;
            }
        }
        else
        {
            sizeX1.GetComponent<Text>().color = Color.red;
            apply2_1 = false;
        }
    }
    public void sizeCheckY_1()
    {
        float divide;
        GameObject slider = GameObject.Find("SliderDensity1");
        divide = Divide(slider);

        float y = 0.0f;

        if (float.TryParse(sizeY1.GetComponent<Text>().text, out y))
        {
            if (!f_modulo(y, divide))
            {
                sizeY1.GetComponent<Text>().color = Color.red;
                Debug.Log("nieparzysta liczba HEIGTH");
                apply3_1 = false;
            }
            else
            {
                sizeY1.GetComponent<Text>().color = Color.black;
                apply3_1 = true;
            }
        }
        else
        {
            sizeY1.GetComponent<Text>().color = Color.red;
            apply3_1 = false;
        }
    }
    public void sizeCheckX_2()
    {
        float divide;
        GameObject slider = GameObject.Find("SliderDensity1");
        divide = Divide(slider);

        float x = 0.0f;
        if (float.TryParse(sizeX2.GetComponent<Text>().text, out x))
        {
            if (!f_modulo(x, divide))
            {
                sizeX2.GetComponent<Text>().color = Color.red;
                Debug.Log("nieparzysta liczba WIDTH");
                apply2_2 = false;
            }
            else
            {
                sizeX2.GetComponent<Text>().color = Color.black;
                apply2_2 = true;
            }
        }
        else
        {
            sizeX2.GetComponent<Text>().color = Color.red;
            apply2_2 = false;
        }
    }
    public void sizeCheckY_2()
    {
        float divide;
        GameObject slider = GameObject.Find("SliderDensity1");
        divide = Divide(slider);

        float y = 0.0f;

        if (float.TryParse(sizeY2.GetComponent<Text>().text, out y))
        {
            if (!f_modulo(y, divide))
            {
                sizeY2.GetComponent<Text>().color = Color.red;
                Debug.Log("nieparzysta liczba HEIGTH");
                apply3_2 = false;
            }
            else
            {
                sizeY2.GetComponent<Text>().color = Color.black;
                apply3_2 = true;
            }
        }
        else
        {
            sizeY2.GetComponent<Text>().color = Color.red;
            apply3_2 = false;
        }
    }

    public void ApplySettings1()
    {
        StopAllCoroutines();
        
        
        if (apply2_1 && apply3_1 && apply2_2 && apply3_2)
        {
            float divide = 1.0f;
            GameObject slider = GameObject.Find("SliderDensity1");
            GameObject sliderTime = GameObject.Find("SliderTime");
            GameObject sliderTimeInterval = GameObject.Find("SliderTimeInterval");
            int temporaryZero = 0;
            timerCounter.GetComponent<Text>().text = temporaryZero.ToString();
            timeIntervals = sliderTimeInterval.GetComponent<Slider>().value;
            float time = sliderTime.GetComponent<Slider>().value;
            this.time = sliderTime.GetComponent<Slider>().value;
            divide = Divide(slider);

            float x1 = float.Parse(sizeX1.GetComponent<Text>().text);
            float y1 = float.Parse(sizeY1.GetComponent<Text>().text);

            float x2 = float.Parse(sizeX2.GetComponent<Text>().text);
            float y2 = float.Parse(sizeY2.GetComponent<Text>().text);

            //pobranie info o metalach
            Dropdown1();
            Dropdown2();


            GameObject gameObject1 = GameObject.Find("CanvasObject1");
            gameObject1.GetComponent<GridGenerator>().DESTROY_THE_CHILD();
            gameObject1.GetComponent<GridGenerator>().Generate((int)(x1 / divide), (int)(y1 / divide));
            gameObject1.GetComponent<RectTransform>().localScale = Vector3.one;
            gameObject1.GetComponent<RectTransform>().localPosition = new Vector3(-160, 0, 0);
            gameObject1.GetComponent<GridGenerator>().width = x1;
            gameObject1.GetComponent<GridGenerator>().heigth = y1;
            gameObject1.GetComponent<GridGenerator>().Temp = sliderTemp1.GetComponent<Slider>().value;
            gameObject1.GetComponent<GridGenerator>().k = (int)k1;
            gameObject1.GetComponent<GridGenerator>().d = d1;
            gameObject1.GetComponent<GridGenerator>().cp = cp1;
            gameObject1.GetComponent<GridGenerator>().dist = divide;
            gameObject1.GetComponent<GridGenerator>().t = time;
            gameObject1.GetComponent<GridGenerator>().DeltaTime = timeIntervals;



            GameObject gameObject2 = GameObject.Find("CanvasObject2");
            gameObject2.GetComponent<GridGenerator>().DESTROY_THE_CHILD();
            gameObject2.GetComponent<GridGenerator>().Generate((int)(x2 / divide), (int)(y2 / divide));
            gameObject2.GetComponent<RectTransform>().localScale = Vector3.one;
            gameObject2.GetComponent<RectTransform>().localPosition = new Vector3(140, 0, 0);
            gameObject2.GetComponent<GridGenerator>().width = x2;
            gameObject2.GetComponent<GridGenerator>().heigth = y2;
            gameObject2.GetComponent<GridGenerator>().Temp = sliderTemp2.GetComponent<Slider>().value;
            gameObject2.GetComponent<GridGenerator>().k = (int)k2;
            gameObject2.GetComponent<GridGenerator>().d = d2;
            gameObject2.GetComponent<GridGenerator>().cp = cp2;
            gameObject2.GetComponent<GridGenerator>().dist = divide;
            gameObject2.GetComponent<GridGenerator>().t = time;
            gameObject2.GetComponent<GridGenerator>().DeltaTime = timeIntervals;

            Debug.Log("generated");
            simulation.CreateBodiesVisible(gameObject1.GetComponent<GridGenerator>(),gameObject2.GetComponent<GridGenerator>());
            

            //pokazanie buttona
            buttonStart1.SetActive(true);
        }
    }
    public void Simulation1()
    {
        GameObject gameObject1 = GameObject.Find("CanvasObject1");
        GameObject gameObject2 = GameObject.Find("CanvasObject2");

        float difference = gameObject1.GetComponent<GridGenerator>().GridDensity / gameObject2.GetComponent<GridGenerator>().GridDensity;
        if (difference < 1)
        {
            gameObject2.GetComponent<RectTransform>().localScale *= difference;
        }
        else
        {
            gameObject1.GetComponent<RectTransform>().localScale *= (1 / difference);
        }


        if (gameObject1.GetComponent<GridGenerator>().GridStartPoint.x == 0)
        {
            //poziomy albo kwadrat
            //object1moveX = gameObject2.GetComponent<RectTransform>().position.x - gameObject1.GetComponent<RectTransform>().position.x - 
            //    (gameObject1.GetComponent<RectTransform>().rect.width/2 * gameObject1.GetComponent<RectTransform>().localScale.x) - 
            //    (gameObject2.GetComponent<RectTransform>().rect.width/2 * gameObject2.GetComponent<RectTransform>().localScale.x);
            object1moveX = (300 - gameObject1.GetComponent<RectTransform>().rect.width * gameObject1.GetComponent<RectTransform>().localScale.x) * 0.5f;
        }
        if (gameObject1.GetComponent<GridGenerator>().GridStartPoint.y == 0)
        {
            //pionowy obiekt albo kwadrat
            //object1moveX = gameObject2.GetComponent<RectTransform>().position.x - gameObject1.GetComponent<RectTransform>().position.x -
            //    (gameObject1.GetComponent<RectTransform>().rect.width/2 * gameObject1.GetComponent<RectTransform>().localScale.x) -
            //    (gameObject2.GetComponent<RectTransform>().rect.width/2 * gameObject2.GetComponent<RectTransform>().localScale.x)
            //   - (gameObject1.GetComponent<GridGenerator>().GridStartPoint.x * gameObject1.GetComponent<RectTransform>().localScale.x);
            //jeszcze ustabilizowac gdyby byla nieparzysta ilosc
            object1moveX = (300 - gameObject1.GetComponent<RectTransform>().rect.width * gameObject1.GetComponent<RectTransform>().localScale.x) * 0.5f + 
                gameObject1.GetComponent<GridGenerator>().GridStartPoint.x * gameObject1.GetComponent<RectTransform>().localScale.x;
        }
        if (gameObject2.GetComponent<GridGenerator>().GridStartPoint.x == 0)
        {
            //pionowy obiuekty
            //przesuwamy o gridstartpoint zeby dotknal lewej sciany
            //object2moveX = gameObject2.GetComponent<GridGenerator>().GridStartPoint.x;
            object2moveX = (300 - gameObject2.GetComponent<RectTransform>().rect.width * gameObject2.GetComponent<RectTransform>().localScale.x) * 0.5f;
        }
        if (gameObject2.GetComponent<GridGenerator>().GridStartPoint.y == 0)
        {
            //poziuomy obiekt
            //X nie ruszamy, tylko zmineiamy Y
            //object2moveY = gameObject2.GetComponent<GridGenerator>().GridStartPoint.y;
            object2moveX = (300 - gameObject2.GetComponent<RectTransform>().rect.width * gameObject2.GetComponent<RectTransform>().localScale.x) * 0.5f +
                gameObject2.GetComponent<GridGenerator>().GridStartPoint.x * gameObject2.GetComponent<RectTransform>().localScale.x;
        }
        //obiekt 1 przesuwamy + object1movex, object2 przesuwamy o -object2movex - bo w lewo

        if (difference < 1)
        {
            //object2moveX *= (1 / difference);
            //object2moveY *= (1 / difference);
        }
        else
        {
            //object1moveX *= difference;
            //object1moveY *= difference;
        }
        startAnimation = true;
        position1 = gameObject1.GetComponent<RectTransform>().localPosition;
        position2 = gameObject2.GetComponent<RectTransform>().localPosition;
        //chwilowe
        //gameObject1.GetComponent<RectTransform>().localPosition += new Vector3(object1moveX, 0, 0);
        //gameObject2.GetComponent<RectTransform>().localPosition -= new Vector3(object2moveX, 0, 0);
        //
        Debug.Log(gameObject1.GetComponent<RectTransform>().localScale + "   " + gameObject2.GetComponent<RectTransform>().localPosition);
        Debug.Log(object1moveX + " " + object1moveY + " " + object2moveX + " " + object2moveY);

    }

    public void FixObject1()
    {
        tempObject1.GetComponent<RectTransform>().localPosition += new Vector3(0, tempObject2.GetComponent<GridGenerator>().GridDensity / 2, 0);
    }


    public bool f_modulo(float a, float b)
    {
        float x = Mathf.RoundToInt(a * 100);
        float y = Mathf.RoundToInt(b * 100);
        float wynik = x % y;
        if (wynik == 0)
            return true;
        return false;
    }

    float Divide(GameObject slider)
    {
        float divide = 1.0f;
        float sliderValue = slider.GetComponent<Slider>().value;
        if (sliderValue == 1)
            divide = 1.0f;
        else if (sliderValue == 2)
            divide = 0.5f;
        else if (sliderValue == 3)
            divide = 0.25f;
        else if (sliderValue == 4)
            divide = 0.2f;
        else if (sliderValue == 5)
            divide = 0.1f;
        else if (sliderValue == 6)
            divide = 0.05f;
        else if (sliderValue == 7)
            divide = 0.01f;
        return divide;
    }

    public void Dropdown1()
    {
        if(metal1.GetComponent<Dropdown>().value == 0)
        {
            k1 = 80.2f;
            d1 = 7870.0f;
            cp1 = 460.0f;
        }
        else if(metal1.GetComponent<Dropdown>().value == 1)
        {
            k1 = 317.0f;
            d1 = 19300.0f;
            cp1 = 129.0f;
        }
        else if(metal1.GetComponent<Dropdown>().value == 2)
        {
            k1 = 401.0f;
            d1 = 8940.0f;
            cp1 = 385.0f;
        }
    }
    public void Dropdown2()
    {
        if (metal2.GetComponent<Dropdown>().value == 0)
        {
            k2 = 80.0f;
            d2 = 7870.0f;
            cp2 = 460.0f;
        }
        else if (metal2.GetComponent<Dropdown>().value == 1)
        {
            k2 = 317.0f;
            d2 = 19300.0f;
            cp2 = 129.0f;
        }
        else if (metal2.GetComponent<Dropdown>().value == 2)
        {
            k2 = 401.0f;
            d2 = 8940.0f;
            cp2 = 385.0f;
        }
    }
}
