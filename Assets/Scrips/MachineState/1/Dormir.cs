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
        if (EstaInvertido)
        {
            ReverseArray();
            EstaInvertido = false;

            ElPathFollowing.currentPointIndex = 0;
            ElPathFollowing.ListaDeTransforms = ListaDeMovimiento;
        }
        ElArrive.Finalizo = false;
        ElPathFollowing.NextPoint();
        if (ElArrive.Finalizo)
        {
            Etapa = EtapaState.Execute;
        }
    }
    public override void Execute()
    {
        LasStats.hambre = Mathf.Clamp(LasStats.hambre - Time.deltaTime * 5, 0, 100);
        LasStats.sueno = Mathf.Clamp(LasStats.sueno + Time.deltaTime * 2, 0, 100);
        LasStats.wc = Mathf.Clamp(LasStats.wc - Time.deltaTime * 3, 0, 100);

        if (LasStats.sueno == 100)
        {
            Etapa = EtapaState.Exit;
            //m_MachineState.NextState(TypeState.Jugar);
        }
    }
    public override void Exit()
    {
        if (!EstaInvertido)
        {
            ReverseArray();
            EstaInvertido = true;
            
            ElPathFollowing.ListaDeTransforms = ListaDeMovimiento;
            ElPathFollowing.currentPointIndex = ElPathFollowing.ListaDeTransforms.Length;
        }
        ElArrive.Finalizo = false;
        ElPathFollowing.NextPoint();
        if (ElArrive.Finalizo)
        {
            m_MachineState.NextState(TypeState.Jugar);
        }
    }
}
