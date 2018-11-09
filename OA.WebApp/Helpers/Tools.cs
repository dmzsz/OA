using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace OA.WebApp.Helpers
{
    public class Tools
    {
        ///<summary>
        ///将字符串二维数组的每一个维度的第n项取出来，拼接成以逗号分割的字符串
        ///</summary>
        ///<param name="arrayString">字符串2维数组，例如："[[1,'index'],[2,'edit']]"</param>
        ///<param name="dimensionIndex">取出2维数组的第几项，例如：用上面的列出的数组，设置为1，结果是"index,edit"</param>
        ///<returns>每个第2维度的第dimensionIndex项的字符串</returns>
        public static string ActionNames(string arrayString, int dimensionIndex=1) {
            Object[,] name = Newtonsoft.Json.JsonConvert.DeserializeObject<Object[,]>(arrayString);
            var actionNames = new List<string>();
            for (var i = 0; i < name.GetUpperBound(0) + 1; i++)
            {
                if(name[i, 0] != null) { 
                    actionNames.Add((string)name[i,dimensionIndex]);
                }
            }
            return string.Join(",", actionNames);
        }

        public static IEnumerable<Dictionary<string, Object>> AllController()
        {
            return Assembly.GetExecutingAssembly()
                                        .GetTypes()
                                        .Where(type => typeof(Controller).IsAssignableFrom(type))
                                        .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                                        .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                                        .GroupBy(x => x.DeclaringType.Name)
                                        .Select(x => new Dictionary<string, Object> {
                                            { "Controller", x.Key },
                                            { "Actions", x.Select(s => s.Name).ToList() }
                                        })
                                        .ToList();
        }


    }
}
