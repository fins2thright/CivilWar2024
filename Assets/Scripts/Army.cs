using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Army
{
    public List<Platoon> Platoons;
    public string Name { get; set; }
    public int PlatoonCount { get; set; }

    public Army()
    {
        PlatoonCount = 0;
        Platoons = new List<Platoon>();
        Name = "Patriots";

        // Add default platoons for a new game
        for(int i = 0; i < 4; i++)
        {
            Platoon platoon = new Platoon();
            platoon.Id = i;
            platoon.Status = PlatoonStatus.Undeployed;
            platoon.DeployedCity = 0;
            PlatoonCount++;
            Platoons.Add(platoon);
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
