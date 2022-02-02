using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using WorldMapStrategyKit;
using UnityEngine.Events;


public class OrganizationParams : IEventParam
{
    //Nothing here yet
}

public class OrganizationPopupPanel : MonoBehaviour
{

    public GameObject PopupUi;
    public TextMeshProUGUI PlatoonCountLabel;
    public TextMeshProUGUI PlatoonIdleCountLabel;
    public Button PlatoonDeployButton;

    //WMSK map;
    Army myArmy;
    private Action<IEventParam> uiUpdateListener;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //map = WMSK.instance;
        myArmy = Army.Instance;
        uiUpdateListener = new Action<IEventParam>(SetUIStatus);
    }

    private void OnEnable()
    {
        EventManager.StartListening("UpdateOrgUI", uiUpdateListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("UpdateOrgUI", uiUpdateListener);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Open()
    {
        PopupUi.SetActive(true);
        OrganizationParams eventparams = new OrganizationParams();
        SetUIStatus(eventparams);
    }


    public void Close()
    {
        PopupUi.SetActive(false);
    }


    public void Deploy()
    {
        myArmy.Mode = ArmyMode.Deploying;
    }


    void SetUIStatus(IEventParam eventparams)
    {
        PlatoonCountLabel.text = myArmy.TotalPlatoonCount.ToString();
        PlatoonIdleCountLabel.text = myArmy.IdlePlatoonCount.ToString();

        if (myArmy.IdlePlatoonCount > 0)
        {
            PlatoonDeployButton.enabled = true;
        }
        else
        {
            PlatoonDeployButton.enabled = false;
        }
    }
}

