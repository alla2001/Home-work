using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    Vector3 targetDir;
    Vector3 hitpoint;
    public Camera cam;
    public Weapon currentwpn;
    public List<Weapon> weapons;
    public List<Texture2D> cursors;
    public LayerMask groundLayer;
    public List<Image> outline;
    private void Awake()
    {

        Cursor.SetCursor(cursors[1],new Vector2(cursors[1].width/2, cursors[1].height / 2),CursorMode.ForceSoftware);
        outline[0].enabled = false;
        outline[1].enabled = true;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { 
        Vector3 _velo= Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            _velo += -Vector3.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _velo += Vector3.right ;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _velo += -Vector3.forward ;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _velo += Vector3.forward ;
        }
        if (Input.GetMouseButtonDown(0))
        {
            currentwpn.Attack();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryUI.instance.Trigger();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentwpn = weapons[0];
            outline[0].enabled = false;
            outline[1].enabled = true;
            Cursor.SetCursor(cursors[0], new Vector2(cursors[0].width / 2, cursors[0].height / 2), CursorMode.ForceSoftware);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentwpn = weapons[1];
            outline[1].enabled = false;
            outline[0].enabled = true;
            Cursor.SetCursor(cursors[1], new Vector2(cursors[1].width / 2, cursors[1].height / 2), CursorMode.ForceSoftware);
        }
        _velo = _velo.normalized*speed*((float)PlayerStats.instance.buffstats.movementBuff/125);
        rb.velocity =Vector3.Lerp(rb.velocity, _velo, Time.deltaTime * 12f)  ;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit,9999,groundLayer))
        {
            hitpoint=hit.point;
            Vector3 lookat = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            targetDir = lookat - transform.position;
        }
        
        transform.forward = Vector3.Lerp(transform.forward, targetDir, Time.deltaTime*4f);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(hitpoint, 0.2f);

    }
}
