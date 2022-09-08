using MiniclipTrick.Game.Scriptables;
using UnityEngine;
using UnityEngine.UI;

namespace MiniclipTrick.Game.HUD
{
    public class PlayerHeart : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private UiHeartScriptable _uiSprites;

        public bool IsFull
        {
            set => _image.sprite = value ? _uiSprites.fullHeartSprite : _uiSprites.emptyHeartSprite;
        }
    }
}