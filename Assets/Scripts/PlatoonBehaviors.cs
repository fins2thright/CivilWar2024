using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WorldMapStrategyKit;

public class PlatoonParams : IEventParam
{
    public bool MoveEnabled;
}


public class PlatoonBehaviors : MonoBehaviour
{
    Army myArmy;
    //public PlatoonProps PlatoonProperties { get; set; }
    
    public bool MoveEnabled { get; set; }

    private Action<IEventParam> PlatoonMoveEnableListener;


    private void Awake()
    {
        myArmy = Army.Instance;
        //PlatoonProperties = new PlatoonProps();
        PlatoonMoveEnableListener = new Action<IEventParam>(SetMoveEnabled);
        MoveEnabled = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EventManager.StartListening("ToggleMoveEnabled", PlatoonMoveEnableListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("ToggleMoveEnabled", PlatoonMoveEnableListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SetMoveEnabled(IEventParam eventparams)
    {
        PlatoonParams pparams = (PlatoonParams)eventparams;
        MoveEnabled = pparams.MoveEnabled;
    }


    //private void OnMouseUpAsButton()
    //{
    //    Debug.Log("Platoon Clicked!");
    //    GameObject parentobj = gameObject;
    //    GameObjectAnimator anim = parentobj.GetComponent<GameObjectAnimator>();
    //    myArmy.SelectedPlatoon = anim.attrib["Id"];
    //}
}


public class PlatoonProps
{
    public int Id;
    public int DeployedCity { get; set; }
    public PlatoonStatus Status { get; set; }
    public int SoldierCount { get; set; }
}


public enum PlatoonStatus
{
    Deployed,
    Captured,
    Undeployed
};
