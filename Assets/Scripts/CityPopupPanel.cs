using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CityPopupPanel : MonoBehaviour
{

    public GameObject PopupUi;
    public TextMeshProUGUI CityNameLabel;

    // Start is called before the first frame update
    void Start()
    {
        //PopupUi.SetActive(false);
        CityNameLabel = GameObject.Find("lblCityName").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        PopupUi.SetActive(true);
    }

    public void Close()
    {
        PopupUi.SetActive(false);
    }


}
