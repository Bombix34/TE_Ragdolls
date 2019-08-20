using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField]
    private float ropeSegLenght = 0.25f;
    [SerializeField]
    private int segmentLenght = 35;
    [SerializeField]
    private float ropeWidth = 0.1f;

    [SerializeField]
    private float gravityForce = 1f;

    [SerializeField]
    private int accuracy = 50;

    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();

    Vector2 randPos;


    private void Awake()
    {
        this.lineRenderer = GetComponent<LineRenderer>();
        segmentLenght = (int)Random.Range(10, 30);
        ropeWidth = Random.Range(0.01f, 0.06f);

        //test
        //randPos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }

    private void Start()
    {
        InitRope();
    }

    private void Update()
    {
        DrawRope();
    }

    private void FixedUpdate()
    {
        UpdateRopePhysics();
    }


    public void InitRope()
    {
        Vector3 startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startPoint = new Vector3(startPoint.x + randPos.x, startPoint.y + randPos.y, 0f);
        for (int i = 0; i < segmentLenght; i++)
        {
            this.ropeSegments.Add(new RopeSegment(startPoint));
            startPoint.y -= ropeSegLenght;
        }
    }

    private void DrawRope()
    {
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;
        Vector3[] ropePositions = new Vector3[segmentLenght];
        for(int i = 0; i < segmentLenght; i++)
        {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }
        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    private void UpdateRopePhysics()
    {
        SimulateRope();
        for(int i =0;i<accuracy;i++)
        {
            ConstraintRope();
        }
    }

    private void SimulateRope()
    {
        Vector2 gravity = new Vector2(0f, -gravityForce);

        for(int i =0; i < this.segmentLenght; i++) {
            //Verlet Integration
            RopeSegment curSegment = this.ropeSegments[i];
            Vector2 velocity = curSegment.posNow - curSegment.posOld;
            curSegment.posOld = curSegment.posNow;
            curSegment.posNow += velocity + (gravity * Time.deltaTime);
            this.ropeSegments[i] = curSegment;
        }
    }

    private void ConstraintRope() {
        //l'origine de la corde est la position du curseur de la souris
        RopeSegment curSegment = this.ropeSegments[0];
        curSegment.posNow = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.ropeSegments[0] = curSegment;
        RopeSegment nextSegment;
        for(int i =0; i < this.segmentLenght-1;i++){
            curSegment = this.ropeSegments[i];
            nextSegment = this.ropeSegments[i + 1];
            float dist = (curSegment.posNow - nextSegment.posNow).magnitude;
            float error = Mathf.Abs(dist - this.ropeSegLenght);
            Vector2 changeDir = Vector2.zero;
            if (dist > this.ropeSegLenght)
                changeDir = (curSegment.posNow - nextSegment.posNow).normalized;
            else
                changeDir = (nextSegment.posNow - curSegment.posNow).normalized;
            Vector2 changeAmount = changeDir * error;
            if(i!=0) {
                curSegment.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = curSegment;
                nextSegment.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = nextSegment;
            }
            else{
                nextSegment.posNow += changeAmount;
                this.ropeSegments[i + 1] = nextSegment;
            }
            
        }
    }

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}
