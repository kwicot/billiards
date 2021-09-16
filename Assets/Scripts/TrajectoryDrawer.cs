using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer mainLineRender;
    [SerializeField] private LineRenderer ballLineRender;
    [SerializeField] private LineRenderer ball2LineRender;
    void Start()
    {
        EventManagers.CueEvents.OnCueDrag += OnCueDrag;
    }

    private void OnCueDrag(object sender, CueEventHandler handler)
    {
        Ray ray = new Ray(handler.ballTransform.position, (handler.ballTransform.position - handler.cuePosition) * 4);
        if (Physics.Raycast(ray, out RaycastHit hit, 5))
        {
            mainLineRender.SetPosition(0,handler.ballTransform.position);
            mainLineRender.SetPosition(1, hit.point);
            //TODO алгоритм просчета траектории шаров

        }
        else
        {
            mainLineRender.SetPosition(0,handler.ballTransform.position);
            mainLineRender.SetPosition(1, (handler.ballTransform.position - handler.cuePosition) * 4);
            
        }

        Debug.Log("Trajecory");
    }
}
