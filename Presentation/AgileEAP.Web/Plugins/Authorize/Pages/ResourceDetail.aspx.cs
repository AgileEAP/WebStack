﻿#region Description
/*================================================================================
 *  Copyright (c) agile.  All rights reserved.
 * ===============================================================================
 * Solution: AuthorizeCenter
 * Module:  Resource
 * Descrption:
 * CreateDate: 2010-11-2 21:27:00
 * Author: trenhui
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


using AgileEAP.Core;
using AgileEAP.Core.Data;

using AgileEAP.Core.Extensions;
using AgileEAP.Core.Web;

using AgileEAP.Infrastructure.Domain;
using AgileEAP.Infrastructure.Service;

namespace AgileEAP.Plugin.Authorize
{
    public partial class ResourceDetail : BasePage
    {

        #region ---界面处理方法---

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack && !IsAjaxPost)
                ShowPageDetail();
        }

        protected void ShowPageDetail()
        {
            cboShowNavigation.BindCombox(Options.Yes);
            cboShowToolBar.BindCombox(Options.Yes);
            cboType.BindCombox(ResourceType.Menu);
            cboOpenMode.BindCombox(MenuTarget.NavigateFrame);

            Resource parent = null;
            if (PageContext.Action == ActionType.Add)
            {
                parent = repository.GetDomain<Resource>(Request.QueryString["ParentID"]);
                if (parent != null)
                {
                    chbParentID.Text = parent.Text;
                    chbParentID.Value = parent.ID;

                    //App parentApp = repository.GetDomain<App>(parent.AppID);
                    //if (parentApp != null)
                    //{
                    //    chbAppID.Text = parentApp.Text;
                    //    chbAppID.Value = parentApp.ID;
                    //}

                    //AgileEAP.Infrastructure.Domain.Module parentModule = repository.GetDomain<Module>(parent.ModuleID);
                    //if (parentModule != null)
                    //{
                    //    chbModuleID.Text = parentModule.Text;
                    //    chbModuleID.Value = parentModule.ID;
                    //}

                    //Organization parentOrg = repository.GetDomain<Organization>(parent.OwnerOrg);
                    //if (parentOrg != null)
                    //{
                    //    chbOwnerOrg.Text = parentOrg.Name;
                    //    chbOwnerOrg.Value = parentOrg.ID;
                    //}
                }
                return;
            }

            Resource entity = repository.GetDomain<Resource>(CurrentId);

            if (entity == null) return;

            SetControlValues(entity);

            parent = repository.GetDomain<Resource>(entity.ParentID);
            if (parent != null)
            {
                chbParentID.Text = parent.Text;
                chbParentID.Value = parent.ID;
            }

            //AgileEAP.Infrastructure.Domain.Module module = repository.GetDomain<Module>(entity.ModuleID);
            //if (module != null)
            //{
            //    chbModuleID.Text = module.Text;
            //    chbModuleID.Value = module.ID;
            //}

            //AgileEAP.Infrastructure.Domain.App app = repository.GetDomain<App>(entity.AppID);
            //if (app != null)
            //{
            //    chbAppID.Text = app.Text;
            //    chbAppID.Value = app.ID;
            //}

            //Organization org = repository.GetDomain<Organization>(entity.OwnerOrg);
            //if (org != null)
            //{
            //    chbOwnerOrg.Text = org.Name;
            //    chbOwnerOrg.Value = org.ID;
            //}

            List<string> operateIDs = repository.Query<Privilege>().Where(p => p.ResourceID == entity.ID).Select(p => p.OperateID).ToList();
            rptOperate.DataSource = repository.All<Operate>().Where(o => operateIDs.Contains(o.ID)).OrderBy(o => o.SortOrder).ToList();
            rptOperate.DataBind();
        }

        #endregion

        #region ---操作处理方法---
        protected Resource GetDomainObject()
        {
            Resource resource = repository.GetDomain<Resource>(CurrentId);

            if (resource == null)
            {
                resource = new Resource();
                resource.ID = CurrentId;
            }

            GetControlValues(ref resource);

            return resource;
        }

        public string Save(string argument)
        {
            AjaxResult ajaxResult = new AjaxResult();

            string errorMsg = string.Empty;
            DoResult doResult = DoResult.Failed;
            string actionMessage = string.Empty;
            try
            {
                Resource resource = JsonConvert.DeserializeObject<Resource>(argument);
                resource.ID = string.IsNullOrEmpty(CurrentId) ? CurrentId = IdGenerator.NewComb().ToString() : CurrentId;

                foreach (var operate in resource.Operates)
                {
                    operate.ID = string.IsNullOrWhiteSpace(operate.ID) ? IdGenerator.NewComb().ToString() : operate.ID;
                }

                IAuthorizeService authService = new AuthorizeService();
                authService.SaveResource(resource);
                doResult = DoResult.Success;

                //获取提示信息
                actionMessage = string.Format("保存菜单资源{0}成功", resource.Name);

                //记录操作日志
                AddActionLog(resource, doResult, actionMessage);

                ajaxResult.Result = doResult;
                ajaxResult.RetValue = resource.ID;
                ajaxResult.PromptMsg = actionMessage;
            }
            catch (Exception ex)
            {
                actionMessage = RemarkAttribute.GetEnumRemark(doResult);
                log.Error(actionMessage, ex);
            }

            return JsonConvert.SerializeObject(ajaxResult);
        }
        #endregion
    }
}
