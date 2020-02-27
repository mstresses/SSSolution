using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Impl
{
    public class BaseService
    {
        //Classe que automatiza a adição de erros e também verifica se existem erros.
        private List<Error> errors = new List<Error>();

        protected void AddError(string fieldName, string error)
        {
            errors.Add(new Error() { Message = error, FieldName = fieldName });
        }

        protected void CheckErrors()
        {
            if (errors.Count > 0)
            {
                throw new NecoException(errors);
            }
        }
    }
}