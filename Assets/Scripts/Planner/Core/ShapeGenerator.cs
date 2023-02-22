using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShapeGenerator : MonoBehaviour
{
    public enum DrawState
    {
        Disabled,
        DrawBase,
        DrawWall
    }

    public DrawState state;

    public ViewController controller = null;

    private LineRenderer lineRenderer = null;

    private Vector3 startPoint = Vector3.zero;
    private Vector3 endPoint = Vector3.zero;

    private Vector2 startScreenPoint = Vector2.zero;
    private Vector2 currentScreenPoint = Vector2.zero;
    private Vector2 delta = Vector2.zero;
    private float height = 0f;

    RaycastHit hit;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if(state == DrawState.DrawBase)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClearAllPoints();

                Ray ray;

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100f))
                {
                    startPoint = hit.point;
                }

                ClickToAddPoint();
            }

            if (Input.GetMouseButton(0))
            {
                Ray ray;

                startScreenPoint = Input.mousePosition;
                ray = Camera.main.ScreenPointToRay(startScreenPoint);

                if (Physics.Raycast(ray, out hit, 100f))
                {
                    endPoint = hit.point;
                }

                DrawQuadWithLineRenderer(startPoint, endPoint);
            }

            if (Input.GetMouseButtonUp(0))
            {
                ClickToAddPoint();
                state = DrawState.DrawWall;
            }
        }
        
        if(state == DrawState.DrawWall)
        {
            currentScreenPoint = Input.mousePosition;

            delta = currentScreenPoint - startScreenPoint;

            height = delta.y / 100f;

            DrawCubeWithLineRenderer(startPoint, endPoint, height);

            if(Input.GetMouseButtonDown(0))
            {
                state = DrawState.Disabled;
            }
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            controller.ModelProperty.GenerateMesh((int)ShapeType.Triangle, controller.ModelProperty.vertices);
        }
    }

    void DrawQuadWithLineRenderer(Vector3 pos1, Vector3 pos2)
    {
        Vector3 p1 = new Vector3(pos1.x, 0f, pos1.z);
        Vector3 p2 = new Vector3(pos1.x, 0f, pos2.z);
        Vector3 p3 = new Vector3(pos2.x, 0f, pos2.z);
        Vector3 p4 = new Vector3(pos2.x, 0f, pos1.z);

        lineRenderer.positionCount = 5;

        lineRenderer.SetPosition(0, p1);
        lineRenderer.SetPosition(1, p2);
        lineRenderer.SetPosition(2, p3);
        lineRenderer.SetPosition(3, p4);
        lineRenderer.SetPosition(4, p1);
    }

    void DrawCubeWithLineRenderer(Vector3 pos1, Vector3 pos2, float vertical)
    {
        //points 

        Vector3 p1 = new Vector3(pos1.x, 0f, pos1.z);
        Vector3 p2 = new Vector3(pos1.x, 0f, pos2.z);
        Vector3 p3 = new Vector3(pos2.x, 0f, pos2.z);
        Vector3 p4 = new Vector3(pos2.x, 0f, pos1.z);

        Vector3 p5 = new Vector3(pos1.x, vertical, pos1.z);
        Vector3 p6 = new Vector3(pos1.x, vertical, pos2.z);
        Vector3 p7 = new Vector3(pos2.x, vertical, pos2.z);
        Vector3 p8 = new Vector3(pos2.x, vertical, pos1.z);

        //lines

        lineRenderer.positionCount = 17;

        lineRenderer.SetPosition(0, p1);
        lineRenderer.SetPosition(1, p2);
        lineRenderer.SetPosition(2, p3);
        lineRenderer.SetPosition(3, p4);
        lineRenderer.SetPosition(4, p1);
        lineRenderer.SetPosition(5, p5);
        lineRenderer.SetPosition(6, p6);
        lineRenderer.SetPosition(7, p7);
        lineRenderer.SetPosition(8, p8);
        lineRenderer.SetPosition(9, p5);
        lineRenderer.SetPosition(10, p2);
        lineRenderer.SetPosition(11, p6);
        lineRenderer.SetPosition(12, p3);
        lineRenderer.SetPosition(13, p7);
        lineRenderer.SetPosition(14, p4);
        lineRenderer.SetPosition(15, p8);
        lineRenderer.SetPosition(16, p1);
    }

    void ClickToAddPoint()
    {
        //generate and add all points to model property
        controller.ModelProperty.vertices.Add(hit.point);
    }

    void ClearAllPoints()
    {
        controller.ModelProperty.vertices.Clear();
    }
}
