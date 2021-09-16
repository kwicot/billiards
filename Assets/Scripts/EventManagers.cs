using UnityEngine;

namespace DefaultNamespace
{
    public static class EventManagers
    {
        public static CueEvents CueEvents = new CueEvents();
        public static BallsEvents BallsEvents = new BallsEvents();
    }

    public class CueEvents
    {
        public delegate void customEvent(object sender, CueEventHandler handler);

        public customEvent OnCueDrag;
        public customEvent OnCueHitBall;
    }

    public class BallsEvents
    {
        public delegate void customEvent(object sender, BallEventHandler handler);

        public delegate void customEvent2(object sender);

        public customEvent2 OnBallsStoped;
        public customEvent OnBallSelected;
    }


    public class CueEventHandler
    {
        public Vector3 cuePosition;
        public Vector3 cueForward;
        public float strange;
        public Transform ballTransform;
    }

    public class BallEventHandler
    {
        public BallController selectedBall;
    }
}