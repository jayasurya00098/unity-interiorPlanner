using UnityEngine;

[RequireComponent(typeof(Model))]
public class ViewController : MonoBehaviour
{
    private Model model;
    public Model ModelProperty { get { return model; } }

    void Start()
    {
        model = GetComponent<Model>();
        model.mesh = GetComponent<MeshFilter>().mesh;
        model.meshRenderer = GetComponent<MeshRenderer>();
    }
}
