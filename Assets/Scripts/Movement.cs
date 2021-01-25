using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // public float turnSmoothVelocity;
    // public float turnSmoothTime = 0.1f;
    // public float Speed = 6f;
    // public Transform cam;
    // public CharacterController controller;  

    public List<Transform> BodyParts = new List<Transform>();
    public float mindistance = 12f;
    public float speed = 5f;
    public float rotationSpeed = 100;
    public GameObject bodyprefab;
    private float dis;
    private Transform curBodyPart;
    private Transform PrevBodyPart;
    public int beginsize;
    
    
    


    void Start()
    {
        for(int i = 0; i < beginsize - 1; i++)
        {
            AddBodyPart();
        }
        gameObject.tag = "Player";
    }
    
    void Update()
    {
        // float horizontal = Input.GetAxisRaw("Horizontal");
        // float vertical = Input.GetAxisRaw("Vertical");
        // Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        // if(direction.magnitude >= 0.1f)
        // {
        //     float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        //     float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        //     transform.rotation = Quaternion.Euler(0f, angle, 0f);
        //     Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        //     controller.Move(moveDir.normalized * Speed * Time.deltaTime);
        // }
        Move();
        if(Input.GetKey(KeyCode.Q))
            AddBodyPart();
    }
    public void Move()
    {
        float curspeed = speed;
        if(Input.GetKey(KeyCode.W))
            curspeed *= 2;

        BodyParts[0].Translate(BodyParts[0].forward * curspeed * Time.smoothDeltaTime, Space.World);

        if(Input.GetAxis("Horizontal") != 0)
            BodyParts[0].Rotate(Vector3.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));

        for(int i = 1; i < BodyParts.Count; i++)
        {
            curBodyPart = BodyParts[i];
            PrevBodyPart = BodyParts[i - 1];
            dis = Vector3.Distance(PrevBodyPart.position, curBodyPart.position);
            Vector3 newpos = PrevBodyPart.position;
            newpos.y = BodyParts[0].position.y;
            float T = Time.deltaTime * dis / mindistance * curspeed;
            if(T > 0.5f)
                T = 0.5f;
            curBodyPart.position = Vector3.Slerp(curBodyPart.position, newpos, T);
            curBodyPart.rotation = Quaternion.Slerp(curBodyPart.rotation, PrevBodyPart.rotation, T);
        }
    }
    public void AddBodyPart()
    {
        Transform newpart = (Instantiate(bodyprefab, BodyParts[BodyParts.Count - 1].position, BodyParts[BodyParts.Count - 1].rotation) as GameObject).transform;
        newpart.SetParent(transform);
        BodyParts.Add(newpart);
    }
    
}
