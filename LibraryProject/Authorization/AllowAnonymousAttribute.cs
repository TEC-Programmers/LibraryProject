using System;

namespace LibraryProject.API.Authorization // 
{

    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute //  klasse  deklareret klasse til at anonymous/alle kan få adgang til
                                                     //  hjemmesiden og nogle af funktionerne
    { }
}

