using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlace : MonoBehaviour
{
    public ARTapToPlace Instance;

    bool isPlaced = false;

    [SerializeField]
    private Transform root;

    private Camera mainCamera;

    private ARRaycastManager _arRaycastManager;
    private ARPlaneManager _arPlaneManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    Vector3 _position = Vector3.zero;
    Quaternion _rotation = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        mainCamera = Camera.main;

        _arRaycastManager = GetComponent<ARRaycastManager>();
        _arPlaneManager = GetComponent<ARPlaneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaced)
            return;

        Vector3 point = mainCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.35f, 0f));

        Ray raycast = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.35f, 0f));
        RaycastHit raycastHit;

        if (Physics.Raycast(raycast, out raycastHit))
        {
            root.position = raycastHit.point;
        }
        else if (_arRaycastManager.Raycast(new Vector2(point.x, point.y), hits, TrackableType.PlaneWithinPolygon))
        {
            var hitpos = hits[0].pose;

            Vector3 _position = hitpos.position;
            Quaternion _rotation = hitpos.rotation;

            root.SetPositionAndRotation(_position, _rotation);
            ViewerController.Instance.parent.transform.SetPositionAndRotation(_position, _rotation);
        }
    }

    public void Place()
    {
        var vc = ViewerController.Instance;

        vc.parent.SetActive(true);
        vc.parent.GetComponent<LeanTwistRotateAxis>().enabled = true;
        vc.parent.GetComponent<LeanPinchScale>().enabled = true;
        vc.placeButton.SetActive(false);

        root.gameObject.SetActive(false);

        RecyclePlane();
        isPlaced = true;
    }

    public void RecyclePlane()
    {
        foreach (var plane in _arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
        _arPlaneManager.requestedDetectionMode = PlaneDetectionMode.None;
    }
}
