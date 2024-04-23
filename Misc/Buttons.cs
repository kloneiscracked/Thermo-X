using Classes;
using Mods;
using static Sigma.Settings;
using static Mods.EnterThings;
using static Mods.Menu;
using static Menu.Connections;
using static Menu.Visuals;
using static Menu.Movement;
using static Menu.Main2;
using static Menu.MoveSett;
using static Menu.Fun;
using static Menu.Safety;
using Photon.Pun;
using UnityEngine;
using static Termo.Misc.Variables;
using System;
using GorillaNetworking;
using Unity.XR.CoreUtils;
using Termo.Misc;
using System.Dynamic;
using System.Linq;
using UnityEngine.ProBuilder.Shapes;
using Steamworks;
using JetBrains.Annotations;
using Patches;
using Unity.Mathematics;
using UnityEngine.Animations.Rigging;

namespace Menu
{
    internal class Buttons
    {
        public static ButtonHelper[][] buttons = new ButtonHelper[][]
        {
            new ButtonHelper[]
            {
                new ButtonHelper { String = "Settings", ExecutePath = () => EnterSettings(), Tog = false },
                new ButtonHelper { String = "Connections", ExecutePath = () => EnterConnections(), Tog = false },
                new ButtonHelper { String = "Main", ExecutePath = () => EnterMain(), Tog = false },
                new ButtonHelper { String = "Visuals", ExecutePath = () => EnterVisuals(), Tog = false },
                new ButtonHelper { String = "Player", ExecutePath = () => EnterPlayer(), Tog = false },
                new ButtonHelper { String = "Fun", ExecutePath = () => EnterFun(), Tog = false },
                new ButtonHelper { String = "Safety", ExecutePath = () => EnterSafety(), Tog = false },
            },
            new ButtonHelper[]
            {
                new ButtonHelper { String = "Page Type: Triggers", ExecutePath = () => DrakeSnake(), Tog = false },
                new ButtonHelper { String = "Change Theme", ExecutePath = () => ChangeTheme(), Tog = false },
                new ButtonHelper { String = "Dont Destroy Menu", ExecutePath = () => Destroys = false, disableMethod = () => Destroys = true, Tog = true },
                new ButtonHelper { String = "Right Hand", ExecutePath = () => rightHanded = true, disableMethod = () => rightHanded = false, Tog = true },
                new ButtonHelper { String = "Add Page Buttons", ExecutePath = () => AddPages(true), disableMethod = () => AddPages(false), Tog = true},
                new ButtonHelper { String = "Menu Outline", ExecutePath =() => Menuoutlinee(true), disableMethod =() => Menuoutlinee(false), enabled = fortniteee, Tog = true},
                new ButtonHelper { String = "Line Renderer Guns", ExecutePath = () => Gun_Library.line = true, disableMethod = () => Gun_Library.line = false, enabled = Gun_Library.line, Tog = true},
                new ButtonHelper { String = "Both Hand Guns", ExecutePath =() => bothhandguns = true, disableMethod=()=> bothhandguns = false, Tog = true},
                new ButtonHelper { String = "Fly Speed: 15", ExecutePath = () => ChangeFlySpeed(), Tog = false },
                new ButtonHelper { String = "Speed Boost: 1.2f", ExecutePath = () => ChangeSpeedBoostSpeed(), Tog = false },
                new ButtonHelper { String = "Time: 0", ExecutePath = () => ChangeTime(), Tog = false },
            },
            new ButtonHelper[] 
            {
                new ButtonHelper { String = "Disconnect", ExecutePath = () => Disconnect(), Tog = false },
                new ButtonHelper { String = "Disconnect [<color=blue>RJoy OR LJoy</color>] [steam]", ExecutePath = () => DisWithJoys(), Tog = true},
                new ButtonHelper { String = "Join Random", ExecutePath = () => PhotonNetwork.JoinRandomRoom(), Tog = false },
                new ButtonHelper { String = "Lobby Hop", ExecutePath = () => LobbyHopper(), Tog = true },
                new ButtonHelper { String = "Rejoin Previous Room", ExecutePath = () => RejoinLastRoom(), Tog = true },
                new ButtonHelper { String = "Create Private Room", ExecutePath = () => CreatePrivateRoom(), Tog = true },
            },

            new ButtonHelper[]
            {
                new ButtonHelper { String = "Sticky Platforms", ExecutePath = () => Platformss(), Tog = true },
                new ButtonHelper { String = "Platforms", ExecutePath = () => Platformsss(), Tog = true },
                new ButtonHelper { String = "Fly [<color=blue>A OR B</color>]", ExecutePath = () => Fly(false, false), Tog = true },
                new ButtonHelper { String = "Noclip Fly [<color=blue>A OR B</color>]", ExecutePath = () => NoClipFly(), Tog = true },
                new ButtonHelper { String = "Fly [<color=green>RT OR LT</color>]", ExecutePath = () =>  Fly(true, false), Tog = true },
                new ButtonHelper { String = "Sling Shot Fly", ExecutePath =()=> Fly(false, true), Tog = true},
                new ButtonHelper { String = "Speed Boost [Forest]", ExecutePath = () => Speed(true), disableMethod = () => Speed(false), Tog = true },
                new ButtonHelper { String = "Noclip [<color=magenta>RT OR LT</color>]", ExecutePath = () => Noclip(), Tog = true },
                new ButtonHelper { String = "No Tag Freeze", ExecutePath = () => GorillaLocomotion.Player.Instance.disableMovement = false, Tog = true },
                new ButtonHelper { String = "Tag Freeze", ExecutePath = () => GorillaLocomotion.Player.Instance.disableMovement = true, disableMethod=()=>GorillaLocomotion.Player.Instance.disableMovement = false, Tog = true },
                new ButtonHelper { String = "Long Arms", ExecutePath = () => LongArms(true), disableMethod =() => LongArms(false), Tog = true },
                new ButtonHelper { String = "TP Gun", ExecutePath = () => TpGun(), Tog = true },
                new ButtonHelper { String = "Airstrike Gun", ExecutePath = () => AirStrikeGuner(), Tog = true },
                new ButtonHelper { String = "Walk Walk [<color=yellow>RG AND LG</color>]", ExecutePath = () => WalkWalk(), Tog = true }, 
                new ButtonHelper { String = "Slide Control", ExecutePath = () => GorillaLocomotion.Player.Instance.slideControl = 1.5f, disableMethod =() => GorillaLocomotion.Player.Instance.slideControl = 0.00425f, Tog = true }, 
                new ButtonHelper { String = "Ghost [<color=red>RT</color>]", ExecutePath =()=> Ghost(), Tog = true},
                new ButtonHelper { String = "Invis [<color=red>RT</color>]", ExecutePath =() => Invis(), Tog = true},
                new ButtonHelper { String = "Rig Gun", ExecutePath =() => RigGun(), disableMethod =() => GorillaTagger.Instance.offlineVRRig.enabled = true, Tog = true},
                new ButtonHelper { String = "Fly Gun", ExecutePath =()=> FlyGUN(), Tog = true},
                new ButtonHelper { String = "Iron Monke", ExecutePath =()=> IronMonk(), Tog = true},
                new ButtonHelper { String = "Up / Down [RT/LT]", ExecutePath =()=> RtLtUpDown(), Tog = true},
                new ButtonHelper { String = "Auto Run? [RG/LG]", ExecutePath =()=> AutoRun(), Tog = true},
                new ButtonHelper { String = "Check point [RG/RT]", ExecutePath =()=> DrawCheckpoint(), Tog = true},
                new ButtonHelper { String = "No Slips", ExecutePath =()=> NoSlip(), Tog = true},
                

            },

            new ButtonHelper[] 
            {
                new ButtonHelper { String = "Head Dot", ExecutePath = () => headdot(), Tog = true },
                new ButtonHelper { String = "Box ESP", ExecutePath = () => boxesp(), Tog = true },
                new ButtonHelper { String = "Chams", ExecutePath = () => chams(), disableMethod = () => turnoffchams(), Tog = true },
                new ButtonHelper { String = "Csgo Esp", ExecutePath = () => CsgoEsp(), Tog = true },
                new ButtonHelper { String = "Skeletons", ExecutePath = () => Skele(), disableMethod =() => DeleSkele(), Tog = true },
                new ButtonHelper { String = "Tracers", ExecutePath = () => Tracers(), Tog = true },
                new ButtonHelper { String = "Rgb All [cs]", ExecutePath = () => RgbAll(), Tog = true },
                new ButtonHelper { String = "Fps Booster", ExecutePath =() => FpsBooster(), disableMethod =()=> ResetGfx(), Tog = true},
            },
            new ButtonHelper[]
            {
                new ButtonHelper { String = "Increase Tag Distance", ExecutePath = () => IncreaseTagDis(true), disableMethod =() => IncreaseTagDis(false), Tog = true },
                new ButtonHelper { String = "Force Tag Lag [lm]", ExecutePath = () => ForceTagLag(true), disableMethod =() => ForceTagLag(false), Tog = true },
                new ButtonHelper { String = "Flick Tag Gun", ExecutePath =() => FlickTagGun(), Tog = true},
                new ButtonHelper { String = "Tag Self", ExecutePath =() => TagSelf(), Tog = true},
            },
            new ButtonHelper[]
            {
                new ButtonHelper { String = "Grab Bug R [O]", ExecutePath=()=>GrabBugR(), Tog = true},
                new ButtonHelper { String = "Grab Bug L [O]", ExecutePath=()=>GrabBugL(), Tog = true},
                new ButtonHelper { String = "Bug Gun [O]", ExecutePath=()=>BugGun(), Tog = true},
                new ButtonHelper { String = "Grab Bat R [O]", ExecutePath=()=>GrabBatR(), Tog = true},
                new ButtonHelper { String = "Grab Bat L [O]", ExecutePath=()=>GrabBatL(), Tog = true},
                new ButtonHelper { String = "Bat Gun [O]", ExecutePath=()=>BatGun(), Tog = true},
                new ButtonHelper { String = "Glider Uplift Boost", ExecutePath=()=>GliderUpLiftBooster(true), disableMethod=()=>GliderUpLiftBooster(false), Tog = true},
                new ButtonHelper { String = "Grab Gliders [BOTH GRIPS] [O]", ExecutePath=()=>GrabAllGliders(), Tog = true},
                new ButtonHelper { String = "Grab Ball R [O]", ExecutePath=()=>GrabBallR(), Tog = true},
                new ButtonHelper { String = "Grab Ball L [O]", ExecutePath=()=>GrabBallL(), Tog = true},
                new ButtonHelper { String = "Ball Gun [O]", ExecutePath=()=>BallGun(), Tog = true},
                new ButtonHelper { String = "Spam Balls Gun", ExecutePath =() => BallerGun(), Tog = true},
                new ButtonHelper { String = "Spam Balls [RG/LG]", ExecutePath =() => SpamBalls(), Tog = true},
                new ButtonHelper { String = "Horror [Vortex/Void/Azora]", ExecutePath =() => Horror(), disableMethod=()=>ResetHorror(), Tog = true},
            },
            new ButtonHelper[]
            {
                new ButtonHelper { String = "No Mouth Movements", ExecutePath =()=> GorillaTagger.Instance.offlineVRRig.GetComponent<GorillaMouthFlap>().enabled = false, disableMethod =()=> GorillaTagger.Instance.offlineVRRig.GetComponent<GorillaMouthFlap>().enabled = true, Tog = true },
                new ButtonHelper { String = "No Finger Movements", ExecutePath =()=>NoFingerMove(true), disableMethod =()=> NoFingerMove(false), Tog = true },
                new ButtonHelper { String = "Antiban [name]", ExecutePath =()=>AntiBanname(), Tog = false},
                new ButtonHelper { String = "Fake oculus menu [RT/LT]", ExecutePath =()=>FakeOculusMenu(), Tog = true},
            }


        };
    }
    internal class Safety
    {
        public static void NoFingerMove(bool r)
        {
            if (r)
                nofingers = true;
            else
                nofingers = false;
        }
        public static void AntiBanname()
        {
            PhotonNetwork.LocalPlayer.NickName = MakeGen();
            PhotonNetwork.NickName = MakeGen();
            PlayerPrefs.Save();
        }
        public static string MakeGen()
        {
            return fortnite[fortnite.Length].ToUpper();
        }
        public static string[] fortnite = new string[]
        {
            "FORTNITE","SKIBIDISID","TOILET","gsdrgdrg","fsgd","frsdgdrg", "fesdgdrtg", "fsdegyhrdg", "dgdrgdrgg5erast","gedrtgws4ert","aertgaserdtg", "ersdzfgserg", "fesdrtygezdrfyhgkj"
        };
        public static void FakeOculusMenu()
        {
            if (InputLiv.RT() || InputLiv.LT())
            {
                GorillaTagger.Instance.rightHandTransform.position = GorillaTagger.Instance.bodyCollider.transform.position;
                GorillaTagger.Instance.leftHandTransform.position = GorillaTagger.Instance.bodyCollider.transform.position;
            }
        }
    }
    internal class Fun
    {
        public static float farClipPlane = Camera.main.farClipPlane;
        public static void ResetHorror()
        {
            BetterDayNightManager.instance.SetTimeOfDay(timeindex);
            Camera.main.farClipPlane = farClipPlane;
        }
        public static void Horror()
        {
            BetterDayNightManager.instance.SetTimeOfDay(0);
            Camera.main.farClipPlane = 7;
        }
        public static void BallModule(Vector3 pos)
        {
            GameObject BallObjecter = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            BallObjecter.transform.position = pos;
            BallObjecter.transform.rotation = Quaternion.identity;
            BallObjecter.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            Rigidbody rb = BallObjecter.AddComponent<Rigidbody>();
            rb.useGravity = false;
            TrailRenderer trailRenderer = BallObjecter.AddComponent<TrailRenderer>();
            trailRenderer.startWidth = 0.1f;
            trailRenderer.endWidth = 0.01f;
            trailRenderer.material = new Material(Shader.Find("GUI/Text Shader"));
            trailRenderer.endColor = Color.blue;
            trailRenderer.startColor = Color.black * Color.blue;
            trailRenderer.time = 2.0f;
            GameObject.Destroy(BallObjecter, 3.0f);
            GameObject.Destroy(BallObjecter.GetComponent<MeshCollider>());
            GameObject.Destroy(BallObjecter.GetComponent<BoxCollider>());
            GameObject.Destroy(BallObjecter.GetComponent<SphereCollider>());
        }
        public static void SpamBalls()
        {
            if (InputLiv.RG())
            {
                BallModule(GorillaTagger.Instance.rightHandTransform.position);
            }
            if (InputLiv.LG())
            {
                BallModule(GorillaTagger.Instance.leftHandTransform.position);
            }
        }
        public static void BallerGun()
        {
            Gun_Library.Gun(() => BallModule(PointerObj.transform.position));
        }
        public static void GliderUpLiftBooster(bool enabled)
        {
            foreach (GliderHoldable item in UnityEngine.GameObject.FindObjectsOfType<GliderHoldable>())
            {
                if (enabled)
                    item.pullUpLiftBonus = 1f;
                else
                    item.pullUpLiftBonus = 0.1f;
            }
        }  
        public static void GrabAllGliders()
        {
            foreach (GliderHoldable item in UnityEngine.GameObject.FindObjectsOfType<GliderHoldable>())
            {
                if (InputLiv.RG())
                {
                    item.transform.position = GorillaTagger.Instance.rightHandTransform.transform.position;
                }
                if (InputLiv.LG())
                {
                    item.transform.position = GorillaTagger.Instance.leftHandTransform.transform.position;
                }
            }
        }
        static GameObject Bug = GameObject.Find("Floating Bug Holdable");
        static GameObject Bat = GameObject.Find("Cave Bat Holdable");
        static GameObject Ball = GameObject.Find("Ball");
        
