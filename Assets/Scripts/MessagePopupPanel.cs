using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessagePopupPanel : MonoBehaviour
{

    public GameObject PopupUi;
    public TextMeshProUGUI MessageLabel;

    // Start is called before the first frame update
    void Start()
    {
        MessageLabel = GameObject.Find("lblMessage").GetComponent<TextMeshProUGUI>();
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
