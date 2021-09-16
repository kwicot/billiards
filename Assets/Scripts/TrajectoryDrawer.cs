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
        Ray ray = new Ray(handler.cuePosition, handler.cueForward);
        if (Physics.Raycast(ray, out RaycastHit hit, 5))
        {
            mainLineRender.SetPosition(0,handler.cuePosition);
            mainLineRender.SetPosition(1, hit.point);

            Vector3 reflect = Vector3.Reflect(ray.direction, hit.normal);
            ballLineRender.SetPosition(0,hit.point);
            ballLineRender.SetPosition(1, reflect.normalized * 2);

            if (hit.transform.gameObject.TryGetComponent(out BallController _))
            {
                ball2LineRender.SetPosition(0, hit.point);
                ball2LineRender.SetPosition(1, -reflect * 2);
            }
        }

        Debug.Log("Trajecory");
    }
}