        public static void BugGun()
        {
            Gun_Library.Gun(() => Bug.transform.localPosition = (PointerObj.transform.position));
        }
        public static void GrabBugR()
        {
            Bug.transform.position = GorillaTagger.Instance.rightHandTransform.position;
        } 
        public static void GrabBugL()
        {
            Bug.transform.position = GorillaTagger.Instance.leftHandTransform.position;
        }

        public static void BatGun()
        {
            Gun_Library.Gun(() => Bat.transform.localPosition = (PointerObj.transform.position));
        }
        public static void GrabBatR()
        {
            Bat.transform.position = GorillaTagger.Instance.rightHandTransform.position;
        }
        public static void GrabBatL()
        {
            Bat.transform.position = GorillaTagger.Instance.leftHandTransform.position;
        }

        public static void BallGun()
        {
            Gun_Library.Gun(() => Ball.transform.localPosition = (PointerObj.transform.position));
        }
        public static void GrabBallR()
        {
            Ball.transform.position = GorillaTagger.Instance.rightHandTransform.position;
        }
        public static void GrabBallL()
        {
            Ball.transform.position = GorillaTagger.Instance.leftHandTransform.position;
        }
    }
    internal class MoveSett
    {
        public static void ChangeTime()
        {
            timeindex++;
            if (timeindex == 0)
            {
                BetterDayNightManager.instance.SetTimeOfDay(timeindex);
            } 
            if (timeindex == 1)
            {
                BetterDayNightManager.instance.SetTimeOfDay(timeindex);
                Main.GetIndex("Time: 0").String = "Time: 1";
            } 
            if (timeindex == 2)
            {
                BetterDayNightManager.instance.SetTimeOfDay(timeindex);
                Main.GetIndex("Time: 1").String = "Time: 2";
            } 
            if (timeindex == 3)
            {
                BetterDayNightManager.instance.SetTimeOfDay(timeindex);
                Main.GetIndex("Time: 2").String = "Time: 3";
            } 
            if (timeindex == 4)
            {
                BetterDayNightManager.instance.SetTimeOfDay(timeindex);
                Main.GetIndex("Time: 3").String = "Time: 4";
            }
            if (timeindex == 5)
            {
                BetterDayNightManager.instance.SetTimeOfDay(timeindex);
                Main.GetIndex("Time: 4").String = "Time: 5";
            } 
            if (timeindex == 6)
            {
                BetterDayNightManager.instance.SetTimeOfDay(timeindex);
                Main.GetIndex("Time: 5").String = "Time: 6";
            }
            if (timeindex >= 7)
            {
                timeindex = 0;
                BetterDayNightManager.instance.SetTimeOfDay(0);
                Main.GetIndex("Time: 6").String = "Time: 0";
            }
        }




