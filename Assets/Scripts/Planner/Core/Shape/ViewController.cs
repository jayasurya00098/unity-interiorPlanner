using UnityEngine;

[RequireComponent(typeof(Model))]
public class ViewController : MonoBehaviour
{
    private Model model;
    public Model GetModel { get { return model; } }

    void Awake()
    {
        model = GetComponent<Model>();
        model.mesh = GetComponent<MeshFilter>().mesh;
        model.meshRenderer = GetComponent<MeshRenderer>();
    }
}
