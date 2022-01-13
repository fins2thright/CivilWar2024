using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States
{
    public List<State> StatesList;
    
    public States()
    {
        StatesList = new List<State>();
        LoadStatesList();
    }
    
    public List<State> LoadStatesList()
    {
        State texas = new State();
        texas.ID = 4297;
        texas.Name = "Texas";
        texas.Population = 1000000;
        StatesList.Add(texas);

        return StatesList;
    }

    public State FindByStateID(int Id)
    {
        var state = StatesList.Find(o => o.ID == Id);
        return state as State;
    }
}


public class State
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Population { get; set; }
}