        public static void ChangeSpeedBoostSpeed()
        {
            SpeedIndex++;
            if (SpeedIndex == 0)
            {
                BoostSpeed = 1.2f;
            }
            if (SpeedIndex == 1)
            {
                BoostSpeed = 1.5f;
                Main.GetIndex("Speed Boost: 1.2f").String = "Speed Boost: 1.5f";
            }
            if (SpeedIndex == 2)
            {
                BoostSpeed = 2f;
                Main.GetIndex("Speed Boost: 1.5f").String = "Speed Boost: 2f";
            }
            if (SpeedIndex >= 3)
            {
                SpeedIndex = 0;
                BoostSpeed = 1.2f;
                Main.GetIndex("Speed Boost: 2f").String = "Speed Boost: 1.2f";
            }
        }
        public static void ChangeFlySpeed()
        {
            FlyModoIndex++;
            if (FlyModoIndex == 0)
            {
                speed = 15f;
            }
            if (FlyModoIndex == 1)
            {
                speed = 30f;
                Main.GetIndex("Fly Speed: 15").String = "Fly Speed: 30";
            }
            if (FlyModoIndex == 2)
            {
                speed = 50f;
                Main.GetIndex("Fly Speed: 30").String = "Fly Speed: 50";
            }
            if (FlyModoIndex == 3)
            {
                speed = 80f;
                Main.GetIndex("Fly Speed: 50").String = "Fly Speed: 80";
            }
            if (FlyModoIndex >= 4)
            {
                FlyModoIndex = 0;
                speed = 15;
                Main.GetIndex("Fly Speed: 80").String = "Fly Speed: 15";
            }
        }
    }
    internal class Movement
    {
        public static void NoSlip()
        {
            foreach (GorillaSurfaceOverride item in UnityEngine.GameObject.FindObjectsOfType<GorillaSurfaceOverride>())
            {
                item.overrideIndex = 0;
            }
        }
        public static void DrawCheckpoint() 
        {
            if (checkker == null)
            {
                checkker = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                checkker.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                checkker.GetComponent<Renderer>().material = new Material(Shader.Find("GUI/Text Shader"));
                checkker.GetComponent<Renderer>().material.color = Color.red;

                UnityEngine.GameObject.Destroy(checkker.GetComponent<Rigidbody>());
                UnityEngine.GameObject.Destroy(checkker.GetComponent<Rigidbody2D>());
                UnityEngine.GameObject.Destroy(checkker.GetComponent<Collider>());
                UnityEngine.GameObject.Destroy(checkker.GetComponent<Collider2D>());
                UnityEngine.GameObject.Destroy(checkker.GetComponent<BoxCollider>());
                UnityEngine.GameObject.Destroy(checkker.GetComponent<MeshCollider>());
            }

            if (InputLiv.RG() && checkker != null)
            {
                checkker.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                checkker.transform.rotation = Quaternion.identity;
            }
            else if (InputLiv.RT() && checkker != null)
            {
                GorillaTagger.Instance.transform.position = checkker.transform.position;
            }
            NoClipModule(InputLiv.RT());
        }
        public static void NoClipModule(bool input)
        {
            if (input == true)
            {
                foreach (MeshCollider item in UnityEngine.GameObject.FindObjectsOfType<MeshCollider>())
                {
                    item.enabled = false;
                }
            }
            else
            {
                foreach (MeshCollider item in UnityEngine.GameObject.FindObjectsOfType<MeshCollider>())
                {
                    item.enabled = true;
                }
            }
        }
        public static void NoClipFly()
        {
            if (InputLiv.A() || InputLiv.B())
            {
                GorillaTagger.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * speed;
                GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;
            }
            NoClipModule(InputLiv.A() || InputLiv.B());
        }
        public static void AutoRun()
        {
            if (InputLiv.RG() || InputLiv.LG())
            {
                Vector3 f = new Vector3(0, 0, 0);
                f.x += 15 * Time.deltaTime * 50;
                f.y += 15 * Time.deltaTime * 50;
                f.z += 15 * Time.deltaTime * 50;
                GorillaLocomotion.Player.Instance.rightControllerTransform.position += f * Time.deltaTime * 15;
                Vector3 f2 = new Vector3(0, 0, 0);
                f.x += 15 * Time.deltaTime * 50;
                f.y += 15 * Time.deltaTime * 50;
                f.z += 15 * Time.deltaTime * 50;
                GorillaLocomotion.Player.Instance.leftControllerTransform.position += f2 * Time.deltaTime * 15;
            }
        }

