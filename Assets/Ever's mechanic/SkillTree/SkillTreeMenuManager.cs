using Unity.FPS.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class SkillTreeMenuManager : MonoBehaviour
{
    int skillPoints = 0;

    private void Start()
    {
        EventManager.AddListener<LevelUpEvent>(LevelUp);
    }

    private void LevelUp(LevelUpEvent _event)
    {
        skillPoints+=2;
        Debug.Log(skillPoints);
    }

    void BuySkill(string _skillName)
    {

    }









    //[Tooltip("Master volume when menu is open")]
    //[Range(0.001f, 1f)]
    //public float VolumeWhenMenuOpen = 0.5f;

    //[Tooltip("Root GameObject of the Skill Tree used to toggle its activation")]
    //public GameObject SkillTree;

    //[Tooltip("GameObject for the physical skills")]
    //public GameObject PhysicalSkillsImage;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    SkillTree.SetActive(false);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (/*!MenuRoot.activeSelf &&*/ !SkillTree.activeSelf && Input.GetMouseButtonDown(0))
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //        Cursor.visible = false;
    //    }
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //        Cursor.visible = true;
    //    }
    //    if (Input.GetButtonDown(GameConstants.k_ButtonNameSkillTree)
    //            || (SkillTree.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel)))
    //    {
    //        if (PhysicalSkillsImage.activeSelf)
    //        {
    //            PhysicalSkillsImage.SetActive(false);
    //            return;
    //        }
    //        Debug.Log("OPEN SKILL TREE");
    //        SetSkillTreeActivation(!SkillTree.activeSelf);
    //    }
    //}
    //public void CloseSkillTree()
    //{
    //    SetSkillTreeActivation(false);
    //}
    //void SetSkillTreeActivation(bool active)
    //{
    //    SkillTree.SetActive(active);

    //    if (SkillTree.activeSelf)
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //        Cursor.visible = true;
    //        Time.timeScale = 0f;
    //        AudioUtility.SetMasterVolume(VolumeWhenMenuOpen);

    //        EventSystem.current.SetSelectedGameObject(null);
    //    }
    //    else
    //    {
    //        Cursor.lockState = CursorLockMode.Locked;
    //        Cursor.visible = false;
    //        Time.timeScale = 1f;
    //        AudioUtility.SetMasterVolume(1);
    //    }

    //}
}
