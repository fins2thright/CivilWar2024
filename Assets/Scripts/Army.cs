using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army
{
    public static Army Instance { get; private set; }

    public List<Platoon> Platoons;
    public string Name { get; set; }
    public int TotalPlatoonCount { get; set; }
    public int DeployedPlatoonCount { get; set; }
    public int IdlePlatoonCount { get; set; }

    public ArmyMode Mode { get; set; }

    public Army()
    {
        Instance = this;

        TotalPlatoonCount = 0;
        DeployedPlatoonCount = 0;
        IdlePlatoonCount = 0;

        Platoons = new List<Platoon>();
        Name = "Patriots";

        Mode = ArmyMode.Idle;

        // Add default platoons for a new game
        for(int i = 0; i < 4; i++)
        {
            Platoon platoon = new Platoon();
            platoon.Id = i;
            platoon.Status = PlatoonStatus.Undeployed;
            platoon.DeployedCity = 0;
            
            Platoons.Add(platoon);

            TotalPlatoonCount++;
            IdlePlatoonCount++;
        }
    }   
}


public class Platoon
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


public enum ArmyMode
{
    Deploying, 
    Maneuvering,
    Idle
}
