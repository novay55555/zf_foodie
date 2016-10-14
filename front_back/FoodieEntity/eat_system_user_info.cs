using System;
using System.ComponentModel;
using System.Text;

namespace Foodie.Entity
{
    /// <summary>  
    /// 吃什么-用户系统表  
    /// <author>  
    ///     <name>weiyankun</name>  
    ///     <date>2016.10.13</date>  
    /// </author>  
    /// </summary>  
    [Description("吃什么-用户系统表")]
    public class eat_system_user_info
    {
        private string _pk_id = null;
        /// <summary>  
        /// 系统主键   
        /// </summary>  
        /// <returns></returns>  
        [Description("系统主键 ")]
        public string pk_id
        {
            get
            {
                return this._pk_id;
            }
            set
            {
                this._pk_id = value;
            }
        }
        private string _user_password = null;
        /// <summary>  
        /// 密码   
        /// </summary>  
        /// <returns></returns>  
        [Description("密码 ")]
        public string user_password
        {
            get
            {
                return this._user_password;
            }
            set
            {
                this._user_password = value;
            }
        }
        private string _user_nick_name = null;
        /// <summary>  
        /// 用户昵称   
        /// </summary>  
        /// <returns></returns>  
        [Description("用户昵称 ")]
        public string user_nick_name
        {
            get
            {
                return this._user_nick_name;
            }
            set
            {
                this._user_nick_name = value;
            }
        }
        private string _remark = null;
        /// <summary>  
        /// 备注   
        /// </summary>  
        /// <returns></returns>  
        [Description("备注 ")]
        public string remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }
        private decimal? _flag_status = null;
        /// <summary>  
        /// 状态   
        /// </summary>  
        /// <returns></returns>  
        [Description("状态 ")]
        public decimal? flag_status
        {
            get
            {
                return this._flag_status;
            }
            set
            {
                this._flag_status = value;
            }
        }
        private decimal? _flag_sort = null;
        /// <summary>  
        /// 序号   
        /// </summary>  
        /// <returns></returns>  
        [Description("序号 ")]
        public decimal? flag_sort
        {
            get
            {
                return this._flag_sort;
            }
            set
            {
                this._flag_sort = value;
            }
        }
        private string _make_emp = null;
        /// <summary>  
        /// 制作人   
        /// </summary>  
        /// <returns></returns>  
        [Description("制作人 ")]
        public string make_emp
        {
            get
            {
                return this._make_emp;
            }
            set
            {
                this._make_emp = value;
            }
        }
        private DateTime? _make_date = null;
        /// <summary>  
        /// 制作时间   
        /// </summary>  
        /// <returns></returns>  
        [Description("制作时间 ")]
        public DateTime? make_date
        {
            get
            {
                return this._make_date;
            }
            set
            {
                this._make_date = value;
            }
        }
        private string _modify_emp = null;
        /// <summary>  
        /// 修改人   
        /// </summary>  
        /// <returns></returns>  
        [Description("修改人 ")]
        public string modify_emp
        {
            get
            {
                return this._modify_emp;
            }
            set
            {
                this._modify_emp = value;
            }
        }
        private DateTime? _modify_date = null;
        /// <summary>  
        /// 修改时间   
        /// </summary>  
        /// <returns></returns>  
        [Description("修改时间 ")]
        public DateTime? modify_date
        {
            get
            {
                return this._modify_date;
            }
            set
            {
                this._modify_date = value;
            }
        }
        private decimal? _flag_power = null;
        /// <summary>  
        /// 用户权限 0：系统用户，1：普通用户  
        /// </summary>  
        /// <returns></returns>  
        [Description("用户权限 0：系统用户，1：普通用户")]
        public decimal? flag_power
        {
            get
            {
                return this._flag_power;
            }
            set
            {
                this._flag_power = value;
            }
        }
        private string _login_state = null;
        /// <summary>  
        /// 登录状态  
        /// </summary>  
        /// <returns></returns>  
        [Description("登录状态")]
        public string login_state
        {
            get
            {
                return this._login_state;
            }
            set
            {
                this._login_state = value;
            }
        }
    }
}