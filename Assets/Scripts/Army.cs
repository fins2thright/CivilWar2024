using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WorldMapStrategyKit;

public class Army
{
    public static Army Instance { get; private set; }

    //public List<PlatoonBehaviors> Platoons;

    public string Name { get; set; }
    public int TotalPlatoonCount { get; set; }
    public int DeployedPlatoonCount { get; set; }
    public int IdlePlatoonCount { get; set; }

    public List<GameObject> Platoons;

    public GameObject SelectedPlatoon;
    public bool PlatoonSelected;

    public ArmyMode Mode { get; set; }

    public Army()
    {
        Instance = this;

        TotalPlatoonCount = 0;
        DeployedPlatoonCount = 0;
        IdlePlatoonCount = 0;

        Platoons = new List<GameObject>();
        Name = "Patriots";

        Mode = ArmyMode.Idle;
        PlatoonSelected = false;

        // Add default platoons for a new game
        for(int i = 0; i < 4; i++)
        {
            AddIdlePlatoon();
        }
    } 
    

    public void AddDeployedPlatoon(GameObject newplatoon)
    {
        Platoons.Add(newplatoon);
        //TotalPlatoonCount++;
        DeployedPlatoonCount++;
        IdlePlatoonCount--;
    }


    public void AddDeployedPlatoon(Vector2 loc)
    {
        GameObject platoonGO = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/PlatoonPiece"));
        //PlatoonBehaviors pb = platoonGO.GetComponent<PlatoonBehaviors>();
        GameObjectAnimator platoon = platoonGO.WMSK_MoveTo(loc);
        platoon.autoRotation = true;
        platoon.terrainCapability = TERRAIN_CAPABILITY.OnlyGround;
        platoon.attrib["Id"] = platoon.uniqueId;
        platoon.attrib["Deployed"] = true;
        platoon.attrib["location"] = JsonUtility.ToJson(loc);

        Platoons.Add(platoonGO);
     
        DeployedPlatoonCount++;
        IdlePlatoonCount--;
    }

    public void AddIdlePlatoon()
    {
        TotalPlatoonCount++;
        IdlePlatoonCount++;
    }


    public void RemoveDeployedPlatoon(GameObject platoon)
    {
        if (platoon.GetComponent<GameObjectAnimator>().attrib["deployed"] == true)
        {
            DeployedPlatoonCount--;      
        }
        else
        {
            IdlePlatoonCount--;
        }
        Platoons.Remove(platoon);
        TotalPlatoonCount--;    
    }




    public void SelectPlatoon(GameObject obj)
    {
        SelectedPlatoon = obj;
    }


    public void SelectPlatoon(Cell cell)
    {
        PlatoonSelected = false;

        foreach (var platoon in Platoons)
        {
            GameObjectAnimator anim = platoon.GetComponent<GameObjectAnimator>();
            if (anim.attrib["Deployed"] == true)
            {
                Vector2 platoonloc = JsonUtility.FromJson<Vector2>(anim.attrib["location"]);
                if (cell.Contains(platoonloc))
                {
                    SelectedPlatoon = platoon;
                    PlatoonSelected = true;
                    Renderer renderer = anim.GetComponentInChildren<Renderer>();
                    anim.attrib["color"] = renderer.sharedMaterial.color;
                    renderer.material.color = Color.yellow;
                    break;
                }
            }
        }
    }


    public bool OccupiesCell(Cell cell)
    {
        foreach (var platoon in Platoons)
        {
            GameObjectAnimator anim = platoon.GetComponent<GameObjectAnimator>();
            if (anim.attrib["Deployed"] == true)
            {
                Vector2 platoonloc = JsonUtility.FromJson<Vector2>(anim.attrib["location"]);
                if (cell.Contains(platoonloc))
                {
                    return true;
                }
            }
        }
        return false;
    }


    public bool MoveSelectedPlatoon(Vector2 celllocation)
    {
        Debug.Log("Platoon Moving");

        GameObjectAnimator anim = SelectedPlatoon.GetComponent<GameObjectAnimator>();
        
        bool canMove = anim.MoveTo(celllocation, 1.0f);
        if (!canMove)
        {
            Debug.Log("Can't move platoon to destination!");
            return false;
        }
        else
        {
            anim.attrib["location"] = JsonUtility.ToJson(celllocation);
            UnSelectPlatoon();
            return true;
        }
    }

    public void UnSelectPlatoon()
    {
        if (SelectedPlatoon != null)
        {
            GameObjectAnimator anim = SelectedPlatoon.GetComponent<GameObjectAnimator>();
            Renderer renderer = anim.GetComponentInChildren<Renderer>();
            anim.attrib["color"] = renderer.sharedMaterial.color;
            renderer.material.color = Color.red;
            PlatoonSelected = false;
            SelectedPlatoon = null;
        }
    }

    public void CancelManeuvers()
    {
        UnSelectPlatoon();
        Mode = ArmyMode.Idle;
    }

    public void CancelDeployments()
    {
        Mode = ArmyMode.Idle;
    }
}


public enum ArmyMode
{
    Deploying, 
    Maneuvering,
    Idle
}
