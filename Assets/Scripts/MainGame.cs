using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldMapStrategyKit;
using TMPro;

public class MainGame : MonoBehaviour
{
    public GameMode GamePlayMode;

    CityPopupPanel CityPopup;
    MessagePopupPanel MessagePopup;

    Button ManeuverButton;
    Button OrganizationButton;
    Button DiplomacyButton;

    ManeuverPopupPanel ManeuverPopup;
    OrganizationPopupPanel OrgPopup;

    public TextMeshProUGUI StateLabel;
    public TextMeshProUGUI StateIdLabel;
    public TextMeshProUGUI CityLabel;
    public TextMeshProUGUI CityIdLabel;
    public TextMeshProUGUI CityPopulationLabel;

    WMSK map;
    Army MyArmy;

    GameObjectAnimator platoon;
    Province stateinfo;
    City cityinfo;
    // Dictionary<string, string> states;
    States states;

    private void Awake()
    {
        MyArmy = new Army();
    }

    // Start is called before the first frame update
    void Start()
    {
        map = WMSK.instance;
        map.OnProvinceClick += new OnProvinceClickEvent(ProvinceClick);
        map.OnCityClick += new OnCityClick(CityClick);

        states = new States();
        states.LoadStatesList();

        CityPopup = GameObject.Find("CityPopupPanel").GetComponent<CityPopupPanel>();
        CityPopup.Close();

        ManeuverButton = GameObject.Find("ManeuverButton").GetComponent<Button>();
        ManeuverButton.onClick.AddListener(ManeuverButtonOnClick);
        DiplomacyButton = GameObject.Find("DiplomacyButton").GetComponent<Button>();
        DiplomacyButton.onClick.AddListener(DiplomacyButtonOnClick);
        OrganizationButton = GameObject.Find("OrganizationButton").GetComponent<Button>();
        OrganizationButton.onClick.AddListener(OrganizationButtonOnClick);

        ManeuverPopup = GameObject.Find("ManeuverPanel").GetComponent<ManeuverPopupPanel>();
        ManeuverPopup.Close();
        OrgPopup = GameObject.Find("OrganizationPanel").GetComponent<OrganizationPopupPanel>();
        OrgPopup.Close();

        GamePlayMode = GameMode.Organization;

        //Message popup, as with all popups, is active by default, so it just pops up when the maingame script runs for the first time.
        MessagePopup = GameObject.Find("MessagePanel").GetComponent<MessagePopupPanel>();
        MessagePopup.MessageLabel.text = "The year is 2024. A failed presidential election has placed a despot ruler in the White House. Partisan lawmakers have usurped the power of Congress. Opposition thinkers are being silenced.  The country is slipping toward autocratic ruin.\n\n";
        MessagePopup.MessageLabel.text += "Over the years, you have assembled a cadre of faithful followers numbering around 180 trained former soldiers as well as many civilians ready for change.  Your mission is to retake America for the true patriots and restore democratic rule.\n\n";
        MessagePopup.MessageLabel.text += "To succeed, you will need to build up your forces by taking over cities, use diplomacy to pursuade others to join you, and maneuver with stealth to defeat the greater and more experienced forces of the Federals.\n\n";
        MessagePopup.MessageLabel.text += "To begin, use the 'Organization' button to drop your initial forces in the city that will be your HQ. Good luck and God speed.\n";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ManeuverButtonOnClick()
    {
        GamePlayMode = GameMode.Maneuvers;
    }

    void DiplomacyButtonOnClick()
    {
        GamePlayMode = GameMode.Diplomacy;
    }

    void OrganizationButtonOnClick()
    {
        GamePlayMode = GameMode.Organization;
    }

    void ProvinceClick(int provinceIndex, int regionIndex, int buttonIndex)
    {
        //State thisstate;
        stateinfo = map.GetProvince(provinceIndex);
        StateLabel.text = stateinfo.name;
        //CountryLabel.text = map.countryHighlighted.name;
        StateIdLabel.text = provinceIndex.ToString();

        ClearCityInfo();
        //thisstate = states.FindByStateID(provinceIndex);
        
    }


    void CityClick(int cityIndex, int buttonIndex)
    {
        ClearCityInfo();

        cityinfo = map.GetCity(cityIndex);

        if (cityinfo != null)
        {
            CityPopulationLabel.text = cityinfo.population.ToString();
            CityLabel.text = cityinfo.name;
            CityIdLabel.text = cityinfo.uniqueId.ToString();
        }
        else
            CityPopulationLabel.text = "unknown";

        CityPopup.Open();
        CityPopup.CityNameLabel.text = cityinfo.name;

        if(MyArmy.Mode == ArmyMode.Deploying)
        {
            if (MyArmy.IdlePlatoonCount > 0)
            {
                Vector2 citylocation = map.cities[cityIndex].unity2DLocation;
                GameObject platoonGO = Instantiate(Resources.Load<GameObject>("Prefabs/PlatoonPiece"));
                platoon = platoonGO.WMSK_MoveTo(citylocation);
                platoon.autoRotation = true;
                platoon.terrainCapability = TERRAIN_CAPABILITY.OnlyGround;
                platoon.attrib["ID"] = 0;

                MyArmy.IdlePlatoonCount--;
                MyArmy.DeployedPlatoonCount++;
            }
            MyArmy.Mode = ArmyMode.Idle;
            EventManager.TriggerEvent("AllowDeploy");
        }
    }


    void ClearCityInfo()
    {
        CityPopulationLabel.SetText("");
        CityLabel.text = "";
        CityIdLabel.text = "";
    }



    //string GetStateNameFromIndex(int stateindex)
    //{
    //    if(stateinfo.Length > 0)
    //    {

    //    }
    //    return null;
    //}


    //void ConvertStatesToList()
    //{
    //    char[] sep = new char[2];
    //    sep[0] = '(';
    //    sep[1] = ')';

    //    if (stateinfo.Length > 0)
    //    {
    //        for(int i = 0; i < stateinfo.Length; i++)
    //        {
    //            Province stateline = stateinfo[i];
    //            string statename = stateline.name;
    //        }

        

    //    }
    //}
    
}

public enum GameMode
{
    Maneuvers,
    Organization,
    Diplomacy
};
