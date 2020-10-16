using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameController : MonoBehaviour
{

    public Text xValueLabel;
    public Text yValueLabel;
    public Text zValueLabel;
    public Slider sliderX;
    public Slider sliderY;
    public Slider sliderZ;
    public Toggle Transform;
    public Toggle Scale;
    public Toggle Rotation;
    public Camera MainCamera;
    public Material selectedMaterial;
    public Text selectedName;
    public MouseOnUI mouseOnUI;
    public Dropdown dropdown;

    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public GameObject cylinderPrefab;

    private Transform selectedObject;
    private Transform selectedParent;
    private Material savedMaterial;

    private int status = 0;// 0 => T , 1 => S, 2=> R

    private bool interactionSwitch = true;
    private bool parentingSelectMode = false;

    // Start is called before the first frame update
    void Start()
    {
        selectedMaterial = createNewMaterial(0.8f, 0.8f, 0.1f, 0.5f);
        Transform.isOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        selectParent();
        if (!parentingSelectMode)
        {
            //print("selecItemMode on");
            selectItem();
        }
        blinkParent();

    }
  
    private void blinkParent()
    {
        if (Input.GetKey(KeyCode.Tab) && selectedObject != null && selectedObject.parent != null) {
            if (Time.fixedTime % .5 < .2)
            {
                selectedObject.parent.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                selectedObject.parent.GetComponent<Renderer>().enabled = true;
            }
        } else if (Input.GetKeyUp(KeyCode.Tab)&& selectedObject != null && selectedObject.parent != null)
        {
            selectedObject.parent.GetComponent<Renderer>().enabled = true;
        }
    }

    private bool checkMyAncestor(GameObject newParent, GameObject newChild) //maybe child/maybe parent
    {
        return false;
    }
    private void selectParent()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //print("parentingSelectMode on");
            parentingSelectMode = true;
            if (Input.GetMouseButtonDown(0) && selectedObject!= null)
            {
                Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if (!mouseOnUI.returnMouseOverUI())
                {
                    if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1 << 8))
                    {
                        selectedParent = hitInfo.transform;
                        Debug.Log("new parent: " + selectedParent.name);
                        Material newMaterial = null;
                        if (selectedParent == selectedObject) //becoming independent
                            selectedObject.parent = null; //get new material
                        else if (selectedParent == selectedObject.parent)
                        {
                            newMaterial = savedMaterial;
                        }
                        else
                        {
                            Transform current = selectedParent;
                            while(current.parent != null)
                            {
                                if(current.parent == selectedObject.transform)
                                {
                                    current.parent = null;
                                    break;
                                }
                                current = current.parent;
                            }
                            //selectedObject.parent = selectedParent; //new parent
                            if (selectedParent.transform.childCount > 0)
                            {//if parent have child old material 
                                newMaterial = selectedParent.transform.GetChild(0).GetComponent<Renderer>().material;
                                //else new material 
                            }
                        }
                        setMaterial(selectedObject.gameObject, newMaterial);
                        selectedObject.parent = selectedParent;
                        selectedObject = null;

                    }
                }
            }

        }
        else
            parentingSelectMode = false;
    }
   
    private void selectItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (!mouseOnUI.returnMouseOverUI())
            {
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, 1 << 8))
                {
                    if (selectedObject != null)
                    {
                        selectedObject.GetComponent<Renderer>().material = savedMaterial;
                    }
                    selectedObject = hitInfo.transform;
                    savedMaterial = selectedObject.GetComponent<Renderer>().material;
                    selectedObject.GetComponent<Renderer>().material = selectedMaterial;
                    selectedName.text = selectedObject.name;

                }
                else
                {
                    if (selectedObject != null)
                    {
                        selectedObject.GetComponent<Renderer>().material = savedMaterial;
                        selectedObject = null;
                        selectedName.text = "None";
                    }
                }
                //Debug.Log(hitInfo.transform.gameObject.name);
                setValueLabel();
            }
        }
    }

   

    private float roundAngle(float angle)
    {
        if (angle > 180f)
        {
            Debug.Log("angleRounded");
            return roundAngle(-180f + (angle - 180f));
        }

        else if (angle < -180f)
        {
            Debug.Log("angleRounded");
            return roundAngle(180f + (angle + 180f));
        }
        else return angle;
    }

    public void setValueLabel()
    {
        if (selectedObject != null)
        {
            if (status == 0)
            {
                
                sliderX.value = selectedObject.transform.position.x;
                sliderY.value = selectedObject.transform.position.y;
                sliderZ.value = selectedObject.transform.position.z;
 
            }
            else if (status == 2)
            {
                //  selectedObject.transform.rotation = Quaternion.Euler(new Vector3(selectedObject.rotation.eulerAngles.x, selectedObject.rotation.eulerAngles.y, sliderZ.value));
                Debug.Log("before Rotation: X: " + selectedObject.rotation.eulerAngles.x + " Y: " + selectedObject.rotation.eulerAngles.y + "Z: " + selectedObject.rotation.eulerAngles.z);

                sliderX.value = roundAngle(selectedObject.rotation.eulerAngles.x);
                Debug.Log("after x change Rotation: X: " + selectedObject.rotation.eulerAngles.x + " Y: " + selectedObject.rotation.eulerAngles.y + "Z: " + selectedObject.rotation.eulerAngles.z);
                sliderY.value = roundAngle(selectedObject.rotation.eulerAngles.y);
                Debug.Log("after y change Rotation: X: " + selectedObject.rotation.eulerAngles.x + " Y: " + selectedObject.rotation.eulerAngles.y + "Z: " + selectedObject.rotation.eulerAngles.z);
                sliderZ.value = roundAngle(selectedObject.rotation.eulerAngles.z);
                Debug.Log("after z change  Rotation: X: " + selectedObject.rotation.eulerAngles.x + " Y: " + selectedObject.rotation.eulerAngles.y + "Z: " + selectedObject.rotation.eulerAngles.z);
 
                //sliderX.value = selectedObject.rotation.eulerAngles.x;
                //sliderY.value = selectedObject.rotation.eulerAngles.y;
                //sliderZ.value = selectedObject.rotation.eulerAngles.z;
            }
            else if (status == 1)
            {

                sliderX.value = selectedObject.transform.localScale.x;
                sliderY.value = selectedObject.transform.localScale.y;
                sliderZ.value = selectedObject.transform.localScale.z;

            }
            return;
        }
        sliderX.value = 0;
        sliderY.value = 0;
        sliderZ.value = 0;
    }

    public void tToggle()
    {
        interactionSwitch = false;
        if (Transform.isOn == true)
        {
            status = 0;
            Scale.isOn = false;
            Rotation.isOn = false;
        }
        setMinMaxValueOfSliders(-10, 10);
        setValueLabel();
        interactionSwitch = true;

    }

    public void sToggle()
    {
        interactionSwitch = false;
        if (Scale.isOn == true)
        {
            status = 1;
            Transform.isOn = false;
            Rotation.isOn = false;
        }
        setMinMaxValueOfSliders(1, 5);
        setValueLabel();
        interactionSwitch = true;
    }

    public void rToggle()
    {
        interactionSwitch = false;
        if (Rotation.isOn == true)
        {
            status = 2;
            Scale.isOn = false;
            Transform.isOn = false;
        }
        setMinMaxValueOfSliders(-180, 180);
        setValueLabel();
        interactionSwitch = true;
    }

    private void setMinMaxValueOfSliders(float minVal, float MaxVal)
    {
        sliderX.minValue = minVal;
        sliderY.minValue = minVal;
        sliderZ.minValue = minVal;
        sliderX.maxValue = MaxVal;
        sliderY.maxValue = MaxVal;
        sliderZ.maxValue = MaxVal;
    }

    public void onxValueChanged()
    {
        //Debug.Log("X slider Value Changed");
        xValueLabel.text = sliderX.value.ToString("0.0000");   
        if (selectedObject != null && interactionSwitch)
        {
            if (status == 0)
            {
                selectedObject.transform.position = new Vector3(sliderX.value, selectedObject.transform.position.y, selectedObject.transform.position.z);
            } else if (status == 1)
            {
                selectedObject.transform.localScale = new Vector3(sliderX.value, selectedObject.transform.localScale.y, selectedObject.transform.localScale.z);
            } else if (status == 2)
            {
                selectedObject.transform.rotation = Quaternion.Euler(new Vector3(sliderX.value, selectedObject.rotation.eulerAngles.y, selectedObject.rotation.eulerAngles.z));
                
            }
        }
    }

    public void onyValueChanged()
    {
        //Debug.Log("Y slider Value Changed");
        yValueLabel.text = sliderY.value.ToString("0.0000");
        if (selectedObject != null && interactionSwitch)
        {
            if (status == 0)
            {
                selectedObject.transform.position = new Vector3(selectedObject.transform.position.x, sliderY.value, selectedObject.transform.position.z);
            }
            else if (status == 1)
            {
                selectedObject.transform.localScale = new Vector3(selectedObject.transform.localScale.x, sliderY.value, selectedObject.transform.localScale.z);
            }
            else if (status == 2)
            {
                selectedObject.transform.rotation = Quaternion.Euler(new Vector3(selectedObject.rotation.eulerAngles.x, sliderY.value, selectedObject.rotation.eulerAngles.z));
            }

        }
    }

    public void onzValueChanged()
    {
        //Debug.Log("Z slider Value Changed");
        zValueLabel.text = sliderZ.value.ToString("0.0000");
        if (selectedObject != null && interactionSwitch)
        {
            if (status == 0)
            {
                selectedObject.transform.position = new Vector3(selectedObject.transform.position.x, selectedObject.transform.position.y, sliderZ.value);
            }
            else if (status == 1)
            {
                selectedObject.transform.localScale = new Vector3(selectedObject.transform.localScale.x, selectedObject.transform.localScale.y, sliderZ.value);
            }
            else if (status == 2)
            {
                selectedObject.transform.rotation = Quaternion.Euler(new Vector3(selectedObject.rotation.eulerAngles.x, selectedObject.rotation.eulerAngles.y ,sliderZ.value));
            }
        }
    }


    public void OnValueChangeDropDown()
    {
        if (dropdown.value == 1)
            createSphere();
        else if (dropdown.value == 2)
            createCube();
        else if (dropdown.value == 3)
            createCylinder();
        else if (dropdown.value == 4) //destory only object
            deleteObject();
        else if (dropdown.value == 5) //destroy children
            deleteChildren();
        else if (dropdown.value == 6) //destroy object&children
            deleteObjectandChildren();
        else if (dropdown.value == 7) //destroy all
            clear();
            
        dropdown.value = 0;
    }
    public void createSphere()
    {
        GameObject newGameObject;
        Material newMaterial = null;
        if (selectedObject != null)
        {
            if (selectedObject.transform.childCount > 0)
                newMaterial = selectedObject.transform.GetChild(0).GetComponent<Renderer>().material;
            newGameObject = Instantiate(spherePrefab, selectedObject.position - new Vector3(1f, 1f, 1f), Quaternion.identity, selectedObject) as GameObject;
        }
        else
            newGameObject = createAsRoot(spherePrefab);
        setMaterial(newGameObject, newMaterial);
    
    }

    public void createCube()
    {
        GameObject newGameObject;
        Material newMaterial = null;
        if (selectedObject != null)
        {
            if (selectedObject.transform.childCount > 0)
                newMaterial = selectedObject.transform.GetChild(0).GetComponent<Renderer>().material;
            newGameObject = Instantiate(cubePrefab, selectedObject.position - new Vector3(0.1f, 0.1f, 0.1f), Quaternion.identity, selectedObject);
        }
        else
            newGameObject = createAsRoot(cubePrefab);
        setMaterial(newGameObject, newMaterial);
    }
    
    public void createCylinder()
    {
        GameObject newGameObject;
        Material newMaterial = null;
        if (selectedObject != null)
        {
            if (selectedObject.transform.childCount > 0)
                newMaterial = selectedObject.transform.GetChild(0).GetComponent<Renderer>().material;
            newGameObject = Instantiate(cylinderPrefab, selectedObject.position - new Vector3(0.1f, 0.1f, 0.1f), Quaternion.identity, selectedObject);
        }
        else
            newGameObject = createAsRoot(cylinderPrefab);
        setMaterial(newGameObject, newMaterial);

    }

    private GameObject createAsRoot(GameObject prefab)
    {
           return Instantiate(prefab, new Vector3(1, 1, 1), Quaternion.identity);
    }

    private void deleteObject()
    {
        if (selectedObject != null)
        {
            foreach (Transform child in selectedObject)
            {
                setMaterial(child.gameObject, null);
                child.parent = null;
            }
            foreach (Transform child in selectedObject)
            {
                child.parent = null;
            }
            Destroy(selectedObject.gameObject);
        }
    }

    private void deleteObjectandChildren()
    {
        if (selectedObject != null)
            Destroy(selectedObject.gameObject);
    }


    private void deleteChildren()
    {
        if (selectedObject != null)
        {
            foreach (Transform child in selectedObject)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void clear()
    {
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
        {
            if(o.layer == 8)
            {
                Destroy(o);
            }
            
        }

    }
    private Material createNewMaterial(float r, float g, float b, float a = 1)
    {
        Debug.Log("color:  r: " + r + " g: " + g + " b: " + b + " a: " + a);
        Material newMaterial;
        newMaterial = new Material(selectedMaterial);
        newMaterial.color = new Color(r, g, b, a);
        return newMaterial;
    }
    

    private void setMaterial(GameObject a, Material newMaterial)
    {
        if (newMaterial == null)
        {
            Debug.Log("New material created");
            newMaterial = createNewMaterial(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
        a.GetComponent<Renderer>().material = newMaterial;
    }
    

}

