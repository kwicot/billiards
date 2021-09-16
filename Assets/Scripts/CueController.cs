using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Object = System.Object;

namespace DefaultNamespace
{
    public class CueController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Transform cueTransform;
        [SerializeField] private Transform targetTransform;
        [SerializeField] private BallController targetBall;
        [SerializeField] private float maxDistance;
        [SerializeField] private float maxStrange;
        private BoxCollider cueCollider;

        private Vector3 startDragPosition;
        private Camera camera;
        private float distanceToCue;

        private CueEventHandler cueData;

        private void Start()
        {
            camera = Camera.main;
            cueData = new CueEventHandler();
            cueCollider = cueTransform.gameObject.TryGetComponent(out BoxCollider box)
                ? box
                : cueTransform.gameObject.AddComponent<BoxCollider>();
            distanceToCue = Vector3.Distance(camera.transform.position, cueTransform.position);

            EventManagers.BallsEvents.OnBallsStoped += OnBallsStoped;
        }

        private void OnBallsStoped(object sender)
        {
            Debug.Log("Balls stoped");
            MoveCue(cueData.cuePosition);
        }


        public void SetTarget(BallController ball)
        {
            targetBall = ball;
            targetTransform = ball.transform;
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            startDragPosition = GetClickPosition(eventData.position);
            cueData.ballTransform = targetTransform;
            Debug.Log("BeginDrag");
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 position = GetClickPosition(eventData.position);
            MoveCue(position);
            cueData.strange = CalculateStrange();
            cueData.cueForward = cueTransform.forward;
            cueData.cuePosition = cueTransform.position;
            
            EventManagers.CueEvents.OnCueDrag?.Invoke(this,cueData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            cueData.cueForward = cueTransform.forward;
            cueData.cuePosition = cueTransform.position;
            cueCollider.enabled = false;
            cueTransform.DOMove(targetTransform.position, 0.1f)
                .SetDelay(0.2f)
                .OnComplete(delegate
                {
                    EventManagers.CueEvents.OnCueHitBall?.Invoke(this, cueData);
                    targetBall.Hit(cueData);
                    cueTransform.DOMove(-cueTransform.forward * 10, 1f)
                        .OnComplete(delegate
                        {
                            cueCollider.enabled = true;
                        });
                }); 
        }

        Vector3 GetClickPosition(Vector3 eventPosition)
        {
            Vector3 clickPoint = eventPosition;
            clickPoint.z = distanceToCue;
            return camera.ScreenToWorldPoint(clickPoint);
        }

        void MoveCue(Vector3 cursorPosition)
        {
            cursorPosition.y = targetTransform.position.y;
            
            var distance = Vector3.Distance(targetTransform.position, cursorPosition);
            Vector3 directToClick = cursorPosition - targetTransform.position;
            directToClick.Normalize();
            

            if (distance <= 0.2f)
                cursorPosition = targetTransform.position + (directToClick * 0.2f);
            else if (distance > maxDistance)
                cursorPosition = targetTransform.position + (directToClick * maxDistance);


            cueTransform.position = cursorPosition; 
            /*BAD*/ cueTransform.LookAt(targetTransform);
              
        }

        private float CalculateStrange()
        {
            float distance = Vector3.Distance(cueTransform.position, targetTransform.position);
            float coeff = distance / maxDistance;
            return maxStrange * coeff;
        }
    }
}