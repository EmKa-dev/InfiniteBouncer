using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    Rigidbody rb;

    TextMesh StartGameText;
    //TextMesh OptionsText;
    TextMesh ExitText;

    [SerializeField]
    Color HighlightColor;
    [SerializeField]
    Color NotHighlightedColor;

    MenuChoices ActiveSelection;

    public float maxVelocity;
    float sqrMaxVelocity;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        sqrMaxVelocity = maxVelocity * maxVelocity;

        StartGameText = GameObject.Find("StartGameText").GetComponent<TextMesh>();
        //OptionsText = GameObject.Find("OptionsText").GetComponent<TextMesh>();
        ExitText = GameObject.Find("ExitText").GetComponent<TextMesh>();

    }

    void FixedUpdate()
    {
        MoveSelector();
        CheckForInput();

    }

    private void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteSelection();
        }
    }

    private void ExecuteSelection()
    {
        switch (ActiveSelection)
        {
            case MenuChoices.StartGame:
                SceneManager.LoadScene("InfiniteBouncer");
                break;
            case MenuChoices.Options:
                break;
            case MenuChoices.Exit:
                Application.Quit();
                break;
            default:
                break;
        }
    }

    private void MoveSelector()
    {
        Vector3 Movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * 10;

        rb.MovePosition(transform.position + Movement * Time.deltaTime);

        //Limit fall speed in case of endless falling between platforms
        if (rb.velocity.sqrMagnitude > sqrMaxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Do nothing when hitting side-edges
        if (collision.gameObject.CompareTag("MenuBorder"))
        {
            return;
        }

        //Bounce on hit
        rb.velocity = new Vector3(0, 8, 0);

        switch (collision.gameObject.tag)
        {
            case "StartGame":
                HighlightSelection(StartGameText, MenuChoices.StartGame);
                break;
            case "Options":
                //HighlightSelection(OptionsText, MenuChoices.Options);
                break;
            case "Exit":
                HighlightSelection(ExitText, MenuChoices.Exit);
                break;
            default:
                break;
        }
    }

    private void HighlightSelection(TextMesh text, MenuChoices selection)
    {

        //Set active selection
        ActiveSelection = selection;

        //Reset color for all texts
        StartGameText.color = NotHighlightedColor;
        //OptionsText.color = NotHighlightedColor;
        ExitText.color = NotHighlightedColor;

        //Highlight chosen text
        text.color = HighlightColor;
    }


    //Make selector reappear at top if falling off
    private void OnTriggerEnter(Collider other)
    {
        Vector3 re = new Vector3(transform.position.x, 10, transform.position.z);
        transform.position = re;
    }
}

public enum MenuChoices
{
    StartGame,
    Options,
    Exit
}