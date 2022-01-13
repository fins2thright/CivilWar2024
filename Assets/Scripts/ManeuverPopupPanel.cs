using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManeuverPopupPanel : MonoBehaviour
{

    public GameObject PopupUi;
    //public TextMeshProUGUI MessageLabel;

    // Start is called before the first frame update
    void Start()
    {
        
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
