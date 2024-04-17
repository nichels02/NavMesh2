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
        ElSensor = GetComponent<VisionSensor>();
    }
    // Update is called once per frame


    public override void Enter()
    {
        ElArrive.LaAccion = QueAccion.wander;
        ElArrive.randomPoint();
        Etapa = EtapaState.Execute;
    }
    public override void Execute()
    {
        if (ElArrive.agent.remainingDistance < ElArrive.agent.stoppingDistance)
        {
            if (ElArrive.LaAccion == QueAccion.SensorDetected)
            {
                ElArrive.LaAccion = QueAccion.wander;
                ElSensor.EnemyView.transform.parent = this.transform;
                ElSensor.EnemyView.GetComponent<Collider>().enabled = false;
            }
            ElArrive.randomPoint();
        }
        else if (ElSensor.EnemyView != null)
        {
            ElArrive.LaAccion = QueAccion.SensorDetected;
            ElArrive.target = ElSensor.EnemyView.transform;
            ElArrive.CambiarPuntoDeSeguimiento();
        }
        else
        {
            LasStats.hambre = Mathf.Clamp(LasStats.hambre - Time.deltaTime * 2.5f, 0, 100);
            LasStats.sueno = Mathf.Clamp(LasStats.sueno - Time.deltaTime * 0.25f, 0, 100);
            LasStats.wc = Mathf.Clamp(LasStats.wc - Time.deltaTime * 1.5f, 0, 100);
        }


        if (LasStats.wc == 0)
        {
            ElArrive.target = null;
            EliminarHijos();
            m_MachineState.NextState(TypeState.Banno);
        }
        else if (LasStats.hambre == 0)
        {
            ElArrive.target = null;
            EliminarHijos();
            m_MachineState.NextState(TypeState.Comer);
        }
        else if (LasStats.sueno == 0)
        {
            ElArrive.target = null;
            EliminarHijos();
            m_MachineState.NextState(TypeState.Dormir);
        }
    }
    public override void Exit()
    {

    }


    public void EliminarHijos()
    {
        int childCount = transform.childCount;
        Transform child;
        // Recorrer todos los hijos
        for (int i = 0; i < childCount; i++)
        {
            // Obtener el hijo en el índice actual
            child = transform.GetChild(i);
            child.GetComponent<Collider>().enabled = true;
            child.transform.parent = null;

        }
    }
}
