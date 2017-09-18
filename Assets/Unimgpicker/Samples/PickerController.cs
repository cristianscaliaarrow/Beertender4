using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Kakera
{
    public class PickerController : MonoBehaviour
    {
        [SerializeField]
        private Unimgpicker imagePicker;

        [SerializeField]
        private Image imageRenderer;
        public Image outPutPreview;
        public Image imageUserNav;
        public GameObject panelPreview;

        void Awake()
        {
            imagePicker.Completed += (string path) =>
            {
                StartCoroutine(LoadImage(path, imageRenderer,outPutPreview));
            };
        }

        public void Start()
        {
           // OnPressShowPicker();
        }

        public void OnPressShowPicker()
        {
            imagePicker.Show("Select Image", "unimgpicker", 1024);
        }

        private IEnumerator LoadImage(string path, Image output,Image outputPreview)
        {
            var url = "file://" + path;
            var www = new WWW(url);
            yield return www;
            yield return new WaitForSeconds(0.5f);
            var texture = www.texture;
            if (texture == null)
            {
                Debug.LogError("Failed to load texture url:" + url);
            }else
            {
                PlayerPrefs.SetString("imagePath-" + Login.debugUser.id, path);
            }
            panelPreview.SetActive(true);
            outputPreview.transform.eulerAngles = Vector3.zero;
            output.transform.eulerAngles = Vector3.zero;
            imageUserNav.transform.eulerAngles = Vector3.zero;
            PlayerPrefs.SetFloat("rotate",0);
            imageUserNav.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector3.one / 2);
            outputPreview.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector3.one / 2);
            output.sprite = Sprite.Create((Texture2D)texture, new Rect(0, 0, texture.width, texture.height), Vector3.one / 2);
            Login.UpdateGUI();
        }
    }
}