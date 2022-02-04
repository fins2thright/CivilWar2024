using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }

    public double SecondsMultiplier;
    public double TotalElapsedTime;
    private double LastTotal;
    private bool paused;


    public int TotalElapsedSeconds
    {
        get; set;
    }

    public int TotalElapsedMinutes
    {
        get; set;    
    }

    public int TotalElapsedHours
    {
        get; set;
    }

    public int TotalElapsedDays
    {
        get; set;
    }


    public GameTimer()
    {
        Instance = this;
        SecondsMultiplier = 3600;
    }

    
    void Start()
    {
        TotalElapsedTime = SecondsMultiplier * Time.deltaTime;
        LastTotal = TotalElapsedTime;
        paused = false;
    }

    void Update()
    {
        if (paused)
        {
            TotalElapsedTime = LastTotal;
            CalcIntervals();
        }
        else
        {
            TotalElapsedTime += SecondsMultiplier * Time.deltaTime;
            CalcIntervals();
            LastTotal = TotalElapsedTime;
        }
    }

    void CalcIntervals()
    {
        TotalElapsedSeconds = Convert.ToInt32(TotalElapsedTime % 60);
        TotalElapsedMinutes = (int)Math.Floor((TotalElapsedTime / 60) % 60);
        TotalElapsedHours = (int)Math.Floor((TotalElapsedTime / 3600) % 24);
        TotalElapsedDays = (int)Math.Floor(TotalElapsedTime / 86400);
    }

    public void Pause()
    {
        paused = true;
    }

    public void Resume()
    {
        paused = false;
    }

    public void Reset()
    {
        TotalElapsedTime = 0;
        LastTotal = 0;
    }
}
