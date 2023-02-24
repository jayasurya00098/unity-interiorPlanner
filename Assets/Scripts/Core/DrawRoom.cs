using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawRoom : MonoBehaviour
{
    public static DrawRoom Instance;

    public enum DrawState
    {
        Disabled,
        DrawBase,
        DrawWall
    }

    public DrawState state;

    [SerializeField]
    private Material defaultMaterial;

    private ViewController controller = null;
    private LineRenderer lineRenderer = null;

    [SerializeField]
    private Vector3 startPoint = Vector3.zero;
    [SerializeField]
    private Vector3 endPoint = Vector3.zero;

    private Vector2 startScreenPoint = Vector2.zero;
    private Vector2 currentScreenPoint = Vector2.zero;
    private Vector2 delta = Vector2.zero;
    private float height = 0f;

    RaycastHit hit;

    private void Start()
    {
        Instance = this;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if(state == DrawState.DrawBase)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray;

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100f))
                {
                    startPoint = hit.point;
                }
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
                CreateCube(startPoint, endPoint, height);
            }

            if (Input.GetMouseButtonUp(0))
            {
                state = DrawState.DrawWall;
            }
        }
        
        if(state == DrawState.DrawWall)
        {
            currentScreenPoint = Input.mousePosition;

            delta = currentScreenPoint - startScreenPoint;

            height = delta.y / 100f;

            CreateCube(startPoint, endPoint, height);
            DrawCubeWithLineRenderer(startPoint, endPoint, height);

            if(Input.GetMouseButtonDown(0))
            {
                CreateCube(startPoint, endPoint, height);

                Collider meshCol = controller.gameObject.AddComponent<MeshCollider>();
                meshCol.tag = "Room";

                //load vertices data to room
                controller.GetModel.LoadData(PlannerController.Instance.room.vertices);
                //allow add objects & save
                PlannerController.Instance.SwitchPanel(1);

                //reset
                Reset();
            }
        }
    }

    private void Reset()
    {
        lineRenderer.positionCount = 0;

        startPoint = Vector3.zero;
        endPoint = Vector3.zero;

        startScreenPoint = Vector2.zero;
        currentScreenPoint = Vector2.zero;
        delta = Vector2.zero;
        height = 0f;

        state = DrawState.Disabled;
    }

    public void Draw()
    {
        GameObject go = new GameObject();
        go.name = "Room";
        controller = go.AddComponent<ViewController>();

        StartCoroutine(SwitchState());
    }

    void DrawQuadWithLineRenderer(Vector3 pos1, Vector3 pos2)
    {
        Vector3 p1 = new Vector3(pos1.x, 0.01f, pos1.z);
        Vector3 p2 = new Vector3(pos1.x, 0.01f, pos2.z);
        Vector3 p3 = new Vector3(pos2.x, 0.01f, pos2.z);
        Vector3 p4 = new Vector3(pos2.x, 0.01f, pos1.z);

        lineRenderer.positionCount = 5;

        lineRenderer.SetPosition(0, p1);
        lineRenderer.SetPosition(1, p2);
        lineRenderer.SetPosition(2, p3);
        lineRenderer.SetPosition(3, p4);
        lineRenderer.SetPosition(4, p1);
    }

    void DrawCubeWithLineRenderer(Vector3 pos1, Vector3 pos2, float vertical)
    {
        //base 
        Vector3 p1 = new Vector3(pos1.x, 0.01f, pos1.z);
        Vector3 p2 = new Vector3(pos1.x, 0.01f, pos2.z);
        Vector3 p3 = new Vector3(pos2.x, 0.01f, pos2.z);
        Vector3 p4 = new Vector3(pos2.x, 0.01f, pos1.z);
        //top
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

    void CreateCube(Vector3 pos1, Vector3 pos2, float vertical)
    {
        //patch for inverted error
        if ((pos2.x > pos1.x && pos2.z > pos1.z) || (pos2.x < pos1.x && pos2.z < pos1.z))
        {
            controller.GetModel.invert = true;
            Debug.Log(controller.GetModel.invert);
        }
        else
        {
            controller.GetModel.invert = false;
            Debug.Log(controller.GetModel.invert);
        }

        //base 
        Vector3 p1 = new Vector3(pos1.x, 0.01f, pos1.z);
        Vector3 p2 = new Vector3(pos1.x, 0.01f, pos2.z);
        Vector3 p3 = new Vector3(pos2.x, 0.01f, pos2.z);
        Vector3 p4 = new Vector3(pos2.x, 0.01f, pos1.z);
        //top
        Vector3 p5 = new Vector3(pos1.x, vertical, pos1.z);
        Vector3 p6 = new Vector3(pos1.x, vertical, pos2.z);
        Vector3 p7 = new Vector3(pos2.x, vertical, pos2.z);
        Vector3 p8 = new Vector3(pos2.x, vertical, pos1.z);

        //back
        controller.GetModel.vertices[0] = p7;
        controller.GetModel.vertices[1] = p6;
        controller.GetModel.vertices[2] = p2;
        controller.GetModel.vertices[3] = p3;
        //front
        controller.GetModel.vertices[4] = p5;
        controller.GetModel.vertices[5] = p8;
        controller.GetModel.vertices[6] = p4;
        controller.GetModel.vertices[7] = p1;

        controller.GetModel.meshRenderer.material = defaultMaterial;
        controller.GetModel.GenerateCube();
        controller.GetModel.UpdateMesh();
    }

    IEnumerator SwitchState()
    {
        yield return new WaitForSeconds(0.1f);
        state = DrawState.DrawBase;
    }
}
