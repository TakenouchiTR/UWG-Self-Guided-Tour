using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScannerController : MonoBehaviour
{
    [SerializeField] 
    private Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        this.backButton.onClick.AddListener(this.handleBack);
    }

    private void handleBack()
    {
        SceneManager.LoadScene(0);
    }
}
