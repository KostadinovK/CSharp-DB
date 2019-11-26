using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using PetStore.Data;

namespace PetStore.Services
{
    public abstract class Service
    {
        protected PetStoreDbContext context;

        protected Service(PetStoreDbContext context)
        {
            this.context = context;
        }

        protected virtual bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, validationResults, true);
        }
    }
}
