using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TimeAdjust()
    {
        float value = gameObject.GetComponent<Slider>().value;
        value = Mathf.Round(value * 10) / 10.0f;
        gameObject.GetComponent<Slider>().value = value;
    }
}
