using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Jugar : State
{
    [SerializeField] VisionSensor ElSensor;
     
    void Start()
    {
        
        LoadComponent();
    }
    
    public override void LoadComponent()
    {
        base.LoadComponent();
        type = TypeState.Jugar;
    }
    // Update is called once per frame
    

    public override void Enter()
    {
        ElArrive.randomPoint();
        Etapa = EtapaState.Execute;
    }
    public override void Execute()
    {
        if (ElArrive.agent.remainingDistance < ElArrive.agent.stoppingDistance)
        {
            ElArrive.randomPoint();
        }
        /*
        else if (ElSensor.EnemyView != null)
        {
            ElArrive.target = ElSensor.EnemyView.transform;
            ElArrive.CambiarPuntoDeSeguimiento();
        }
        */
        else
        {
            LasStats.hambre = Mathf.Clamp(LasStats.hambre - Time.deltaTime * 2.5f, 0, 100);
            LasStats.sueno = Mathf.Clamp(LasStats.sueno - Time.deltaTime * 0.25f, 0, 100);
            LasStats.wc = Mathf.Clamp(LasStats.wc - Time.deltaTime * 1.5f, 0, 100);
        }
        

        if (LasStats.wc == 0)
        {
            m_MachineState.NextState(TypeState.Banno);
        }
        else if (LasStats.hambre == 0)
        {
            m_MachineState.NextState(TypeState.Comer);
        }
        else if (LasStats.sueno == 0)
        {
            m_MachineState.NextState(TypeState.Dormir);
        }
    }
    public override void Exit()
    {

    }
}
