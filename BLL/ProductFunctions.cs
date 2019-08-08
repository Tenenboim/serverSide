using System.Collections.Generic;
using System.Linq;
using DTO;
using DAL;
using BLL.customClasses;
using System.Drawing;
using System.Net;
using System.IO;
using System;

namespace BLL
{

    public static class ProductFunctions
    {
        static AshavatAvedaEntities db = new AshavatAvedaEntities();
        public static string FunctionsToaddNewProduct(DTO.ProductDTO productdDto
            , List<DTO.ParameterOfProductDTO> ParameterOfProductAreExistDto,
            List<DTO.ParameterDTO> NewParametersDto,
            List<DTO.ParameterOfProductDTO> NewParameterOfProductDto)


        {
            DAL.Product productdDAL = BLL.Convertions.ProductDtoToDAL(productdDto);
            List<DAL.ParameteOfProduct> ParameterOfProductAreExistDAL = new List<ParameteOfProduct>();
            List<DAL.Parameter> NewParametersDAL = new List<Parameter>();
            List<DAL.ParameteOfProduct> NewParameterOfProductDAL = new List<ParameteOfProduct>();
            //המרת האוביקטים לסוג DAL
            foreach (var item in ParameterOfProductAreExistDto)
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
            AddNewParametersAndNewParametersOfProduct(NewParametersDAL, NewParameterOfProductDAL, productId);
            return "exellent!";
        }
        //הוספת פרמטרים חדשים לטבלת פרמטרים וכן לטבלת פרמטרים למוצר 
        private static void AddNewParametersAndNewParametersOfProduct(List<Parameter> newParametersDAL, List<ParameteOfProduct> newParameterOfProductDAL, int productId)
        {
            for (int i = 0; i < newParametersDAL.Count(); i++)
            {
                if (newParametersDAL[i].ParameterName!=null&&newParameterOfProductDAL[i].Value != null)
                {
                    db.Parameters.Add(newParametersDAL[i]);
                    db.SaveChanges();
                    newParameterOfProductDAL[i].ParameterId = newParametersDAL[i].ParameterId;
                    newParameterOfProductDAL[i].ProductId = productId;
                    db.ParameteOfProducts.Add(newParameterOfProductDAL[i]);
                }
            }
        }

