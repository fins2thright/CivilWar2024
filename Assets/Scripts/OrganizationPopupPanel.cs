using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using WorldMapStrategyKit;
using UnityEngine.Events;

public class OrganizationPopupPanel : MonoBehaviour
{

    public GameObject PopupUi;
    public TextMeshProUGUI PlatoonCountLabel;
    public TextMeshProUGUI PlatoonIdleCountLabel;
    public Button PlatoonDeployButton;

    //WMSK map;
    Army myArmy;
    private UnityAction deployListener;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //map = WMSK.instance;
        myArmy = Army.Instance;
        deployListener = new UnityAction(SetUIStatus);
    }

    private void OnEnable()
    {
        EventManager.StartListening("AllowDeploy", deployListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("AllowDeploy", deployListener);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Open()
    {
        PopupUi.SetActive(true);
        SetUIStatus();
    }

    public void Close()
    {
        PopupUi.SetActive(false);
    }


    public void Deploy()
    {
        myArmy.Mode = ArmyMode.Deploying;
        SetUIStatus();
    }

    void SetUIStatus()
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

