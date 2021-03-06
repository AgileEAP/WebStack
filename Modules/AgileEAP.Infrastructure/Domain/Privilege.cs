﻿#region Description
/*================================================================================
 *  Copyright (c) SunTek.  All rights reserved.
 * ===============================================================================
 * Solution: AuthorizeCenter
 * Module:  Privilege
 * Descrption:
 * CreateDate: 2010/11/18 13:55:34
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
    public partial class Privilege :DomainObject<string>
    {
        #region Fields
		
		private string _name = string.Empty;
		private short _type = default(Int16);
		private string _resourceID = string.Empty;
		private string _operateID = string.Empty;
		private string _metaDataID = string.Empty;
		private string _moduleID = string.Empty;
		private string _appID = string.Empty;
		private int _sortOrder = default(Int32);
		private string _description = string.Empty;
		private string _ownerOrg = string.Empty;
		private string _creator = string.Empty;
		private System.DateTime _createTime = DateTime.Now;
		
		
        #endregion

        #region Constructors
		public Privilege(){}
		
		
		public Privilege(string id,string name,short type,string resourceID,string operateID,string metaDataID,string moduleID,string appID,int sortOrder,string description,string ownerOrg,string creator,System.DateTime createTime)
		{
		  this.ID=id;
		this._name=name;
		this._type=type;
		this._resourceID=resourceID;
		this._operateID=operateID;
		this._metaDataID=metaDataID;
		this._moduleID=moduleID;
		this._appID=appID;
		this._sortOrder=sortOrder;
		this._description=description;
		this._ownerOrg=ownerOrg;
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
			sb.Append(_type);
			sb.Append(_resourceID);
			sb.Append(_operateID);
			sb.Append(_metaDataID);
			sb.Append(_moduleID);
			sb.Append(_appID);
			sb.Append(_sortOrder);
			sb.Append(_description);
			sb.Append(_ownerOrg);
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
        /// 权限名称
        /// </summary>
		public virtual string Name
        {
            get { return  _name; }
			set	{	_name =  value;}
        }
		
		/// <summary>
        /// 权限类型
        /// </summary>
		public virtual short Type
        {
            get { return  _type; }
			set	{	_type =  value;}
        }
		
		/// <summary>
        /// 资源ID
        /// </summary>
		public virtual string ResourceID
        {
            get { return  _resourceID; }
			set	{	_resourceID =  value;}
        }
		
		/// <summary>
        /// 操作ID
        /// </summary>
		public virtual string OperateID
        {
            get { return  _operateID; }
			set	{	_operateID =  value;}
        }
		
		/// <summary>
        /// 元数据ID
        /// </summary>
		public virtual string MetaDataID
        {
            get { return  _metaDataID; }
			set	{	_metaDataID =  value;}
        }
		

		
        ///// <summary>
        ///// 应用ID
        ///// </summary>
        //public virtual string AppID
        //{
        //    get { return  _appID; }
        //    set	{	_appID =  value;}
        //}
		
		/// <summary>
        /// 序号
        /// </summary>
		public virtual int SortOrder
        {
            get { return  _sortOrder; }
			set	{	_sortOrder =  value;}
        }

        ///// <summary>
        ///// 模块ID
        ///// </summary>
        //public virtual string ModuleID
        //{
        //    get { return _moduleID; }
        //    set { _moduleID = value; }
        //}

        ///// <summary>
        ///// 描述
        ///// </summary>
        //public virtual string Description
        //{
        //    get { return  _description; }
        //    set	{	_description =  value;}
        //}
		
        ///// <summary>
        ///// 归属组织
        ///// </summary>
        //public virtual string OwnerOrg
        //{
        //    get { return  _ownerOrg; }
        //    set	{	_ownerOrg =  value;}
        //}
		
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
