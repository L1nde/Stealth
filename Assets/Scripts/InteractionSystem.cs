using UnityEngine;

namespace Assets.Scripts {
    public class InteractionSystem : MonoBehaviour {


        private ItemHolding holding;
        // Use this for initialization
        void Start () {
            holding = GetComponent<ItemHolding>();
        }
	
        // Update is called once per frame
        void Update () {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 1.5f, LayerMask.GetMask("Interactable"))) {
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward* hit.distance, Color.red);
                UIController.instance.enableInteractable();
                if (Input.GetButtonDown("Interact1")) {
                    hit.collider.gameObject.GetComponent<Interactable>().interact();
                }
            }
            else { 
                UIController.instance.disableInteractable();
            }
        }
    }
}
