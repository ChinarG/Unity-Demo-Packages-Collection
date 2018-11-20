using System.Collections;
using UnityEngine;


public enum ButtonType
{
    Up,
    Down,
    Left,
    Right,
    Null
}

public class CameraOrbit : MonoBehaviour
{
    public  Transform  pivot;                      // 被跟踪的对象：pivot——以什么为轴
    public  Vector3    pivotOffset = Vector3.zero; // 与目标的偏移量
    public  Transform  target;                     // 像一个被选中的对象(用于检查cam和target之间的对象)
    public  float      distance       = 10.0f;     // 距目标距离(使用变焦)
    public  float      minDistance    = 2f;        //最小距离
    public  float      maxDistance    = 15f;       //最大距离
    public  float      zoomSpeed      = 1f;        //速度倍率
    public  float      xSpeed         = 250.0f;    //x速度
    public  float      ySpeed         = 120.0f;    //y速度
    public  bool       allowYTilt     = true;      //允许Y轴倾斜
    public  float      yMinLimit      = -90f;      //相机向下最大角度
    public  float      yMaxLimit      = 90f;       //相机向上最大角度
    private float      x              = 0.0f;      //x变量
    private float      y              = 0.0f;      //y变量
    private float      targetX        = 0f;        //目标x
    private float      targetY        = 0f;        //目标y
    private float      targetDistance = 0f;        //目标距离
    private float      xVelocity      = 1f;        //x速度
    private float      yVelocity      = 1f;        //y速度
    private float      zoomVelocity   = 1f;        //速度倍率
    private bool       isRotateAroundUp;           //朝上转镜头
    private bool       isRotateAroundDown;         //朝下转镜头
    private int        index;                      //镜头标志位，容错
    public  float      testSpeed  = 30f;           //用于外部动态测试相机速度
    public  ButtonType buttonType = ButtonType.Null;
    private bool       isStart    = true; //默认第一次开始


    void Start()
    {
        index          = -1;
        var angles     = transform.eulerAngles;
        targetX        = x = angles.x;
        targetY        = y = ClampAngle(angles.y, yMinLimit, yMaxLimit);
        targetDistance = distance;
        isStart        = true;
    }


    void Update()
    {
        if (pivot) //如果存在设定的目标
        {
            GunLun();
            MouseControlCamera();
            switch (buttonType)
            {
                case ButtonType.Up:
                    SetDirectional(0);
                    break;
                case ButtonType.Down:
                    SetDirectional(1);
                    break;
                case ButtonType.Left:
                    SetDirectional(2);
                    break;
                case ButtonType.Right:
                    SetDirectional(3);
                    break;
                case ButtonType.Null:
                    break;
            }
            AddTransform();
        }
    }


    /// <summary>
    /// 根据点击按钮类型，设置方向
    /// </summary>
    /// <param name="arg"></param>
    void SetDirectional(int arg)
    {
        switch (arg)
        {
            case 0:
                targetY += testSpeed * 0.04f * Time.deltaTime;
                break;
            case 1:
                targetY -= testSpeed * 0.04f * Time.deltaTime;
                break;
            case 2:
                targetX += testSpeed * 0.04f * Time.deltaTime;
                break;
            case 3:
                targetX -= testSpeed * 0.04f * Time.deltaTime;
                break;
        }
    }


    /// <summary>
    /// 鼠标控制镜头
    /// </summary>
    void MouseControlCamera()
    {
        if (Input.GetMouseButton(1)) //鼠标右键
        {
            targetX += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            targetY -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
        }
    }


    /// <summary>
    /// 添加矩阵信息
    /// </summary>
    void AddTransform()
    {
        if (isStart)
        {
            targetX                 = Mathf.SmoothDampAngle(180, targetX, ref xVelocity, 5f);
            if (allowYTilt) targetY = Mathf.SmoothDampAngle(37,  targetY, ref yVelocity, 5f);
            distance                = Mathf.SmoothDamp(distance, targetDistance, ref zoomVelocity, 0.5f);
            isStart                 = false;
        }
        targetY             = ClampAngle(targetY, yMinLimit, yMaxLimit);
        Quaternion rotation = Quaternion.Euler(targetY, targetX, 0);
        transform.rotation  = rotation;
        transform.position  = rotation * new Vector3(0.0f, 0.0f, -targetDistance) + pivot.position + pivotOffset;
    }


    /// <summary>
    /// 滚轮放大缩小
    /// </summary>
    void GunLun()
    {
        float scroll                      = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0.0f) targetDistance -= zoomSpeed;
        else if (scroll < 0.0f)
            targetDistance += zoomSpeed;
        targetDistance     =  Mathf.Clamp(targetDistance, minDistance, maxDistance);
    }


    /// <summary>
    /// 限定范围
    /// </summary>
    /// <param name="angle"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle  -= 360;
        return Mathf.Clamp(angle, min, max);
    }


    //Chinar-2018-4-20号：删除孙浩方案，添加2个按钮控制镜头移动方法
    /// <summary>
    /// 镜头方向控制
    /// </summary>
    public void DirectionalControl(int arg)
    {
        switch (arg)
        {
            case 0:
                buttonType = ButtonType.Up;
                break;
            case 1:
                buttonType = ButtonType.Down;
                break;
            case 2:
                buttonType = ButtonType.Left;
                break;
            case 3:
                buttonType = ButtonType.Right;
                break;
            case 4:
                buttonType = ButtonType.Null;
                break;
        }
    }
}