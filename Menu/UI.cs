using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using Classes;
using Menu;
using System;
using System.Collections.Generic;
using Termo.Misc;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GUI;

namespace Termo.Menu
{
    [BepInPlugin("Azora", "UI", "1.0.0")]
    internal class UI : BaseUnityPlugin
    {
        private float deltaTime = 0.0f;

        private bool showRoomTab = false;
        private bool showSelfTab = false;
        private bool showPlayerListTab = false;
        private bool showMiscTab = false;
        private bool showMenuButtonsTab = false;
        private bool showMosaSettingsTab = false;
        private bool showModCheckerTab = false;

        void Setuper()
        {
            backgroundColor = new Color(0.2f, 0.2f, 0.2f); 
            skin.window.normal.textColor = Color.white;
            skin.label.fontStyle = FontStyle.BoldAndItalic;
            skin.button.fontStyle = FontStyle.BoldAndItalic;
            skin.window.fontStyle = FontStyle.BoldAndItalic;
        }

        void Setup2()
        {
            color = Color.gray;
            color = Color.white;
            color = Color.white;
            backgroundColor = Color.black;
            backgroundColor = Color.white;
            backgroundColor = Color.white;
            skin.label.alignment = TextAnchor.UpperLeft;
            skin.label.fontStyle = FontStyle.Normal;
            skin.button.fontStyle = FontStyle.Normal;
        }

        static Rect Recter = new Rect(0, 0, 550, 339);
        public static bool fortnite, PathFinding;
       
