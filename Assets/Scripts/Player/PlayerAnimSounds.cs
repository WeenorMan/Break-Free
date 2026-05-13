using UnityEngine;


namespace Player
{
    public class PlayerAnimSounds : MonoBehaviour
    {
        public void Footsteps()
        {
            int ran = Random.Range(0, 2);

            switch (ran)
            {
                case 0:
                    AudioManager.instance.PlaySFXClip(11);
                    break;

                case 1:
                    AudioManager.instance.PlaySFXClip(12);
                    break;

                case 2:
                    AudioManager.instance.PlaySFXClip(13);
                    break;

            }
        }

        public void SwingSFX()
        {
            AudioManager.instance.PlaySFXClip(6);
        }
    }
}