        public static ProductDTO getProduct(int productId)
        {
            return BLL.Convertions.ProductToDto(db.Products.First(p => p.ProductId == productId));
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
        public static ProductDTO FunctionsToEditProduct(ProductDTO product, List<ParameterOfProductDTO> parameterOfProductAreExist, List<ParameterDTO> newParameters, List<ParameterOfProductDTO> newParameterOfProduct, List<ParametersWithParametersOfProducts> parametersOfCategoryWithParametersOfProduct)
        {
            product = UpdateEditProduct(product);
            List<DAL.Parameter> NewParametersDAL = new List<Parameter>();
            List<DAL.ParameteOfProduct> NewParameterOfProductDAL = new List<ParameteOfProduct>();
            //המרת האוביקטים לסוג DAL

            foreach (var item in newParameters)
            {
                NewParametersDAL.Add(BLL.Convertions.ParameterDtoToDAL(item));
            }
            foreach (var item in newParameterOfProduct)
            {
                NewParameterOfProductDAL.Add(BLL.Convertions.ParameterOfProductDtoToDAL(item));
            }
            

            //parametersOfCategoryWithParametersOfProduct בדיקה האם יש נתונים באוביקט
            //parametersOfCategoryWithParametersOfProduct שנמצאים ב parameteOfProduct כלומר האם לא שינו את הקטגוריה של הפריט ולכן צריך רק לעדכן את הערכים של ה 
            if (parametersOfCategoryWithParametersOfProduct.Count() > 0)
            {
                EditParametersOfCategoryWithParametersOfProduct(parametersOfCategoryWithParametersOfProduct, product.ProductId);
            }
            else
            {
                List<DAL.ParameteOfProduct> ParameterOfProductAreExistDAL = new List<ParameteOfProduct>();
                //מהפרמטרים הקשורים למוצר שנערך כרגע parameterOfProduct ריקון טבלת 
                deleteItemsFromParametersOfProductTable(product.ProductId);
                foreach (var item in parameterOfProductAreExist)
                {
                    ParameterOfProductAreExistDAL.Add(BLL.Convertions.ParameterOfProductDtoToDAL(item));
                    AddParameterOfProductAreExist(ParameterOfProductAreExistDAL, product.ProductId);
                }

            }

            AddNewParametersAndNewParametersOfProduct(NewParametersDAL, NewParameterOfProductDAL, product.ProductId);
            db.SaveChanges();
            return product;
        }


        //שלהם value  עריכת הפרמטרים של המוצר בעצם עריכת ה
        //נערך מחדש value אלו פרמטרים של מוצר שהוכנסו כבר קודם ועכשיו ה
        //הם נמצאים באוביקט מטיפוס של מחלקה שיצרתי המכילה קוד קטגוריה קוד פרמטר שם פרמטר וערך הפרמטר
        private static void EditParametersOfCategoryWithParametersOfProduct(List<ParametersWithParametersOfProducts> parametersOfCategoryWithParametersOfProduct, int productId)
        {
            //מהפרמטרים הקשורים למוצר שנערך כרגע parameterOfProduct ריקון טבלת 
            deleteItemsFromParametersOfProductTable(productId);
            for (int i = 0; i < parametersOfCategoryWithParametersOfProduct.Count(); i++)
            {
                if (parametersOfCategoryWithParametersOfProduct[i].ParameterOfProductValue != null)
                    db.ParameteOfProducts.Add(new ParameteOfProduct { ParameterId = parametersOfCategoryWithParametersOfProduct[i].ParameterId, ProductId = productId, Value = parametersOfCategoryWithParametersOfProduct[i].ParameterOfProductValue });
            }
        }

        private static void deleteItemsFromParametersOfProductTable(int productId)
        {
            db.ParameteOfProducts.RemoveRange(db.ParameteOfProducts.Where(p => p.ProductId == productId));
            db.SaveChanges();
        }

        //עריכת המוצר
        public static ProductDTO UpdateEditProduct(DTO.ProductDTO product)
        {
            DAL.Product u1 = BLL.Convertions.ProductDtoToDAL(product);
            DAL.Product u2 = db.Products.FirstOrDefault(u => u.ProductId == u1.ProductId);
            if (u2 != null)
            {
                u2.ProductName = u1.ProductName;
                u2.CategoryId = u1.CategoryId;
                u2.ProductDescription = u1.ProductDescription;
                u2.AddressPointX = u1.AddressPointX;
                u2.AddressPointY = u1.AddressPointY;
                u2.AddressDescription = u1.AddressDescription;
                u2.DateFound = u1.DateFound;
                u2.CleverAgent = u1.CleverAgent;
                u2.WasDone = u1.WasDone;


                db.SaveChanges();
                return BLL.Convertions.ProductToDto(u2);
            }
            return BLL.Convertions.ProductToDto(u1);
        }
        //מחזירה את כל האבדות  userId==0 פונקציה שמחזירה את האבדות של המשתמש שקיבלה אם ה
        public static List<DTO.ProductDTO> getLosts(int userId)
        {
            List<DTO.ProductDTO> losts = new List<ProductDTO>();
            if (userId == 0)
                db.Products.Where(p => !p.WasDone && p.LostOrFound).ToList().ForEach(d => losts.Add(BLL.Convertions.ProductToDto(d)));
            else
                db.Products.Where(p => !p.WasDone && p.UserId == userId && p.LostOrFound).ToList().ForEach(d => losts.Add(BLL.Convertions.ProductToDto(d)));
            return losts;
        }
        //מחזירה את כל המציאות userId==0 פונקציה שמחזירה את המציאות של המשתמש שקיבלה אם ה
        public static List<DTO.ProductDTO> getFounds(int userId)
        {
            List<DTO.ProductDTO> founds = new List<ProductDTO>();
            if (userId == 0)
                db.Products.Where(p => !p.WasDone && !p.LostOrFound).ToList().ForEach(d => founds.Add(BLL.Convertions.ProductToDto(d)));
            else
                db.Products.Where(p => !p.WasDone && p.UserId == userId && !p.LostOrFound).ToList().ForEach(d => founds.Add(BLL.Convertions.ProductToDto(d)));
            return founds;
        }
        public static List<DTO.ProductDTO> search(int productId)
        {
            DAL.Product product = db.Products.FirstOrDefault(p => p.ProductId == productId);
            List<DAL.Product> matches = new List<DAL.Product>();
            db.Products.Where(p =>
            p.ProductId != product.ProductId &&
            !p.WasDone && p.CategoryId == product.CategoryId &&
            p.LostOrFound != product.LostOrFound &&
            (product.LostOrFound && product.DateFound < p.DateFound || !product.LostOrFound && product.DateFound > p.DateFound) &&
           ((product.AddressDescription == null && p.AddressDescription == null) || product.AddressDescription != null) &&
           checkTheParameters(productId, product.ProductId)
            ).ToList().ForEach(d => matches.Add(d));
            return null;
        }

        private static bool checkTheParameters(int productId1, int productId2)
        {
            List<DAL.ParameteOfProduct> p1 = db.ParameteOfProducts.Where(p => p.ProductId == productId1).ToList();
            List<DAL.ParameteOfProduct> p2 = db.ParameteOfProducts.Where(p => p.ProductId == productId2).ToList();
            DAL.ParameteOfProduct p3;
            for (int i = 0; i < p1.Count(); i++)
            {
                p3 = p2.FirstOrDefault(p => p.ParameterId == p1[i].ParameterId);
                if (p3 != null)
                    if (!p3.Value.Contains(p1[i].Value) && !p1[i].Value.Contains(p3.Value))
                        return false;
                p3 = null;
            }
            return true;
        }
        //private static List<string> GetDistanceAndDuration()
        //{
        //    var distance = "";
        //    var duration = "";
        //    try
        //    {
        //        var url = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={selector.Address.Latitude},{selector.Address.Longtitude}&destinations={business.Address.Latitude},{business.Address.Longtitude}&key={GlobalService.KEY}&language=he";
        //        var request = WebRequest.Create(url);
        //        request.ContentType = "application/json; charset=utf-8";
        //        var response = (HttpWebResponse)request.GetResponse();
        //        using (var sr = new StreamReader(response.GetResponseStream()))
        //        {
        //            dynamic result = JsonConvert.DeserializeObject(sr.ReadToEnd());
        //            var details = result["rows"][0]["elements"][0];
        //            distance = details["distance"]["text"];
        //            duration = details["duration"]["text"];
        //        }
        //    }
        //    catch (Exception) { }
        //    return new List<string> { distance, duration };
        //}
        
        private static void getDes()
        {
            string url = @"https://maps.googleapis.com/maps/api/directions/json?origin=75+9th+Ave+New+York,+NY&destination=MetLife+Stadium+1+MetLife+Stadium+Dr+East+Rutherford,+NJ+07073&key=YOUR_API_KEY";

            WebRequest request = WebRequest.Create(url);

            WebResponse response = request.GetResponse();

            Stream data = response.GetResponseStream();

            StreamReader reader = new StreamReader(data);

            // json-formatted string from maps api
            string responseFromServer = reader.ReadToEnd();

            response.Close();
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