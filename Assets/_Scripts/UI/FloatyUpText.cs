using UnityEngine;
using UnityEngine.UI;

namespace TumbleNet.CookieClicker.UI
{
    public class FloatyUpText : MonoBehaviour
    {

        public string text;
        public float speed;

        [SerializeField] private Text textComponent;
        [SerializeField] private RectTransform rectTransform;

        // Use this for initialization
        void Start()
        {
            textComponent = GetComponent<Text>();
            rectTransform = GetComponent<RectTransform>();

            textComponent.text = text;
        }

        // Update is called once per frame
        void Update()
        {
            if (textComponent.color.a <= 0)
            {
                Destroy(gameObject);
                return;
            }
            textComponent.color = new Color(textComponent.color.r, textComponent.color.g, textComponent.color.b, textComponent.color.a - speed / 255);
            rectTransform.Translate(Vector3.up * speed / 2);
        }
    }
}