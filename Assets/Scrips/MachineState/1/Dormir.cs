using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dormir : State
{
     
    void Start()
    {
        LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
        type = TypeState.Dormir;
    }

    public override void Enter()
    {
        if (ElArrive.target == null)
        {
            ElArrive.target = ListaDeMovimiento[0];
            ElArrive.CambiarPuntoDeSeguimiento();
        }
        else if (ElArrive.agent.remainingDistance <= ElArrive.agent.stoppingDistance)
        {
            ElArrive.target = null;
            Etapa = EtapaState.Execute;
        }
        else
        {
            LasStats.hambre = Mathf.Clamp(LasStats.hambre - Time.deltaTime * 2.5f, 0, 100);
            LasStats.sueno = Mathf.Clamp(LasStats.sueno - Time.deltaTime * 0.25f, 0, 100);
            LasStats.wc = Mathf.Clamp(LasStats.wc - Time.deltaTime * 1.5f, 0, 100);
        }
    }
    public override void Execute()
    {
        LasStats.hambre = Mathf.Clamp(LasStats.hambre - Time.deltaTime * 2.5f, 0, 100);
        LasStats.sueno = Mathf.Clamp(LasStats.sueno + Time.deltaTime * 2, 0, 100);
        LasStats.wc = Mathf.Clamp(LasStats.wc - Time.deltaTime * 1.5f, 0, 100);

        if (LasStats.sueno == 100)
        {
            Etapa = EtapaState.Exit;
            //m_MachineState.NextState(TypeState.Jugar);
        }
    }
    public override void Exit()
    {
        if (ElArrive.target == null)
        {
            ElArrive.target = ListaDeMovimiento[1];
            ElArrive.CambiarPuntoDeSeguimiento();
        }
        else if (ElArrive.agent.remainingDistance <= ElArrive.agent.stoppingDistance)
        {
            ElArrive.target = null;
            Etapa = EtapaState.Enter;
            m_MachineState.NextState(TypeState.Jugar);
        }
        else
        {
            LasStats.hambre = Mathf.Clamp(LasStats.hambre - Time.deltaTime * 2.5f, 0, 100);
            LasStats.sueno = Mathf.Clamp(LasStats.sueno - Time.deltaTime * 0.25f, 0, 100);
            LasStats.wc = Mathf.Clamp(LasStats.wc - Time.deltaTime * 1.5f, 0, 100);
        }
    }
}
