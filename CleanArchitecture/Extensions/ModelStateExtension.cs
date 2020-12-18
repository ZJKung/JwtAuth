using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CleanArchitecture.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrorMessage(this ModelStateDictionary dic)
        {
            return dic.SelectMany(m => m.Value.Errors)
            .Select(x => x.ErrorMessage).ToList();
        }
    }
}
