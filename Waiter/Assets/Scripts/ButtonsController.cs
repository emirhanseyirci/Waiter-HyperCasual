using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class ButtonsController : MonoBehaviour
{
    public bool isRightPressed;
    public bool isLeftPressed;
    public float horizontal; //butondan alınan input basılı tutup çekmeye göre değişiyor
    public float horizontalSpeed = 1f;
    public float OrientationAdjustmentValue; /*horizontal yani yataydaki horizontal
                                               * değişkeni ile balance değerinin toplamıdır*/

    public static ButtonsController Instance;
    public float horizontalLerpspeed;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Update()
    {
        horizontalLerpspeed = 1f;

        if (isRightPressed) /*sağ tuşa basılıyorsa horizontal değerini artırıyoruz
                             * max 1f,min 0 olacak şekilde*/
            horizontal = Mathf.Clamp(horizontal + horizontalSpeed * Time.deltaTime, 0f, 1f);
        else if (isLeftPressed) /*sol tuşa basılıyorsa horizontal değerini azaltıyoruz
                                 * max 0,min -1 olacak şekilde*/
            horizontal = Mathf.Clamp(horizontal - horizontalSpeed * Time.deltaTime, -1f, 0f);

        else /*eğer hiç bir tuşa basılmıyorsa horizontal değerimiz 
              * manmovement scriptteki balance değerine eşit oluyor yani meyillenme değerine eşit oluyor*/
            horizontal = PlayerMovement.Instance.balance;


        Down_Left(); //tuşlarla kontrol etmek için
        Down_Right(); //tuşlarla kontrol etmek için
        CalculateOrientationAdjustment();

    }

    /*horizontal yani yataydaki horizontal
     * değişkeni ile balance değerinin toplamıdır*/
    public void CalculateOrientationAdjustment()
    {
        // horizontal_with_inclination = horizontal;

        OrientationAdjustmentValue = horizontal + (PlayerMovement.Instance.balance); /*inputların true false oluşuna göre elde edilen horizontal değeri * ile meyillenme değeri olan balanceı topluyoruz*/
        OrientationAdjustmentValue = Mathf.Clamp(OrientationAdjustmentValue, -1f, 1f); //-1 ile 1 arasında tutmak için 
        OrientationAdjustmentValue = Mathf.Lerp(OrientationAdjustmentValue, 1f, horizontalLerpspeed * Time.deltaTime);
    }


    public void Down_Left()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isLeftPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            isLeftPressed = false;
        }

    }

    public void Down_Right()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            isRightPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            isRightPressed = false;
        }
    }




    public void ButtonDown_Left() //sol butona basılıyken çalışıyor
    {
        isLeftPressed = true;
    }

    public void ButtonDown_Right() //sağ butona basılıyken çalışıyor
    {
        isRightPressed = true;
    }

    public void ButtonUp_Left() //sol butondan parmağını kaldırdığında çalışıyor
    {
        isLeftPressed = false;
    }

    public void ButtonUp_Right() //sağ butondan parmağını kaldırdığında çalışıyor
    {
        isRightPressed = false;
    }
}
