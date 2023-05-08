using UnityEngine;
public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    var clickable = raycastHit.transform.GetComponent<IClickable>();
                    if (clickable != null)
                        clickable.Click();
                }
            }
        }
    }

}