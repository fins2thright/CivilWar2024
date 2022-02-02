using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using WorldMapStrategyKit;


public class ManeuverParams : IEventParam
{
    //Nothing here yet
}


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
        ManeuverParams eventparams = new ManeuverParams();
        SetUIStatus(eventparams);
    }

    public void Close()
    {
        PopupUi.SetActive(false);
    }


    public void MoveTroops()
    {
        myArmy.Mode = ArmyMode.Maneuvering;
    }


    void SetUIStatus(IEventParam eventparams)
    {
        if (myArmy.DeployedPlatoonCount > 0)
        {
            PlatoonMoveButton.enabled = true;
            myArmy.Mode = ArmyMode.Maneuvering;
        }
        else
        {
            PlatoonMoveButton.enabled = false;
            myArmy.Mode = ArmyMode.Idle;
        }
    }
}
