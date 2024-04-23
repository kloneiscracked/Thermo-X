using static Menu.Main;
using static Sigma.Settings;
using static Termo.Misc.Variables;

namespace Mods
{
    internal class EnterThings
    {
        public static void ReturnHome()
        {
            buttonsType = 0;
            pageNumber = 0;
        }
        public static void EnterSettings()
        {
            buttonsType = 1;
            pageNumber = 0;
        }

        public static void EnterConnections()
        {
            buttonsType = 2;
            pageNumber = 0;
        }

        public static void EnterMain()
        {
            pageNumber = 0;
            buttonsType = 3;
        }

        public static void EnterVisuals()
        {
            buttonsType = 4;
            pageNumber = 0;
        } 
        public static void EnterPlayer()
        {
            buttonsType = 5;
            pageNumber = 0;
        } 
        public static void EnterFun()
        {
            buttonsType = 6;
            pageNumber = 0;
        }
        public static void EnterSafety()
        {
            buttonsType = 7;
            pageNumber = 0;
        }




    }
    internal class Menu
    {
        public static void DrakeSnake()
        {
            TriggerType++;
            if (TriggerType == 0)
            {
                TrigPages = true;
                GripPages = false;
            }
            if (TriggerType == 1)
            {
                TrigPages = false;
                GripPages = true;
                GetIndex("Page Type: Triggers").String = "Page Type: Grips";
            }
            if (TriggerType >= 2)
            {
                TrigPages = true;
                GripPages = false;
                TriggerType = 0;
                GetIndex("Page Type: Grips").String = "Page Type: Triggers";
            }
            RecreateMenu();
        }
        public static void ChangeTheme()
        {
            Toiletter++;
            RecreateMenu();
        }
    }


}
