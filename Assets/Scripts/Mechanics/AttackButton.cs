using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.UI;

public class AttackButton : MonoBehaviour {

    public TMPro.TextMeshProUGUI labelText;

    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("Hello");
        labelText.color = Color.white; 
    }
 
    public void OnPointerExit(PointerEventData eventData) {
        labelText.color = Color.black; //Or however you do your color
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
