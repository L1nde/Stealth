using UnityEngine;

namespace Assets.Scripts {
    public abstract class Interactable : MonoBehaviour {

        // Use this for initialization
        void Awake () {
            gameObject.layer = LayerMask.NameToLayer("Interactable");
        }
	
        // Update is called once per frame
        void Update () {
		    
        }

        public abstract void interact();

        protected void disable() {
            enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