        public static void RtLtUpDown()
        {
            if (InputLiv.RT() && !InputLiv.LT())
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * 100f, ForceMode.Acceleration);
            }
            if (!InputLiv.RT() && InputLiv.LT())
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.down * 100f, ForceMode.Acceleration);
            }
        }
        public static void IronMonk()
        {
            if (InputLiv.RG())
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(12f * GorillaLocomotion.Player.Instance.rightControllerTransform.right, ForceMode.Acceleration);
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tagHapticStrength / 2f, GorillaTagger.Instance.tagHapticDuration / 2f);
            }
            if (InputLiv.LG())
            {
                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tagHapticStrength / 2f, GorillaTagger.Instance.tagHapticDuration / 2f);
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(-12f * GorillaLocomotion.Player.Instance.leftControllerTransform.right, ForceMode.Acceleration);
            }
            if (InputLiv.LG() | InputLiv.RG())
            {
                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tagHapticStrength / 2f, GorillaTagger.Instance.tagHapticDuration / 2f);
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tagHapticStrength / 2f, GorillaTagger.Instance.tagHapticDuration / 2f);
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = Vector3.ClampMagnitude(GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity, 50f);
            }
        }
        public static void Platformsss()
        {
            if (InputLiv.RG() == true && Rtoggle == true)
            {
                Robject = GameObject.CreatePrimitive(PlatformPrim);
                Robject.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                Robject.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                Robject.transform.localScale = new Vector3(0.0105f, 0.26f, 0.3585f);
                GameObject.Destroy(Robject.GetComponent<Rigidbody>());
                GameObject.Destroy(Robject.GetComponent<Rigidbody2D>());

                MeshRenderer mesh = Robject.GetComponent<MeshRenderer>();
                mesh.material = Uber;
                mesh.material.color = Color.Lerp(Color.magenta, Main.Purple(), Mathf.PingPong(Time.time, 1f));



                Rtoggle = false;
            }
            else if (InputLiv.RG() == false && Rtoggle == false)
            {
                GameObject.Destroy(Robject);
                Robject = null;
                Rtoggle = true;
            }
            if (InputLiv.LG() == true && Ltoggle == true)
            {
                Lobject = GameObject.CreatePrimitive(PlatformPrim);
                Lobject.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                Lobject.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                Lobject.transform.localScale = new Vector3(0.0105f, 0.26f, 0.3585f);
                GameObject.Destroy(Lobject.GetComponent<Rigidbody>());
                GameObject.Destroy(Lobject.GetComponent<Rigidbody2D>());

                MeshRenderer mesh = Lobject.GetComponent<MeshRenderer>();
                mesh.material = Uber;
                mesh.material.color = Color.Lerp(Color.magenta, Main.Purple(), Mathf.PingPong(Time.time, 1f));



                Ltoggle = false;
            }
            else if (InputLiv.LG() == false && Ltoggle == false)
            {
                GameObject.Destroy(Lobject);
                Lobject = null;
                Ltoggle = true;
            }
        }
        public static void Platformss()
        {
            if (InputLiv.RG() == true && Rtoggle == true)
            {
                Robject = GameObject.CreatePrimitive(PlatformPrim);
                Robject.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                Robject.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
                Robject.transform.localScale = new Vector3(0.3f, 0.199f, 0.3f);
                GameObject.Destroy(Robject.GetComponent<Rigidbody>());
                GameObject.Destroy(Robject.GetComponent<Rigidbody2D>());

                MeshRenderer mesh = Robject.GetComponent<MeshRenderer>();
                mesh.material = Uber;
                mesh.material.color = Color.Lerp(Color.magenta, Main.Purple(), Mathf.PingPong(Time.time, 1f));



                Rtoggle = false;
            }
            else if (InputLiv.RG() == false && Rtoggle == false)
            {
                GameObject.Destroy(Robject);
                Robject = null;
                Rtoggle = true;
            }
            if (InputLiv.LG() == true && Ltoggle == true)
            {
                Lobject = GameObject.CreatePrimitive(PlatformPrim);
                Lobject.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                Lobject.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                Lobject.transform.localScale = new Vector3(0.3f, 0.199f, 0.3f);
                GameObject.Destroy(Lobject.GetComponent<Rigidbody>());
                GameObject.Destroy(Lobject.GetComponent<Rigidbody2D>());

                MeshRenderer mesh = Lobject.GetComponent<MeshRenderer>();
                mesh.material = Uber;
                mesh.material.color = Color.Lerp(Color.magenta, Main.Purple(), Mathf.PingPong(Time.time, 1f));



                Ltoggle = false;
            }
            else if (InputLiv.LG() == false && Ltoggle == false)
            {
                GameObject.Destroy(Lobject);
                Lobject = null;
                Ltoggle = true;
            }
        }
        public static void WalkWalk()
        {
            if (InputLiv.RG())
                GorillaTagger.Instance.rigidbody.velocity += GorillaTagger.Instance.rightHandTransform.right * Time.deltaTime * WalkWalkspeed;
            if (InputLiv.LG())
                GorillaTagger.Instance.rigidbody.velocity += GorillaTagger.Instance.leftHandTransform.right * Time.deltaTime * -WalkWalkspeed;
            
        }
        public static void TagSelf()
        {
            if (GorillaTagger.Instance.offlineVRRig.enabled)
            {
                GhostPatch.Prefix(GorillaTagger.Instance.offlineVRRig);
            }
            foreach (VRRig item in GorillaParent.instance.vrrigs)
            {
                if (item.mainSkin.name.Contains("fected"))
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                    GorillaTagger.Instance.offlineVRRig.transform.position = item.rightHandTransform.position;
                }else if (GorillaTagger.Instance.offlineVRRig.mainSkin.name.Contains("fected"))
                {
                    Main.GetIndex("Tag Self").enabled = false;
                    Main.RecreateMenu();
                }
            }
        }
        public static void Ghost()
        {
            if (GorillaTagger.Instance.offlineVRRig.enabled)
            {
                GhostPatch.Prefix(GorillaTagger.Instance.offlineVRRig);
            }
            if (InputLiv.RT())
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void RigModule(Vector3 pos)
        {
            if (GorillaTagger.Instance.offlineVRRig.enabled)
            {
                GhostPatch.Prefix(GorillaTagger.Instance.offlineVRRig);
            }
            GorillaTagger.Instance.offlineVRRig.enabled = false;
            GorillaTagger.Instance.offlineVRRig.transform.position = pos;
        }
        public static void RigGun()
        {
            Gun_Library.Gun(() => RigModule(PointerObj.transform.position));
        }
        public static void Invis()
        {
            if (InputLiv.RT())
            {
                GorillaTagger.Instance.offlineVRRig.headBodyOffset.x = 180f;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.headBodyOffset.x = 0f;
            }
        }
        public static void MoveTowards(Transform nig)
        {
            GorillaTagger.Instance.transform.position += nig.position * 15f * Time.deltaTime;
            GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;
        }
        public static void FlyGUN()
        {
            Gun_Library.Gun(() => MoveTowards(PointerObj.transform));
        }

        public static void TpTo(Vector3 pos)
        {
            if (Time.time > Timmmemrmemrmermsemrmse34mrmse + 0.5f)
            {
                Timmmemrmemrmermsemrmse34mrmse = Time.time;
                GorillaTagger.Instance.transform.position = pos;
            }
        }
        public static void TpGun()
        {
            Gun_Library.Gun(()=>TpTo(PointerObj.transform.position));
        }
        public static void AirstrikeSelf(Vector3 pos)
        {
            GorillaTagger.Instance.rigidbody.AddExplosionForce(5000f, pos, float.MaxValue * byte.MaxValue * int.MaxValue);
        }
        public static void AirStrikeGuner()
        {
            Gun_Library.Gun(() => AirstrikeSelf(PointerObj.transform.position));
        }
        public static void LongArms(bool kys)
        {
            if (kys)
            {
                GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
            else
            {
                GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        public static void DisWithJoys()
        {
            if (InputLiv.RightJoystickDown() || InputLiv.LeftJoyStickDown())
            {
                PhotonNetwork.Disconnect();
            }
        }
        public static void Noclip()
        {
            NoClipModule(InputLiv.RT() || InputLiv.LT());
        }    

        public static void IncreaseTagDis(bool enabled)
        {
            if (enabled)
            {
                foreach (GorillaTagManager item in UnityEngine.GameObject.FindObjectsOfType<GorillaTagManager>())
                {
                    item.tagDistanceThreshold = 8f;
                }
            }
            else
            {
                foreach (GorillaTagManager item in UnityEngine.GameObject.FindObjectsOfType<GorillaTagManager>())
                {
                    item.tagDistanceThreshold = 4;
                }
            }
        }
        public static void FlickModule(Vector3 pos)
        {
            GorillaTagger.Instance.rightHandTransform.position = pos;
        }
        public static void FlickTagGun()
        {
            Gun_Library.Gun(() => FlickModule(PointerObj.transform.position));
        }
        public static void ForceTagLag(bool enabled)
        {
            if (enabled)
            {
                foreach (GorillaTagManager item in UnityEngine.GameObject.FindObjectsOfType<GorillaTagManager>())
                {
                    item.tagCoolDown = 9999999f;
                    item.checkCooldown = 9999999f;
                }
            }
            else
            {
                foreach (GorillaTagManager item in UnityEngine.GameObject.FindObjectsOfType<GorillaTagManager>())
                {
                    item.tagCoolDown = 5f;
                    item.checkCooldown = 5f;
                }
            }
           
        }
        public static void Fly(bool TriggerFly, bool Slingshotfly)
        {
            if (TriggerFly)
            {
                if (InputLiv.RT())
                {
                    GorillaTagger.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * speed;
                    GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;
                }
                if (InputLiv.LT())
                {
                    GorillaTagger.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * speed;
                    GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;
                }
            }
            else
            {
                if (InputLiv.A())
                {
                    GorillaTagger.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * speed;
                    GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;
                }
                if (InputLiv.B())
                {
                    GorillaTagger.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * speed;
                    GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;
                }
            }
            if (Slingshotfly)
            {
                if (InputLiv.A() || InputLiv.B())
                {
                    GorillaTagger.Instance.rigidbody.velocity += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * speed;
                }
            }
        }
        public static void Speed(bool enabled)
        {
            if (enabled == true)
            {
                foreach (GorillaSurfaceOverride item in UnityEngine.GameObject.FindObjectsOfType<GorillaSurfaceOverride>())
                {
                    item.extraVelMaxMultiplier = BoostSpeed;
                    item.extraVelMultiplier = BoostSpeed;
                }
            }
            else
            {
                foreach (GorillaSurfaceOverride itemer in UnityEngine.GameObject.FindObjectsOfType<GorillaSurfaceOverride>())
                {
                    itemer.extraVelMaxMultiplier = 1f;
                    itemer.extraVelMultiplier = 1f;
                }
            }
        }


    }
    internal class Visuals
    {
        public static void fsg()
        {
        }
        public static void LineRendererModule(Vector3 FirstPos, Vector3 LastPos)
        {
            GameObject renderer = new GameObject("LineRenderer");
            LineRenderer render = renderer.AddComponent<LineRenderer>();
            render.material.shader = Shader.Find("GUI/Text Shader");
            render.startColor = Color.Lerp(Color.magenta, Main.Purple(), Mathf.PingPong(Time.time, 1f));
            render.endColor = Color.Lerp(Color.magenta, Main.Purple(), Mathf.PingPong(Time.time, 1f));
            render.positionCount = 2;
            render.useWorldSpace = true;
            render.startWidth = 0.019f;
            render.endWidth = 0.019f;
            render.SetPosition(0, FirstPos);
            render.SetPosition(1, LastPos);
            UnityEngine.Object.Destroy(renderer, Time.deltaTime);
        }
        public static void Tracers()
        {
            foreach (VRRig item in GameObject.FindObjectsOfType<VRRig>())
            {
                if (!InputLiv.RG() && !InputLiv.LG())
                    LineRendererModule(GorillaTagger.Instance.bodyCollider.transform.position, item.transform.position);
                if (InputLiv.RG() && !InputLiv.LG())
                    LineRendererModule(GorillaTagger.Instance.rightHandTransform.position, item.transform.position);
                if (InputLiv.LG() && !InputLiv.RG())
                    LineRendererModule(GorillaTagger.Instance.leftHandTransform.position, item.transform.position);
            }
        }
        public static void ResetGfx()
        {
            QualitySettings.masterTextureLimit = 0;
            QualitySettings.globalTextureMipmapLimit = 0;
        }
        public static void FpsBooster()
        {
            QualitySettings.masterTextureLimit = 9999;
            QualitySettings.globalTextureMipmapLimit = 9999;
        }
        public static void RgbAll()
        {
            foreach (VRRig item in GorillaParent.instance.vrrigs)
            {
                item.mainSkin.material.color = new Color32((byte)UnityEngine.Random.value, (byte)UnityEngine.Random.value, (byte)UnityEngine.Random.value, byte.MaxValue);
                GorillaTagger.Instance.UpdateColor(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
            }
        }
        public static void CsgoEsp()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (!vrrig.isOfflineVRRig)
                    {
                        if (vrrig.gameObject.GetComponent<LineRenderer>() == null)
                        {
                            vrrig.gameObject.AddComponent<LineRenderer>();
                        }
                        LineRenderer BoxLines = vrrig.gameObject.GetComponent<LineRenderer>();
                        BoxLines.startWidth = 0.04f;
                        BoxLines.endWidth = 0.04f;
                        BoxLines.startColor = Color.Lerp(Color.magenta, Main.Purple(), Mathf.PingPong(Time.time, 1f));
                        BoxLines.endColor = Color.Lerp(Color.magenta, Main.Purple(), Mathf.PingPong(Time.time, 1f));
                        BoxLines.material.shader = Shader.Find("GUI/Text Shader");
                        BoxLines.positionCount = 4;
                        BoxLines.loop = true;

                        Vector3 pivotPos = vrrig.transform.position;
                        Vector3 directionToHead = GorillaTagger.Instance.headCollider.transform.position - pivotPos;
                        Vector3 rightOffset = Vector3.Cross(directionToHead.normalized, Vector3.up).normalized * 0.35f;
                        Vector3 upOffset = Vector3.up * 0.45f;

                        Vector3 playerBoxOffset0 = pivotPos - rightOffset - upOffset;
                        Vector3 playerBoxOffset1 = pivotPos + rightOffset - upOffset;
                        Vector3 playerBoxOffset2 = pivotPos + rightOffset + upOffset;
                        Vector3 playerBoxOffset3 = pivotPos - rightOffset + upOffset;

                        BoxLines.SetPosition(0, playerBoxOffset0);
                        BoxLines.SetPosition(1, playerBoxOffset1);
                        BoxLines.SetPosition(2, playerBoxOffset2);
                        BoxLines.SetPosition(3, playerBoxOffset3);
                        UnityEngine.Object.Destroy(BoxLines, Time.deltaTime);
                    }
                }
            }
        }
        public static void Skele()
        {
            Material material = new Material(Shader.Find("GUI/Text Shader"));
            material.color = Color.Lerp(Color.magenta, Main.Purple(), Mathf.PingPong(Time.time, 1f));
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (!vrrig.head.rigTarget.gameObject.GetComponent<LineRenderer>())
                {
                    vrrig.head.rigTarget.gameObject.AddComponent<LineRenderer>();
                }
                vrrig.head.rigTarget.gameObject.GetComponent<LineRenderer>().endWidth = 0.01f;
                vrrig.head.rigTarget.gameObject.GetComponent<LineRenderer>().startWidth = 0.01f;
                vrrig.head.rigTarget.gameObject.GetComponent<LineRenderer>().material = material;
                vrrig.head.rigTarget.gameObject.GetComponent<LineRenderer>().SetPosition(0, vrrig.head.rigTarget.transform.position + new Vector3(0f, 0.16f, 0f));
                vrrig.head.rigTarget.gameObject.GetComponent<LineRenderer>().SetPosition(1, vrrig.head.rigTarget.transform.position - new Vector3(0f, 0.4f, 0f));
                for (int i = 0; i < Enumerable.Count<int>(bones); i += 2)
                {
                    if (!vrrig.mainSkin.bones[bones[i]].gameObject.GetComponent<LineRenderer>())
                    {
                        vrrig.mainSkin.bones[bones[i]].gameObject.AddComponent<LineRenderer>();
                    }
                    vrrig.mainSkin.bones[bones[i]].gameObject.GetComponent<LineRenderer>().endWidth = 0.01f;
                    vrrig.mainSkin.bones[bones[i]].gameObject.GetComponent<LineRenderer>().startWidth = 0.01f;
                    vrrig.mainSkin.bones[bones[i]].gameObject.GetComponent<LineRenderer>().material = material;
                    vrrig.mainSkin.bones[bones[i]].gameObject.GetComponent<LineRenderer>().SetPosition(0, vrrig.mainSkin.bones[bones[i]].position);
                    vrrig.mainSkin.bones[bones[i]].gameObject.GetComponent<LineRenderer>().SetPosition(1, vrrig.mainSkin.bones[bones[i + 1]].position);
                }
            }
        }
        public static int[] bones = new int[]
{
            4,
            3,
            5,
            4,
            19,
            18,
            20,
            19,
            3,
            18,
            21,
            20,
            22,
            21,
            25,
            21,
            29,
            21,
            31,
            29,
            27,
            25,
            24,
            22,
            6,
            5,
            7,
            6,
            10,
            6,
            14,
            6,
            16,
            14,
            12,
            10,
            9,
            7
};
        public static void DeleSkele()
        {
            foreach (VRRig vrrig2 in GorillaParent.instance.vrrigs)
            {
                for (int j = 0; j < Enumerable.Count<int>(bones); j += 2)
                {
                    if (vrrig2.mainSkin.bones[bones[j]].gameObject.GetComponent<LineRenderer>())
                    {
                        UnityEngine.Object.Destroy(vrrig2.mainSkin.bones[bones[j]].gameObject.GetComponent<LineRenderer>());
                    }
                    if (vrrig2.head.rigTarget.gameObject.GetComponent<LineRenderer>())
                    {
                        UnityEngine.Object.Destroy(vrrig2.head.rigTarget.gameObject.GetComponent<LineRenderer>());
                    }
                }
            }
        }
        public static void boxesp()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != GorillaTagger.Instance.offlineVRRig)
                {
                    GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    box.transform.position = rig.transform.position;
                    box.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    box.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    box.GetComponent<Renderer>().material.color = Color.Lerp(Color.magenta, Main.Purple(), Mathf.PingPong(Time.time, 1f));
                    UnityEngine.Object.Destroy(box.GetComponent<BoxCollider>());
                    UnityEngine.Object.Destroy(box, Time.deltaTime);
                }
            }
            
        }

        public static void headdot()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != GorillaTagger.Instance.offlineVRRig)
                {
                    GameObject dot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    dot.transform.position = rig.headMesh.transform.position;
                    dot.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    dot.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                    dot.GetComponent<Renderer>().material.color = Color.Lerp(Color.magenta, Main.Purple(), Mathf.PingPong(Time.time, 1f));
                    UnityEngine.Object.Destroy(dot.GetComponent<BoxCollider>());
                    UnityEngine.Object.Destroy(dot, Time.deltaTime);
                }
            }

        }

        public static void chams()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig != GorillaTagger.Instance.offlineVRRig)
                {
                    rig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                    rig.mainSkin.material.color = Color.Lerp(Color.magenta, Main.Purple(), Mathf.PingPong(Time.time, 1f));
                }
            }
        }

        public static void turnoffchams()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                rig.mainSkin.material.shader = Shader.Find("GorillaTag/UberShader");
                rig.mainSkin.material.color = rig.playerColor;
            }
        }
    }

    internal class Main2
    {
        public static void AddPages(bool fun)
        {
            if (fun)
            {
                PageButtons = true;
                Main.RecreateMenu();
            }
            else
            {
                PageButtons = false;
                Main.RecreateMenu();
            }
        }
        public static void Menuoutlinee(bool truefalse)
        {
            if (truefalse)
            {
                Menuoutline = 0;
                fortniteee = true;
                Main.RecreateMenu();
            }
            else
            {
                fortniteee = false;
                Menuoutline = 1;
                Main.RecreateMenu();
            }
        }

    }

    internal class Connections
    {
        public static void Disconnect()
        {
            SavedRoom = PhotonNetwork.CurrentRoom.Name;
            if (SavedRoom == PhotonNetwork.CurrentRoom.Name)
            PhotonNetwork.Disconnect();
        }
        static string SavedRoom;
        public static void RejoinLastRoom()
        {
            PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(SavedRoom.ToUpper());
        }
        public static void CreatePrivateRoom()
        {
            PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(random().ToUpper());
        }
        public static void CreatePubRoom()
        {
            PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(randomer().ToUpper());
        }

        static int gothroughrandomnames;
        static int gothroughrandomnames2;
        public static string randomer()
        {
            gothroughrandomnames2 = UnityEngine.Random.Range(0, fortnite.Length);
            return roomnames[gothroughrandomnames2];
        }
        public static string[] fortnite = { "DAEF", "AEGS", "RFES", "DAISY09", "K9", "JMAN"};

        public static string random()
        {
            gothroughrandomnames = UnityEngine.Random.Range(0, roomnames.Length);
            return roomnames[gothroughrandomnames];
        }
        

        public static string[] roomnames = new string[]
        {
         "eOh14jXl8G57YpR", "sN2qT0eZ1c5fLdG", "mH3rYb6xV7zFjOw", "vC9pA3lK0jG7qZd", "tM8bN9xH1cJ5kGq",
         "rX5iL0aS2pR7jWv", "oV3sZ4mU1lA6gPx", "iS6nH2yP9oB8qEj", "fT3uW4dX2eR7hIg", "wZ5vM0qA8pJ9cHb",
         "dF1kS6jL7tY9oVz", "jX2oC5hG3rQ4sBm", "bU0nI4rF8tD3kHs", "gJ1mV6oL9fY2xSd", "aC7fP3zQ6yK4rEw",
         "hG4bT9wS8pX1zFv", "kD3gO7rV2jZ5mTn", "xW5eY1pH6qR0iMl", "yN0zQ8wV4uE7lAe", "pL9oX6iW7sR2kCf",
    "qV7tK0uG6wE3jOy", "uB2aZ5sD1xM4cQv", "cE1vR4uP8mY0oLg", "lH8xI3fZ9nU7jDq", "zR2dJ5bA0qP3nGt",
    "nY1pW3mG9vB8iLx", "mT4cZ7jN2xK5uSf", "oH6sA8dY0nV3qJb", "iU7rX5gM4yP1tEw", "eF3qV9cK1dW0gZl",
    "xO1rW5yP6dZ2vMh", "bG9zK2lH3sX8jRf", "aS7hR0wQ1mU6oTc", "cB8tD4sX5gY2zAq", "dE3uV7oP6tH4wSb",
    "jQ9wM1xY8lN0fDg", "fZ4jT6uH7yV9wAe", "qP3oW7gK5nD8uXl", "kN0mR2aS9zT1pCv", "pX5dV3hL4rQ2bOy",
    "rU6bM2tW1aG8zQn", "tA9vJ3yF7xK4iCg", "vD8nF6cZ5oQ0mYs", "gB1eH4lS9fV7jRt", "hL0zU7mY8bN3qGf",
    "iR4qZ9cT0uE3wMv", "wO8yK1jX5pH6gDl", "xG5tW4uQ2jC8zNv", "sP9yE3nK7lM0fHb", "bV6gA0zO1dY8cQw"

        };

        public static void LobbyHopper()
        {
            SavedRoom = PhotonNetwork.CurrentRoom.Name;
            if (PhotonNetwork.InRoom || PhotonNetwork.InLobby && Time.time > timer + 0.5f && SavedRoom == PhotonNetwork.CurrentRoom.Name)
            {
                timer = Time.time;
                PhotonNetwork.Disconnect();
            }
            else
            {
                PhotonNetwork.JoinRandomRoom();
            }
        }

    }
}
