using UnityEngine;
using UnityEngine.UI;

public class RotateUserIcon : MonoBehaviour {
    public GameObject panelPreview;
    public Image previewImage;
    public Image TopImage;
    public Image userImage;

	public void BTN_Rotate()
    {
        previewImage.transform.eulerAngles += new Vector3(0, 0, -90);
        TopImage.transform.eulerAngles += new Vector3(0, 0, -90);
        userImage.transform.eulerAngles += new Vector3(0, 0, -90);
        PlayerPrefs.SetFloat("rotate", previewImage.transform.eulerAngles.z);
    }

    public void BTN_CLOSE()
    {
        panelPreview.SetActive(false);
    }


}
