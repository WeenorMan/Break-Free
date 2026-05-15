using UnityEngine;


namespace Player
{
    public class PlayerAnimSounds : MonoBehaviour
    {
        float timer;

        const float Delay = 0.3f;

        public void Start()
        {
            timer = 0;
        }

        public void Footsteps()
        {
            int ran = Random.Range(0, 2);

            if( timer > 0 )
            {

                return;
            }

            switch (ran)
            {
                case 0:
                    AudioManager.instance.PlaySFXClip(11);
                    timer = Delay;
                    break;

                case 1:
                    AudioManager.instance.PlaySFXClip(12);
                    timer = Delay;
                    break;

                case 2:
                    AudioManager.instance.PlaySFXClip(13);
                    timer = Delay;
                    break;

            }
        }

        private void Update()
        {
            timer -= Time.deltaTime;
        }

        public void SwingSFX()
        {
            AudioManager.instance.PlaySFXClip(6);
        }
    }
}

