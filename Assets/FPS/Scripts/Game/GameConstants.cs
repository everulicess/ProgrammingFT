using System.Collections.Generic;
namespace Unity.FPS.Game
{
    public enum MySkills
    {
        Health,
        Healing,
        LauncherDamage,
        LauncherAmmo,
        ShotgunDamage,
        ShotgunAmmo,
        BlasterDamage,
        BlasterAmmo,
        Dash,
        Speed,
    }
    public class GameConstants
    {
        public static Dictionary<MySkills, float> SkillData = new Dictionary<MySkills, float>()
        {
            { MySkills.BlasterDamage, 10f},
            { MySkills.BlasterAmmo, 5f},
            { MySkills.LauncherDamage, 10f},
            { MySkills.LauncherAmmo, 2f},
            { MySkills.ShotgunDamage, 4f},
            { MySkills.ShotgunAmmo, 5f},
            { MySkills.Healing, 20f},
            { MySkills.Health, 50f},
            { MySkills.Speed, 5f}
        };
        // all the constant string used across the game
        public const string k_AxisNameVertical = "Vertical";
        public const string k_AxisNameHorizontal = "Horizontal";
        public const string k_MouseAxisNameVertical = "Mouse Y";
        public const string k_MouseAxisNameHorizontal = "Mouse X";
        public const string k_AxisNameJoystickLookVertical = "Look Y";
        public const string k_AxisNameJoystickLookHorizontal = "Look X";

        public const string k_ButtonNameAim = "Aim";
        public const string k_ButtonNameFire = "Fire";
        public const string k_ButtonNameSprint = "Sprint";
        public const string k_ButtonNameJump = "Jump";
        public const string k_ButtonNameCrouch = "Crouch";

        public const string k_ButtonNameGamepadFire = "Gamepad Fire";
        public const string k_ButtonNameGamepadAim = "Gamepad Aim";
        public const string k_ButtonNameSwitchWeapon = "Mouse ScrollWheel";
        public const string k_ButtonNameGamepadSwitchWeapon = "Gamepad Switch";
        public const string k_ButtonNameNextWeapon = "NextWeapon";
        public const string k_ButtonNamePauseMenu = "Pause Menu";
        public const string k_ButtonNameSubmit = "Submit";
        public const string k_ButtonNameCancel = "Cancel";
        public const string k_ButtonReload = "Reload";
        //LevelUP
        public const string k_ButtonNameLevelUp = "LevelUp";
        //SkillTree
        public const string k_ButtonNameSkillTree = "Skill Tree";
        //Dash
        public const string k_ButtonNameDash = "Dash";

    }
}