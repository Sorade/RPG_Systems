using UnityEngine;
using UnityEngine.UI;

public class DisplayTestAttribute : MonoBehaviour {
    public CharacterStats c;

    Attribute testAttribute;

    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        testAttribute = c.testAttribute;
	}
	
	// Update is called once per frame
	void Update () {
        text.text = testAttribute.value.ToString();
	}
}
