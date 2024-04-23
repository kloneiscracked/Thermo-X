using BepInEx;
using Photon.Pun;
using Menu;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static Termo.Misc.Variables;
using static UnityEngine.GUI;
using static Menu.Visuals;

namespace Termo.Misc
{
    internal class Gun_Library
    {
        public static bool line = true;
        public static void Gun(Action act)
        {
            if (InputLiv.RG())
            {
                RaycastHit raycastHit;
                Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position - GorillaLocomotion.Player.Instance.rightControllerTransform.up, -GorillaLocomotion.Player.Instance.rightControllerTransform.up, out raycastHit);
                PointerObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                PointerObj.AddComponent<Renderer>();
                PointerObj.GetComponent<Renderer>().material.color = Color.Lerp(Color.magenta, Main.Purple(), Mathf.PingPong(Time.time, 1f));
                PointerObj.gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                PointerObj.gameObject.transform.position = raycastHit.point;
                  

                UnityEngine.Object.Destroy(PointerObj.gameObject.GetComponent<SphereCollider>());
                UnityEngine.Object.Destroy(PointerObj, Time.deltaTime);
                if (line)
                {
                    LineRendererModule(GorillaTagger.Instance.rightHandTransform.position, raycastHit.point);
                }
                if (InputLiv.RT()) 
                {
                    act();
                }

            }
            if (bothhandguns == true)
            {
                if (InputLiv.LG())
                {
                    RaycastHit raycastHit;
                    Physics.Raycast(GorillaLocomotion.Player.Instance.leftControllerTransform.position - GorillaLocomotion.Player.Instance.leftControllerTransform.up, -GorillaLocomotion.Player.Instance.leftControllerTransform.up, out raycastHit);
                    PointerObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    PointerObj.AddComponent<Renderer>();
                    PointerObj.GetComponent<Renderer>().material.color = Color.Lerp(Color.magenta, Main.Purple(), Mathf.PingPong(Time.time, 1f));
                    PointerObj.gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                    PointerObj.gameObject.transform.position = raycastHit.point;


                    UnityEngine.Object.Destroy(PointerObj.gameObject.GetComponent<SphereCollider>());
                    UnityEngine.Object.Destroy(PointerObj, Time.deltaTime);
                    if (line)
                    {
                        LineRendererModule(GorillaTagger.Instance.leftHandTransform.position, raycastHit.point);
                    }
                    if (InputLiv.LT())
                    {
                        act();
                    }

                }
            } 
        }

    }
}
