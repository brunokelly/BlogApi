using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace BlogApi.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var resuit = new List<string>();
            foreach(var item in modelState.Values)
            {
                resuit.AddRange(item.Errors.Select(erro => erro.ErrorMessage));
            }

            return resuit;
        }
    }
}
