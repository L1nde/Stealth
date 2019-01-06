using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    [CreateAssetMenu(menuName = "AudioClipGroup")]
    public class AudioClipGroup : ScriptableObject {

        [Range(0, 2)]
        public float volumeMin;
        [Range(0, 2)]
        public float volumeMax;
        [Range(0, 2)]
        public float pitchMin;
        [Range(0, 2)]
        public float pitchMax;
        [Range(0, 2)]
        public float cooldown;

        public List<AudioClip> clips;

        private float timestamp;
        private AudioSourcePool pool;

        private void OnEnable() {
            timestamp = Time.time;
            pool = FindObjectOfType<AudioSourcePool>();
        }
        
        

        public void play(AudioSource source) {
            if (Time.time - timestamp > cooldown) {
                source.clip = clips[Random.Range(0, clips.Count)];
                source.volume = Random.Range(volumeMin, volumeMax);
                source.pitch = Random.Range(pitchMin, pitchMax);
                source.Play();
                timestamp = Time.time;
            }
            
        }

        public void play() {
            if (pool == null) {
                pool = FindObjectOfType<AudioSourcePool>();
            }
            play(pool.getSource());
        }

        public void playAtLocation(Vector3 loc) {
            if (pool == null) {
                pool = FindObjectOfType<AudioSourcePool>();
            }
            playAtLocation(pool.getSource(), loc);
        }

        private void playAtLocation(AudioSource audioSource, Vector3 loc) {
            if (Time.time - timestamp > cooldown) {
                audioSource.gameObject.transform.position = loc;
                audioSource.clip = clips[Random.Range(0, clips.Count)];
                audioSource.spatialBlend = 1f;
                audioSource.volume = Random.Range(volumeMin, volumeMax);
                audioSource.pitch = Random.Range(pitchMin, pitchMax);
                audioSource.Play();
            }
        }

        public void changeVol(float vol) {
            volumeMin = vol;
            volumeMax = vol;
        }
    }
}
