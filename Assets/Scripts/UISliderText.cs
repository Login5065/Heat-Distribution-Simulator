using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISliderText : MonoBehaviour
{
    public GameObject slider;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Text>().text = slider.GetComponent<Slider>().value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnUpdateText()
    {
        this.gameObject.GetComponent<Text>().text = slider.GetComponent<Slider>().value.ToString();
    }
}
