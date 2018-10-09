using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using OA.WebApp.Helpers;
using OA.WebApp.Models;

namespace OA.WebApp.ViewModels
{
    public class PrivilegeDto : BaseEntity
    {
        public int ID { get; set; }

        [Display(Name = "权限名")]
        public string Name { get; set; }

        [Display(Name = "描述")]
        public string Description { get; set; }

        [Display(Name = "控制器分组")]
        
        public string Namespace { get; set; }

        [Display(Name = "控制器")]
        public string ControllerName { get; set; }
        [Display(Name = "Action Name集合")]
        public IEnumerable<Privilege> ActionNames { get; set; }

        [Display(Name = "所属的控制器")]
        public IEnumerable<Dictionary<Privilege, IEnumerable<Privilege>>> Controllers { get; set; }

        [Display(Name = "Action Name 字符串")]
        public string ActionNamesStr { get; set; }

        //以name和和controller进行分组，找到所属的Actions
        [Display(Name = "Action集合")]
        public IEnumerable<Privilege> Actions { get; set; }

        [Display(Name = "是否已分配")]
        public bool IsUsed { get; set; }

        [Display(Name = "所有的控制器")]
        public IEnumerable<Dictionary<string, Object>> AllControllers
        {
            get
            {
                return Tools.AllController();
            }
        }

        public IEnumerable<Privilege> ScopeEnables { get; set; }
    }
}
