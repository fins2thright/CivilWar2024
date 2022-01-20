using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using WorldMapStrategyKit;

public class ManeuverPopupPanel : MonoBehaviour
{

    //WMSK map;
    Army myArmy;

    public GameObject PopupUi;
    public Button PlatoonMoveButton;

    //public TextMeshProUGUI MessageLabel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //map = WMSK.instance;
        myArmy = Army.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        PopupUi.SetActive(true);
        if (myArmy.DeployedPlatoonCount > 0)
        {
            PlatoonMoveButton.enabled = true;
        }
        else
        {
            PlatoonMoveButton.enabled = false;
        }
    }

    public void Close()
    {
        PopupUi.SetActive(false);
    }


    public void MoveTroops()
    {
        myArmy.Mode = ArmyMode.Maneuvering;
    }
}
