using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class AudioSourcePool : MonoBehaviour {
        private List<AudioSource> audios;

        private void Awake() {
            audios = new List<AudioSource>();
        }

        public AudioSource getSource() {
            foreach (var source in audios) {
                if (!source.isPlaying) {
                    return source;
                }
            }

            var o = new GameObject("AudioSource");
            o.transform.parent = transform;
            AudioSource audioSource = o.AddComponent<AudioSource>();
            audios.Add(audioSource);
            return audioSource;
        }


        // Use this for initialization
        void Start () {
		
        }
	
        // Update is called once per frame
        void Update () {
        }
    }
}
