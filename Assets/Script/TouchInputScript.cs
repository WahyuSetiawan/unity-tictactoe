using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInputScript : MonoBehaviour 
{
	
	public LayerMask touchInputMask;
	public GameObject camera;
	
	private List<GameObject> touchList = new List<GameObject>();
	private GameObject[] touchesOld;
	RaycastHit hit;
	
	// Update is called once per frame
	void Update () 
	{
		#if UNITY_EDITOR
		if(Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
		{
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo(touchesOld);
			touchList.Clear();
			
			Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
			Debug.Log("Mouse Down");
			if(Physics.Raycast(ray, out hit ))
			{

				GameObject recipient = hit.transform.gameObject;
				touchList.Add(recipient);
				
				if(Input.GetMouseButtonDown(0)) 
				{
					Debug.Log(recipient.name);
					recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);

				}
				
				if(Input.GetMouseButtonUp(0))
				{
					recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
				}
				
				if(Input.GetMouseButton(0))
				{
					recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
			
			foreach (GameObject g in touchesOld)
			{
				if(!touchList.Contains(g))
				{
					g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
		#endif
		
		if(Input.touchCount > 0)
		{
			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo(touchesOld);
			touchList.Clear();
			
			foreach (Touch touch in Input.touches) 
			{
				Ray ray = camera.GetComponent<Camera>().ScreenPointToRay(touch.position);
				
				if(Physics.Raycast(ray, out hit))
				{
					GameObject recipient = hit.transform.gameObject;
					touchList.Add(recipient);
					
					if(touch.phase == TouchPhase.Began) 
					{
						recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					
					if(touch.phase == TouchPhase.Ended)
					{
						recipient.SendMessage("OnTouchUp", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					
					if(touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
					{
						recipient.SendMessage("OnTouchStay", hit.point, SendMessageOptions.DontRequireReceiver);
					}
					
					if(touch.phase == TouchPhase.Canceled)
					{
						recipient.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
					}
				}
			}
			
			foreach (GameObject g in touchesOld)
			{
				if(!touchList.Contains(g))
				{
					g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
