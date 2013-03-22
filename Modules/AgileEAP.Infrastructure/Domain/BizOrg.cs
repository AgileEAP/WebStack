﻿#region Description
/*================================================================================
 *  Copyright (c) SunTek.  All rights reserved.
 * ===============================================================================
 * Solution: AuthorizeCenter
 * Module:  BizOrg
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
    public partial class BizOrg :DomainObject<string>
    {
        #region Fields
		
		private string _name = string.Empty;
		private int _grade = default(Int32);
		private string _parentID = string.Empty;
		private short _bizType = default(Int16);
		private string _orgID = string.Empty;
		private int _sortOrder = default(Int32);
		private string _governPosition = string.Empty;
		private string _creator = string.Empty;
		private System.DateTime _createTime = DateTime.Now;
		
		
        #endregion

        #region Constructors
		public BizOrg(){}
		
		
		public BizOrg(string id,string name,int grade,string parentID,short bizType,string orgID,int sortOrder,string governPosition,string creator,System.DateTime createTime)
		{
		  this.ID=id;
		this._name=name;
		this._grade=grade;
		this._parentID=parentID;
		this._bizType=bizType;
		this._orgID=orgID;
		this._sortOrder=sortOrder;
		this._governPosition=governPosition;
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
			sb.Append(_grade);
			sb.Append(_parentID);
			sb.Append(_bizType);
			sb.Append(_orgID);
			sb.Append(_sortOrder);
			sb.Append(_governPosition);
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
        /// 业务机构名称
        /// </summary>
		public virtual string Name
        {
            get { return  _name; }
			set	{	_name =  value;}
        }
		
		/// <summary>
        /// 业务机构等级
        /// </summary>
		public virtual int Grade
        {
            get { return  _grade; }
			set	{	_grade =  value;}
        }
		
		/// <summary>
        /// 上级组织
        /// </summary>
		public virtual string ParentID
        {
            get { return  _parentID; }
			set	{	_parentID =  value;}
        }
		
		/// <summary>
        /// 业务字典ABF_NODETYPE
        ///虚拟节点，机构节点，如果是机构节点，则对应机构信息表的一个机构
        /// </summary>
		public virtual short BizType
        {
            get { return  _bizType; }
			set	{	_bizType =  value;}
        }
		
		/// <summary>
        /// 组织机构ID
        /// </summary>
		public virtual string OrgID
        {
            get { return  _orgID; }
			set	{	_orgID =  value;}
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
        /// 主管岗位
        /// </summary>
		public virtual string GovernPosition
        {
            get { return  _governPosition; }
			set	{	_governPosition =  value;}
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