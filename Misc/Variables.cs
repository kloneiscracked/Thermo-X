using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Viveport;

namespace Termo.Misc
{
    internal class Variables
    {


        public static GameObject menu, PointerObj;
        public static GameObject menuBackground;
        public static GameObject reference;
        public static GameObject canvasObject;
        public static bool CheckpointTog = false, ColorChangeThingy = false, nofingers = false;
        public static int Menuoutline = 0;
        public static bool fortniteee = true;
        public static GameObject checkker;
        public static SphereCollider buttonCollider;
        public static Camera TPC;
        public static Text fpsObject;
        public static int buttonsType = 0, TriggerType, pageNumber = 0, FlyModoIndex = 0, SpeedIndex = 0, Toiletter, timeindex = 0;
        public static float timer, speed = 15, BoostSpeed = 1.2f, Timerr, Timerr2, WalkWalkspeed = 5, Timmmemrmemrmermsemrmse34mrmse;
        public static bool DoOnce = true, Destroys = true, PageButtons = false, GlassMat = false, Rtoggle = true, Ltoggle = true;
        public static GameObject Robject, Lobject;
        public static PrimitiveType PlatformPrim = PrimitiveType.Cube;
        public static Material Uber = new Material(Shader.Find("GorillaTag/UberShader"));
        public static bool TrigPages = true, GripPages = false;
        public static bool bothhandguns = false;
        public static Material loadimagefromurl(string url)
        {
            Material m;
            WebClient c = new WebClient();
            byte[] array = c.DownloadData(url);
            m = new Material(Shader.Find("GorillaTag/UberShader"));
            m.shaderKeywords = new string[]
            {
                    "_USE_TEXTURE"
            };
            string text = Application.dataPath;
            text = text.Replace("/Gorilla Tag_Data", "");
            Texture2D texture2D = new Texture2D(4096, 4096);
            ImageConversion.LoadImage(texture2D, array);
            m.mainTexture = texture2D;
            texture2D.Apply();
            return m;
        }
        public static Material MenuMaterial = loadimagefromurl("https://inara.cz/data/gallery/251/251423x1848.jpg");
        
    }
}
