using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOyun : MonoBehaviour
{
    public UnityEngine.UI.Button btn;

    public UnityEngine.UI.Text zaman, can,durum;
    

    private Rigidbody rg;
    public float hiz = 4.5f;
    float zamanSayaci = 10;
    int canSayaci = 3;
    bool oyunDevam = true;
    bool oyunTamam = false;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (oyunDevam && !oyunTamam)
        {
            zamanSayaci -= Time.deltaTime; //Zamansayacı değerini saniyede 1 düşürür.
                                           //zamanSayaci=zamanSayaci-Time.deltaTime; ile aynı.
            zaman.text = (int)zamanSayaci + ""; //"" MANTIĞI, zamansayaci bir string değere çevirir.

        }
        else if (!oyunTamam)
        {
            durum.text = "Oyun Tamamlanamadı.";
            btn.gameObject.SetActive(true);

        }


        if (zamanSayaci < 0)
        {


            oyunDevam = false;


            
        }
         

    }
    void FixedUpdate()
    {
        if (oyunDevam && !oyunTamam)
        {
            float yatay = Input.GetAxis("Horizontal");
            float dikey = Input.GetAxis("Vertical");
            Vector3 kuvvet = new Vector3(-dikey, 0, yatay);
            rg.AddForce(kuvvet * hiz); //* ile hızı arttıarack.
        }
        else
        {
            rg.velocity = Vector3.zero;
            rg.angularVelocity = Vector3.zero; 
        }
    }
    void OnCollisionEnter(Collision cls)
    {
        string objIsmi = cls.gameObject.name;
        //Debug.Log(cls.gameObject.name); //top hangisine çarparsa onun ismi çıkar ekrana.
        if (objIsmi.Equals("Bitis"))
        {
            print("Oyun Tamamlandı!");
            oyunTamam = true;
            durum.text = "Oyun tamamlandı. Tebrikler!";
            btn.gameObject.SetActive(true);
        }
        else if(!objIsmi.Equals("labirentZemini")&&!objIsmi.Equals("zemin"))
        {
            canSayaci -= 1;
            
            if (canSayaci < 0)
                oyunDevam = false;
            else
                can.text = canSayaci + "";

        }
    }
}
