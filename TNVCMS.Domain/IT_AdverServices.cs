﻿using System.Collections.Generic;
using TNVCMS.Utilities;
using TNVCMS.Domain.Model;

namespace TNVCMS.Domain.Services
{
    public interface IT_AdverServices
    {
        IEnumerable<T_Adver> GetAll();
        T_Adver GetByID(int id);
        ReturnValue<bool> AddNewAdver(T_Adver iAdver);
        T_Adver AddNewAdverAndReturn(T_Adver iAdver);
        ReturnValue<bool> UpdateAdver(T_Adver iAdver);
        ReturnValue<bool> DeleteAdver(T_Adver iAdver);
        ReturnValue<bool> DeleteAdver(int id);
        IEnumerable<T_Adver> AdverSearch(string term);

    }
}
