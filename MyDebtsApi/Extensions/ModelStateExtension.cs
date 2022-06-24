using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MyDebtsApi.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErros(this ModelStateDictionary modelState)
        {
            var result = new List<string>();
            foreach(var valor in modelState.Values)
            {
                //foreach(var erro in valor.Erros)
                //{
                //    result.Add(erro.ErrorMessage);
                //}

                //Posso usar uma expressão link
                result.AddRange(valor.Errors.Select(erro => erro.ErrorMessage));
            }
            return result;
        }
    }
}