using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScrollableMap : MonoBehaviour
{
    bool pressed;
    Vector2 pressedPosition;
    float tapTime;
    RectTransform rectTransform;

    [SerializeField]
    BoxCollider2D largeCollider;

    public UnityEvent Tapped;

    private void Start()
    {
        this.rectTransform = base.GetComponent<RectTransform>();
        this.largeCollider.size = new Vector2(this.rectTransform.rect.width, this.rectTransform.rect.height);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.pressed)
        {
            Vector3 movePosition = this.transform.position;
            movePosition.x = Input.mousePosition.x - pressedPosition.x;
            this.transform.position = movePosition;
            Debug.Log(this.transform.position.x);
            if (this.transform.position.x > rectTransform.rect.xMax)
            {
                Vector3 newPosition = this.transform.position;
                newPosition.x = rectTransform.rect.xMax;
                this.transform.position = newPosition;
            }
            if (this.transform.position.x < rectTransform.rect.xMin + Screen.width)
            {
                Vector3 newPosition = this.transform.position;
                newPosition.x = rectTransform.rect.xMin + Screen.width;
                this.transform.position = newPosition;
            }
        }
    }

    public void makecollidernotmslall()
    {
        this.largeCollider.size = new Vector2(this.rectTransform.rect.width, this.rectTransform.rect.height);
        this.largeCollider.offset = Vector2.zero;
    }

    public void makecollidernormbitg()
    {
        this.largeCollider.size = new Vector2(this.rectTransform.rect.width, this.rectTransform.rect.height * .3f);
        this.largeCollider.offset = Vector2.up * .35f * this.rectTransform.rect.height;
    }

    private void OnMouseDown()
    {
        this.pressed = true;
        this.pressedPosition = Input.mousePosition - this.transform.position;
        this.tapTime = Time.time;
    }

    private void OnMouseUp()
    {
        this.pressed = false;
        if (Time.time - tapTime < .25f)
        {
            this.Tapped?.Invoke();
        }
    }
}
