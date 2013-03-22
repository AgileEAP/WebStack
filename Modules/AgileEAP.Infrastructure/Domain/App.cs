﻿#region Description
/*================================================================================
 *  Copyright (c) SunTek.  All rights reserved.
 * ===============================================================================
 * Solution: Infrastructure
 * Module:  App
 * Descrption:
 * CreateDate: 2010/11/23 10:05:33
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
    public partial class App :DomainObject<string>
    {
        #region Fields
		
		private string _name = string.Empty;
		private string _text = string.Empty;
		private short _isUse = default(Int16);
		private System.DateTime _useTime = DateTime.Now;
		private string _manageRole = string.Empty;
		private string _appURL = string.Empty;
		private int _sortOrder = default(Int32);
		private string _description = string.Empty;
		private string _creator = string.Empty;
		private System.DateTime _createTime = DateTime.Now;
		
		
        #endregion

        #region Constructors
		public App(){}
		
		
		public App(string id,string name,string text,short isUse,System.DateTime useTime,string manageRole,string appURL,int sortOrder,string description,string creator,System.DateTime createTime)
		{
		  this.ID=id;
		this._name=name;
		this._text=text;
		this._isUse=isUse;
		this._useTime=useTime;
		this._manageRole=manageRole;
		this._appURL=appURL;
		this._sortOrder=sortOrder;
		this._description=description;
		this._creator=creator;
		this._createTime=createTime;
		}
        #endregion

        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(_name);
			sb.Append(_text);
			sb.Append(_isUse);
			sb.Append(_useTime);
			sb.Append(_manageRole);
			sb.Append(_appURL);
			sb.Append(_sortOrder);
			sb.Append(_description);
			sb.Append(_creator);
			sb.Append(_createTime);

            return sb.ToString().GetHashCode();
        }
		
		public virtual bool Validate()
        {
			return true;
        }

        #endregion

        #region Properties
		
		/// <summary>
        /// 应用名
        /// </summary>
		public virtual string Name
        {
            get { return  _name; }
			set	{	_name =  value;}
        }
		
		/// <summary>
        /// 应用显示名
        /// </summary>
		public virtual string Text
        {
            get { return  _text; }
			set	{	_text =  value;}
        }
		
		/// <summary>
        /// 是否开通
        /// </summary>
		public virtual short IsUse
        {
            get { return  _isUse; }
			set	{	_isUse =  value;}
        }
		
		/// <summary>
        /// 开通时间
        /// </summary>
		public virtual System.DateTime UseTime
        {
            get { return  _useTime.ToSafeDateTime(); }
			set	{	_useTime =  value.ToSafeDateTime();}
        }
		
		/// <summary>
        /// 管理角色
        /// </summary>
		public virtual string ManageRole
        {
            get { return  _manageRole; }
			set	{	_manageRole =  value;}
        }
		
		/// <summary>
        /// 访问URL
        /// </summary>
		public virtual string AppURL
        {
            get { return  _appURL; }
			set	{	_appURL =  value;}
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
        /// 描述
        /// </summary>
		public virtual string Description
        {
            get { return  _description; }
			set	{	_description =  value;}
        }
		
		/// <summary>
        /// 创建者
        /// </summary>
		public virtual string Creator
        {
            get { return  _creator; }
			set	{	_creator =  value;}
        }
		
		/// <summary>
        /// 创建时间
        /// </summary>
		public virtual System.DateTime CreateTime
        {
            get { return  _createTime.ToSafeDateTime(); }
			set	{	_createTime =  value.ToSafeDateTime();}
        }
		
        #endregion
    }
}
