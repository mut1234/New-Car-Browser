using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoPattrenWithUnitOfWork.Core.Helper
{
    public class NormalizePageSize
    {
        public int NormalizePage(int pageNumber)
        {
            return pageNumber > 0 ? pageNumber : 1;
        }
        public int NormalizeSize(int pageSize, int max = 500, int defaultSize = 10)
        {
            if (pageSize <= 0)
                return defaultSize;

            return pageSize > max ? max : pageSize;
        }
    }

}
