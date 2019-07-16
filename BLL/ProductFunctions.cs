using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using DTO;
using DAL;

namespace BLL
{
    
    public static class ProductFunctions
    {
        static AshavatAvedaEntities db = new AshavatAvedaEntities();
        public static string FunctionsToaddNewProduct(DTO.ProductDTO productdDto
            ,List< DTO.ParameterOfProductDTO> ParameterOfProductAreExistDto,
            List< DTO.ParameterDTO> NewParametersDto,
            List< DTO.ParameterOfProductDTO> NewParameterOfProductDto)


        {
            DAL.Product productdDAL = BLL.Convertions.ProductDtoToDAL(productdDto);
            List<DAL.ParameteOfProduct> ParameterOfProductAreExistDAL = new List<ParameteOfProduct>();
            List<DAL.Parameter> NewParametersDAL = new List<Parameter>();
            List<DAL.ParameteOfProduct> NewParameterOfProductDAL = new List<ParameteOfProduct>();
            //המרת האוביקטים לסוג DAL
            foreach( var item in  ParameterOfProductAreExistDto)
            {
                ParameterOfProductAreExistDAL.Add(BLL.Convertions.ParameterOfProductDtoToDAL(item));
            }
            foreach (var item in NewParametersDto)
            {
                NewParametersDAL.Add(BLL.Convertions.ParameterDtoToDAL(item));
            }
            foreach (var item in NewParameterOfProductDto)
            {
                NewParameterOfProductDAL.Add(BLL.Convertions.ParameterOfProductDtoToDAL(item));
            }
            //ID של המוצר החדש שמוסיפים
             addNewProduct(productdDAL);
            int productId = productdDAL.ProductId;
            AddParameterOfProductAreExist(ParameterOfProductAreExistDAL, productId);
            AddNewParametersAndNewParametersOfProduct(NewParametersDAL, NewParameterOfProductDAL,productId);
            return "exellent!";
        }
        //הוספת פרמטרים חדשים לטבלת פרמטרים וכן לטבלת פרמטרים למוצר 
        private static void AddNewParametersAndNewParametersOfProduct(List<Parameter> newParametersDAL, List<ParameteOfProduct> newParameterOfProductDAL,int productId)
        {
           for(int i=0;i<newParametersDAL.Count();i++)
            {
                if(newParameterOfProductDAL[i].Value!=null)
                {
                   db.Parameters.Add(newParametersDAL[i]);
                    db.SaveChanges();
                    newParameterOfProductDAL[i].ParameterId = newParametersDAL[i].ParameterId;
                    newParameterOfProductDAL[i].ProductId = productId;
                    db.ParameteOfProducts.Add(newParameterOfProductDAL[i]);
                }
            }
        }
        //value,productId הוספה לטבלת פרמטרים למוצר את הפרמטרים שהיו כבר קיימים כלומר חדש רק ה 
        private static void AddParameterOfProductAreExist(List<ParameteOfProduct> parameterOfProductAreExistDAL, int productId)
        {
            foreach (var item in parameterOfProductAreExistDAL)
            {
                if (item.Value != null)
                {
                    item.ProductId = productId;
                    db.ParameteOfProducts.Add(item);
                }
               
            }
        }

        //הוספת המוצר
        public static void addNewProduct(DAL.Product product)
        {
           db.Products.Add(product);
            db.SaveChanges();
           
        }
        //פונקציה שמחזירה את האבדות של המשתמש שקיבלה 
        public static List<DTO.ProductDTO> getLosts(int userId)
        {
            List<DTO.ProductDTO> losts = new List<ProductDTO>();
            db.Products.Where(p => !p.WasDone && p.UserId == userId && p.LostOrFound).ToList().ForEach(d => losts.Add(BLL.Convertions.ProductToDto(d)));
            return losts;
        }
        //פונקציה שמחזירה את המציאות של המשתמש שקיבלה 
        public static List<DTO.ProductDTO> getFounds(int userId)
        {
            List<DTO.ProductDTO> founds = new List<ProductDTO>();
            db.Products.Where(p => !p.WasDone && p.UserId == userId && !p.LostOrFound).ToList().ForEach(d => founds.Add(BLL.Convertions.ProductToDto(d)));
            return founds;
        }
    }
}
