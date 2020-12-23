using Demoproductos.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demoproductos.Web.Service
{
    public interface ITestService
    {
        IEnumerable<TestModel> GetTestList();

        TestModel GetProductById(int id);

        bool CreateProduct(TestModel producto);

        TestModel UpdateProduct(int id, TestModel producto);


        IEnumerable<TestModel> DeleteProduct(int id);

    }
}
