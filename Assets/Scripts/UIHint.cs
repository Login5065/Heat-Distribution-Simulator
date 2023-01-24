using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hint;
    // Start is called before the first frame update
    void Start()
    {
        hint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hint.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hint.SetActive(false);
    }
}
