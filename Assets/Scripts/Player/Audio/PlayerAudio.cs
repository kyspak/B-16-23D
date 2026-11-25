using UnityEngine;

namespace Player.Audio
{
    public class PlayerAudio : MonoBehaviour
    {
        private AudioSource _shotAudio;

        private void Awake()
        {
            _shotAudio = GetComponent<AudioSource>();
        }
        
        
    }
}
