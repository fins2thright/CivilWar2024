using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldMapStrategyKit;
using TMPro;
using System;

public class MainGame : MonoBehaviour
{
    public GameMode GamePlayMode;

    CityPopupPanel CityPopup;
    MessagePopupPanel MessagePopup;
    ManeuverPopupPanel ManeuverPopup;
    OrganizationPopupPanel OrgPopup;


    Button ManeuverButton;
    Button OrganizationButton;
    Button DiplomacyButton;

    

    public TextMeshProUGUI StateLabel;
    public TextMeshProUGUI StateIdLabel;
    public TextMeshProUGUI CityLabel;
    public TextMeshProUGUI CityIdLabel;
    public TextMeshProUGUI CityPopulationLabel;
    public TextMeshProUGUI ElapsedTimeLabel;

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
        map.OnCellClick += new OnCellClickEvent(CellClick);

        states = new States();
        states.LoadStatesList();

        CityPopup = GameObject.Find("CityPopupPanel").GetComponent<CityPopupPanel>();
        CityPopup.Close();
        ManeuverPopup = GameObject.Find("ManeuverPanel").GetComponent<ManeuverPopupPanel>();
        ManeuverPopup.Close();
        OrgPopup = GameObject.Find("OrganizationPanel").GetComponent<OrganizationPopupPanel>();
        OrgPopup.Close();

        ManeuverButton = GameObject.Find("ManeuverButton").GetComponent<Button>();
        ManeuverButton.onClick.AddListener(ManeuverButtonOnClick);
        DiplomacyButton = GameObject.Find("DiplomacyButton").GetComponent<Button>();
        DiplomacyButton.onClick.AddListener(DiplomacyButtonOnClick);
        OrganizationButton = GameObject.Find("OrganizationButton").GetComponent<Button>();
        OrganizationButton.onClick.AddListener(OrganizationButtonOnClick);  

        GamePlayMode = GameMode.Idle;

        //Message popup, as with all popups, is active by default, so it just pops up when the maingame script runs for the first time.
        MessagePopup = GameObject.Find("MessagePanel").GetComponent<MessagePopupPanel>();
        MessagePopup.MessageLabel.text = "The year is 2024. A failed presidential election has placed a despot ruler in the White House. Partisan lawmakers have usurped the power of Congress. Opposition thinkers are being silenced.  The country is slipping toward autocratic ruin.\n\n";
        MessagePopup.MessageLabel.text += "Over the years, you have assembled a cadre of faithful followers numbering around 180 trained former soldiers as well as many civilians ready for change.  Your mission is to retake America for the true patriots and restore democratic rule.\n\n";
        MessagePopup.MessageLabel.text += "To succeed, you will need to build up your forces by taking over cities, use diplomacy to pursuade others to join you, and maneuver with stealth to defeat the greater and more experienced forces of the Federals.\n\n";
        MessagePopup.MessageLabel.text += "To begin, use the 'Organization' button to deploy your initial forces onto the map. Good luck and God speed.\n";
        
    }


    // Update is called once per frame
    void Update()
    {
        ElapsedTimeLabel.text = string.Format("{0:00}-{1:00}:{2:00}", GameTimer.Instance.TotalElapsedDays, GameTimer.Instance.TotalElapsedHours, GameTimer.Instance.TotalElapsedMinutes);
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


    void CellClick(int cellIndex, int buttonIndex)
    {
        Cell cell = map.cells[cellIndex];
        Vector2 celllocation = cell.center;
        
        if (MyArmy.Mode == ArmyMode.Deploying)
        {
            if (MyArmy.IdlePlatoonCount > 0)
            {
                if (!CellIsOccupied(cell))
                {
                    MyArmy.AddDeployedPlatoon(celllocation);
                }
                else
                {
                    MessagePopup.MessageLabel.text = "Cell is occupied.  Choose another map cell to deploy asset.";
                    MessagePopup.Open(); 
                }
            }
        }
        else if (MyArmy.Mode == ArmyMode.Maneuvering)
        {
            
            if (MyArmy.PlatoonSelected)
            {
                if (!CellIsOccupied(cell))
                {
                    MyArmy.MoveSelectedPlatoon(celllocation);
                }
                else
                {
                    MessagePopup.MessageLabel.text = "Cell is occupied.  Choose another map cell to move asset.";
                    MessagePopup.Open();
                }
            }
            else
            {
                if (CellIsOccupied(cell))
                {
                    MyArmy.SelectPlatoon(cell);
                }
                else
                {
                    MessagePopup.MessageLabel.text = "There is no asset in this cell to choose.  Please choose an asset to move.";
                    MessagePopup.Open();
                }
            }
        }
            
        //This triggers the Organization Panel class to update the Organization UI
        OrganizationParams eventparams = new OrganizationParams();
        EventManager.TriggerEvent("UpdateOrgUI", eventparams);
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

        //if(MyArmy.Mode == ArmyMode.Deploying)
        //{
        //    if (MyArmy.IdlePlatoonCount > 0)
        //    {
        //        Vector2 citylocation = map.cities[cityIndex].unity2DLocation;
        //        GameObject platoonGO = Instantiate(Resources.Load<GameObject>("Prefabs/PlatoonPiece"));
        //        PlatoonBehaviors pb = platoonGO.GetComponent<PlatoonBehaviors>();
        //        pb.ID = platoonGO.GetInstanceID();

        //        platoon = platoonGO.WMSK_MoveTo(citylocation);
        //        platoon.autoRotation = true;
        //        platoon.terrainCapability = TERRAIN_CAPABILITY.OnlyGround;
        //        platoon.attrib["ID"] = 0;

        //        MyArmy.IdlePlatoonCount--;
        //        MyArmy.DeployedPlatoonCount++;
        //    }
        //    MyArmy.Mode = ArmyMode.Idle;

        //    //This triggers the Organization Panel class to update the Organization UI
        //    OrganizationParams eventparams = new OrganizationParams();
        //    EventManager.TriggerEvent("UpdateOrgUI", eventparams);
        //}
    }


    void ClearCityInfo()
    {
        CityPopulationLabel.SetText("");
        CityLabel.text = "";
        CityIdLabel.text = "";
    }


    bool CellIsOccupied(Cell cell)
    {
        if (!MyArmy.OccupiesCell(cell))
        {
            return false;
        }
        else
        {
            return true;
        }
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
    Diplomacy,
    Idle
};
