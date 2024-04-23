using BepInEx;
using HarmonyLib;
using Classes;
using Mods;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Menu.Buttons;
using static Sigma.Settings;
using static Termo.Misc.Variables;
using Photon.Pun;
using System.Linq.Expressions;
using Oculus.Interaction.Deprecated;

namespace Menu
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]
    public class Main : MonoBehaviour
    {
        public static void Prefix()
        {
            try
            {
                if (DoOnce == true)
                {
                    CreateMenu();
                    DoOnce = false;
                }
                if (!PageButtons && menu != null)
                {
                    if (TrigPages)
                    {
                        if (ControllerInputPoller.instance.rightControllerIndexFloat == 1f && Time.time > Timerr + 0.5f)
                        {
                            Timerr = Time.time;
                            Toggle("NextPage");
                        }
                        if (ControllerInputPoller.instance.leftControllerIndexFloat == 1f && Time.time > Timerr + 0.5f)
                        {
                            Timerr = Time.time;
                            Toggle("PreviousPage");
                        }
                    }
                    if (GripPages)
                    {
                        if (ControllerInputPoller.instance.rightControllerGripFloat == 1f && Time.time > Timerr2 + 0.5f)
                        {
                            Timerr2 = Time.time;
                            Toggle("NextPage");
                        }
                        if (ControllerInputPoller.instance.leftControllerGripFloat == 1f && Time.time > Timerr2 + 0.5f)
                        {
                            Timerr2 = Time.time;
                            Toggle("PreviousPage");
                        }
                    }
                }

                bool toOpen = (!rightHanded && ControllerInputPoller.instance.leftControllerSecondaryButton) || (rightHanded && ControllerInputPoller.instance.rightControllerSecondaryButton);
                bool keyboardOpen = UnityInput.Current.GetKey(keyboardButton);

                if (menu == null)
                {
                    if (toOpen || keyboardOpen)
                    {

                        CreateMenu();
                        RecenterMenu(rightHanded || keyboardOpen);
                        if (reference == null)
                        {
                            CreateReference(rightHanded || keyboardOpen);
                        }
                    }
                }
                else
                {
                    if ((toOpen|| keyboardOpen))
                    {
                        RecenterMenu(rightHanded || keyboardOpen);
                    }
                    else
                    {
                        if (Destroys == true)
                        {
                            UnityEngine.Object.Destroy(menu);
                            menu = null;

                            UnityEngine.Object.Destroy(reference);
                            reference = null;
                        }

                    }
                }
            }
            catch (Exception exc)
            {
                UnityEngine.Debug.LogError(string.Format("{0} // Error initializing at {1}: {2}", Sigma.PluginInfo.Name, exc.StackTrace, exc.Message));
            }
            try
            {
                foreach (ButtonHelper[] buttonlist in buttons)
                {
                    foreach (ButtonHelper v in buttonlist)
                    {
                        if (v.enabled)
                        {
                            if (v.ExecutePath != null)
                            {
                                try
                                {
                                    v.ExecutePath.Invoke();
                                }
                                catch (Exception exc)
                                {
                                    UnityEngine.Debug.LogError(string.Format("{0} // Error with mod {1} at {2}: {3}", Sigma.PluginInfo.Name, v.String, exc.StackTrace, exc.Message));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                UnityEngine.Debug.LogError(string.Format("{0} // Error with executing mods at {1}: {2}", Sigma.PluginInfo.Name, exc.StackTrace, exc.Message));
            }
        }
        public static Color32 Green()
        {
            return new Color32(17, 93, 8, byte.MaxValue);
        }
        public static Color32 Purple()
        {
            return new Color32(88, 39, 162, 255);
        }
        public static Color darkSlateBlue = new Color(0.28f, 0.24f, 0.55f);
        public static Color BorderColor = new Color32(84, 6, 201, byte.MaxValue);
        public static void CreateMenu()
        {

            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menu.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menu.GetComponent<BoxCollider>());
            UnityEngine.Object.Destroy(menu.GetComponent<Renderer>());
            menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.3825f);

            menuBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            UnityEngine.Object.Destroy(menuBackground.GetComponent<Rigidbody>());
            UnityEngine.Object.Destroy(menuBackground.GetComponent<BoxCollider>());
            menuBackground.transform.parent = menu.transform;
            menuBackground.transform.rotation = Quaternion.identity;
            menuBackground.transform.localScale = menuSize;

            if (Toiletter == 0)
            {
                ButtonCOlor = Color.red;
                menuBackground.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
                BorderColor = new Color32(84, 6, 201, byte.MaxValue);
            }
            if (Toiletter == 1)
            {
                menuBackground.GetComponent<Renderer>().material.color = new Color32(56, 56, 57, byte.MaxValue);//new Color32(85, 84, 87, byte.MaxValue);
                BorderColor = new Color32(85, 84, 87, byte.MaxValue);
                ButtonCOlor = new Color32(76, 76, 76, byte.MaxValue);
            }
            if (Toiletter == 2)
            {
                // menuBackground.GetComponent<Renderer>().material.color = new Color32(143, 92, 221, byte.MaxValue);
                ButtonCOlor = new Color32(58, 27, 106, byte.MaxValue);
                BorderColor = new Color32(99, 10, 234, 255);
                menuBackground.GetComponent<Renderer>().material.color = Color.magenta * 8;
            }
            if (Toiletter >= 3)
            {
                Menuoutline = 0;
                fortniteee = true;
                ColorChangeThingy = false;
                menuBackground.GetComponent<Renderer>().material = MenuMaterial;
            }
            if (Toiletter >= 4)
            {
                ColorChangeThingy = false;
                ButtonCOlor = Color.red;
                Toiletter = 0;
                menuBackground.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
                BorderColor = new Color32(84, 6, 201, byte.MaxValue);
            }
            menuBackground.transform.position = new Vector3(0.05f, 0f, 0f);

           /* int numberOfStars = 200; 
            for (int i = 0; i < numberOfStars; i++)
            {
                GameObject star = GameObject.CreatePrimitive(PrimitiveType.Quad);
                star.transform.parent = menu.transform;
                star.transform.localScale = Vector3.one * 0.07f;
                float x = UnityEngine.Random.Range(-0.45f, 0.45f);
                float y = UnityEngine.Random.Range(-0.35f, 0.35f);
                float z = UnityEngine.Random.Range(-0.45f, 0.45f);
                star.transform.localPosition = new Vector3(x, y, z);
                star.GetComponent<MeshRenderer>().material.color = Color.white;
                UnityEngine.Object.Destroy(star.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(star.GetComponent<BoxCollider>());
                UnityEngine.Object.Destroy(star.GetComponent<MeshCollider>());
                star.AddComponent<StarMovement>();
            }*/
            if (Menuoutline == 0)
            {
                GameObject borderobj1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(borderobj1.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(borderobj1.GetComponent<BoxCollider>());
                borderobj1.transform.parent = menu.transform;
                borderobj1.transform.rotation = Quaternion.identity;
                borderobj1.name = "Right Border";
                borderobj1.transform.localScale = new Vector3(0.17f, 0.02f, 1f);
                borderobj1.GetComponent<MeshRenderer>().material.color = BorderColor;
                borderobj1.transform.localPosition = new Vector3(0.5f, -0.5f, 0f);


                // Left border
                GameObject borderobj2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(borderobj2.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(borderobj2.GetComponent<BoxCollider>());
                borderobj2.transform.parent = menu.transform;
                borderobj2.transform.rotation = Quaternion.identity;
                borderobj2.name = "Left Border";
                borderobj2.transform.localScale = new Vector3(0.17f, 0.02f, 1f);
                borderobj2.GetComponent<MeshRenderer>().material.color = BorderColor;
                borderobj2.transform.localPosition = new Vector3(0.5f, 0.5f, 0f);


                // bottom border
                GameObject borderobj3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(borderobj3.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(borderobj3.GetComponent<BoxCollider>());
                borderobj3.transform.parent = menu.transform;
                borderobj3.transform.rotation = Quaternion.identity;
                borderobj3.name = "Bottom Border";
                borderobj3.transform.localScale = new Vector3(0.17f, 1.02f, 0.01f);
                borderobj3.GetComponent<MeshRenderer>().material.color = BorderColor;
                borderobj3.transform.localPosition = new Vector3(0.5f, 0.001f, -0.5f);

                // top border
                GameObject borderobj4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(borderobj4.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(borderobj4.GetComponent<BoxCollider>());
                borderobj4.transform.parent = menu.transform;
                borderobj4.transform.rotation = Quaternion.identity;
                borderobj4.name = "Top Border";
                borderobj4.transform.localScale = new Vector3(0.17f, 1.02f, 0.01f);
                borderobj4.GetComponent<MeshRenderer>().material.color = BorderColor;
                borderobj4.transform.localPosition = new Vector3(0.5f, 0.001f, 0.5f);
            }
          


            canvasObject = new GameObject();
                canvasObject.transform.parent = menu.transform;
                Canvas canvas = canvasObject.AddComponent<Canvas>();
                CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
                canvasObject.AddComponent<GraphicRaycaster>();
                canvas.renderMode = RenderMode.WorldSpace;
                canvasScaler.dynamicPixelsPerUnit = 1000f;


                text = new GameObject
                {
                    transform =
                    {
                        parent = canvasObject.transform
                    }
                }.AddComponent<Text>();
                text.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);
                text.color = Green();
                text.text = "<color=white>Thermo-</color><color=magenta>X</color>";
                text.fontSize = 1;
                text.color = Color.white;
                text.supportRichText = true;
                text.fontStyle = FontStyle.Bold;
                text.alignment = TextAnchor.MiddleCenter;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 0;
                RectTransform component = text.GetComponent<RectTransform>();
                component.localPosition = Vector3.zero;
                component.sizeDelta = new Vector2(0.28f, 0.05f);
                component.position = new Vector3(0.06f, 0f, 0.165f);
                component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));   

            Text text2 = new GameObject
            {
                transform =
                {
                    parent = canvasObject.transform
                }
            }.AddComponent<Text>();
            text2.font = (Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font);

            if (buttonsType == 1)
                text2.text = "Catogory: Settings";
            if (buttonsType == 2)
                text2.text = "Catogory: Connections";
            if (buttonsType == 3)
                text2.text = "Catogory: Main";
            if (buttonsType == 4)
                text2.text = "Catogory: Visuals";
            if (buttonsType == 5)
                text2.text = "Catogory: Player";
            if (buttonsType == 6)
                text2.text = "Catogory: Fun";  
            if (buttonsType == 7)
                text2.text = "Catogory: Safety";

            text2.fontSize = 1;
            text2.color = Color.white;
            text2.supportRichText = true;
            text2.fontStyle = FontStyle.Bold;
            text2.alignment = TextAnchor.MiddleCenter;
            text2.resizeTextForBestFit = true;
            text2.resizeTextMinSize = 0;
            RectTransform componente = text2.GetComponent<RectTransform>();
            componente.localPosition = Vector3.zero;
            componente.sizeDelta = new Vector2(0.28f, 0.05f);
            componente.position = new Vector3(0.06f, 0f, 0.205f);
            componente.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));


                if (PageButtons == true)
                {
                    AddPagers();
                }
                else
                {
                    buttonsPerPage = 8;
                }
            DrawTabs();
                    // AddPages();
                    //AddReturnButtonWhenInTabs();
                    ButtonHelper[] activeButtons = buttons[buttonsType].Skip(pageNumber * buttonsPerPage).Take(buttonsPerPage).ToArray();
                    for (int i = 0; i < activeButtons.Length; i++)
                    {
                        CreateButton(i * 0.1f, activeButtons[i]);
                    }
        }
        public static Text text;
        public static void AddPagers()
        {
            buttonsPerPage = 6;
            Text texte = new GameObject
            {
                transform =
                    {
                        parent = canvasObject.transform
                    }
            }.AddComponent<Text>();
            texte.font = currentFont;
            int PAdffgsgde = pageNumber + 1;
            texte.text = "Pg-[" + PAdffgsgde + "]";
            texte.name = "Page Text";
            texte.fontSize = 1;
            texte.color = Color.white;
            texte.supportRichText = true;
            texte.fontStyle = FontStyle.Bold;
            texte.alignment = TextAnchor.MiddleCenter;
            texte.resizeTextForBestFit = true;
            texte.resizeTextMinSize = 0;
            RectTransform componente = texte.GetComponent<RectTransform>();
            componente.localPosition = Vector3.zero;
            componente.sizeDelta = new Vector2(0.2f, 0.03f);
            componente.position = new Vector3(0.06f, 0f, -0.15f);
            componente.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    gameObject.layer = 2;
                }
                UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
                gameObject.transform.parent = menu.transform;
                gameObject.name = "Previous Page";
                gameObject.transform.rotation = Quaternion.identity;
                gameObject.transform.localScale = new Vector3(0.09f, 0.23f, 0.16f);
                gameObject.transform.localPosition = new Vector3(0.66f, 0.3567f, -0.37f);
                gameObject.GetComponent<MeshRenderer>().material.color = ButtonCOlor;
                gameObject.AddComponent<Classes.Button>().relatedText = "PreviousPage";



                Text text = new GameObject
                {
                    transform =
                        {
                            parent = canvasObject.transform
                        }
                }.AddComponent<Text>();
                text.font = currentFont;
                text.text = "<";
                text.name = "<";
                text.fontSize = 1;
                text.color = Color.white;
                text.alignment = TextAnchor.MiddleCenter;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 0;
                RectTransform component = text.GetComponent<RectTransform>();
                component.localPosition = Vector3.zero;
                component.sizeDelta = new Vector2(0.2f, 0.03f);
                component.localPosition = new Vector3(0.075f, 0.109f, -0.14f);
                component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));



            gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.name = "Next Page";
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.23f, 0.16f);
            gameObject.transform.localPosition = new Vector3(0.66f, -0.3567f, -0.37f);
            gameObject.GetComponent<MeshRenderer>().material.color = ButtonCOlor;
            gameObject.AddComponent<Classes.Button>().relatedText = "NextPage";



            Text textee = new GameObject
            {
                transform =
                        {
                            parent = canvasObject.transform
                        }
            }.AddComponent<Text>();
            textee.font = currentFont;
            textee.text = ">";
            textee.name = ">";
            textee.fontSize = 1;
            textee.color = Color.white;
            textee.alignment = TextAnchor.MiddleCenter;
            textee.resizeTextForBestFit = true;
            textee.resizeTextMinSize = 0;
            RectTransform componenter = textee.GetComponent<RectTransform>();
            componenter.localPosition = Vector3.zero;
            componenter.sizeDelta = new Vector2(0.2f, 0.03f);
            componenter.localPosition = new Vector3(0.075f, -0.109f, -0.14f);
            componenter.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }
        public static void AddReturnButtonWhenInTabs()
        {
            if (buttonsType > 0)
            {
                GameObject Returner = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    Returner.layer = 2;
                }
                UnityEngine.Object.Destroy(Returner.GetComponent<Rigidbody>());
                Returner.GetComponent<BoxCollider>().isTrigger = true;
                Returner.transform.parent = menu.transform;
                Returner.transform.rotation = Quaternion.identity;
                Returner.transform.localScale = new Vector3(0.09f, 0.93f, 0.1f);
                Returner.transform.localPosition = new Vector3(0.56f, 0, -0.6f);
                Returner.GetComponent<Renderer>().material.color = Color.gray * Color.black * Color.gray * 0.5f;
                Returner.AddComponent<Classes.Button>().relatedText = "ReturnToHome";
                Text text = new GameObject
                {
                    transform =
                {
                    parent = canvasObject.transform
                }
                }.AddComponent<Text>();
                text.font = currentFont;
                text.text = "Return";
                text.supportRichText = true;
                text.fontSize = 1;
                text.alignment = TextAnchor.MiddleCenter;
                text.fontStyle = FontStyle.Bold;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 0;
                RectTransform component = text.GetComponent<RectTransform>();
                component.localPosition = Vector3.zero;
                component.sizeDelta = new Vector2(.2f, .03f);
                component.localPosition = new Vector3(.064f, 0f, -.231f);
                component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            }
        }
        static Color ButtonCOlor = Color.red;
        public static void CreateButton(float offset, ButtonHelper method)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.88f, 0.08f); 
            gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);
            gameObject.AddComponent<Classes.Button>().relatedText = method.String;
            if (method.enabled)
            {
                gameObject.GetComponent<Renderer>().material.color = Purple();
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = ButtonCOlor;
            }
            Text text = new GameObject
            {
                transform =
                {
                    parent = canvasObject.transform
                }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = method.String;
            if (method.overlapText != null)
            {
                text.text = method.overlapText;
            }
            text.supportRichText = true;
            text.fontSize = 1;
            text.alignment = TextAnchor.MiddleCenter;
            text.fontStyle = FontStyle.BoldAndItalic;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.2f, .03f);
            component.localPosition = new Vector3(.064f, 0, .111f - offset / 2.6f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }
        public static void DrawTabs()
        {
            GameObject er = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                er.layer = 2;
            }
            UnityEngine.Object.Destroy(er.GetComponent<Rigidbody>());
            er.GetComponent<BoxCollider>().isTrigger = true;
            er.transform.parent = menu.transform;
            er.transform.rotation = Quaternion.identity;
            er.transform.localScale = new Vector3(0.09f, 0.1f, 0.08f);
            er.transform.localPosition = new Vector3(0.56f, 0.60f, 0.28f);
            if (buttonsType < 1 || buttonsType > 1)
                er.GetComponent<Renderer>().material.color = Color.red;
            if (buttonsType == 1)
                er.GetComponent<Renderer>().material.color = Color.green;

            er.AddComponent<Classes.Button>().relatedText = "Settings";

            float offset = 0.10f;

            GameObject er2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                er2.layer = 2;
            }
            UnityEngine.Object.Destroy(er.GetComponent<Rigidbody>());
            er2.GetComponent<BoxCollider>().isTrigger = true;
            er2.transform.parent = menu.transform;
            er2.transform.rotation = Quaternion.identity;
            er2.transform.localScale = new Vector3(0.09f, 0.1f, 0.08f);
            er2.transform.localPosition = new Vector3(0.56f, 0.60f, 0.28f - offset);
            if (buttonsType > 2 || buttonsType < 2)
                er2.GetComponent<Renderer>().material.color = Color.red;
            if (buttonsType == 2)
                er2.GetComponent<Renderer>().material.color = Color.green;
            er2.AddComponent<Classes.Button>().relatedText = "Connections";   
            
            GameObject er3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                er3.layer = 2;
            }
            UnityEngine.Object.Destroy(er.GetComponent<Rigidbody>());
            er3.GetComponent<BoxCollider>().isTrigger = true;
            er3.transform.parent = menu.transform;
            er3.transform.rotation = Quaternion.identity;
            er3.transform.localScale = new Vector3(0.09f, 0.1f, 0.08f);
            er3.transform.localPosition = new Vector3(0.56f, 0.60f, 0.28f - offset - offset);
            if (buttonsType > 3 || buttonsType < 3)
                er3.GetComponent<Renderer>().material.color = Color.red;
            if (buttonsType == 3)
                er3.GetComponent<Renderer>().material.color = Color.green;
            er3.AddComponent<Classes.Button>().relatedText = "Main"; 

            GameObject er4 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                er4.layer = 2;
            }
            UnityEngine.Object.Destroy(er.GetComponent<Rigidbody>());
            er4.GetComponent<BoxCollider>().isTrigger = true;
            er4.transform.parent = menu.transform;
            er4.transform.rotation = Quaternion.identity;
            er4.transform.localScale = new Vector3(0.09f, 0.1f, 0.08f);
            er4.transform.localPosition = new Vector3(0.56f, 0.60f, 0.28f - offset - offset - offset);
            if (buttonsType > 4 || buttonsType < 4)
                er4.GetComponent<Renderer>().material.color = Color.red;
            if (buttonsType == 4)
                er4.GetComponent<Renderer>().material.color = Color.green;
            er4.AddComponent<Classes.Button>().relatedText = "Visuals";
            
            GameObject er5 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                er5.layer = 2;
            }
            UnityEngine.Object.Destroy(er.GetComponent<Rigidbody>());
            er5.GetComponent<BoxCollider>().isTrigger = true;
            er5.transform.parent = menu.transform;
            er5.transform.rotation = Quaternion.identity;
            er5.transform.localScale = new Vector3(0.09f, 0.1f, 0.08f);
            er5.transform.localPosition = new Vector3(0.56f, 0.60f, 0.28f - offset - offset - offset - offset);
            if (buttonsType > 5 || buttonsType < 5)
                er5.GetComponent<Renderer>().material.color = Color.red;
            if (buttonsType == 5)
                er5.GetComponent<Renderer>().material.color = Color.green;
            er5.AddComponent<Classes.Button>().relatedText = "Player"; 

            GameObject er6 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                er6.layer = 2;
            }
            UnityEngine.Object.Destroy(er6.GetComponent<Rigidbody>());
            er6.GetComponent<BoxCollider>().isTrigger = true;
            er6.transform.parent = menu.transform;
            er6.transform.rotation = Quaternion.identity;
            er6.transform.localScale = new Vector3(0.09f, 0.1f, 0.08f);
            er6.transform.localPosition = new Vector3(0.56f, 0.60f, 0.28f - offset - offset - offset - offset - offset);
            if (buttonsType > 6 || buttonsType < 6)
                er6.GetComponent<Renderer>().material.color = Color.red;
            if (buttonsType == 6)
                er6.GetComponent<Renderer>().material.color = Color.green;
            er6.AddComponent<Classes.Button>().relatedText = "Fun";  

            GameObject er7 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                er7.layer = 2;
            }
            UnityEngine.Object.Destroy(er7.GetComponent<Rigidbody>());
            er7.GetComponent<BoxCollider>().isTrigger = true;
            er7.transform.parent = menu.transform;
            er7.transform.rotation = Quaternion.identity;
            er7.transform.localScale = new Vector3(0.09f, 0.1f, 0.08f);
            er7.transform.localPosition = new Vector3(0.56f, 0.60f, 0.28f - offset - offset - offset - offset - offset - offset);
            if (buttonsType > 7 || buttonsType < 7)
                er7.GetComponent<Renderer>().material.color = Color.red;
            if (buttonsType == 7)
                er7.GetComponent<Renderer>().material.color = Color.green;
            er7.AddComponent<Classes.Button>().relatedText = "Safety";



        }
        public static void RecreateMenu()
        {
            if (menu != null)
            {
                UnityEngine.Object.Destroy(menu);
                menu = null;

                CreateMenu();
                RecenterMenu(rightHanded, UnityInput.Current.GetKey(keyboardButton));
            }
        }
        public class TimedBehaviour : MonoBehaviour
        {
            public virtual void Start()
            {
                this.startTime = Time.time;
            }
            public virtual void Update()
            {
                bool flag = this.complete;
                if (!flag)
                {
                    this.progress = Mathf.Clamp((Time.time - this.startTime) / this.duration, 0f, 1.5f);
                    bool flag2 = (double)Time.time - (double)this.startTime > (double)this.duration;
                    if (flag2)
                    {
                        bool flag3 = this.loop;
                        if (flag3)
                        {
                            this.OnLoop();
                        }
                        else
                        {
                            this.complete = true;
                        }
                    }
                }
            }
            public virtual void OnLoop()
            {
                this.startTime = Time.time;
            }
            public bool complete = false;
            public bool loop = true;
            public float progress = 0f;
            protected bool paused = false;
            protected float startTime;
            protected float duration = 2f;
        }
        public class ColorChanger : TimedBehaviour
        {
            public override void Start()
            {
                base.Start();
                this.gameObjectRenderer = base.GetComponent<Renderer>();
            }
            public override void Update()
            {
                base.Update();
                if (this.colors != null && gameObjectRenderer != null)
                {
                    this.gameObjectRenderer.material.color = color;
                    this.gameObjectRenderer.material.SetColor("_Color", this.color);
                    this.gameObjectRenderer.material.SetColor("_EmissionColor", this.color);
                }
                if (this.timeBased)
                {
                    this.color = this.colors.Evaluate(this.progress);
                }
            }
            public Renderer gameObjectRenderer;
            public Gradient colors = null;
            public Color color;
            public bool timeBased = true;
        }
        public static void RecenterMenu(bool isRightHanded, bool isKeyboardCondition = false)
        {
            if (!isKeyboardCondition)
            {
                if (!isRightHanded)
                {
                    menu.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                   menu.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                }
                else
                {
                    menu.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    Vector3 rotation = GorillaTagger.Instance.rightHandTransform.rotation.eulerAngles;
                    rotation += new Vector3(0f, 0f, 180f);
                    menu.transform.rotation = Quaternion.Euler(rotation);
                }
            }
            else
            {
                try
                {
                    TPC = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera").GetComponent<Camera>();
                }
                catch { }
                if (TPC != null)
                {
                    TPC.transform.position = new Vector3(-63.3738f, 12.8934f, -82.7083f);
                    TPC.transform.rotation = Quaternion.identity;
                    menu.transform.parent = TPC.transform;
                    menu.transform.position = (TPC.transform.position + (Vector3.Scale(TPC.transform.forward, new Vector3(0.5f, 0.5f, 0.5f)))) + (Vector3.Scale(TPC.transform.up, new Vector3(-0.02f, -0.02f, -0.02f)));
                    Vector3 rot = TPC.transform.rotation.eulerAngles;
                    rot = new Vector3(rot.x - 90, rot.y + 90, rot.z);
                    menu.transform.rotation = Quaternion.Euler(rot);

                    if (reference != null)
                    {
                        if (Mouse.current.leftButton.isPressed)
                        {
                            Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                            RaycastHit hit;
                            bool worked = Physics.Raycast(ray, out hit, 100);
                            if (worked)
                            {
                                Classes.Button collide = hit.transform.gameObject.GetComponent<Classes.Button>();
                                if (collide != null)
                                {
                                    collide.OnTriggerEnter(buttonCollider);
                                }
                            }
                        }
                        else
                        {
                            reference.transform.position = new Vector3(999f, -999f, -999f);
                        }
                    }
                }
            }
        }

        public static void CreateReference(bool isRightHanded)
        {
            reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            if (isRightHanded)
            {
                reference.transform.parent = GorillaTagger.Instance.leftHandTransform;
            }
            else
            {
                reference.transform.parent = GorillaTagger.Instance.rightHandTransform;
            }
            reference.GetComponent<Renderer>().material.color = Purple();
            reference.transform.localPosition = new Vector3(0f, -0.1f, -0.12f);
            reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            buttonCollider = reference.GetComponent<SphereCollider>();

            ColorChanger colorChanger = reference.AddComponent<ColorChanger>();
            colorChanger.color = Color.black;
            colorChanger.Start();
        }

        public static void Toggle(string buttonText)
        {
            if (buttonText == "Safety")
            {
                EnterThings.EnterSafety();
            }
            if (buttonText == "Fun")
            {
                EnterThings.EnterFun();
            }
            if (buttonText == "Player")
            {
                EnterThings.EnterPlayer();
            }
            if (buttonText == "Visuals")
            {
                EnterThings.EnterVisuals();
            }
            if (buttonText == "Main")
            {
                EnterThings.EnterMain();
            }
            if (buttonText == "Connections")
            {
                EnterThings.EnterConnections();
            }
            if (buttonText == "Settings")
            {
                EnterThings.EnterSettings();
            }
            int lastPage = ((buttons[buttonsType].Length + buttonsPerPage - 1) / buttonsPerPage) - 1;
            if (buttonText == "ReturnToHome")
            {
                EnterThings.ReturnHome();
                pageNumber = 0;
            }
            if (buttonText == "PreviousPage")
            {
                pageNumber--;
                if (pageNumber < 0)
                {
                    pageNumber = lastPage;
                }
            } else
            {
                if (buttonText == "NextPage")
                {
                    pageNumber++;
                    if (pageNumber > lastPage)
                    {
                        pageNumber = 0;
                    }
                } else
                {
                    ButtonHelper target = GetIndex(buttonText);
                    if (target != null)
                    {
                        if (target.Tog)
                        {
                            target.enabled = !target.enabled;
                            if (target.enabled)
                            {
                                if (target.enableMethod != null)
                                {
                                    try { target.enableMethod.Invoke(); } catch { }
                                }
                            }
                            else
                            {
                                if (target.disableMethod != null)
                                {
                                    try { target.disableMethod.Invoke(); } catch { }
                                }
                            }
                        }
                        else
                        {
                            if (target.ExecutePath != null)
                            {
                                try { target.ExecutePath.Invoke(); } catch { }
                            }
                        }
                    }
                    else
                    {
                        UnityEngine.Debug.LogError(buttonText + " does not exist");
                    }
                }
            }
            RecreateMenu();
        }

        public static GradientColorKey[] GetSolidGradient(Color color)
        {
            return new GradientColorKey[] { new GradientColorKey(color, 0f), new GradientColorKey(color, 1f) };
        }

        public static ButtonHelper GetIndex(string buttonText)
        {
            foreach (ButtonHelper[] buttons in Menu.Buttons.buttons)
            {
                foreach (ButtonHelper button in buttons)
                {
                    if (button.String == buttonText)
                    {
                        return button;
                    }
                }
            }

            return null;
        }
    
    }
    public class StarMovement : MonoBehaviour
    {
        private Vector3 velocity;
        private float speed = 0.1f; // Adjust the speed of the stars

        private void Start()
        {
            velocity = UnityEngine.Random.onUnitSphere * speed;
        }

        private void Update()
        {
            transform.localPosition += velocity * Time.deltaTime;
            // Wrap stars around if they go out of menu boundaries
            WrapAround();
        }

        private void WrapAround()
        {
            Vector3 pos = transform.localPosition;
            if (Mathf.Abs(pos.x) > 0.5f || Mathf.Abs(pos.y) > 0.5f || Mathf.Abs(pos.z) > 0.5f)
            {
                pos = new Vector3(
                    Mathf.Repeat(pos.x + 0.5f, 1f) - 0.5f,
                    Mathf.Repeat(pos.y + 0.5f, 1f) - 0.5f,
                    Mathf.Repeat(pos.z + 0.5f, 1f) - 0.5f);
                transform.localPosition = pos;
            }
        }
    }

}
