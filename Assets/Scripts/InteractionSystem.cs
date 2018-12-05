using UnityEngine;

namespace Assets.Scripts {
    public class InteractionSystem : MonoBehaviour {

        // Use this for initialization
        void Start () {
		
        }
	
        // Update is called once per frame
        void Update () {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f, LayerMask.GetMask("Interactable"))) {
                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward* hit.distance, Color.red);
                UIController.instance.enableInteractable();
                if (Input.GetKeyDown(KeyCode.E)) {
                    hit.collider.gameObject.GetComponent<Interactable>().interact();
                }
                
            }
            else {
                UIController.instance.disableInteractable();
            }
        }
    }
}
