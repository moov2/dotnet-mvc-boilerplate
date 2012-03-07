using System;

namespace DotnetMvcBoilerplate.Core.Provider
{
    public interface IDatabaseProvider
    {
        dynamic GetDb();
    }
}