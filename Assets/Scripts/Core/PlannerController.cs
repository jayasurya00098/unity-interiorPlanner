using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlannerController : MonoBehaviour
{
    public static PlannerController Instance;

    public enum STATE
    {
        None = -1,
        Instruction,
        Menu,
        Object
    }

    public STATE state;

    public enum AddProp
    {
        Disabled,
        Add
    }

    [Space]
    public AddProp addPropState;

    public Room room;

    [SerializeField]
    private GameObject[] panels;

    [SerializeField]
    private GameObject[] props;
    private int id = -1;

    private Ray ray;
    private RaycastHit hit;

    private GameObject currentObject;

    private void Start()
    {
        Instance = this;

        room = new Room();
        room.name = "Room";

        SwitchPanel(0);
    }

    private void FixedUpdate()
    {
        if(addPropState == AddProp.Add)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.CompareTag("Room"))
                {
                    currentObject.transform.position = hit.point;
                    currentObject.SetActive(true);

                    if (Input.GetMouseButtonDown(0))
                    {
                        room.props.Add(new Prop(id, hit.point.x, hit.point.y, hit.point.z));
                        addPropState = AddProp.Disabled;
                    }
                }
                else
                {
                    currentObject.SetActive(false);
                }
            }
        }
    }

    public void SwitchPanel(int panel)
    {
        foreach (var p in panels)
        {
            p.SetActive(false);
        }

        state = (STATE)panel;

        if(panel != -1)
            panels[panel].SetActive(true);
    }

    public void AddObject(int _id)
    {
        id = _id;
        StartCoroutine(SwitchState());
    }

    IEnumerator SwitchState()
    {
        yield return new WaitForSeconds(0.1f);
        addPropState = AddProp.Add;
        currentObject = Instantiate(props[id]);
        currentObject.SetActive(false);
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(room);
        APIHelper.Instance.PutData(json);
        Debug.Log(json);
    }

    public void NewScene()
    {
        SceneManager.LoadScene(0);
    }
}
