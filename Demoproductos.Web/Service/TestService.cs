using Dapper;
using Demoproductos.Web.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Demoproductos.Web.Service
{
    public class TestService : ITestService
    {
        public IEnumerable<TestModel> GetTestList()
        {
            List<TestModel> registros = new List<TestModel>();
            using (SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Productos;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                connection.Open();
             var prod = connection.ExecuteReader("SP_ALLPRODUCTS", commandType: CommandType.StoredProcedure); 

             while(prod.Read())
                {
                    registros.Add(new TestModel
                    {
                        Id = (int)prod["ID"],
                        Name = prod["Name"].ToString(),
                        Price =(decimal) prod["Price"],
                        Creation =(DateTime) prod["Creation"],
                        Modification =(DateTime) prod["Modification"]

                    });
                }
                 
            };

            return registros;

        }

       public  TestModel GetProductById(int id)
        {
            //List<TestModel> registros = new List<TestModel>();
            var parametros = new DynamicParameters();
            var registro = new TestModel { };
             parametros.Add("@ID", id); 
            using (SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Productos;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                connection.Open();
                var prod = connection.ExecuteReader("SP_PRODUCTBYID",parametros, commandType: CommandType.StoredProcedure);
                
                if(prod.Read())
                {
                    registro.Id = (int)prod["ID"];
                     registro.Name=(string) prod["Name"];
                     registro.Price = (decimal)prod["Price"];
                     registro.Creation = (DateTime)prod["Creation"];
                     registro.Modification = (DateTime)prod["Modification"];
                }

            };

            return registro;
        }

        public bool CreateProduct(TestModel producto)
        {
           
           var parametros = new DynamicParameters();
            parametros.Add("@NAME",producto.Name );
            parametros.Add("@PRICE", producto.Price);
            parametros.Add("@CREATION", producto.Creation);
            parametros.Add("@MODIFICATION", producto.Modification);
            using (SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Productos;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                connection.Open();
                var prod = connection.ExecuteReader("SP_CREATEPRODUCT", parametros, commandType: CommandType.StoredProcedure);
   
                    return true;
          

            };
          
        }

        public TestModel UpdateProduct(int id, TestModel producto)
        {
            var registro = new TestModel { };
            var parametros = new DynamicParameters();
            parametros.Add("@ID", id);
            parametros.Add("@NAME", producto.Name);
            parametros.Add("@PRICE", producto.Price);
            parametros.Add("@CREATION", producto.Creation);
            parametros.Add("@MODIFICATION", producto.Modification);
            using (SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Productos;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                connection.Open();
                var prod = connection.ExecuteReader("SP_UPDATEPRODUCT", parametros, commandType: CommandType.StoredProcedure);


                if (prod.Read())
                {
                    registro.Id =(int)prod["ID"]; 
                    registro.Name = (string)prod["Name"];
                    registro.Price = (decimal)prod["Price"];
                    registro.Creation = (DateTime)prod["Creation"];
                    registro.Modification = (DateTime)prod["Modification"];
                }

            };
            return registro; 
        }

       public  IEnumerable<TestModel> DeleteProduct(int id)
        {

            List<TestModel> registros = new List<TestModel>();
            var parametros = new DynamicParameters();
            parametros.Add("@ID", id);
            using (SqlConnection connection = new SqlConnection("Data Source=localhost\\SQLEXPRESS;Initial Catalog=Productos;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                connection.Open();
                var prod = connection.ExecuteReader("DELETEPRODUCT", parametros, commandType: CommandType.StoredProcedure);

                while (prod.Read())
                {
                    registros.Add(new TestModel
                    {
                        Id = (int)prod["ID"],
                         Name = prod["Name"].ToString(),
                        Price = (decimal)prod["Price"],
                        Creation = (DateTime)prod["Creation"],
                        Modification = (DateTime)prod["Modification"]

                    });
                }


            };
            return registros;
        }

    }
}
