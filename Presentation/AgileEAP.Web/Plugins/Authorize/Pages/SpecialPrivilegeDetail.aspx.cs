﻿#region Description
/*================================================================================
 *  Copyright (c) agile.  All rights reserved.
 * ===============================================================================
 * Solution: AuthorizeCenter
 * Module:  RolePrivilege
 * Descrption:
 * CreateDate: 2010/11/18 13:55:37
 * Author: ljz
 * Version:1.0
 * ===============================================================================
 * History
 *
 * Update Descrption:
 * Remark:
 * Update Time:
 * Author:generated by codesmithTemplate
 * 
 * ===============================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;


using AgileEAP.Core;

using AgileEAP.Core.Service;
using AgileEAP.Core.Security;
using AgileEAP.Core.Extensions;
using AgileEAP.Core.Web;
using AgileEAP.Core.Caching;
using AgileEAP.Core.Utility;
using AgileEAP.Core.FastInvoker;
using AgileEAP.WebControls;
using AgileEAP.Infrastructure.Domain;
using AgileEAP.Infrastructure.Service;



namespace AgileEAP.Plugin.Authorize
{

    public partial class SpecialPrivilegeDetail : BasePage
    {
        AuthorizeService authService = new AuthorizeService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string userID = Request.QueryString["UserID"];
                short authFlag = Request.QueryString["AuthFlag"].ToSafeString().ToShort(0);
                //初始化角色权限
                InitRolePrivilege(userID, authFlag);
            }
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitRolePrivilege(string userID, short authFlag)
        {
            try
            {
                AjaxTree1.PostType = PostType.NoPost;
                AjaxTree1.IsAjaxLoad = false;
                AjaxTree1.ShowNodeIco = false;
                AjaxTree1.ShowCheckBox = true;
                AjaxTree1.SelectionMode = authFlag == 2 ? SelectionMode.Multiple : SelectionMode.RelatedMultiple;
                AjaxTree1.Nodes.Clear();

                Resource appResource = repository.All<Resource>().FirstOrDefault(o => o.ID == AppID);
                string rootPriId = GetPidByResId(appResource.ID);
                List<string> privilegeIDs = authService.GetAllPrivilegeIDs(userID);

                IDictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.SafeAdd("OperatorID", userID);
                parameters.SafeAdd("AuthFlag", authFlag);
                List<SpecialPrivilege> sPrivileges =repository.FindAll<SpecialPrivilege>(parameters).ToList();
                List<string> sPrivilegeIDs = sPrivileges == null ? new List<string>() : sPrivileges.Select(o => o.PrivilegeID).ToList();

                AjaxTreeNode appNode = new AjaxTreeNode()
                {
                    ID = rootPriId,
                    Text = appResource.Text,
                    Value = rootPriId,
                    Tag = "root",
                    IcoSrc = string.Format("{0}Plugins/Authorize/Content/Themes/{1}/Images/menu.gif", WebUtil.GetRootPath(), Skin),
                    NodeState = NodeState.Open,
                    Checked = sPrivilegeIDs.Contains(rootPriId),
                    ShowCheckBox = !(authFlag == 1 && (privilegeIDs.Contains(rootPriId)))

                };
                AjaxTree1.Nodes.Add(appNode);

                List<Resource> resources = repository.All<Resource>().Where(o => o.ParentID == appResource.ID && (IsAdmin || GetResourceIDsByUser().Contains(o.ID))).OrderBy(o => o.SortOrder).ToList();
                resources = ResourcesFilter(resources, privilegeIDs, authFlag);
                foreach (Resource resource in resources)
                {
                    string priId = GetPidByResId(resource.ID);
                    AjaxTreeNode node = new AjaxTreeNode()
                    {
                        ID = priId,
                        Text = resource.Text,
                        Value = resource.ID,
                        Tag = ResourceType.Page.ToString(),
                        NodeState = NodeState.Open,
                        Checked = sPrivilegeIDs.Contains(priId),
                        ShowCheckBox = !(authFlag == 1 && privilegeIDs.Contains(priId))
                    };
                    AddChildResource(node, privilegeIDs, sPrivilegeIDs, authFlag);
                    appNode.ChildNodes.SafeAdd(node);
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("加载用户{0}特殊权限出错", userID), ex);
            }
        }

        /// <summary>
        /// 根据特殊权限的类型过滤相应的权限
        /// </summary>
        /// <param name="resources"></param>
        /// <param name="privilegeIDs"></param>
        /// <param name="authFlag"></param>
        /// <returns></returns>
        List<Resource> ResourcesFilter(List<Resource> resources, List<string> privilegeIDs, short authFlag)
        {
            //按类型过滤
            if (authFlag == 1) //开通
            {
                return resources;
            }
            else if (authFlag == 2)
            {
                return resources.Where(o => privilegeIDs.Contains(GetPidByResId(o.ID))).ToList();//只显示角色已经开通了的
            }
            return null;
        }

        List<string> authorizedResources = new List<string>();
        /// <summary>
        /// 获取当前操作员的资源ID集合
        /// </summary>
        private List<string> GetResourceIDsByUser()
        {
            if (authorizedResources == null || authorizedResources.Count == 0)
            {
                List<string> privilegeIDsByUser = authService.GetPrivilegeIDs(User.ID);
                foreach (string privilegeIDByUser in privilegeIDsByUser)
                {
                    if (repository.GetDomain<Privilege>(privilegeIDByUser) != null)
                        authorizedResources.Add((repository.GetDomain<Privilege>(privilegeIDByUser)).ResourceID);
                }
            }

            return authorizedResources;
        }

        void AddChildResource(AjaxTreeNode parentNode, List<string> privilegeIDs, List<string> sPrivilegeIDs, short authFlag)
        {
            List<Resource> resources = repository.All<Resource>().Where(o => o.ParentID == parentNode.Value && (IsAdmin || authorizedResources.Contains(o.ID))).OrderBy(o => o.SortOrder).ToList();
            resources = ResourcesFilter(resources, privilegeIDs, authFlag);

            foreach (var resource in resources)
            {
                string priId = GetPidByResId(resource.ID);
                AjaxTreeNode node = new AjaxTreeNode()
                {
                    ID = priId,
                    Text = resource.Text,
                    Value = resource.ID,
                    NodeState = NodeState.Close,
                    IcoSrc = string.Format("{0}Plugins/Authorize/Content/Themes/{1}/Images/{2}", WebUtil.GetRootPath(), Skin, getResourceIcon(resource.Type.Cast<ResourceType>(ResourceType.Menu))),
                    Checked = sPrivilegeIDs.Contains(priId),
                    ShowCheckBox = !(authFlag == 1 && privilegeIDs.Contains(priId))
                };

                BuildOperate(node, privilegeIDs, sPrivilegeIDs, authFlag);

                parentNode.ChildNodes.SafeAdd(node);
                AddChildResource(node, privilegeIDs, sPrivilegeIDs, authFlag);
            }
        }

        /// <summary>
        /// 绑定操作项（因为树用了级联，所以判断如果下边有操作项的时候，将模拟一个访问）
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="privilegeIDs">权限集合</param>
        void BuildOperate(AjaxTreeNode parentNode, List<string> privilegeIDs, List<string> sPrivilegeIDs, short authFlag)
        {
            List<AgileEAP.Infrastructure.Domain.Privilege> operates = repository.All<Privilege>().Where(p => p.ResourceID == parentNode.Value && !string.IsNullOrWhiteSpace(p.OperateID)).ToList(); //取得资源相关的所有操作项id

            //按类型过滤
            if (authFlag == 2)
            {
                operates = operates.Where(o => privilegeIDs.Contains(o.ID)).ToList();//只显示角色已经开通了的
            }

            //判断如果下边有操作项的时候，将模拟一个访问
            if (operates.Count > 0)
            {
                AjaxTreeNode node = new AjaxTreeNode()
                {
                    ID = "tempAccess",
                    Text = "访问",
                    Value = "tempAccess",
                    Tag = "Operate",
                    ShowCheckBox = parentNode.ShowCheckBox
                };
                parentNode.ChildNodes.SafeAdd(node);
            }

            foreach (var operate in operates)
            {
                AjaxTreeNode node = new AjaxTreeNode()
                {
                    ID = operate.ID,
                    Text = operate.Name,
                    Value = operate.ID,
                    Tag = "Operate",
                    Checked = sPrivilegeIDs.Contains(operate.ID),
                    ShowCheckBox = !(authFlag == 1 && privilegeIDs.Contains(operate.ID))
                };
                parentNode.ChildNodes.SafeAdd(node);
            }
        }


        /// <summary>
        /// 设置目录图标
        /// </summary>
        /// <param name="ResourceType"></param>
        /// <returns></returns>
        private string getResourceIcon(ResourceType ResourceType)
        {
            string icon = "menu.gif";

            switch (ResourceType)
            {
                case ResourceType.Button:
                    icon = "menu.gif";
                    break;
                case ResourceType.Menu:
                    icon = "menu.gif";
                    break;
                case ResourceType.Page:
                    icon = "menu.gif";
                    break;
            }
            return icon;
        }

        /// <summary>
        /// 创建目录树方法
        /// </summary>
        /// <param name="tn">目录树的节点</param>
        private void BuildTree(AjaxTreeNode tn)
        {
            List<string> privilegeIDs = authService.GetPrivilegeIDs(User.ID);
            List<string> resourceIDs = new List<string>();
            foreach (string privilegeID in privilegeIDs)
            {
                resourceIDs.Add(repository.GetDomain<Privilege>(privilegeID).ResourceID);
            }
            List<Resource> resources = repository.All<Resource>().Where(o => o.ParentID == tn.ID && resourceIDs.Contains(o.ID)).OrderBy(o => o.SortOrder).ToList();

            foreach (var res in resources)
            {
                AjaxTreeNode node = new AjaxTreeNode()
                {
                    ID = res.ID,
                    Text = res.Text,
                    Value = res.ID,
                    Tag = res.Type.ToString(),
                    NodeIcoSrc = tn.NodeIcoSrc,
                    IcoSrc = string.Format("{0}Plugins/Authorize/Content/Themes/{1}/Images/{2}", WebUtil.GetRootPath(), Skin, getResourceIcon(res.Type.Cast<ResourceType>(ResourceType.Menu)))
                };

                tn.ChildNodes.Add(node);
                //递归获取目录树
                BuildTree(node);
            }
        }

        public string Save(string argument)
        {
            AjaxResult ajaxResult = new AjaxResult();
            try
            {
                IList<string> parameters = argument.Trim(',').Split(',').ToList();
                // IList<AjaxTreeNode> parameters = JsonConvert.DeserializeObject<IList<AjaxTreeNode>>(argument);
                string userID = Request.QueryString["UserID"];
                short authFlag = Request.QueryString["AuthFlag"].ToSafeString().ToShort(0);
                if (string.IsNullOrEmpty(userID))
                {
                    ajaxResult.Result = DoResult.Failed;
                    ajaxResult.PromptMsg = "用户对象为空！";
                    log.Error("用户对象为空");
                    return JsonConvert.SerializeObject(ajaxResult);
                }

                if (string.IsNullOrEmpty(userID))
                {
                    ajaxResult.Result = DoResult.Failed;
                    ajaxResult.PromptMsg = "角色ID为空！";
                    log.Error("角色ID为空");
                    return JsonConvert.SerializeObject(ajaxResult);
                }

                if (parameters == null || parameters.Count == 0)
                {
                    ajaxResult.PromptMsg = "您没有选择任何资源，请选择后再保存！";
                    return JsonConvert.SerializeObject(ajaxResult);
                }

                IList<SpecialPrivilege> specialPrivileges = parameters.Where(o => o != "tempAccess").Select(p => new SpecialPrivilege()
                {
                    ID = IdGenerator.NewComb().ToSafeString(),
                    AuthFlag = authFlag,
                    Authorizer = User.ID,
                    AuthTime = DateTime.Now,
                    OperatorID = userID,
                    OwnerOrg = GetOrgPath(),
                    PrivilegeID = p
                }).ToList();

                authService.SaveSpecialPrivilege(specialPrivileges);

                ajaxResult.RetValue = string.Empty;
                ajaxResult.Result = DoResult.Success;
                ajaxResult.PromptMsg = "保存成功！";
                WebUtil.CloseDialog();
            }
            catch (Exception ex)
            {
                ajaxResult.Result = DoResult.Failed;
                ajaxResult.PromptMsg = "保存角色出错，请联系管理员！";
                log.Error(ex);
            }
            return JsonConvert.SerializeObject(ajaxResult);
        }

        string GetPidByResId(string resourceID)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.SafeAdd("id", new AgileEAP.Core.Data.Condition(string.Format("(type={0} or type={1}) and ResourceID='{2}'", (int)ResourceType.Menu, (int)ResourceType.Page, resourceID)));

            var p = repository.FindAll<Privilege>(parameters);
            if (p != null && p.Count > 0) return p[0].ID;

            return string.Empty;
            // return new AgileEAP.Infrastructure.Service.PrivilegeService().GetPidByResId(resourceID);
        }
    }
}
