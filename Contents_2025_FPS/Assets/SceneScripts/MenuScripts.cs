using UnityEngine;
using UnityEngine.UI;

public class MenuScripts : MonoBehaviour
{
    [SerializeField]
    GameObject menuBg;

    Image menuImage;
    // Start is called before the first frame update
    void Start()
    {
        menuImage = menuBg.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuImage.enabled == false)
            { 
                menuImage.enabled = true;
            }
            else
            {
                menuImage.enabled = false;
            }
        }
    }
}
