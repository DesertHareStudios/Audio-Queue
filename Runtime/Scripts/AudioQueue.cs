using UnityEngine;
using System.Collections.Generic;

namespace DesertHareStudios.AudioQueue {

    [RequireComponent(typeof(AudioSource))]
    public class AudioQueue : MonoBehaviour {

        private Queue<AudioSource> queue = new Queue<AudioSource>();
        private AudioSource lastSource;

        private void Start() {
            lastSource = GetComponent<AudioSource>();
            lastSource.loop = false;
            queue.Enqueue(lastSource);
        }

        public void Play() {
            lastSource = queue.Dequeue();
            if(lastSource.isPlaying) {
                GameObject newSource = new GameObject(name);
                newSource.transform.parent = transform;
                newSource.transform.localPosition = Vector3.zero;
                newSource.transform.localRotation = Quaternion.identity;
                AudioSource source = newSource.AddComponent<AudioSource>();

                source.clip = lastSource.clip;
                source.outputAudioMixerGroup = lastSource.outputAudioMixerGroup;
                source.bypassEffects = lastSource.bypassEffects;
                source.bypassListenerEffects = lastSource.bypassListenerEffects;
                source.bypassReverbZones = lastSource.bypassReverbZones;
                source.playOnAwake = false;
                source.loop = lastSource.loop;
                source.priority = lastSource.priority;
                source.volume = lastSource.volume;
                source.pitch = lastSource.pitch;
                source.panStereo = lastSource.panStereo;
                source.spatialBlend = lastSource.spatialBlend;
                source.spatialize = lastSource.spatialize;
                source.spatializePostEffects = lastSource.spatializePostEffects;
                source.dopplerLevel = lastSource.dopplerLevel;
                source.spread = lastSource.spread;
                source.rolloffMode = lastSource.rolloffMode;
                source.maxDistance = lastSource.maxDistance;
                source.minDistance = lastSource.minDistance;
                source.reverbZoneMix = lastSource.reverbZoneMix;
                source.Play();
                queue.Enqueue(lastSource);
                queue.Enqueue(source);
                lastSource = source;
            } else {
                lastSource.Play();
                queue.Enqueue(lastSource);
            }
        }

    }

}