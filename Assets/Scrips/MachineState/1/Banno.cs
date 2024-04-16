using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banno : State
{
    

    void Start()
    {
        LoadComponent();
    }
    public override void LoadComponent()
    {
        base.LoadComponent();
        type = TypeState.Banno;
    }
    // Update is called once per frame
     
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
        LasStats.sueno = Mathf.Clamp(LasStats.sueno - Time.deltaTime * 0.25f, 0, 100);
        LasStats.wc = Mathf.Clamp(LasStats.wc + Time.deltaTime * 50, 0, 100);

        if (LasStats.wc == 100)
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
