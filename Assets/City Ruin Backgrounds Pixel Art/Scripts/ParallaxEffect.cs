using UnityEngine;

namespace CityRuinBackgroundsPixelArt
{
    public class ParallaxEffect : MonoBehaviour
    {
        public float independantSpeed;

        private float spriteWidth;
        private Vector2 initialPos;
        private float translationOffset = 0;

        private void Start()
        {
            spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x / 3;
            initialPos = transform.position;
        }

        private void LateUpdate()
        {
            translationOffset += independantSpeed * Time.deltaTime;

            // Simple continuous scrolling without camera dependency
            float newX = initialPos.x + translationOffset;
            transform.position = new Vector2(newX, initialPos.y);

            // Handle sprite tiling for seamless looping
            if (translationOffset > spriteWidth)
                translationOffset -= spriteWidth;
            else if (translationOffset < -spriteWidth)
                translationOffset += spriteWidth;
        }
    }
}

