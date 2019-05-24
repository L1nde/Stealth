using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualCursor : UIBehaviour {

    public GameObject Crosshair;
    public string HorizontalAxis = "Mouse X";
    public string VerticalAxis = "Mouse Y";

    public float Sensitivity;
    public bool EnableOnStart;

    private Image cursor;
    private RectTransform _rectTransform;

    public bool Enabled { get; private set; }

    protected override void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        cursor = GetComponent<Image>();
    }

    protected override void Start() {
        if (EnableOnStart) {
            Enable();
        }
        else {
            Disable();
        }
    }

    public void Enable() {
        Enabled = true;
        cursor.enabled = true;
    }

    public void Disable() {
        LockCursor();
        Enabled = false;
        cursor.enabled = false;
    }

    void LockCursor() {
        _rectTransform.anchoredPosition = Crosshair.GetComponent<RectTransform>().anchoredPosition;
    }

    private void Update() {
        if (Enabled) {
            MovePointer();
        }
    }

    private void MovePointer() {
        if (_rectTransform == null) {
            return;
        }

        var movement = new Vector2(Input.GetAxis(HorizontalAxis), Input.GetAxis(VerticalAxis)) * Sensitivity * Time.deltaTime;
        _rectTransform.anchoredPosition = Clamp(_rectTransform.anchoredPosition + movement);
    }

    private Vector2 Clamp(Vector2 pos) {
        pos.x = Mathf.Clamp(pos.x, -1 * Screen.width / 2f, Screen.width / 2f);
        pos.y = Mathf.Clamp(pos.y, -1 * Screen.height / 2f, Screen.height / 2f);
        return pos;
    }
}