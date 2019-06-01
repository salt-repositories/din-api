﻿using System.Reflection;
using Din.Domain.Validators.Concrete;
using Din.Domain.Validators.Interfaces;
using SimpleInjector;

namespace Din.Application.WebAPI.Injection.SimpleInjector
{
    public static class Validators
    {
        public static void RegisterValidators(this Container container, Assembly[] assemblies)
        {
            container.Register(typeof(IValidatorBus<>), typeof(ValidatorBus<>));
            container.Collection.Register(typeof(IValidator<>), assemblies);
        }
    }
}