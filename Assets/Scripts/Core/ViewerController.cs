using Lean.Touch;
using UnityEngine;

public class ViewerController : MonoBehaviour
{
    public static ViewerController Instance = null;
    public Room room;

    [SerializeField]
    private GameObject[] props;

    [SerializeField]
    private Material defaultMaterial;

    private ViewController controller = null;

    public GameObject placeButton;

    [HideInInspector]
    public GameObject parent;

    void Start()
    {
        Instance = this;
        APIHelper.Instance.GetData();
    }

    public void DrawRoom()
    {
        Draw();
    }

    void Draw()
    {
        parent = new GameObject();
        parent.transform.position = Vector3.zero;
        parent.transform.rotation = Quaternion.identity;

        parent.name = "RoomParent";
        parent.AddComponent<LeanTwistRotateAxis>().enabled = false;
        parent.AddComponent<LeanPinchScale>().enabled = false;


        GameObject go = new GameObject();

        go.name = "Room";
        go.transform.SetParent(parent.transform, true);
        controller = go.AddComponent<ViewController>();

        for (int i = 0; i < room.vertices.Length; i++)
        {
            controller.GetModel.vertices[i] = new Vector3(room.vertices[i].x, room.vertices[i].y, room.vertices[i].z);
        }

        controller.GetModel.meshRenderer.material = defaultMaterial;
        controller.GetModel.GenerateCube();
        controller.GetModel.UpdateMesh();

        //spawn props

        for (int i = 0; i < room.props.Count; i++)
        {
            var prop = room.props[i];
            GameObject prop_go = Instantiate(props[prop.id]);
            prop_go.name = "Prop" + i;
            prop_go.transform.position = new Vector3(prop.position.x, prop.position.y, prop.position.z);
            prop_go.transform.SetParent(parent.transform, true);
        }

        parent.SetActive(false);
    }
}
