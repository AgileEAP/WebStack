﻿#region Description
/*================================================================================
 *  Copyright (c) SunTek.  All rights reserved.
 * ===============================================================================
 * Solution: AuthorizeCenter
 * Module:  Shortcut
 * Descrption:
 * CreateDate: 2010/11/18 13:55:35
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
using System.Collections;
using System.Collections.Generic;

using AgileEAP.Core.Domain;
using AgileEAP.Core.Extensions;



namespace AgileEAP.Infrastructure.Domain
{
    public partial class Shortcut :DomainObject<string>
    {
        #region Fields
		
		private string _operatorID = string.Empty;
		private string _privilegeID = string.Empty;
		private string _appID = string.Empty;
		private int _sortOrder = default(Int32);
		private string _icon = string.Empty;
		
		
        #endregion

        #region Constructors
		public Shortcut(){}
		
		
		public Shortcut(string id,string operatorID,string privilegeID,string appID,int sortOrder,string icon)
		{
		  this.ID=id;
		this._operatorID=operatorID;
		this._privilegeID=privilegeID;
		this._appID=appID;
		this._sortOrder=sortOrder;
		this._icon=icon;
		}
        #endregion

        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(_operatorID);
			sb.Append(_privilegeID);
			sb.Append(_appID);
			sb.Append(_sortOrder);
			sb.Append(_icon);

            return sb.ToString().GetHashCode();
        }
		
		public virtual bool Validate()
        {
			return true;
        }

        #endregion

        #region Properties
		
		/// <summary>
        /// 操作员ID
        /// </summary>
		public virtual string OperatorID
        {
            get { return  _operatorID; }
			set	{	_operatorID =  value;}
        }
		
		/// <summary>
        /// 权限ID
        /// </summary>
		public virtual string PrivilegeID
        {
            get { return  _privilegeID; }
			set	{	_privilegeID =  value;}
        }
		
		/// <summary>
        /// 冗余字段
        /// </summary>
		public virtual string AppID
        {
            get { return  _appID; }
			set	{	_appID =  value;}
        }
		
		/// <summary>
        /// 序号
        /// </summary>
		public virtual int SortOrder
        {
            get { return  _sortOrder; }
			set	{	_sortOrder =  value;}
        }
		
		/// <summary>
        /// 快捷菜单图标
        /// </summary>
		public virtual string Icon
        {
            get { return  _icon; }
			set	{	_icon =  value;}
        }
		
        #endregion
    }
}