        public void OnGUI()
        {
            {
                GUILayout.BeginArea(new Rect(Screen.width - 120, 10, 100, 100));
                GUILayout.BeginHorizontal();

                if (GUILayout.Button(fortnite ? "Close Ui" : "Open Ui"))
                {
                    fortnite = !fortnite;
                }

                GUILayout.EndHorizontal();
                GUILayout.EndArea();
            }
            if (fortnite)
            {
                GUI.Box(new Rect(Screen.width - 565, 10, 400, 200), "<color=white>AI Controller</color>");
                GUILayout.BeginArea(new Rect(Screen.width - 550, 10, 350, 200));
                GUILayout.Space(30f);
                Toggler(PathFinding);
                if (GUILayout.Button("Start Path Finding"))
                {
                    PathFinding = !PathFinding;
                }

                GUILayout.EndArea();
            }


            {
                Setuper();

                Recter = Window(0, Recter, Mainer, "");
            }
        }
        void Update()
        {
            if (PathFinding)
            {
                GameObject gameobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                gameobject.transform.position = new Vector3(-75.0558f, 4.045f, -42.574f);
                gameobject.transform.localScale = Vector3.one;
                gameobject.transform.rotation = Quaternion.identity;

                gameobject.GetComponent<Renderer>().material = Variables.Uber;
                gameobject.GetComponent<Renderer>().material.color = Color.red;

                GameObject.Destroy(gameobject.GetComponent<Rigidbody>());
                GameObject.Destroy(gameobject.GetComponent<Rigidbody2D>());
                GameObject.Destroy(gameobject.GetComponent<MeshCollider>());
                GameObject.Destroy(gameobject.GetComponent<BoxCollider>());

                Pathfinding.ABPath path = Pathfinding.ABPath.Construct(GorillaLocomotion.Player.Instance.transform.position, gameobject.transform.position);
                path.vectorPath = new List<Vector3>(); 
                path.vectorPath.Add(gameobject.transform.position);
                path.vectorPath.Add(GorillaLocomotion.Player.Instance.transform.position); 

                if (path.vectorPath != null && path.vectorPath.Count > 0)
                {
                    Vector3 targetPosition = path.vectorPath[0];
                    Vector3 moveDirection = (targetPosition - GorillaLocomotion.Player.Instance.transform.position).normalized;
                    GorillaLocomotion.Player.Instance.transform.position += moveDirection * Time.deltaTime * 15f;
                    if (Vector3.Distance(GorillaLocomotion.Player.Instance.transform.position, targetPosition) < 0.1f)
                    {
                        path.vectorPath.RemoveAt(0);
                    }
                }
            }










            if (wasdsdddd)
                moverig();
            if (mostaesf)
            {
                foreach (GorillaSurfaceOverride f in UnityEngine.GameObject.FindObjectsOfType<GorillaSurfaceOverride>())
                {
                    f.extraVelMaxMultiplier = mosavalue;
                    f.extraVelMultiplier = mosavalue;
                }
            }
            else
            {
                foreach (GorillaSurfaceOverride f in UnityEngine.GameObject.FindObjectsOfType<GorillaSurfaceOverride>())
                {
                    f.extraVelMaxMultiplier = 1f;
                    f.extraVelMultiplier = 1f;
                }
            }
        }
        public  void moverig()
        {
            if (UnityInput.Current.GetKey(KeyCode.W))
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * MoveSpeed;
            }
            if (UnityInput.Current.GetKey(KeyCode.S))
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * -MoveSpeed;
            }
            if (UnityInput.Current.GetKey(KeyCode.D))
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.right * Time.deltaTime * MoveSpeed;
            }
            if (UnityInput.Current.GetKey(KeyCode.A))
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.right * Time.deltaTime * -MoveSpeed;
            }
            if (UnityInput.Current.GetKey(KeyCode.LeftArrow))
            {
                GorillaLocomotion.Player.Instance.Turn(-RotationSpeed);
            }
            if (UnityInput.Current.GetKey(KeyCode.RightArrow))
            {
                GorillaLocomotion.Player.Instance.Turn(RotationSpeed);
            }
            if (UnityInput.Current.GetKey(KeyCode.LeftControl))
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.up * Time.deltaTime * -15;
            }
            if (UnityInput.Current.GetKey(KeyCode.Space))
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.up * Time.deltaTime * Jumpspeed;
            }
            GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        static string srtttt = "".ToUpper();
        void Mainer(int max)
        {
            Setup2();

            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;

            GUILayout.Label($"<size=16>{Main.text.text.ToUpper()} [{Mathf.Ceil(fps)}]</size>");

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Room"))
            {
                showRoomTab = true;
                showSelfTab = false;
                showPlayerListTab = false;
                showMiscTab = false;
                showMenuButtonsTab = false;
                showMosaSettingsTab = false;
                showModCheckerTab = false;
            }

            if (GUILayout.Button("Self"))
            {
                showRoomTab = false;
                showSelfTab = true;
                showPlayerListTab = false;
                showMiscTab = false;
                showMenuButtonsTab = false;
                showMosaSettingsTab = false;
                showModCheckerTab = false;
            }

            if (GUILayout.Button("Player List"))
            {
                showRoomTab = false;
                showSelfTab = false;
                showPlayerListTab = true;
                showMiscTab = false;
                showMenuButtonsTab = false;
                showMosaSettingsTab = false;
                showModCheckerTab = false;
            }

            if (GUILayout.Button("Misc"))
            {
                showRoomTab = false;
                showSelfTab = false;
                showPlayerListTab = false;
                showMiscTab = true;
                showMenuButtonsTab = false;
                showMosaSettingsTab = false;
                showModCheckerTab = false;
            }

            if (GUILayout.Button("Menu Buttons"))
            {
                showRoomTab = false;
                showSelfTab = false;
                showPlayerListTab = false;
                showMiscTab = false;
                showMenuButtonsTab = true;
                showMosaSettingsTab = false;
                showModCheckerTab = false;
            }

            if (GUILayout.Button("Mosa Settings"))
            {
                showRoomTab = false;
                showSelfTab = false;
                showPlayerListTab = false;
                showMiscTab = false;
                showMenuButtonsTab = false;
                showMosaSettingsTab = true;
                showModCheckerTab = false;
            }

            if (GUILayout.Button("Mod Checker"))
            {
                showRoomTab = false;
                showSelfTab = false;
                showPlayerListTab = false;
                showMiscTab = false;
                showMenuButtonsTab = false;
                showMosaSettingsTab = false;
                showModCheckerTab = true;
            }

            GUILayout.EndHorizontal();

          
            GUILayout.Space(10);
            if (showRoomTab)
            {
                GUILayout.Label("Room");
                GUILayout.BeginHorizontal();

                srtttt = GUILayout.TextArea(srtttt);
                if (GUILayout.Button("Join room"))
                {
                    PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(srtttt.ToUpper());
                }
                if (GUILayout.Button("Create Public"))
                {
                    Connections.CreatePubRoom();
                }

                if (GUILayout.Button("Disconnect"))
                {
                    Connections.Disconnect();
                }

                if (GUILayout.Button("Rejoin room"))
                {
                    Connections.RejoinLastRoom();  
                }

                GUILayout.EndHorizontal();

                GUILayout.BeginVertical();
                GUILayout.Label("Server IP: " + PhotonNetwork.ServerAddress);
                GUILayout.Label("Ping: " + PhotonNetwork.GetPing());
                GUILayout.Label("Player Count: " + PhotonNetwork.CountOfPlayers);
                GUILayout.Label("Room Count: " + PhotonNetwork.CountOfRooms);
                GUILayout.Label("Game Version: " + PhotonNetwork.GameVersion);
                GUILayout.Label("HWID: " + SystemInfo.deviceUniqueIdentifier);

                GUILayout.EndVertical();
            }
            else if (showSelfTab)
            {
                Toggler(wasdsdddd);
                if (GUILayout.Button("Freecam"))
                {
                    wasdsdddd = !wasdsdddd;
                }
                backgroundColor = Color.black;
                backgroundColor = Color.white;
                backgroundColor = Color.white;
                DrawSliderWithValue(ref MoveSpeed, "Movement Speed: ", 1, 30);
                DrawSliderWithValue(ref Jumpspeed, "Jump Speed: ", 1, 30);
                DrawSliderWithValue(ref RotationSpeed, "Rotation Speed: ", 1, 30);
            }
            else if (showPlayerListTab)
            {
                GUILayout.Label("Player List:");
                foreach (Photon.Realtime.Player item in PhotonNetwork.PlayerList)
                {
                    GUILayout.Label(item.NickName.ToUpper());
                }
            }
            else if (showMiscTab)
            {
                GUILayout.Label("Misc");
                GUILayout.Label("This is currently not made, this will be made in the full release.");
            }
            else if (showMenuButtonsTab)
            {
                using (var scrollViewScope = new GUILayout.ScrollViewScope(scrollPosition))
                {
                    scrollPosition = scrollViewScope.scrollPosition;

                    foreach (ButtonHelper[] buttons in Buttons.buttons)
                    {
                        foreach (ButtonHelper button in buttons)
                        {
                            Toggler(button.enabled);
                            if (!button.String.Contains("Player") && !button.String.Contains("Main") && !button.String.Contains("Visuals") && !button.String.Contains("Settings") && !button.String.Contains("Connections"))
                            {
                                if (button.Tog && !button.String.Contains("Disconnect") && !button.String.Contains("Page Type: Triggers") && !button.String.Contains("Fly Speed: 15") && !button.String.Contains("Speed Boost: 1.2f"))
                                {
                                    if (GUILayout.Button("<color=white>" + button.String + "</color>"))
                                    {
                                        button.enabled = !button.enabled;
                                        GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, false, 0.25f);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (showMosaSettingsTab)
            {
                Toggler(mostaesf);
                if (GUILayout.Button("Mosa Speed"))
                {
                    mostaesf = !mostaesf;
                }
                DrawSliderWithValue(ref mosavalue, "Mosa Speed: ", 1f, 1.8f);
            }
            else if (showModCheckerTab)
            { 
               
            }

            GUI.DragWindow();
        }
        static bool wasdsdddd, mostaesf;
        static Vector2 scrollPosition;
        static float MoveSpeed = 8, Jumpspeed = 10, RotationSpeed = 1, mosavalue;
        void Toggler(bool e)
        {
            if (e){
                GUI.backgroundColor = Color.green;
            }
            else{
                backgroundColor = Color.black;
                backgroundColor = Color.white;
                backgroundColor = Color.white;
            }
        }
        void DrawSliderWithValue(ref float value, string text, float leftval, float rightval)
        {
            GUILayout.Label(text + value.ToString("F2"));
            value = GUILayout.HorizontalSlider(value, leftval, rightval);
        }

    }
}
