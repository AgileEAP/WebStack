﻿#region Description
/*================================================================================
 *  Copyright (c) SunTek.  All rights reserved.
 * ===============================================================================
 * Solution: Infrastructure
 * Module:  PrivateCalendar
 * Descrption:
 * CreateDate: 2010/11/23 10:05:34
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
    public partial class PrivateCalendar :DomainObject<string>
    {
        #region Fields
		
		private short _type = default(Int16);
		private int _year = default(Int32);
		private short _eventType = default(Int16);
		private string _description = string.Empty;
		private int _day = default(Int32);
		private double _ownerOrg = default(Double);
		private string _operatorID = string.Empty;
		private string _creator = string.Empty;
		private System.DateTime _createTime = DateTime.Now;
		
		
        #endregion

        #region Constructors
		public PrivateCalendar(){}
		
		
		public PrivateCalendar(string id,short type,int year,short eventType,string description,int day,double ownerOrg,string operatorID,string creator,System.DateTime createTime)
		{
		  this.ID=id;
		this._type=type;
		this._year=year;
		this._eventType=eventType;
		this._description=description;
		this._day=day;
		this._ownerOrg=ownerOrg;
		this._operatorID=operatorID;
		this._creator=creator;
		this._createTime=createTime;
		}
        #endregion

        #region Methods

        public override int GetHashCode()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            
            sb.Append(this.GetType().FullName);
			sb.Append(_type);
			sb.Append(_year);
			sb.Append(_eventType);
			sb.Append(_description);
			sb.Append(_day);
			sb.Append(_ownerOrg);
			sb.Append(_operatorID);
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
        /// 间隔单位：天，位：天小时，分钟
        /// </summary>
		public virtual short Type
        {
            get { return  _type; }
			set	{	_type =  value;}
        }
		
		/// <summary>
        /// 年份
        /// </summary>
		public virtual int Year
        {
            get { return  _year; }
			set	{	_year =  value;}
        }
		
		/// <summary>
        /// 0表示请假，1表示加班
        /// </summary>
		public virtual short EventType
        {
            get { return  _eventType; }
			set	{	_eventType =  value;}
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
        /// 日期
        /// </summary>
		public virtual int Day
        {
            get { return  _day; }
			set	{	_day =  value;}
        }
		
		/// <summary>
        /// 所属机构
        /// </summary>
		public virtual double OwnerOrg
        {
            get { return  _ownerOrg; }
			set	{	_ownerOrg =  value;}
        }
		
		/// <summary>
        /// 操作员ID
        /// </summary>
		public virtual string OperatorID
        {
            get { return  _operatorID; }
			set	{	_operatorID =  value;}
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
