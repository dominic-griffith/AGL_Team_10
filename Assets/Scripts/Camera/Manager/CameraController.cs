using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] 
    private Transform playerOnTop;
    [SerializeField] 
    private Transform playerOnBottom;

    [Header("Settings")]
    [SerializeField] 
    private Vector3 cameraOffset;
    [SerializeField] 
    private float cameraSmoothTime;
    [SerializeField] [Tooltip("Distance from player")]
    private float cameraDistanceFromPlayer;
    [SerializeField] [Tooltip("Doesnt move the cam when moving")]
    private bool ignoreY;
    [SerializeField] 
    private float maxLeftValueX;
    [SerializeField]
    private float maxRightValueX;

    //inner methods
    private Vector3 _velocity;
    private bool _isPlayerOnTopNull;
    private bool _isPlayerOnBottomNull;
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _isPlayerOnBottomNull = playerOnBottom == null;
        _isPlayerOnTopNull = playerOnTop == null;
        _camera.orthographicSize = cameraDistanceFromPlayer;
    }

    private void LateUpdate()
    {
        if (_isPlayerOnTopNull || _isPlayerOnBottomNull) return;

        Vector3 centerPoint = GetCenterPlayers();
        Vector3 centerPointOffset = centerPoint + cameraOffset;

        centerPointOffset.y = ignoreY ? 0f : centerPointOffset.y;
        centerPointOffset.x = centerPointOffset.x < maxLeftValueX ? maxLeftValueX : centerPointOffset.x;
        centerPointOffset.x = centerPointOffset.x > maxRightValueX ? maxRightValueX : centerPointOffset.x;
        
        transform.position = Vector3.SmoothDamp(transform.position, centerPointOffset, ref _velocity, cameraSmoothTime);
    }

    private Vector3 GetCenterPlayers()
    {
        Bounds bounds = new Bounds(playerOnTop.position, Vector3.zero);
        bounds.Encapsulate(playerOnBottom.position);
        return bounds.center;
    }
}
